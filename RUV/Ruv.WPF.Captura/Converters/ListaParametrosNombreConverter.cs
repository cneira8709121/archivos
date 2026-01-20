using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Para una lista de enteros retorna los nombres de los 
    /// parámetros correspondientes.
    /// </summary>
    public class ListaParametrosNombreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (RUV.I.Util.EnModoDeDiseño) return null;

            //CAMBIO: Int?
            var Lista = value as List<int?>;
            if (Lista == null) return null;

            var Resultado = from par in RUV.I.InfoGeneral.ListaParametros
                            where Lista.Contains(par.Id)
                            orderby par.Nombre
                            select par.Nombre;

            return Resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
