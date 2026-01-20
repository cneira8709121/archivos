using System;
using System.Linq;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{

    /// <summary>
    /// Multibinding que recibe dos propiedades para el caso de los entornos:
    /// EntornoId (int32)
    /// EntornoOtro (String)
    /// Determina la no vacía y retorna el texto correspondiente.
    /// </summary>
    public class ConsultaDatoImpresionConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !value.Any() || parameter == null) return "";

            string Resultado = string.Empty;
            string Indicativo = string.Empty;

            long Valor1 = 0;
            string Valor2 = string.Empty;
            string Valor3 = string.Empty;

            if (value[0] != null)
            {
                long.TryParse(value[0].ToString(), out Valor1);
            }
            if (value[1] != null)
            {
                Valor2 = value[1].ToString();
            }

            switch (System.Convert.ToString(parameter).ToUpper())
            {
                case "INDICATIVO_CELULAR":

                    if (Valor1 > 0)
                    {
                        var DatoPais = RUV.I.InfoGeneral.ListaPaises
                            .FirstOrDefault(x => x.Id == Valor1);

                        if (DatoPais != null && DatoPais.CodigoTelefono.HasValue)
                            Indicativo = string.Format("({0})", DatoPais.CodigoTelefono.Value.ToString());
                    }
                    Resultado = string.Format("{0} {1}", Indicativo, Valor2);
                    break;
                case "INDICATIVO_FIJO":
                    if (Valor1 > 0)
                    {
                        if (value[2] != null)
                        {
                            Valor3 = value[2].ToString();
                        }
                        var DatoPais = RUV.I.InfoGeneral.ListaPaises
                                    .FirstOrDefault(x => x.Id == Valor1);

                        if (DatoPais.CodigoTelefono.HasValue)
                            Indicativo = DatoPais.CodigoTelefono.Value.ToString();

                    }
                    Resultado = string.Format("({0} {1}) {2}", Indicativo, Valor2, Valor3);
                    break;
            }
            return Resultado;
        }



        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
