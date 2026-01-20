using Ruv.Infrastructure.Crosscutting.Common.General;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    public class NacionalidadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<clsParametroNacionalidad> nacionalidades = new List<clsParametroNacionalidad>();
            try
            {
                nacionalidades.Add(new clsParametroNacionalidad { Id = 0, Nacionalidad="Sin información" });
                nacionalidades.AddRange(RUV.I.InfoGeneral.ListaNacionalidades);
                return nacionalidades;
            }
            catch (Exception ex)
            {
                return nacionalidades;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
