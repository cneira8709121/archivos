using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Para una ilsta de enteros, retorna la lista de personas afectadas.
    /// En value se obtiene el objeto de datos del anexo.
    /// </summary>
    class ListaAfectadosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            IEnumerable<int> Lista = value as IEnumerable<int>;
            if (Lista == null) return null;

            return RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas
              .Where(x => x.EstadoRegistro != eEstadoRegistro.Eliminado
                && Lista.Contains(x.ID.Value))
              .OrderBy(x => x.NombreCompleto);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
