using System;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Retorna el conjunto de parámetros correspondientes 
    /// a un grupo de parámetros.
    /// </summary>
    public class ListaDetallesGrupoParamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string NombreConjunto = System.Convert.ToString(parameter);
            if (string.IsNullOrWhiteSpace(NombreConjunto)) return null;

            eGruposParametros Grupo;
            if (!Enum.TryParse<eGruposParametros>(NombreConjunto, out Grupo))
                return null;

            return RUV.I.InfoGeneral.ListaDetallesGrupoParam(Grupo)
              .OrderBy(x => x.Nombre);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
