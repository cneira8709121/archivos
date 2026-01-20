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

namespace Ruv.WPF.Captura.Registro.Secciones
{
  /// <summary>
  /// Lógica de interacción para TituloConBorde.xaml
  /// </summary>
  public partial class TituloConBorde : UserControl
  {
    public TituloConBorde()
    {
      InitializeComponent();
    }

    public Thickness Borde
    {
      set { bor01.BorderThickness = value; }
      get { return bor01.BorderThickness; }
    }

    public TextAlignment AlineacionHorizontal
    {
      set { txt01.TextAlignment = value; }
      get { return txt01.TextAlignment; }
    }

    public VerticalAlignment AlineacionVertical
    {
      set { txt01.VerticalAlignment = value; }
      get { return txt01.VerticalAlignment; }
    }

    public string Texto
    {
      set { txt01.Text = value; }
      get { return txt01.Text; }
    }

    public string Celda
    {
      set
      {
        string[] Valores = value.Split(',');
        Grid.SetColumn(this, Convert.ToInt32(Valores[0]));
        Grid.SetRow(this, Convert.ToInt32(Valores[1]));
      }
    }

    /// <summary>
    /// El estailo de la caja de texto.
    /// </summary>
    public Style EstiloTexto
    {
      get { return txt01.Style; }
      set { txt01.Style = value; }
    }

    #region TAMAÑO DEL TEXTO

    /// <summary>
    /// El tamaño del tipo de letra.
    /// </summary>
    public double TamañoTexto
    {
      get { return (double)GetValue(TamañoTextoProperty); }
      set { SetValue(TamañoTextoProperty, value); }
    }

    public static readonly DependencyProperty TamañoTextoProperty =
        DependencyProperty.Register("TamañoTexto", typeof(double),
        typeof(TituloConBorde), new UIPropertyMetadata(10d, TamañoTextoChanged));

    static void TamañoTextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var TCB = d as TituloConBorde;

      if (e.NewValue == null) return;
      double? Valor = (double?)e.NewValue;

      if (Valor.HasValue)
        TCB.txt01.FontSize = Valor.Value;
    }

    #endregion


  }
}
