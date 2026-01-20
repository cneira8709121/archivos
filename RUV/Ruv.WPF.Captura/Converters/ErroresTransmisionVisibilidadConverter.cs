using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Ruv.WPF.Captura.Infrastructure.ColaProcesos;

namespace Ruv.WPF.Captura.Converters
{

    /// <summary>
    /// Si existe la lista de errores se retorna visibilidad.
    /// </summary>
    public class ErroresTransmisionVisibilidadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            clsProceso Proceso = value as clsProceso;
            Visibility Resultado = Visibility.Collapsed;

            if (Proceso != null)
                if (
                  (Proceso.ErroresDB != null && Proceso.ErroresDB.AsEnumerable().Any())
                  || (Proceso.AdvertenciasDB != null && Proceso.AdvertenciasDB.AsEnumerable().Any()))
                    Resultado = Visibility.Visible;

            return Resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
