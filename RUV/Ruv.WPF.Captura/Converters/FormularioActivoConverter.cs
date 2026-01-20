using System;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Converters
{
    [ValueConversion(typeof(eEstadoFormulario), typeof(bool))]
    public class FormularioActivoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bActive = false;
            if ((eEstadoFormulario)value != eEstadoFormulario.INACTIVO) bActive = true;
            return bActive;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(bool)value) return eEstadoFormulario.INACTIVO;
            return null;
        }
    }
}
