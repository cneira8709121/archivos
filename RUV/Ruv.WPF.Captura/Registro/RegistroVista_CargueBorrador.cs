using System;
using System.Windows.Controls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Registro
{
    public partial class RegistroVista : Page
    {
        /// <summary>
        /// Bandera que indica si la declaración actual se ha cargado
        /// desde el borrador.
        /// </summary>
        bool HayBorradorCargado;

        /// <summary>
        /// El nombre del archivo temporal para la declaración borrador.
        /// </summary>
        string NombreArchivoBorradorDeclaracion
        {
            get
            {
                return System.IO.Path.Combine(
                  RUV.I.Util.RutaArchivosLocales, "DeclaracionBorrador.tmp");
            }
        }

        /// <summary>
        /// El nombre del archivo temporal para la declaración que se almacena antes de transmitir.
        /// </summary>
        string NombreArchivoBorradorAntesTransmicion
        {
            get
            {
                return System.IO.Path.Combine(
                  RUV.I.Util.RutaArchivosLocales, "DeclaracionSinTransmitir.tmp");
            }
        }

        /// <summary>
        /// Grabar la declaración como un borrador.
        /// Sólo hay un borrador en cualquier momento.
        /// </summary>
        void GrabarBorradorDeclaracion()
        {
            //foreach (var item in Sipod.I.DeclaracionActual.A11[0].BienesInmuebles)
            //{
            //  System.Diagnostics.Debug.WriteLine(
            //    string.Format(">> TipoPoblacionId:{0} - EntornoId:{1} - EntornoOtro:{2}",
            //    item.TipoPoblacionId,
            //    item.EntornoId,
            //    item.EntornoOtro));
            //}

            RUV.I.Util.GrabarArchivoSerializado<clsDeclaracion>(
              NombreArchivoBorradorDeclaracion,
              RUV.I.DeclaracionActual);
        }

        /// <summary>
        /// Cargar la declaración de borrador.
        /// </summary>
        void CargarBorradorDeclaracion()
        {
            try
            {


                // Verificar si el archivo existe.
                if (!System.IO.File.Exists(NombreArchivoBorradorDeclaracion))
                {
                    RUV.I.UIPrincipal.ReportarInformacionDeUsuario("No existe una declaracion grabada previamente");
                    return;
                }

                if (!RUV.I.UIPrincipal.UsuarioConfirmar(
                  "Al cargar el último borrador se perderá cualquier cambio\nque esté realizando y no haya finalizado.\n¿Desea continuar?")) return;

                RUV.I.UIPrincipal.BloquearInterfase = "Cargando";

                // Vaciar la interfaze.
                ListaSecciones.Clear();
                spValidadores.Children.Clear();
                svMain.Content = null;
                ListaSecciones.Clear();

                RUV.I.MultiTarea.EjecutarEnBackground(
                  (() =>
                  {
                      GC.Collect();
                      RUV.I.DeclaracionActual =
                        RUV.I.Util.CargarArchivoSerializado<clsDeclaracion>(NombreArchivoBorradorDeclaracion);
                      // Corregir algunos vínculos.
                      CrearDeclarantePrimeraVez();

                      RUV.I.DeclaracionActual.CrearEnlacesPostCargue();



                      // Actualizar la lista de hechos.
                      RUV.I.DeclaracionActual.ActualizarConteoHechos();

                      this.Dispatcher.Invoke(
                        new Action(() =>
                        {
                            CargueInicialTomaDeclaracion(false);
                            RUV.I.UIPrincipal.BloquearInterfase = null;
                            HayBorradorCargado = true;
                        }
                        ), System.Windows.Threading.DispatcherPriority.Normal, null);
                  }));

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Borrar los archivos de borrador.
        /// </summary>
        void BorrarBorrador()
        {
            //if (RUV.I.Configuraciones.ConfiguracionGeneral.PreservarBorradorDespuesDeEnvio) 
            if (System.IO.File.Exists(NombreArchivoBorradorDeclaracion))
                System.IO.File.Delete(NombreArchivoBorradorDeclaracion);
            return;
        }

        /// <summary>
        /// Grabar la declaración como un borrador.
        /// Sólo hay un borrador en cualquier momento.
        /// </summary>
        void GrabarBorradorAntesTransmicion()
        {
            RUV.I.Util.GrabarArchivoSerializado<clsDeclaracion>(
              NombreArchivoBorradorAntesTransmicion,
              RUV.I.DeclaracionActual);
        }
    }
}