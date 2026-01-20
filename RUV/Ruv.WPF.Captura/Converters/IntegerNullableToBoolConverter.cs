using System;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    [ValueConversion(typeof(int?), typeof(bool))]
    class IntegerNullableToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? nValue = (int?)value;
            if (!nValue.HasValue) return false;
            if (nValue <= 0 || nValue > 1) return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
