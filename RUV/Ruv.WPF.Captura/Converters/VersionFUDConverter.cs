using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class VersionFUDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibilidad = Visibility.Collapsed;
            int? versionFUD = (int?)value;
            if(versionFUD.HasValue)
            {
                if (parameter.Equals("VisibleEnVersion2"))
                {
                    if (versionFUD.Value == 2) 
                        visibilidad = Visibility.Visible; 
                    else 
                        visibilidad = Visibility.Collapsed;
                }
                else
                {
                    if (versionFUD.Value == 1)
                        visibilidad = Visibility.Visible;
                    else
                        visibilidad = Visibility.Collapsed;
                }
            }
            return visibilidad;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
