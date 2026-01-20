using System;
using System.Linq;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{

  /// <summary>
  /// Para una NiñosNacidosPorAbusoSexual, aqui se retorna la información
  /// relacionada con la persona afectada correspondiente.
  /// </summary>
    public class VictimaA03NiñosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            int NiñoId = System.Convert.ToInt32(value);

            string parametro = System.Convert.ToString(parameter);
            if (string.IsNullOrWhiteSpace(parametro)) return null;

            string Resultado = null;

            var PA = RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
              .FirstOrDefault(x => x.ID == NiñoId);
            if (PA == null) return null;

            switch (parametro.ToLower())
            {
                case "documentoidentificacion":
                    string TipoDoc = null;
                    if (!PA.TipoDocumento.HasValue)
                        Resultado = null;
                    else
                    {
                        var Tipodoc = RUV.I.InfoGeneral.ListaTiposDocumentos
                          .FirstOrDefault(x => x.Id == PA.TipoDocumento.Value);
                        if (Tipodoc == null)
                            TipoDoc = null;
                        else
                            TipoDoc = Tipodoc.Nombre;
                    }
                    if (string.IsNullOrWhiteSpace(TipoDoc)
                      || string.IsNullOrWhiteSpace(PA.NumeroDocumento))
                        Resultado = null;
                    else
                        Resultado = string.Format("{0}: {1}", TipoDoc, PA.NumeroDocumento);
                    break;
                case "primerapellido":
                    Resultado = PA.PrimerApellido;
                    break;
                case "segundoapellido":
                    Resultado = PA.SegundoApellido;
                    break;
                case "primernombre":
                    Resultado = PA.PrimerNombre;
                    break;
                case "segundonombre":
                    Resultado = PA.SegundoNombre;
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
