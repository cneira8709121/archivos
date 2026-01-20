using System;
using System.Windows;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Dado un dato del tipo int?, retorna Visibile si hay valor, de lo contrario Collapsed.
    /// </summary>
    public class HayValorVisibilidadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            int? Valor = (int?)value;
            if (Valor.HasValue)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class HayValorOFalseVisibilidadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            int? Valor = (int?)value;
            if (Valor.HasValue)
            {
                if ((int)value == 1)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
