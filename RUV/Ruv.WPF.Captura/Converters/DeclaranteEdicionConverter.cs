using System;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{

  /// <summary>
  /// Retorna información para el declarante en tiempo de edición.
  /// </summary>
    public class DeclaranteEdicionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (RUV.I.Util.EnModoDeDiseño) return null;

            if (parameter == null) return null;

            clsPersonaAfectada PA = RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas
              .FirstOrDefault(x => x.ID == RUV.I.DeclaracionActual.TomaDeclaracion.DeclaranteId
                && x.ID.HasValue);

            if (PA == null) return null;

            string Resultado = null;
            switch (System.Convert.ToString(parameter).ToUpper())
            {
                case "NOMBRECOMPLETO":
                    Resultado = PA.NombreCompleto;
                    break;

                case "NUMERODOCUMENTO":
                    Resultado = PA.NumeroDocumento;
                    break;
            }

            return Resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
