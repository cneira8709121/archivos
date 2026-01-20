using System.Linq;
using System.Windows.Controls;
using Ruv.WPF.Captura.Controles;

namespace Ruv.WPF.Captura.Registro
{
    public partial class RegistroVista : Page
    {

        /// <summary>
        /// ¿Se puede ejecutar el comando de grabación?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrabarDeclaracion_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            if (RUV.I.DeclaracionActual.SoloLectura
              || btnGrabarDeclaracion.Opacity != 1d)
                e.CanExecute = false;
            else
                e.CanExecute = true;

            e.Handled = true;
        }

        /// <summary>
        /// Invocar el comando de grabación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrabarDeclaracion_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            btnGrabarDeclaracion.LanzarEventoClick();
        }

        /// <summary>
        /// Determina si se puede navegar hacia un error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UbicarError_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            var Validador = spValidadores.Children.OfType<ResumenValidacion>().FirstOrDefault();
            if (Validador == null || !Validador.SePuedeNavegar)
                e.CanExecute = false;
            else
                e.CanExecute = true;

            e.Handled = true;
        }

        /// <summary>
        /// Pasa el foco al siguiente error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UbicarSiguienteError_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var Validador = spValidadores.Children.OfType<ResumenValidacion>().FirstOrDefault();
            Validador.NavegarAdelante();
        }

        /// <summary>
        /// Pasa el foco al error anterior.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UbicarAnteriorError_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var Validador = spValidadores.Children.OfType<ResumenValidacion>().FirstOrDefault();
            Validador.NavegarAtras();
        }

        /// <summary>
        /// Siempre informa que el el comando se puede ejecutar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComandoGenerico_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        /// <summary>
        /// Comando para abrir la lista de los anexos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirListaDeAnexos_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            InvocarAnexo();
        }

        /// <summary>
        /// Comando para abrir la hoja 01.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirHoja01_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            InvocarHoja(eSeccionRegistro.H01_TomaDeclaracion);
        }

        /// <summary>
        /// Comando para abrir la hoja 02.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirHoja02_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            InvocarHoja(eSeccionRegistro.H02_PersonasAfectadas);
        }

        /// <summary>
        /// Comando para abrir la hoja 03.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirHoja03_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            InvocarHoja(eSeccionRegistro.H03_DescripcionHechos);
        }

        /// <summary>
        /// Comando para abrir la hoja 04.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirHoja04_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            InvocarHoja(eSeccionRegistro.H04_VerificacionProcedimiento);
        }

    }
}