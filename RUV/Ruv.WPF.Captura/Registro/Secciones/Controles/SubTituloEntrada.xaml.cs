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
  /// Lógica de interacción para SubTituloEntrada.xaml
  /// </summary>
  public partial class SubTituloEntrada : UserControl
  {
    public SubTituloEntrada()
    {
      InitializeComponent();
    }

    public string Numero
    {
      get { return Convert.ToString(lbl01.Content); }
      set
      {
        lbl01.Content = value;
        if (string.IsNullOrWhiteSpace(value))
        {
          lbl01.Visibility = System.Windows.Visibility.Collapsed;
          bor01.Margin = new Thickness(0d);
          //bor01.SetValue(Border.MarginProperty, DependencyProperty.UnsetValue);
        }
        else
        {
          lbl01.Visibility = System.Windows.Visibility.Visible;
          bor01.Margin = new Thickness(0d, 0d, 0d, 0d);
        }
      }
    }



    public string Texto
    {
      get { return (string)GetValue(TextoProperty); }
      set { SetValue(TextoProperty, value); }
    }

    public static readonly DependencyProperty TextoProperty =
        DependencyProperty.Register("Texto", typeof(string),
        typeof(SubTituloEntrada), new UIPropertyMetadata(null, TextoChanged));

    static void TextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var STE = d as SubTituloEntrada;
      string value = Convert.ToString(e.NewValue);

      STE.txtTexto.Text = value;
      if (string.IsNullOrWhiteSpace(value) && value != ".")
        STE.bor01.Visibility = System.Windows.Visibility.Collapsed;
      else
        STE.bor01.Visibility = System.Windows.Visibility.Visible;

      if (value == ".") STE.txtTexto.Text = " ";
    }

    //public string Texto
    //{
    //  get { return Convert.ToString(txtTexto.Text); }
    //  set
    //  {
    //    txtTexto.Text = value;
    //    if (string.IsNullOrWhiteSpace(value) && value != ".")
    //      bor01.Visibility = System.Windows.Visibility.Collapsed;
    //    else
    //      bor01.Visibility = System.Windows.Visibility.Visible;
    //    if (value == ".") txtTexto.Text = " ";
    //  }
    //}

    private eFondoSubTitulos _ColorTexto = eFondoSubTitulos.Normal;
    /// <summary>
    /// Verdadero: Se utiliza un fondo gris oscuro y texto en letras blancas.
    /// </summary>
    public eFondoSubTitulos ColorTexto
    {
      get { return _ColorTexto; }
      set
      {
        _ColorTexto = value;
        switch (value)
        {
          case eFondoSubTitulos.Inverso:
            txtTexto.Background = new SolidColorBrush(Colors.Gray);
            txtTexto.Foreground = new SolidColorBrush(Colors.White);
            txtTexto.SetValue(TextBlock.FontWeightProperty, DependencyProperty.UnsetValue);
            bor01.SetValue(Border.BorderThicknessProperty, DependencyProperty.UnsetValue);
            txtTexto.SetValue(TextBlock.TextAlignmentProperty, DependencyProperty.UnsetValue);

            txtTextoAtras.Background = new SolidColorBrush(Colors.Gray);
            txtTextoAtras.Foreground = new SolidColorBrush(Colors.White);
            txtTextoAtras.SetValue(TextBlock.FontWeightProperty, DependencyProperty.UnsetValue);
            txtTextoAtras.SetValue(TextBlock.TextAlignmentProperty, DependencyProperty.UnsetValue);

            break;
          case eFondoSubTitulos.FondoBlanco:
            txtTexto.Background = new SolidColorBrush(Colors.White);
            txtTexto.Foreground = new SolidColorBrush(Colors.Black);
            txtTexto.FontWeight = FontWeights.Normal;
            bor01.BorderThickness = new Thickness(0d);
            txtTexto.TextAlignment = TextAlignment.Left;

            txtTextoAtras.Background = new SolidColorBrush(Colors.White);
            txtTextoAtras.Foreground = new SolidColorBrush(Colors.Black);
            txtTextoAtras.FontWeight = FontWeights.Normal;
            txtTextoAtras.TextAlignment = TextAlignment.Left;

            break;
          default:
            txtTexto.SetValue(TextBlock.BackgroundProperty, DependencyProperty.UnsetValue);
            txtTexto.SetValue(TextBlock.ForegroundProperty, DependencyProperty.UnsetValue);
            txtTexto.SetValue(TextBlock.FontWeightProperty, DependencyProperty.UnsetValue);
            txtTexto.SetValue(TextBlock.TextAlignmentProperty, DependencyProperty.UnsetValue);

            txtTextoAtras.SetValue(TextBlock.BackgroundProperty, DependencyProperty.UnsetValue);
            txtTextoAtras.SetValue(TextBlock.ForegroundProperty, DependencyProperty.UnsetValue);
            txtTextoAtras.SetValue(TextBlock.FontWeightProperty, DependencyProperty.UnsetValue);
            txtTextoAtras.SetValue(TextBlock.TextAlignmentProperty, DependencyProperty.UnsetValue);
            bor01.SetValue(Border.BorderThicknessProperty, DependencyProperty.UnsetValue);
            break;
        }
      }
    }

    public TextAlignment AlineacionTexto
    {
      get { return txtTexto.TextAlignment; }
      set { txtTexto.TextAlignment = value; }
    }

    public VerticalAlignment AlineacionVerticalTexto
    {
      get { return txtTexto.VerticalAlignment; }
      set { txtTexto.VerticalAlignment = value; }
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
        typeof(SubTituloEntrada), new UIPropertyMetadata(null, TamañoTextoChanged));

    static void TamañoTextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var Valor = (double?)e.NewValue;

      if (!Valor.HasValue) return;

      var STE = d as SubTituloEntrada;

      STE.txtTexto.FontSize = Valor.Value;
      STE.lbl01.FontSize = Valor.Value;
    }


    #endregion



  }
}
