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
  public partial class DatoSiNoColumna : UserControl
  {
    #region CONSTRUCTOR

    public DatoSiNoColumna()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(DatoSiNoColumna_Loaded);
    }

    void DatoSiNoColumna_Loaded(object sender, RoutedEventArgs e)
    {
        if (this.IncluirNsNr && !Valor.HasValue)
            this.txtTexto.Text = "Ns/Nr";
    }
    #endregion

    #region SELECCION DEL VALOR POR RATÓN

    /// <summary>
    /// Al hacer click sobre el control se alterna el valor.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Seleccion_Click(object sender, MouseButtonEventArgs e)
    {
      if(Valor < 0)
        Valor = null;
      if (IncluirNsNr && !Valor.HasValue)
        Valor = 1;
      else if (Valor.HasValue && Valor.Value == 1)
        Valor = 0;
      else if (!Valor.HasValue || Valor.Value == 0)
      {
        if (IncluirNsNr)
          Valor = null;
        else
          Valor = 1;
      }

      // Tomar el foco.

      //var Anterior = this.PredictFocus(FocusNavigationDirection.Next);
      //(Anterior as UIElement).Focus();
      //(Anterior as UIElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
    }

    #endregion

    #region PROPIEDADES

    public int? Valor
    {
      get { return (int?)GetValue(ValorProperty); }
      set { SetValue(ValorProperty, value); }
    }

    public static readonly DependencyProperty ValorProperty =
        DependencyProperty.Register("Valor", typeof(int?),
        typeof(DatoSiNoColumna), new UIPropertyMetadata(null, ValorChanged));

    static void ValorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {

      int? Valor = (int?)e.NewValue;
      DatoSiNoColumna DMC = d as DatoSiNoColumna;

      if (DMC.IncluirNsNr && !Valor.HasValue)
        DMC.txtTexto.Text = "Ns/Nr";
      else
      {
        if (!DMC.IncluirNsNr && !Valor.HasValue)
        {
          DMC.txtTexto.Text = "    ";
        }
        else if (Valor.Value == 0)
        {
          DMC.txtTexto.Text = "NO";
        }
        else if (Valor.Value > 0)
        {
          DMC.txtTexto.Text = "SI";
        }
      }
    }


    /// <summary>
    /// La dirección de funcionamiento del zoom.
    /// </summary>
    public StretchDirection DireccionTamaño
    {
      get { return vbMain.StretchDirection; }
      set { vbMain.StretchDirection = value; }
    }

    private bool _IncluirNsNr = false;
    /// <summary>
    /// True: Se incluye el valor Ns/Nr=null.
    /// </summary>
    public bool IncluirNsNr
    {
      get { return _IncluirNsNr; }
      set { _IncluirNsNr = value; }
    }

    #endregion

    #region CONTROL DEL FOCO

    /// <summary>
    /// Dado que este control no puede recibir el foco de manera natural,
    /// aqui se implementa dicha funcionalidad.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void grdMain_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      bool ConFoco = Convert.ToBoolean(e.NewValue);
      recFoco.Visibility = ConFoco ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion

    #region SELECCIÓN DEL VALOR POR TECLADO

    /// <summary>
    /// Permite la selección del valor por medio del teclado:
    /// SI = Teclas S o 1.
    /// NO = Teclas N o 0.
    /// Ns/Nr = Tecla Espacio.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void grdMain_PreviewKeyDown(object sender, KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.S:
        case Key.NumPad1:
        case Key.D1:
          Valor = 1;
          break;

        case Key.N:
        case Key.NumPad0:
        case Key.D0:
          Valor = 0;
          break;

        case Key.Space:
          if (IncluirNsNr)
            Valor = null;
          break;
      }
    }

    #endregion

  }
}
