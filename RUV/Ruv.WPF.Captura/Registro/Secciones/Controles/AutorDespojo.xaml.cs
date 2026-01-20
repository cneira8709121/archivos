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

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Lógica de interacción para AutorDespojo.xaml
  /// </summary>
  public partial class AutorDespojo : UserControl
  {
    public AutorDespojo()
    {
      InitializeComponent();
      CajasTexto = new TextBox[] { tbx01, tbx02, tbx03, tbx04 };
      this.Loaded += new RoutedEventHandler(AutorDespojo_Loaded);
    }

    bool CambioManual = false;
    TextBox CajaTexto = null;
    TextBox[] CajasTexto = null;

    void AutorDespojo_Loaded(object sender, RoutedEventArgs e)
    {

      // Crear los bindings a mano.
      //foreach (var item in CajasTexto)
      //{
      //  Extensiones.BindingEstablecer(Texto,
      //    "Text",
      //    item,
      //    TextBox.TextProperty,
      //     BindingMode.TwoWay);
      //}
      CajaTexto = null;
    }

    /// <summary>
    /// Sucede cuando se chequean las opciones.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SeleccionCambia(object sender, RoutedEventArgs e)
    {
      if (CambioManual) return;

      CambioManual = true;

      var Seleccionado = grdMain.Children.OfType<RadioButton>()
        .Where(x => x.IsChecked.Value).FirstOrDefault();

      if (Seleccionado == null)
      {
        ValorSeleccionado = null;
        Texto = null;
        CajaTexto = null;
      }
      else
      {
        ValorSeleccionado = Convert.ToInt32(Seleccionado.Tag);
        CajaTexto = CajasTexto[
          Convert.ToInt32(Seleccionado.Name.Substring(3, 2)) - 1];
        Texto = CajaTexto.Text;
      }

      CambioManual = false;
    }

    /// <summary>
    /// El valor de la opción seleccionada.
    /// </summary>
    public int? ValorSeleccionado
    {
      get { return (int?)GetValue(ValorSeleccionadoProperty); }
      set { SetValue(ValorSeleccionadoProperty, value); }
    }

    public static readonly DependencyProperty ValorSeleccionadoProperty =
        DependencyProperty.Register("ValorSeleccionado", typeof(int?),
        typeof(AutorDespojo), new UIPropertyMetadata(null, ValorSeleccionadoChanged));

    static void ValorSeleccionadoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var AD = d as AutorDespojo;

      if (AD.CambioManual) return;

      AD.CambioManual = true;

      int? Valor = (int?)e.NewValue;

      AD.grdMain.Children.OfType<RadioButton>().ToList()
        .ForEach(x => x.IsChecked = false);

      if (Valor.HasValue)
      {
        var Seleccionado = AD.grdMain.Children.OfType<RadioButton>()
          .Where(x => Convert.ToInt32(x.Tag) == Valor.Value).FirstOrDefault();
        if (Seleccionado != null)
          Seleccionado.IsChecked = true;
      }

      AD.CambioManual = false;
    }

    /// <summary>
    /// El texto escrito.
    /// </summary>
    public string Texto
    {
      get { return (string)GetValue(TextoProperty); }
      set { SetValue(TextoProperty, value); }
    }

    public static readonly DependencyProperty TextoProperty =
        DependencyProperty.Register("Texto", typeof(string),
        typeof(AutorDespojo), new UIPropertyMetadata(null, TextoChanged));

    static void TextoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var AD = d as AutorDespojo;
      if (AD.CambioManual) return;
      AD.CambioManual = true;

      if (AD.CajaTexto == null)
      {
        // establecerlo en todas las cajas
        AD.CajasTexto.ToList().ForEach(x => x.Text = Convert.ToString(e.NewValue));
      }
      else
      {
        AD.CajaTexto.Text = Convert.ToString(e.NewValue);
      }


      AD.CambioManual = false;
    }

    /// <summary>
    /// Cambio del texto a través del teclado.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CambioTexto(object sender, TextChangedEventArgs e)
    {
      if (CambioManual) return;
      CambioManual = true;

      if (CajaTexto != null)
        Texto = CajaTexto.Text;

      CambioManual = false;
    }
  }
}
