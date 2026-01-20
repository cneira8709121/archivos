using System;
using System.Collections.Generic;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;

namespace Ruv.WPF.Captura.Converters
{
    public class FirmasConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is List<clsFirma>)
            {
                List<clsFirma> lstFirmas = (List<clsFirma>)value;
                if (lstFirmas.Count > 0)
                {
                    byte[] firma = null;
                    foreach (clsFirma x in lstFirmas)
                    {
                        if (x.firmaOwner.ToString() == (string)parameter)
                        {
                            firma = x.firma;
                            break;
                        }
                    }
                    return firma;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
