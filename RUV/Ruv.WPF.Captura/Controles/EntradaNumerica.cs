using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Ruv.WPF.Captura.Controles
{
  public class EntradaNumerica : TextBox
  {
    public EntradaNumerica()
      : base()
    { TextAlignment = System.Windows.TextAlignment.Center; }

    private string ExpresionValida = "0123456789";

    protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
    {
      base.OnPreviewTextInput(e);
      if (!ExpresionValida.Contains(e.Text))
        e.Handled = true;
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
      base.OnGotFocus(e);
      SelectAll();
    }

    private bool _CeroSiempreVisible = false;
    /// <summary>
    /// Verdadero: Si se borra el contenido, se reescribe un cero.
    /// </summary>
    public bool CeroSiempreVisible
    {
      get { return _CeroSiempreVisible; }
      set { _CeroSiempreVisible = value; }
    }

    bool CambioPorTeclado = false;

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
      base.OnTextChanged(e);

      if (Text.Any(x => !ExpresionValida.Contains(x)))
      {
        string Texto = "";
        Text.Where(x => ExpresionValida.Contains(x)).ToList()
          .ForEach(x => Texto += x);

        Text = Texto;
      }

      if (CeroSiempreVisible && string.IsNullOrEmpty(Text))
      {
        Text = "0";
        SelectAll();
      }

      if (string.IsNullOrEmpty(Text))
        Valor = null;
      else
        Valor = Convert.ToInt32(Text);
    }

    /// <summary>
    /// El valor numérico de la caja de texto.
    /// </summary>
    public int? Valor
    {
      get { return (int?)GetValue(ValorProperty); }
      set { SetValue(ValorProperty, value); }
    }

    public static readonly DependencyProperty ValorProperty =
        DependencyProperty.Register("Valor", typeof(int?),
        typeof(EntradaNumerica), new UIPropertyMetadata(null, ValorChanged));

    static void ValorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var EN = d as EntradaNumerica;
      EN.CambioPorTeclado = true;
      EN.Text = Convert.ToString(e.NewValue);
      EN.CambioPorTeclado = false;
    }

    /// <summary>
    /// Verdadero si hay un dato ingresado.
    /// </summary>
    public bool TieneValor
    {
      get { return !string.IsNullOrWhiteSpace(Text); }
    }

  }
}
