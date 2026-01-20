using System;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Utilities;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Generals;

namespace Ruv.WPF.Captura.Converters
{
    [ValueConversion(typeof(int?), typeof(string))]
    public class ParametroNombreConverter : IValueConverter
    {
        /// <summary>
        /// Convierte un parámetro general (entero) a su respectivo nombre
        /// </summary>
        /// <param name="value">Valor del parámetro a convertir</param>
        /// <param name="targetType">Tipo del target</param>
        /// <param name="parameter">Parámetro que puede ser enviado en la conversión</param>
        /// <param name="culture"></param>
        /// <returns>Nombre del parámetro que se convirtió</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            clsParametroGeneral param = RUV.I.InfoGeneral.ListaParametros.Find(x => x.Id == (int?)value);

            if (param == null) return null;

            string cPorReemplazar = param.Nombre.Match(resx::Filters.NumeroNombreParametro);
            return cPorReemplazar == string.Empty ? param.Nombre : param.Nombre.Replace(cPorReemplazar, string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
