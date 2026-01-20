using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Ruv.WPF.Captura.Controles
{
  public class EntradaDouble : TextBox
  {
    public EntradaDouble()
      : base()
    {
      TextAlignment = System.Windows.TextAlignment.Right;
      SeparadorDecimales = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
      ExpresionValida += SeparadorDecimales;
      CambioPorTeclado = false;
    }

    string ExpresionValida = "0123456789";
    string SeparadorDecimales = null;

    protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
    {
      base.OnPreviewTextInput(e);

      // Sólo se permiten los caracteres numéricos.
      if (!ExpresionValida.Contains(e.Text))
        e.Handled = true;

      // Permitir sólo un separador de decimales.
      else if (e.Text == SeparadorDecimales
        && Text.Contains(SeparadorDecimales))
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
      {
        string TextoArreglado = Text;

        if (TextoArreglado.StartsWith(SeparadorDecimales))
          TextoArreglado = "0" + TextoArreglado;

        if (TextoArreglado.EndsWith(SeparadorDecimales))
          TextoArreglado = TextoArreglado + "0";

        //// No permitir más de 4 enteros, dos decimales.
        //if (!TextoArreglado.Contains(SeparadorDecimales))
        //{
        //  if (TextoArreglado.Length > 4)
        //  {
        //    // Sólo 4 enteros.
        //    TextoArreglado = TextoArreglado.Substring(TextoArreglado.Length - 4);
        //  }
        //}
        //else
        //{
        //  // Sólo dos dígitos.
        //  if (
        //}


        Valor = Convert.ToDouble(TextoArreglado);
      }
    }

    /// <summary>
    /// El valor numérico de la caja de texto.
    /// </summary>
    public double? Valor
    {
      get { return (double?)GetValue(ValorProperty); }
      set { SetValue(ValorProperty, value); }
    }

    public static readonly DependencyProperty ValorProperty =
        DependencyProperty.Register("Valor", typeof(double?),
        typeof(EntradaDouble), new UIPropertyMetadata(null, ValorChanged));

    static void ValorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var EN = d as EntradaDouble;
      EN.CambioPorTeclado = true;
      if (e.NewValue != null) EN.Text = Convert.ToString(e.NewValue);
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
