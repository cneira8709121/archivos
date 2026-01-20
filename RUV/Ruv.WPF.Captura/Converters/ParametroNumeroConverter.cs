using System;
using System.Linq;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Para un número que señala un parámetro retorna el número indicado.
    /// </summary>
    class ParametroNumeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            int Valor = System.Convert.ToInt32(value);
            var Param = RUV.I.InfoGeneral.ListaParametros.FirstOrDefault(x => x.Id == Valor);
            if (Param == null)
                return null;
            else
                return Param.Numero;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
