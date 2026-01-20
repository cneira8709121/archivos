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
  /// Interaction logic for DatoColumna.xaml
  /// </summary>
  public partial class DatoColumna : UserControl
  {
    public DatoColumna()
    {
      InitializeComponent();
    }

    public string Texto
    {
      get { return (string)GetValue(TextoProperty); }
      set { SetValue(TextoProperty, value); }
    }

    public static readonly DependencyProperty TextoProperty =
        DependencyProperty.Register("Texto", typeof(string),
        typeof(DatoColumna), new UIPropertyMetadata(null, TextoChanged));

    static void TextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      (d as DatoColumna).txtTexto.Text = Convert.ToString(e.NewValue);
    }

    public TextAlignment AlineacionTexto
    {
      get { return txtTexto.TextAlignment; }
      set { txtTexto.TextAlignment = value; }
    }

    public TextWrapping WrappingTexto
    {
      get { return txtTexto.TextWrapping; }
      set { txtTexto.TextWrapping = value; }
    }

    public Visibility VisibilidadDato
    {
      get { return (Visibility)GetValue(VisibilidadDatoProperty); }
      set { SetValue(VisibilidadDatoProperty, value); }
    }

    public static readonly DependencyProperty VisibilidadDatoProperty =
        DependencyProperty.Register("VisibilidadDato", typeof(Visibility),
        typeof(DatoColumna), new UIPropertyMetadata(Visibility.Visible, VisibilidadDatoChanged));

    static void VisibilidadDatoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      (d as DatoColumna).txtTexto.Visibility = (Visibility)e.NewValue;
    }

    static Thickness SinBorde = new Thickness(0d);
    static Thickness ConBorde = new Thickness(1d);

    /// <summary>
    /// Mostrar/Ocultar el borde.
    /// </summary>
    public bool BordeVisible
    {
      set
      {
        borBorde.BorderThickness = value ? ConBorde : SinBorde;
      }
    }

  }
}
