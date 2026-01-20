using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Collections.ObjectModel;

namespace Ruv.WPF.Captura.Converters
{

    /// <summary>
    /// Agrega a una lista de personas afectadas una selección nula.
    /// </summary>
    public class ListaPersonasAfectadasNuloConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var Fuente = value as ObservableCollection<clsPersonaAfectada>;

            clsPersonaAfectada Nulo = new clsPersonaAfectada { ID = null };
            List<clsPersonaAfectada> Lista = new List<clsPersonaAfectada>();
            Lista.Add(Nulo);

            var Resultado = Lista.AsEnumerable().Concat(Fuente);

            return Resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
