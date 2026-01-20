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
  /// Interaction logic for DatoFechaColumna.xaml
  /// </summary>
  public partial class DatoFechaColumna : UserControl
  {
    public DatoFechaColumna()
    {
      InitializeComponent();
    }

    public DateTime? FechaTexto
    {
      get { return (DateTime?)GetValue(FechaTextoProperty); }
      set { SetValue(FechaTextoProperty, value); }
    }

    public static readonly DependencyProperty FechaTextoProperty =
        DependencyProperty.Register("FechaTexto", typeof(DateTime?),
        typeof(DatoFechaColumna), new UIPropertyMetadata(null, FechaTextoChanged));

    static void FechaTextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var DFC = (d as DatoFechaColumna);
      var grdMain = DFC.grdMain;

      if (e.NewValue == null)
        return;

      var Fecha = (DateTime?)e.NewValue;
      if (!Fecha.HasValue)
        return;

      int[] Numeros = new int[] { Fecha.Value.Day
        ,Fecha.Value.Month,
        Fecha.Value.Year};

      for (int i = 0; i < 3; i++)
      {
        TextBlock TB = new TextBlock()
        {
          Text = Numeros[i].ToString(),
          VerticalAlignment = System.Windows.VerticalAlignment.Center,
          TextAlignment = TextAlignment.Center
        };

        if (i == 1)
        {
          // Mes en letras.
          TB.Text = NombreMes(Numeros[i]);
        }

        Grid.SetColumn(TB, i);
        grdMain.Children.Insert(0, TB);
      }

    }

    /// <summary>
    /// Retorna las inciales del mes.
    /// </summary>
    /// <param name="numeroMes"></param>
    /// <returns></returns>
    static string NombreMes(int numeroMes)
    {
      var CulCo = new System.Globalization.CultureInfo("ES-CO");
      return CulCo.DateTimeFormat.AbbreviatedMonthNames[numeroMes - 1].ToLower();
    }

  }
}
