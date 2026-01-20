using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WPF.Captura.Impresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Ruv.WPF.Captura.Converters
{
    public class EliminadosConverter : IValueConverter
    {
        static SolidColorBrush ColorTextoEliminado = new SolidColorBrush(Colors.Red);
        static SolidColorBrush ColorTexto = new SolidColorBrush(Colors.Black);
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            eEstadoRegistro estado = (eEstadoRegistro)value;
            if (estado == eEstadoRegistro.Eliminado)
            {
                return ColorTextoEliminado;
            }
            else
            {
                return ColorTexto;
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
