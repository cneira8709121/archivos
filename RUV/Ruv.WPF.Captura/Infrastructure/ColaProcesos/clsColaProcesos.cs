using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Ionic.Zip;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Utilities;

namespace Ruv.WPF.Captura.Infrastructure.ColaProcesos
{
    public partial class clsColaProcesos : DependencyObject
    {
        #region CONSTRUCTOR

        public clsColaProcesos()
            : base()
        {

        }

        public void InicializarColaProcesos()
        {
            // Diego Alvarez - Este código unicamente se utiliza para pruebas de cargue de declaraciones, 
            // NO se debe subir a ningún ambiente.
            //******************************************************************************************************
            //RUV.I.LocalDB.Purge();
            //DirectoryInfo di = new DirectoryInfo(@"C:\Users\diego.alvarez\AppData\LocalRUVWPF_Desarrollo\Cola");
            //List<clsDeclaracion> declaraciones = new List<clsDeclaracion>();
            //List<clsColaProcesos> procesos = new List<clsColaProcesos>();
            //bool primero = true;
            //foreach (FileInfo fi in di.GetFiles())
            //{
            //    if (fi.Extension.Equals(""))
            //    {
            //        clsProceso Proceso = new clsProceso()
            //        {
            //            Id = Guid.NewGuid().ToString() + RUV.I.Usuario.Id.ToString(),
            //            NombreDeclarante = RUV.I.Usuario.Id.ToString(),
            //            AdvertenciasDB = new System.Collections.Specialized.StringCollection(),
            //            ErroresDB = new System.Collections.Specialized.StringCollection(),
            //            ArchivoDeclaracion = fi.Name,
            //            ArchivoDocumentoEscaneado = "2f94fb15-fe47-4fb8-9d45-e639c972de6c.tif",
            //            Estado = (int)eEstadoProcesoCola.PendienteTransmitir,
            //            FechaEnCola = primero ? DateTime.Now : DateTime.Now.AddMinutes(5),
            //            FechaUltimaTransmision = null
            //        };
            //        primero = false;
            //        // 4) Agregarlo a la base de datos local.
            //        RUV.I.LocalDB.Save<clsProceso>(Proceso);
            //        RUV.I.LocalDB.Flush();
            //    }
            //}

            //******************************************************************************************************

            EstablecerEstadoCola("Inicializando cola de procesos", eEstadoProcesoCola.Ninguno);

            ListaProcesos = new ObservableCollection<clsProceso>();

            // Obtener la cola actual desde la base de datos local.
            List<clsProceso> Lista = new List<clsProceso>();
            string usuarioId = RUV.I.Usuario.Id.ToString();
            try
            {
                if (RUV.I.LocalDB.Query<clsProceso, string>().Any())
                    try
                    {
                        // Diego Alvarez - 15/11/2013 - Ajuste para filtrar la cola de transmisión por usuario
                        var lst = RUV.I.LocalDB.Query<clsProceso, string>().ToList();
                        foreach (var item in lst)
                        {
                            if(item.LazyValue != null)
                            {
                                try
                                {
                                    if (item.LazyValue.Value != null)
                                    {
                                        if (item.LazyValue.Value.Estado == (int)eEstadoProcesoCola.PendienteTransmitir ||
                                    item.LazyValue.Value.Estado == (int)eEstadoProcesoCola.RequiereRevision ||
                                    item.LazyValue.Value.Estado == (int)eEstadoProcesoCola.Transmitiendo)
                                        {
                                            if(
                                                item.LazyValue.Value.Id != string.Empty && item.LazyValue.Value.Id.Length > 0 ?
                                                item.LazyValue.Value.Id.ToString().Substring(item.LazyValue.Value.Id.Length - usuarioId.Length, usuarioId.Length) == usuarioId
                                                : item.LazyValue.Value.Id == item.LazyValue.Value.Id
                                              )
                                            {
                                                Lista.Add(item.LazyValue.Value);
                                            }
                                        }
                                    }
                                }
                                catch(Exception e)
                                {
                                }
                            }
                        }

                        //Lista = RUV.I.LocalDB.Query<clsProceso, string>()
                        //    .Where(x =>
                        //        (x.LazyValue.Value != null && (
                        //        x.LazyValue.Value.Estado == (int)eEstadoProcesoCola.PendienteTransmitir ||
                        //        x.LazyValue.Value.Estado == (int)eEstadoProcesoCola.RequiereRevision ||
                        //        x.LazyValue.Value.Estado == (int)eEstadoProcesoCola.Transmitiendo) &&
                        //        (x.LazyValue.Value.Id != string.Empty && x.LazyValue.Value.Id.Length > 0? 
                        //        x.LazyValue.Value.Id.ToString().Substring(x.LazyValue.Value.Id.Length - usuarioId.Length, usuarioId.Length) == usuarioId
                        //        : x.LazyValue.Value.Id == x.LazyValue.Value.Id)
                        //        ))
                        //    .Select(x => x.LazyValue.Value);
                    }
                    catch(Exception e)
                    {
                        Lista = null;
                    }

            }
            catch
            {
            }

            if (Lista != null && Lista.Any())
            {
                // Agregar la cola en disco a la cola en memoria.
                Lista.ToList().ForEach(x => ListaProcesos.Add(x));
            }

            Inicializar();
            ActivarBackgroundWorker();

            EstablecerEstadoCola(null, eEstadoProcesoCola.Ninguno);
        }

        #endregion

        #region AGREGAR UN PROCESO

        /// <summary>
        /// Bloqueo para el acceso de la cola.
        /// </summary>
        static object ColaLock = new object();

        /// <summary>
        /// Agrega una declaración a la cola de transmisión.
        /// </summary>
        /// <param name="declaracion"></param>
        public void AgregarProceso(clsDeclaracion declaracion)
        {
            // 0) Si es un proceso que ya existe en la cola, procesarlo como actualización.
            if (ActualizarProceso(declaracion)) return;

            // 1) Grabar en la carpeta de colas la declaración por transmitir.
            string ArchivoDeclaracion = Guid.NewGuid().ToString();
            string documentoDigital = string.Empty;
            declaracion.CodigoProceso = ArchivoDeclaracion;

            
            // 2) Mover el archivo de documento.
            // Si documento digital es nulo, entonces es un toma en línea, de lo contrario es una digitación o glosa
            if (declaracion.DocumentoDigital == null)
            {
                // Si el existe el archivo a transmitir entonces se mueve a la cola de procesos
                if ((!string.IsNullOrEmpty(declaracion.DocumentoDigitalNombre) &&
                    File.Exists(RutaRaiz(declaracion.DocumentoDigitalNombre))) || (!string.IsNullOrEmpty(declaracion.DocumentosSoporteNombre) &&
                    File.Exists(RutaRaiz(declaracion.DocumentosSoporteNombre))))
                {
                    var tmpFileUnido = Path.GetTempFileName() + ".zip";
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                        if (!string.IsNullOrWhiteSpace(declaracion.DocumentoDigitalNombre))
                        {
                            zip.AddFile(declaracion.DocumentoDigitalNombre,"Declaracion_Firmada");
                        }
                        if (!string.IsNullOrWhiteSpace(declaracion.DocumentosSoporteNombre))
                        {
                            zip.AddFile(declaracion.DocumentosSoporteNombre, "Soportes");
                        }
                        zip.Save(tmpFileUnido);
                    }
                    string fileName = Path.GetFileName(tmpFileUnido);
                    documentoDigital = fileName;
                    File.Move(tmpFileUnido,
                      RutaCola(fileName));
                    declaracion.DocumentoDigitalNombre = RutaCola(fileName);
                    declaracion.DocumentoDigital = File.ReadAllBytes(RutaCola(fileName));

                }
                else
                {
                    // Si la declaración viene como ZIP de documentos XPS permite la transmisión de la misma.
                    if (declaracion.DocumentoAnexo == null)
                    {
                        lock (ColaLock)
                        {
                            // 3) Agregarlo a la cola.
                            clsProceso Proceso = new clsProceso()
                            {
                                Id = Guid.NewGuid().ToString() + RUV.I.Usuario.Id.ToString(),
                                NombreDeclarante = string.IsNullOrEmpty(declaracion.DeclaracionNumero) ? declaracion.TomaDeclaracion.DeclaranteNombreCompleto : string.Format("{0} / {1}", declaracion.TomaDeclaracion.DeclaranteNombreCompleto, declaracion.DeclaracionNumero),
                                AdvertenciasDB = new System.Collections.Specialized.StringCollection(),
                                ErroresDB = new System.Collections.Specialized.StringCollection(),
                                ArchivoDeclaracion = ArchivoDeclaracion,
                                ArchivoDocumentoEscaneado = documentoDigital,
                                Estado = (int)eEstadoProcesoCola.RequiereRevision,
                                FechaEnCola = DateTime.Now,
                                FechaUltimaTransmision = null
                            };

                            //Jhon TL1 -> Si el nombre de declarante esta null lo dejamos empity
                            if (Proceso.NombreDeclarante == null)
                                Proceso.NombreDeclarante = string.Empty;

                            Proceso.ErroresDB.Add("Se debe cargar la declaración escaneada.");

                            ListaProcesos.Add(Proceso);

                            // 4) Agregarlo a la base de datos local.
                            RUV.I.LocalDB.Save<clsProceso>(Proceso);
                            RUV.I.LocalDB.Flush();
                        }

                        ActivarBackgroundWorker();
                        return;
                    }
                }
            }
            RUV.I.Util.GrabarArchivoSerializado<clsDeclaracion>(
              RutaCola(ArchivoDeclaracion), declaracion);


            lock (ColaLock)
            {
                // 3) Agregarlo a la cola.
                clsProceso Proceso = new clsProceso()
                {
                    Id = Guid.NewGuid().ToString() + RUV.I.Usuario.Id.ToString(),
                    NombreDeclarante = string.IsNullOrEmpty(declaracion.DeclaracionNumero) ? declaracion.TomaDeclaracion.DeclaranteNombreCompleto : string.Format("{0} / {1}", declaracion.TomaDeclaracion.DeclaranteNombreCompleto, declaracion.DeclaracionNumero),
                    AdvertenciasDB = new System.Collections.Specialized.StringCollection(),
                    ErroresDB = new System.Collections.Specialized.StringCollection(),
                    ArchivoDeclaracion = ArchivoDeclaracion,
                    ArchivoDocumentoEscaneado = declaracion.DocumentoDigitalNombre,
                    Estado = (int)eEstadoProcesoCola.PendienteTransmitir,
                    FechaEnCola = DateTime.Now,
                    FechaUltimaTransmision = null
                };

                //Jhon TL1 -> Si el nombre de declarante esta null lo dejamos empity
                if (Proceso.NombreDeclarante == null)
                    Proceso.NombreDeclarante = string.Empty;

                ListaProcesos.Add(Proceso);

                // 4) Agregarlo a la base de datos local.
                RUV.I.LocalDB.Save<clsProceso>(Proceso);
                RUV.I.LocalDB.Flush();
            }

            // 5) Activar el proceso en el background.
            ActivarBackgroundWorker();
        }

        /// <summary>
        /// Actualiza el proceso si existía previamente en la lista de procesos.
        /// Retorna verdadero si la actualización se realizó.
        /// </summary>
        /// <param name="declaracion"></param>
        /// <returns></returns>
        bool ActualizarProceso(clsDeclaracion declaracion)
        {
            clsProceso Proceso = null;

            // recorrido para quitar los null del NombreDeclarante
            foreach ( var listaProceso in ListaProcesos )
            {
                if (listaProceso.NombreDeclarante == null)
                    listaProceso.NombreDeclarante = string.Empty;
            }

            // Para declaraciones con número de formulario existente (i.e Digitación), evitar duplicidad de lineas en cola de procesos
            var existingElement = ListaProcesos.FirstOrDefault(x => x.NombreDeclarante.Contains("/") && x.NombreDeclarante.Split('/').Length == 2 && x.NombreDeclarante.Split('/')[1].Trim() == declaracion.DeclaracionNumero);
            if (existingElement != null && !string.IsNullOrWhiteSpace(existingElement.ArchivoDeclaracion))
            {
                declaracion.CodigoProceso = existingElement.ArchivoDeclaracion;
            }

            if (!string.IsNullOrWhiteSpace(declaracion.CodigoProceso))
            {
                Proceso = ListaProcesos.FirstOrDefault(x => x.ArchivoDeclaracion == declaracion.CodigoProceso);
                if (Proceso == null) return false;
                else
                {
                    // 1) Borrar las versiones anteriores en la cola.
                    if (File.Exists(RutaCola(declaracion.CodigoProceso)))
                        File.Delete(RutaCola(declaracion.CodigoProceso));

                    // 2) Mover el archivo de documento.
                    if (!RUV.I.Configuraciones.ConfiguracionGeneral.PreservarBorradorDespuesDeEnvio)
                    {
                        if (declaracion.DocumentoDigital == null)
                        {
                            // Si el existe el archivo a transmitir entonces se mueve a la cola de procesos
                            if ((!string.IsNullOrEmpty(declaracion.DocumentoDigitalNombre) &&
                                File.Exists(RutaRaiz(declaracion.DocumentoDigitalNombre))) || (!string.IsNullOrEmpty(declaracion.DocumentosSoporteNombre) &&
                                File.Exists(RutaRaiz(declaracion.DocumentosSoporteNombre))))
                            {
                                var tmpFileUnido = Path.GetTempFileName() + ".zip";
                                using (ZipFile zip = new ZipFile())
                                {
                                    if (!string.IsNullOrWhiteSpace(declaracion.DocumentoDigitalNombre))
                                    {
                                        zip.AddFile(declaracion.DocumentoDigitalNombre, "Declaracion_Firmada");
                                    }
                                    if (!string.IsNullOrWhiteSpace(declaracion.DocumentosSoporteNombre))
                                    {
                                        zip.AddFile(declaracion.DocumentosSoporteNombre, "Soportes");
                                    }
                                    zip.Save(tmpFileUnido);
                                }
                                string fileName = Path.GetFileName(tmpFileUnido);
                                File.Move(tmpFileUnido,
                                  RutaCola(fileName));

                                declaracion.DocumentoDigital = File.ReadAllBytes(RutaCola(fileName));
                                declaracion.DocumentoDigitalNombre = RutaCola(fileName);
                            }
                        }
                    }

                    // 3) Grabar en la carpeta de colas la declaración por transmitir.
                    RUV.I.Util.GrabarArchivoSerializado<clsDeclaracion>(
                      RutaCola(declaracion.CodigoProceso), declaracion);

                    // Vaciar los errores.
                    Proceso.AdvertenciasDB.Clear();
                    Proceso.ErroresDB.Clear();

                    lock (ColaLock)
                    {
                        // 5) Actualizar algunos datos del proceso.
                        Proceso.NombreDeclarante = declaracion.TomaDeclaracion.DeclaranteNombreCompleto + (string.IsNullOrWhiteSpace(declaracion.DeclaracionNumero) ? string.Empty : " / " + declaracion.DeclaracionNumero);
                        Proceso.Estado = (int)eEstadoProcesoCola.PendienteTransmitir;

                        // 6) Actualizarlo en la base de datos local.
                        RUV.I.LocalDB.Save<clsProceso>(Proceso);
                        RUV.I.LocalDB.Flush();
                    }

                    // 7) Activar el proceso en el background.
                    ActivarBackgroundWorker();

                    return true;
                }
            }
            else
                return false;
        }

        #endregion

        #region HABLITAR UN PROCESO PARA EDICIÓN

        /// <summary>
        /// Lanza la edición de una declaración.
        /// </summary>
        /// <param name="proceso"></param>
        public void LanzarEdicionProceso(clsProceso proceso)
        {
            // 1) Copiar el archivo adjunto a la carpeta raiz.
            string fileName = Path.GetFileName(proceso.ArchivoDocumentoEscaneado);

            if (!string.IsNullOrEmpty(proceso.ArchivoDocumentoEscaneado)
                && File.Exists(RutaCola(fileName)))
            {
                if (File.Exists(RutaRaiz(fileName)))
                    File.Delete(RutaRaiz(fileName));

                File.Copy(
                  RutaCola(fileName),
                  RutaRaiz(fileName));
            }

            // 2) De-serializar la declaración.
            var Declaracion = RUV.I.Util.CargarArchivoSerializado<clsDeclaracion>(
              RutaCola(proceso.ArchivoDeclaracion));

            Declaracion.CrearEnlacesPostCargue();

            // 3) Lanzar la edición de la declaración.
            var RV = new Ruv.WPF.Captura.Registro.RegistroVista(Declaracion);
            RUV.I.UIPrincipal.NavegarA(RV);
        }

        public byte[] ExportarColaExcel(List<clsProceso> ColaProceso, ref string cError)
        {
            if (ColaProceso == null || ColaProceso.Count <= 0)
            {
                cError = "No hay datos a exportar";
                return null;
            }

            List<clsProcesoExcel> lstColaProceso =
                ColaProceso.Select(x =>
                    {
                        string[] sDeclaranteFud = x.NombreDeclarante.Split(new char[] { '/' });
                        string sDeclarante = sDeclaranteFud[0];
                        string sFUD = sDeclaranteFud.Length > 1 ? sDeclaranteFud[1] : null;
                        return new clsProcesoExcel { FUD = sFUD, NombreDeclarante = sDeclarante, FechaEnCola = x.FechaEnCola, FechaUltimaTransmision = x.FechaUltimaTransmision };
                    }).ToList();
            ExcelHelper eh = new ExcelHelper();
            try
            {
                return eh.ExportToExcel<clsProcesoExcel>(lstColaProceso);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }
        }

        #endregion

        #region UTILS

        /// <summary>
        /// Retorna la ruta completa de un archivo en la cola.
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        string RutaCola(string nombreArchivo)
        {
            string Carpeta =
              Path.Combine(RUV.I.Util.RutaArchivosLocales, "Cola");

            if (!Directory.Exists(Carpeta))
                Directory.CreateDirectory(Carpeta);

            return Path.Combine(Carpeta, nombreArchivo);
        }

        /// <summary>
        /// Retorna la ruta completa de un archivo en la raiz.
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        string RutaRaiz(string nombreArchivo)
        {
            return Path.Combine(RUV.I.Util.RutaArchivosLocales, nombreArchivo);
        }

        public bool PrepararInfoColilla(clsProceso proceso, ref clsDeclaracion declaracion)
        {
            if (proceso == null || (eEstadoProcesoCola)proceso.Estado != eEstadoProcesoCola.Transmitido) return false;

            declaracion = RUV.I.Util.CargarArchivoSerializado<clsDeclaracion>(
              RutaCola(proceso.ArchivoDeclaracion));

            //declaracion = RUV.I.Red.ServicioGeneral.ObtenerDeclaracion(declaracion.ID.Value, RUV.I.Seguridad.LlaveUsuario);

            return true;
        }

        #endregion

        #region PROPIEDADES

        private ObservableCollection<clsProceso> _ListaProcesos;
        /// <summary>
        /// La lista de procesos.
        /// </summary>
        public ObservableCollection<clsProceso> ListaProcesos
        {
            get { return _ListaProcesos; }
            set { _ListaProcesos = value; }
        }

        /// <summary>
        /// Establece el último mensaje de la cola.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="estado"></param>
        void EstablecerEstadoCola(string mensaje, eEstadoProcesoCola estado)
        {
            RUV.I.UIPrincipal.Dispatcher.BeginInvoke(
             System.Windows.Threading.DispatcherPriority.Normal,
             new Action(() =>
                {
                    UltimoMensajeCola = mensaje;
                    UltimoEstadoCola = estado;
                }));
        }

        public string UltimoMensajeCola
        {
            get { return (string)GetValue(UltimoMensajeColaProperty); }
            set { SetValue(UltimoMensajeColaProperty, value); }
        }

        public static readonly DependencyProperty UltimoMensajeColaProperty =
            DependencyProperty.Register("UltimoMensajeCola", typeof(string),
            typeof(clsColaProcesos), new UIPropertyMetadata(null));

        public eEstadoProcesoCola UltimoEstadoCola
        {
            get { return (eEstadoProcesoCola)GetValue(UltimoEstadoColaProperty); }
            set { SetValue(UltimoEstadoColaProperty, value); }
        }

        public static readonly DependencyProperty UltimoEstadoColaProperty =
            DependencyProperty.Register("UltimoEstadoCola", typeof(eEstadoProcesoCola),
            typeof(clsColaProcesos), new UIPropertyMetadata(null));

        #endregion

        #region VACIAR LA COLA DE PROCESOS

        /// <summary>
        /// Borrar toda la cola de procesos.
        /// </summary>
        public void PurgarCola()
        {
            // Vaciar los archivos.
            //foreach (var item in (new DirectoryInfo(RutaCola(""))).GetFiles())
            //  item.Delete();

            // Vaciar la tabla.
            RUV.I.LocalDB.Truncate(typeof(clsProceso));

            // Vaciar la lista.
            ListaProcesos.Clear();
        }

        #endregion
    }
}