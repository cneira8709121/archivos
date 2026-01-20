using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Multibinding que recibe dos propiedades para el caso de los entornos:
    /// EntornoId (int32)
    /// EntornoOtro (String)
    /// Determina la no vacía y retorna el texto correspondiente.
    /// </summary>
    public class GeoEntornoConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !value.Any() || parameter == null) return null;

            string Resultado = null;
            eTipoEntorno TE;

            switch (System.Convert.ToString(parameter).ToUpper())
            {
                case "TIPOENTORNO":
                    if (Enum.TryParse<eTipoEntorno>(System.Convert.ToString(value[0]), out TE))
                        Resultado = TE.ToString();
                    break;

                case "ENTORNOCOMPLETO":
                    if (value.Count() != 5) return null;
                    if (!Enum.TryParse<eTipoEntorno>(System.Convert.ToString(value[0]), out TE))
                        return null;

                    string Texto;
                    StringBuilder SB = new StringBuilder();

                    if (TE == eTipoEntorno.Urbano)
                    {
                        Texto = ObtenerPoblacion((int?)value[1], System.Convert.ToString(value[3]));
                        if (Texto != null) SB.AppendFormat("Barrio: {0}", Texto);

                        Texto = ObtenerPoblacion((int?)value[2], System.Convert.ToString(value[4]));
                        if (SB.Length >= 0) SB.Append(", ");
                        if (Texto != null) SB.AppendFormat("Localidad: {0}", Texto);
                    }
                    else if (TE == eTipoEntorno.Rural)
                    {
                        Texto = ObtenerPoblacion((int?)value[1], System.Convert.ToString(value[3]));
                        if (Texto != null) SB.AppendFormat("Vereda: {0}", Texto);

                        Texto = ObtenerPoblacion((int?)value[2], System.Convert.ToString(value[4]));
                        if (SB.Length >= 0) SB.Append(", ");
                        if (Texto != null) SB.AppendFormat("Corregimiento: {0}", Texto);
                    }

                    Resultado = SB.ToString();
                    break;
            }

            return Resultado;
        }

        string ObtenerPoblacion(int? valorId, string valorTxt)
        {
            if (!string.IsNullOrWhiteSpace(valorTxt))
            {
                return valorTxt;
            }
            else if (valorId.HasValue)
            {
                var Item = RUV.I.InfoGeneral.ListaPoblaciones
                  .FirstOrDefault(x => x.Key == valorId.Value);
                if (Item != null)
                    return Item.LazyValue.Value.Nombre;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
