using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    [ValueConversion(typeof(List<int>), typeof(List<string>))]
    public class LstEnteroALstCausalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.GetType() != typeof(List<int>) || value == null) return null;

            List<int> lstId = (List<int>)value;
            IEnumerable<string> lstCausal = lstId.Select(x => RUV.I.InfoGeneral.ListaCausales.Where(y => y.NId == x).SingleOrDefault().CNombre);
            return lstCausal == null ? null : lstCausal.ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
            //if (value.GetType() != typeof(List<string>) || value == null) return null;
            
            //List<clsCausal> lstCausal = (List<clsCausal>)value;
            //IEnumerable<int> lstId = lstCausal.Select(x => x.NId);
            //return lstId == null ? null : lstId.ToList();
        }
    }
}
