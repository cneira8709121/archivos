using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ruv.WPF.Captura.Impresion
{
  /// <summary>
  /// Interaction logic for DatoMarcaColumna.xaml
  /// </summary>
  public partial class DatoMarcaColumna : UserControl
  {
    public DatoMarcaColumna()
    {
      InitializeComponent();
    }

    public int? Valor
    {
      get { return (int?)GetValue(ValorProperty); }
      set { SetValue(ValorProperty, value); }
    }

    public static readonly DependencyProperty ValorProperty =
        DependencyProperty.Register("Valor", typeof(int?),
        typeof(DatoMarcaColumna), new UIPropertyMetadata(null, ValorChanged));

    static void ValorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      int? Valor = (int?)e.NewValue;
      DatoMarcaColumna DMC = d as DatoMarcaColumna;
      if (!Valor.HasValue || Valor.Value == 0)
        DMC.vbEquis.Visibility = Visibility.Hidden;
      else
        DMC.vbEquis.Visibility = Visibility.Visible;
    }

    private void Seleccion_Click(object sender, MouseButtonEventArgs e)
    {
      if (Valor.HasValue && Valor.Value == 1)
        Valor = 0;
      else
        Valor = 1;
    }

  }
}
