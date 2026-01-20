using System;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{

  /// <summary>
  /// Dado el número de años, meses y días, returna el texto correspondiente.
  /// </summary>
    public class CantidadTiempoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null) return null;

            string Resultado = null;
            switch (System.Convert.ToString(parameter).ToUpper())
            {
                case "VICTIMA05TIEMPORESIDENCIA":
                    var Victima = value as clsAnexo05;
                    if (Victima != null)
                    {
                        Resultado = string.Format("{0} Años, {1} Meses, {2} Días",
                          TextoNumerico(Victima.TiempoResidenciaEnLugarExpulsorAños),
                          TextoNumerico(Victima.TiempoResidenciaEnLugarExpulsorMeses),
                          TextoNumerico(Victima.TiempoResidenciaEnLugarExpulsorDias));
                    }
                    break;
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
