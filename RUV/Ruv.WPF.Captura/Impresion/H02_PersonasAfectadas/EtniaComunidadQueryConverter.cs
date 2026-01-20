using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Windows;

namespace Ruv.WPF.Captura.Impresion.H02_PersonasAfectadas
{
  /// <summary>
  /// Si la PersonaAfectada perteneca a la Etnia indicada en el parámetro 
  /// retorna 'Visible' de lo contrario 'Collapsed'.
  /// </summary>
  public class EtniaComunidadQueryConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      clsPersonaAfectada PA = value as clsPersonaAfectada;
      if (PA == null || !PA.PertenenciaEtnica.HasValue) return 0;

      int Codigo = System.Convert.ToInt32(parameter);
      if (Codigo == PA.PertenenciaEtnica.Value)
        return Visibility.Visible;
      else
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return null;
    }
  }
}
