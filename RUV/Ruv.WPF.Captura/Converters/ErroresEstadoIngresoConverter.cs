using System;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    class ErroresEstadoIngresoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool ContenedorEsCorrecto = System.Convert.ToBoolean(value);
            if (!ContenedorEsCorrecto)
                return eEstadoIngreso.IngresoIncompleto;
            else
                return eEstadoIngreso.IngresoCompleto;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
