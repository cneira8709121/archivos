using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace System.Windows
{
    static class Extensiones2
    {

        public static string Tamaño(this Rect rectangulo)
        {
            return string.Format("({0} - {1})  ({2} - {3})",
                rectangulo.X.ToString("0.00"),
                rectangulo.Y.ToString("0.00"),
                rectangulo.Width.ToString("0.00"),
                rectangulo.Height.ToString("0.00"));
        }
    }
}

namespace System.Windows.Data
{
    static class Extensiones
    {
        /// <summary>
        /// Establece un Bingind en código.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="objetoFuente"></param>
        /// <param name="propiedadFuente"></param>
        /// <param name="objetoDestino"></param>
        /// <param name="propiedadDestino"></param>
        /// <param name="modo"></param>
        /// <param name="convertidor"></param>
        /// <param name="reportarErroresDeValidacion">True: La propiedad debe reportar errores de validación para mostrarlas en el tooltip.</param>
        public static Binding BindingEstablecer(
          object objetoFuente, string propiedadFuente,
          FrameworkElement objetoDestino, DependencyProperty propiedadDestino,
          BindingMode modo = BindingMode.OneWay,
          IValueConverter convertidor = null,
          bool reportarErroresDeValidacion = false)
        {
            Binding Bi = null;
            if (propiedadFuente != null)
                Bi = new Binding(propiedadFuente);
            else
                Bi = new Binding();
            if (objetoFuente != null)
                Bi.Source = objetoFuente;

            Bi.Mode = modo;

            if (convertidor != null) Bi.Converter = convertidor;

            if (reportarErroresDeValidacion)
            {
                Bi.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                Bi.ValidatesOnDataErrors = true;
                Bi.NotifyOnValidationError = true;
            }

            objetoDestino.SetBinding(propiedadDestino, Bi);

            return Bi;
        }

        /// <summary>
        /// Establece un Bingind en código desde una AttachedProperty.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="objetoFuente"></param>
        /// <param name="propiedadFuente"></param>
        /// <param name="objetoDestino"></param>
        /// <param name="propiedadDestino"></param>
        /// <param name="modo"></param>
        /// <param name="convertidor"></param>
        /// <param name="reportarErroresDeValidacion">True: La propiedad debe reportar errores de validación para mostrarlas en el tooltip.</param>
        public static Binding BindingEstablecerAttachedProperty(
          object objetoFuente, DependencyProperty propiedadFuente,
          FrameworkElement objetoDestino, DependencyProperty propiedadDestino,
          BindingMode modo = BindingMode.OneWay,
          IValueConverter convertidor = null,
          bool reportarErroresDeValidacion = false)
        {
            Binding Bi = null;
            if (propiedadFuente != null)
            {
                Bi = new Binding();
                Bi.Path = new PropertyPath(propiedadFuente);
            }
            else
                Bi = new Binding();
            if (objetoFuente != null)
                Bi.Source = objetoFuente;

            Bi.Mode = modo;

            if (convertidor != null) Bi.Converter = convertidor;

            if (reportarErroresDeValidacion)
            {
                Bi.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                Bi.ValidatesOnDataErrors = true;
                Bi.NotifyOnValidationError = true;
            }

            BindingOperations.SetBinding(objetoDestino, propiedadDestino, Bi);
            //objectoDestino.SetBinding(propiedadDestino, Bi);
            return Bi;
        }

        /// <summary>
        /// Refresca un binding.
        /// </summary>
        /// <param name="objetoDestino"></param>
        /// <param name="propiedadDestino"></param>
        public static void BindingRefrescar(DependencyObject objetoDestino, DependencyProperty propiedadDestino)
        {
            BindingOperations.GetBindingExpressionBase(objetoDestino, propiedadDestino).UpdateTarget();
        }

        public static void DoEvents(this Application application)
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new ExitFrameHandler(frm => frm.Continue = false), frame);
            Dispatcher.PushFrame(frame);
        }

        private delegate void ExitFrameHandler(DispatcherFrame frame);

    }
}

namespace System.Collections.Specialized
{
    static class Extensiones
    {
        public static IEnumerable<string> AsEnumerable(this StringCollection coleccion)
        {
            if (coleccion == null) return null;
            return coleccion.Cast<string>().AsEnumerable();
        }

    }
}
