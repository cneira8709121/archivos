using System;
using System.Windows;
using System.Windows.Data;
using resx = Ruv.Infrastructure.Crosscutting.Resources;

namespace Ruv.WPF.Captura.Converters
{
    [ValueConversion(typeof(bool?), typeof(Visibility))]
    class VisibilidadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? bValue = (bool?)value;
            if (bValue == null || (bool)!bValue) return (string)parameter == resx::General.ParametroNegacionLogica ? Visibility.Visible : Visibility.Collapsed;
            return (string)parameter == resx::General.ParametroNegacionLogica ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(object), typeof(Visibility))]
    class ObjetoNuloAVisibilidadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return (string)parameter == resx::General.ParametroNegacionLogica ? Visibility.Visible : Visibility.Collapsed;
            return (string)parameter == resx::General.ParametroNegacionLogica ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    class VisibilidadBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value == null)
                return Visibility.Collapsed;
            bool bValue = (bool)value;
            if (bValue) return Visibility.Visible; else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
