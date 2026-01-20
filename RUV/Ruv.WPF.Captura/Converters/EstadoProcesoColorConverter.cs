using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Ruv.WPF.Captura.Converters
{

    /// <summary>
    /// Retorna un color para cada estado de la cola de proceso.
    /// </summary>
    public class EstadoProcesoColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            eEstadoProcesoCola Estado;
            if (!Enum.TryParse<eEstadoProcesoCola>(System.Convert.ToString(value), out Estado))
                return App.Current.Resources["ColorPendienteTransmitir"] as SolidColorBrush;

            SolidColorBrush Resultado = null;

            switch (Estado)
            {
                case eEstadoProcesoCola.PendienteTransmitir:
                    Resultado = App.Current.Resources["ColorPendienteTransmitir"] as SolidColorBrush;
                    break;
                case eEstadoProcesoCola.Transmitiendo:
                    Resultado = App.Current.Resources["ColorTransmitiendo"] as SolidColorBrush;
                    break;
                case eEstadoProcesoCola.Transmitido:
                    Resultado = App.Current.Resources["ColorTransmitido"] as SolidColorBrush;
                    break;
                case eEstadoProcesoCola.RequiereRevision:
                    Resultado = App.Current.Resources["ColorRequiereRevision"] as SolidColorBrush;
                    break;
                case eEstadoProcesoCola.Ninguno:
                    Resultado = App.Current.Resources["ColorNinguno"] as SolidColorBrush;
                    break;
            }

            return Resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
