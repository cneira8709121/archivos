using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Ruv.WPF.Captura
{
    /// <summary>
    ///  Rutinas para la obtención de una declaración.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Invoca la ventana para la búsqueda de una declaración.
        /// </summary>
        void ObtenerDeclaracionDesdeServidor()
        {
            //var Ventana = new Ruv.WPF.Captura.Registro.Secciones.Controles.ObtenerDeclaracion(); 
            var Ventana = Ruv.WPF.Captura.Registro.Secciones.Controles.ObtenerDeclaracion.GetInstance();
            Ventana.ShowDialog();

            // No se hizo selección.
            if (!Ventana.IdDeclaracionSeleccionada.HasValue)
                return;

            // Se procede a traer la declaración.
            BloquearInterfase = "Obteniendo Declaración";

            RUV.I.Red.ServicioGeneral.ObtenerDeclaracionCompleted
              += new EventHandler<GeneralService.ObtenerDeclaracionCompletedEventArgs>(ObtencionDeclaracion_Completa);
            RUV.I.Red.ServicioGeneral.ObtenerDeclaracionAsync(
              Ventana.IdDeclaracionSeleccionada.Value,
              RUV.I.Seguridad.LlaveUsuario);
        }

        /// <summary>
        /// Se obtuvo la declaración.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObtencionDeclaracion_Completa(object sender, GeneralService.ObtenerDeclaracionCompletedEventArgs e)
        {
            BloquearInterfase = null;
            if (e.Error != null)
            {
                RUV.I.Log.Registrar("ObtencionDeclaracion_Completa", e.Error);
                RUV.I.UIPrincipal.ReportarErrorDeUsuario("No fué posible obtener la declaración");
                return;
            }

            var Resultado = e.Result as Ruv.Infrastructure.Crosscutting.Common.Entidades.clsDeclaracion;

            if (Resultado != null && Resultado.TomaDeclaracion != null) Resultado.TomaDeclaracion.InicializarHechos();

            //Se crean los enlaces postcargue, incluyendo el delegado para las validaciones del anexo5
            Resultado.CrearEnlacesPostCargue();

            // Proceder a cargar la declaración.
            Ruv.WPF.Captura.Registro.RegistroVista RV = new Registro.RegistroVista(Resultado);
            Dispatcher.BeginInvoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(() =>
               {
                   frmMain.Navigate(RV);
               }));

        }
    }
}