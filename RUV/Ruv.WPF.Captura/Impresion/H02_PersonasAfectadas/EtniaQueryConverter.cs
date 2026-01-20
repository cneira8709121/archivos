using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Impresion.H02_PersonasAfectadas
{
  /// <summary>
  /// Si la PersonaAfectada perteneca a la Etnia indicada en el parámetro retorna 1 de lo contrario 0.
  /// </summary>
  public class EtniaQueryConverter:IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      clsPersonaAfectada PA = value as clsPersonaAfectada;
      if (PA == null || !PA.PertenenciaEtnica.HasValue) return 0;

      int Codigo = System.Convert.ToInt32(parameter);
      if (Codigo == PA.PertenenciaEtnica.Value)
        return 1;
      else
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return null;
    }
  }
}
