using System;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{

    /// <summary>
    /// Retorna información para el declarante en impresión.
    /// </summary>
    public class DeclaranteImpresionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (RUV.I.Util.EnModoDeDiseño) return null;

            if (parameter == null) return null;

            clsPersonaAfectada PA = RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
              .FirstOrDefault(x => x.ID == RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.TomaDeclaracion.DeclaranteId
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

                case "IDENTIFICACIONCOLILLA":
                    var Valores = Enum.GetValues(
                      typeof(Ruv.Infrastructure.Crosscutting.Common.eTipoDocumentoSinNumero)).Cast<int>();

                    // Si el tipo de documento no incluye el número, no se retorna nada.
                    if (PA.TipoDocumento.HasValue
                      && !Valores.Contains(PA.TipoDocumento.Value))
                    {
                        Resultado = string.Format(", identificado(a) con {0} {1} de _______________________________",
                          RUV.I.InfoGeneral.ListaTiposDocumentos
                          .FirstOrDefault(x => x.Id == PA.TipoDocumento.Value).Nombre,
                          PA.NumeroDocumento);
                    }
                    else
                    {
                        Resultado = "";
                    }
                    break;

                case "REPRESENTANTENOMBRECOMPLETO":
                    Resultado = "el texto que es";
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
