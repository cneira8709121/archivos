using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
//using ServiceStack.Text;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Ruv.WPF.Captura.Infrastructure.ColaProcesos
{
    /// <summary>
    /// Aqui se realizan todas las operaciones de transmisión.
    /// </summary>
    public partial class clsColaProcesos : DependencyObject
    {
        #region BACKGROUND WORKER

        void Inicializar()
        {
            RUV.I.Red.EstadoRedCambio += Red_EstadoRedCambio;
        }

        /// <summary>
        /// Estar pendiente del cambio de estado en la red.
        /// </summary>
        /// <param name="nuevoEstado"></param>
        void Red_EstadoRedCambio(eEstadoRed nuevoEstado)
        {
            if (nuevoEstado == eEstadoRed.NoDisponible
              && ProcesoActual != null)
            {
                // No hay Red y se estaba procesando una transmisión.

                // 1) Cancelar la transmisión.
                if (BW != null)
                    BW.CancelAsync();

                // 2) Cambiar el estado del proceso.
                ProcesoActual.Estado = (int)eEstadoProcesoCola.PendienteTransmitir;
                RUV.I.Log.Registrar("Transmision cancelada '{0}' por falta de conexión",
                  ProcesoActual.NombreDeclarante);

                // 3) Destruir el proceso en el background.
                if (BW != null)
                {
                    BW.Dispose();
                    BW = null;
                }
            }
            else
            {
                // Crear y lanzar el proceso en el background.
                ActivarBackgroundWorker();
            }
        }

        /// <summary>
        /// Activar el Background Worker.
        /// </summary>
        void ActivarBackgroundWorker()
        {
            if (BW != null) return;

            BW = new BackgroundWorker();
            BW.WorkerSupportsCancellation = true;
            BW.DoWork += Procesar_DoWork;
            BW.RunWorkerCompleted += Procesar_RunWorkerCompleted;
            BW.RunWorkerAsync();
        }

        /// <summary>
        /// Al terminar todas las transmisiones, destruir el BW.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Procesar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BW.Dispose();
            BW = null;
        }

        BackgroundWorker BW;

        /// <summary>
        /// Detener la cola de transmisión.
        /// </summary>
        public void DetenerCola()
        {
            if (BW != null)
                BW.CancelAsync();

            RUV.I.Red.EstadoRedCambio -= Red_EstadoRedCambio;
        }

        #endregion

        #region VARIABLES

        /// <summary>
        /// El proceso que se está transmitiendo.
        /// </summary>
        clsProceso ProcesoActual;

        List<Thread> Hilos = new List<Thread>();

        #endregion

        #region PROCESO DE ENVIO

        /// <summary>
        /// Realizar el proceso de transmisión.
        /// Diego Alvarez - 28/10/2013 - Se modifica la transmisión para que se envién las declaraciones que están en cola cuando no es posible enviar alguna y no se queden estancadas
        /// Ivan Suarez - 20/02/2014 - Se independiza el proceso, de tal manera que se pueda controlar cada porceso en la cola por medio de un hilo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Procesar_DoWork(object sender, DoWorkEventArgs e)
        {

            //Luis.Esteban: Filtrar cola de procesos
            //Lista de Proceso: Eliminar las declaraciones que no pertenecen al usuario logeado
            /*
            for (int i = ListaProcesos.Count - 1; i >= 0; i--)
            {
                clsProceso ProcesoActual = ListaProcesos.ElementAt(i);
                if (ProcesoActual.Estado == (int)eEstadoProcesoCola.PendienteTransmitir)
                {
                    // Cargar la declaración desde el archivo local.
                    clsDeclaracion DeclaTrans =
                      RUV.I.Util.CargarArchivoSerializado<clsDeclaracion>(
                        RutaCola(ProcesoActual.ArchivoDeclaracion));

                    if (DeclaTrans.UsuarioId != RUV.I.Usuario.Id)
                    {
                        ListaProcesos.Remove(ProcesoActual);
                    }
                }
            }
            */

            int cantidadProcesosPendientes = ListaProcesos.Count;
            // Intentar transmitir mientras existan procesos en la cola.
            while (ListaProcesos.Any() && cantidadProcesosPendientes > 0)
            {

                // ¿Cancelación pendiente?
                if (BW.CancellationPending) return;

                // Tomar el primer elemento de la lista que no tenga estado.
                ProcesoActual = ListaProcesos.FirstOrDefault().Estado == (int)eEstadoProcesoCola.PendienteTransmitir ?
                    ListaProcesos.FirstOrDefault() : ListaProcesos.FirstOrDefault(x => x.Estado == (int)eEstadoProcesoCola.PendienteTransmitir);

                this.Dispatcher.Invoke(
                    new Action(() =>
                    {
                        cantidadProcesosPendientes--;
                        Proceso(BW, ProcesoActual);
                    }), System.Windows.Threading.DispatcherPriority.Normal, null);
            }
        }

        /// <summary>
        /// Ivan Suarez - 20/2/2014 - Proceso de transmisión de la cola de procesos.
        /// </summary>
        /// <param name="BW"></param>
        /// <param name="Proceso"></param>
        public void Proceso(BackgroundWorker BW, clsProceso Proceso)
        {

            bool transmitidook = false;

            // Si no hay pendientes por transmitir, terminar.
            if (Proceso != null)
            {

                // Cambiar el estado.
                Proceso.Estado = (int)eEstadoProcesoCola.Transmitiendo;

                // Cargar la declaración desde el archivo local. 
                clsDeclaracion DeclaTrans =
                  RUV.I.Util.CargarArchivoSerializado<clsDeclaracion>(
                    RutaCola(Proceso.ArchivoDeclaracion));

                DeclaTrans.CrearEnlacesPostCargue();

                // Actualizar los mensajes.
                EstablecerEstadoCola(
                  "Transmitiendo: " + Proceso.NombreDeclarante,
                  eEstadoProcesoCola.Transmitiendo);

                // Cargar el documento escaneado.
                // Si documento digital es nulo, entonces es un toma en línea, de lo contrario es una digitación o glosa
                if (DeclaTrans.DocumentoDigital == null)
                {
                    if (!string.IsNullOrEmpty(Proceso.ArchivoDocumentoEscaneado))
                    {
                        try
                        {
                            DeclaTrans.DocumentoDigital = RUV.I.Util.CargarArchivo(RutaCola(Proceso.ArchivoDocumentoEscaneado));
                            DeclaTrans.DocumentoDigitalNombre = Path.GetFileName(Proceso.ArchivoDocumentoEscaneado);
                        }
                        catch (Exception ex)
                        {
                            Proceso.ErroresDB = new System.Collections.Specialized.StringCollection();
                            Proceso.ErroresDB.Add(ex.Message);
                            Proceso.Estado = (int)eEstadoProcesoCola.RequiereRevision;
                            DeclaTrans = null;

                            RUV.I.LocalDB.Save<clsProceso>(Proceso);
                            RUV.I.LocalDB.Flush();

                            EstablecerEstadoCola(
                              "Última transmisión no exitosa",
                              eEstadoProcesoCola.RequiereRevision);

                            RUV.I.UIPrincipal.Notificar(null, "Requiere revisión la declaración de: " + Proceso.NombreDeclarante, Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Error);

                            lock (ColaLock)
                            {
                                this.Dispatcher.Invoke(
                                new Action(() =>
                                {
                                    ListaProcesos.Remove(Proceso);
                                    ListaProcesos.Add(Proceso);
                                }), System.Windows.Threading.DispatcherPriority.Normal, null);

                                Proceso = null;
                            }
                            //continue;
                        }
                    }
                    else
                    {
                        // Si la declaración viene como ZIP de documentos XPS permite la transmisión de la misma.
                        if (DeclaTrans.DocumentoAnexo == null)
                        {
                            Proceso.ErroresDB = new System.Collections.Specialized.StringCollection();
                            Proceso.ErroresDB.Add("Se debe cargar la declaración escaneada.");
                            Proceso.Estado = (int)eEstadoProcesoCola.RequiereRevision;
                            DeclaTrans = null;

                            RUV.I.LocalDB.Save<clsProceso>(Proceso);
                            RUV.I.LocalDB.Flush();

                            EstablecerEstadoCola(
                              "Última transmisión no exitosa",
                              eEstadoProcesoCola.RequiereRevision);

                            RUV.I.UIPrincipal.Notificar(null, "Requiere revisión la declaración de: " + Proceso.NombreDeclarante, Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Error);

                            lock (ColaLock)
                            {
                                this.Dispatcher.Invoke(
                                new Action(() =>
                                {
                                    ListaProcesos.Remove(Proceso);
                                    ListaProcesos.Add(Proceso);
                                }), System.Windows.Threading.DispatcherPriority.Normal, null);

                                Proceso = null;
                            }
                            //continue;
                        }
                    }
                }

                // ¿Cancelación pendiente?
                if (BW.CancellationPending)
                {
                    Proceso.Estado = (int)eEstadoProcesoCola.PendienteTransmitir;
                    DeclaTrans = null;
                    EstablecerEstadoCola(
                      "Transmisión cancelada",
                      eEstadoProcesoCola.Ninguno);
                    RUV.I.UIPrincipal.Notificar(null, "Se cancelo la transmisión de la declaracion de: " + Proceso.NombreDeclarante);
                    //return;
                }

                // Transmitir.
                GeneralService.clsResultado Resultado = null;
                bool TransmisionExitosa = false;

                try
                {
                    DeclaTrans.IdValoracion = RUV.I.IdValoracion;
                    Resultado = RUV.I.Red.ServicioGeneral.DeclaracionAlmacenar(
                     DeclaTrans,
                     RUV.I.Seguridad.LlaveUsuario, RUV.I.Usuario);
                    TransmisionExitosa = true;
                }
                catch (Exception ex)
                {
                    RUV.I.Log.Registrar("Excepción en transmisión declaración '{0}'", Proceso.NombreDeclarante);
                    RUV.I.Log.Registrar("Detalle excepción: ", ex);
                    TransmisionExitosa = false;
                }

                if (Resultado == null)
                {
                    RUV.I.UIPrincipal.Notificar(null, "Falló la transmisión (servicio) de la declaración : " + Proceso.NombreDeclarante, Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Error);
                    //return;
                }

                // ¿Hubo algún problema?
                //Se cambia Pendiente por transmitir por requiere revision en caso de 
                //Se intente transmitir y no sea satisfactoria por error en cliente 
                if (!TransmisionExitosa)
                {
                    Proceso.Estado = (int)eEstadoProcesoCola.RequiereRevision;
                    DeclaTrans = null;
                    EstablecerEstadoCola(
                      "Última transmisión no exitosa",
                      eEstadoProcesoCola.RequiereRevision);

                    RUV.I.UIPrincipal.Notificar(null, "Requiere revisión la declaración de: " + Proceso.NombreDeclarante, Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Error);

                    //return;
                }

                // ¿Se reportó algún error desde el servidor?
                if (Resultado != null && Resultado.ErroresDB != null && Resultado.ErroresDB.Any())
                {
                    Proceso.ErroresDB = new System.Collections.Specialized.StringCollection();
                    Resultado.ErroresDB.ToList().ForEach(x => Proceso.ErroresDB.Add(x));
                    Proceso.Estado = (int)eEstadoProcesoCola.RequiereRevision;

                    RUV.I.LocalDB.Save<clsProceso>(Proceso);
                    RUV.I.LocalDB.Flush();
                    EstablecerEstadoCola(
                      "Última transmisión requiere revisión",
                      eEstadoProcesoCola.RequiereRevision);

                    RUV.I.UIPrincipal.Notificar(null, "Requiere revisión la declaración de: " + Proceso.NombreDeclarante, Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Warning);
                    //break;
                }

                // ¿Se reportó alguna advertencia?
                if (Resultado != null && Resultado.AdvertenciasDB != null && Resultado.AdvertenciasDB.Any())
                {
                    Proceso.AdvertenciasDB = new System.Collections.Specialized.StringCollection();
                    Resultado.AdvertenciasDB.ToList().ForEach(x => Proceso.AdvertenciasDB.Add(x));
                }

                // ========================== \\
                // LA TRANSMISIÓN FUÉ EXITOSA \\
                // ========================== \\

                if (TransmisionExitosa && !(Resultado != null && Resultado.ErroresDB != null && Resultado.ErroresDB.Any()))
                {
                    // Marcar el proceso para que el log quede pequeño.
                    Proceso.Estado = (int)eEstadoProcesoCola.Transmitido;
                    if (!Proceso.NombreDeclarante.Contains('/') && !string.IsNullOrEmpty(Resultado.Declaracion.DeclaracionNumero))
                    {
                        Proceso.NombreDeclarante = string.Format("{0} / {1}", Proceso.NombreDeclarante, Resultado.Declaracion.DeclaracionNumero);
                    }
                    //Proceso.AdvertenciasDB = null;
                    Proceso.ErroresDB = null;
                    Proceso.FechaUltimaTransmision = DateTime.Now;

                    // Sobreescribir la declaración en disco con la retornada por el servicio.
                    Resultado.Declaracion.DocumentoDigital = null;
                    Resultado.Declaracion.SoloLectura = true;
                    if (File.Exists(RutaCola(Proceso.ArchivoDeclaracion)))
                        File.Delete(RutaCola(Proceso.ArchivoDeclaracion));

                    RUV.I.Util.GrabarArchivoSerializado<clsDeclaracion>(
                      RutaCola(Proceso.ArchivoDeclaracion),
                      Resultado.Declaracion);

                    RUV.I.LocalDB.Save<clsProceso>(Proceso);
                    RUV.I.LocalDB.Flush();

                    // Ahora la declaración no se quita de la cola para que se puede
                    // re-abrir e imprimir, si se necesita.

                    // Quitar los archivos relacionados.
                    //if (File.Exists(RutaCola(Proceso.ArchivoDeclaracion)))
                    //    File.Delete(RutaCola(Proceso.ArchivoDeclaracion));
                    if (!string.IsNullOrEmpty(Proceso.ArchivoDocumentoEscaneado))
                    {
                        if (File.Exists(RutaCola(Proceso.ArchivoDocumentoEscaneado)))
                            File.Delete(RutaCola(Proceso.ArchivoDocumentoEscaneado));
                    }

                    //if (File.Exists(DeclaTrans.DocumentoDigital.ToString()))
                    //    File.Delete(DeclaTrans.DocumentoDigital.ToString());

                    if (File.Exists(DeclaTrans.DocumentoDigitalNombre))
                        File.Delete(DeclaTrans.DocumentoDigitalNombre);

                    string NombreDeclarante = Proceso.NombreDeclarante;

                    // Enjuague y repita.
                    EstablecerEstadoCola(
                      "Última transmisión exitosa: " + Proceso.NombreDeclarante,
                      eEstadoProcesoCola.Transmitido);
                    //if (File.Exists(RutaCola(Proceso.ArchivoDeclaracion)))
                    //File.Delete(RutaCola(Proceso.ArchivoDeclaracion));

                    // Quitarlo de la memoria (la cola de lo pendiente).
                    lock (ColaLock)
                    {
                        this.Dispatcher.Invoke(
                        new Action(() => ListaProcesos.Remove(Proceso)),
                        System.Windows.Threading.DispatcherPriority.Normal, null);
                        transmitidook = true;
                        Proceso = null;
                    }

                    if (transmitidook)
                    {
                        RUV.I.UIPrincipal.Notificar(null, "Se transmitio con exito: " + NombreDeclarante);
                        if (RUV.I.IdValoracion > 0) //si se esta editando desde valoracion
                        {
                            try
                            {
                                RUV.I.ColaProcesos.DetenerCola();
                                RUV.I.IdDeclaracion = -1;
                                Application.Current.Shutdown();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    else
                    {
                        RUV.I.UIPrincipal.Notificar(null, "Requiere revisión la declaración de: " + Proceso.NombreDeclarante, Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Warning);
                        //break;
                    }
                }
                else
                {
                    lock (ColaLock)
                    {
                        this.Dispatcher.Invoke(
                        new Action(() =>
                        {
                            ListaProcesos.Remove(Proceso);
                            ListaProcesos.Add(Proceso);
                        }), System.Windows.Threading.DispatcherPriority.Normal, null);

                        Proceso = null;
                    }
                }

            }
        }

        #endregion

    }
}