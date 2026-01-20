using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Ruv.WPF.Captura.Controles
{

  /// <summary>
  /// Control que se encarga de mostrar una marca de error.
  /// </summary>
  public class DummyControl : Border
  {

    public object FuenteDeDatos
    {
      get { return (object)GetValue(FuenteDeDatosProperty); }
      set { SetValue(FuenteDeDatosProperty, value); }
    }

    public static readonly DependencyProperty FuenteDeDatosProperty =
        DependencyProperty.Register("FuenteDeDatos", typeof(object), 
        typeof(DummyControl), new UIPropertyMetadata(null));

  }
}
