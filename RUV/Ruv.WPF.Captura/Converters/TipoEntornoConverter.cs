using System;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Convierte entre el tipo eTipoConverter y su equivalente entero.
    /// </summary>
    public class TipoEntornoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            eTipoEntorno TE;
            if (Enum.TryParse<eTipoEntorno>(System.Convert.ToString(value), out TE))
                return (int)TE;
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            int Numero;
            if (int.TryParse(System.Convert.ToString(value), out Numero))
            {
                eTipoEntorno TE;
                if (Enum.TryParse<eTipoEntorno>(System.Convert.ToString(value), out TE))
                    return TE;
                else
                    return null;
            }
            else
                return null;
        }
    }
}
