using System;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{

  /// <summary>
  /// Para una víctima del anexo 5, retorna alguna información.
  /// </summary>
    public class Victima05Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null) return null;
            clsAnexo05_Victima Victima = value as clsAnexo05_Victima;
            if (Victima == null) return null;

            int? Resultado = null;
            switch (System.Convert.ToString(parameter).ToUpper())
            {
                case "NUMEROCONSECUTIVO":
                    var PA = RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
                  .FirstOrDefault(x => x.ID == Victima.PersonaAfectadaId);
                    if (PA == null) return null;
                    return PA.NumeroConsecutivo;

                case "SEDESPLAZO":
                    return Victima.SeDesplazo;

                case "VISIBILIDAD":
                    return System.Windows.Visibility.Visible;
            }

            return Resultado;
        }

        string TextoNumerico(int? valor)
        {
            if (!valor.HasValue)
                return "0";
            else
                return valor.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
