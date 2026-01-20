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
using System.Windows.Markup;

namespace Ruv.WPF.Captura.Impresion
{
  /// <summary>
  /// Interaction logic for EncabezadoColumna.xaml
  /// </summary>
  public partial class EncabezadoColumna : UserControl
  {
    public EncabezadoColumna()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(EncabezadoColumna_Loaded);
    }

    void EncabezadoColumna_Loaded(object sender, RoutedEventArgs e)
    {
    }

    private Orientation _OrientacionTexto;
    public Orientation OrientacionTexto
    {
      get { return _OrientacionTexto; }
      set
      {
        _OrientacionTexto = value;
        if (value == Orientation.Horizontal)
          rtRotacion.Angle = 0d;
        else
          rtRotacion.Angle = 270d;
      }
    }

    private string _Texto;
    public string Texto
    {
      get { return _Texto; }
      set
      {
        _Texto = value;
        txtTexto.Inlines.Clear();
        if (value == null) return;
        StringBuilder SB = new
         StringBuilder("<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">");
        SB.Append(value.ToString().Replace("\\n", "<LineBreak/>"));
        SB.Append("</TextBlock>");

        var TB =
          XamlReader.Load(new System.Xml.XmlTextReader(
            new System.IO.StringReader(SB.ToString()))) as TextBlock;
        var Lista = TB.Inlines.ToArray();

        foreach (var item in Lista)
        {
          txtTexto.Inlines.Add(item);
        }
      }
    }


    #region TAMAÑO DEL TEXTO

    /// <summary>
    /// El tamaño del texto.
    /// </summary>
    public double? TamañoTexto
    {
      get { return (double?)GetValue(TamañoTextoProperty); }
      set { SetValue(TamañoTextoProperty, value); }
    }

    public static readonly DependencyProperty TamañoTextoProperty =
        DependencyProperty.Register("TamañoTexto", typeof(double?),
        typeof(EncabezadoColumna), new UIPropertyMetadata(null, TamañoTextoChanged));

    static void TamañoTextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var Valor = (double?)e.NewValue;

      if (!Valor.HasValue) return;

      var STE = d as EncabezadoColumna;

      STE.txtTexto.FontSize = Valor.Value;
    }


    #endregion

  }
}
