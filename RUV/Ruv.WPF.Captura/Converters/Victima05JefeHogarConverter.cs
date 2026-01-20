using System;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{

  /// <summary>
  /// Para el jefe de hogar del anexo 5, retorna alguna información.
  /// </summary>
    public class Victima05JefeHogarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null) return null;
            clsAnexo05 Anexo = value as clsAnexo05;
            if (Anexo == null) return null;

            int? Resultado = null;
            switch (System.Convert.ToString(parameter).ToUpper())
            {
                case "NUMEROCONSECUTIVO":
                    var PA = RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
                  .FirstOrDefault(x => x.ID == Anexo.JefeGrupoFamiliarId);
                    if (PA == null) return null;
                    return PA.NumeroConsecutivo;

                case "SEDESPLAZO":
                    var Victima = Anexo.Victimas.FirstOrDefault(x => x.PersonaAfectadaId == Anexo.JefeGrupoFamiliarId);
                    if (Victima == null) return null;
                    return Victima.SeDesplazo;
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
