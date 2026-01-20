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
using System.Windows.Threading;

namespace Ruv.WPF.Captura.Controles
{
  /// <summary>
  /// Lógica de interacción para BotonMenu.xaml
  /// </summary>
  public partial class BotonMenu : UserControl
  {
    public BotonMenu()
    {
      InitializeComponent();
      this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(BotonMenu_IsEnabledChanged);
    }


    #region ESTADO: FOCO

    protected override void OnMouseEnter(MouseEventArgs e)
    {
      base.OnMouseEnter(e);
      if (IsEnabled)
        VisualStateManager.GoToState(this, "ConFoco", false);
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
      base.OnMouseLeave(e);
      if (IsEnabled)
        VisualStateManager.GoToState(this, "SinFoco", false);
    }

    #endregion

    #region ESTADO: HABILITADO

    void BotonMenu_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      if ((bool)e.NewValue)
        VisualStateManager.GoToState(this, "Habilitado", false);
      else
        VisualStateManager.GoToState(this, "Deshabilitado", false);
    }

    #endregion

    #region ESTADO: PENDIENTE DIGITACIÓN

    /// <summary>
    /// Estado que marca si el botón señala la falta digitar información.
    /// </summary>
    public eEstadoIngreso EstadoIngreso
    {
      get { return (eEstadoIngreso)GetValue(EstadoIngresoProperty); }
      set { SetValue(EstadoIngresoProperty, value); }
    }

    public static readonly DependencyProperty EstadoIngresoProperty =
        DependencyProperty.Register("EstadoIngreso", typeof(eEstadoIngreso),
        typeof(BotonMenu),
        new UIPropertyMetadata(eEstadoIngreso.NoRequiereIngreso, EstadoIngresoChangedCallback));

    static void EstadoIngresoChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BotonMenu MB = d as BotonMenu;
      if (MB.IsEnabled)
        VisualStateManager.GoToState(
          MB, ((eEstadoIngreso)e.NewValue).ToString(), false);
    }

    //public enum eEstadoIngreso
    //{
    //  NoRequiereIngreso,
    //  IngresoIncompleto,
    //  IngresoCompleto
    //}

    #endregion

    #region PROPIEDADES
      
    /// <summary>
    /// El texto sobre el botón.
    /// </summary>
    public string Texto
    {
      get { return txtMensaje.Text; }
      set { txtMensaje.Text = value; }
    }

    #endregion

    #region Bloqueo de doble click

    private static bool bHandled = false;

    private static DispatcherTimer myClickWaitTimer =
                                          new DispatcherTimer(
                                              new TimeSpan(0, 0, 0, 1),
                                              DispatcherPriority.Background,
                                              mouseWaitTimer_Tick,
                                              Dispatcher.CurrentDispatcher);

    private static void mouseWaitTimer_Tick(object sender, EventArgs e)
    {
        myClickWaitTimer.Stop();

        // Handle Single Click Actions
        bHandled = false;
    }

    #endregion

    #region CLICK

    private void RatonClick(object sender, MouseButtonEventArgs e)
    {
        if (bHandled) return;

        if (e != null)
        {
            myClickWaitTimer.Start();
            bHandled = true;
            e.Handled = true;
        }

        if (!IsEnabled) return;
        RaiseEvent(new RoutedEventArgs(BotonMenu.SeleccionEvent, this));
    }

    /// <summary>
    /// Lanza programaticamente el click sobre el botón.
    /// </summary>
    public void LanzarEventoClick()
    {
      RatonClick(this, null);
    }

    // Register the routed event
    public static readonly RoutedEvent SeleccionEvent =
        EventManager.RegisterRoutedEvent(
        "Seleccion", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(BotonMenu));

    /// <summary>
    /// Se lanza este evento cuando se hace click sobre el botón.
    /// </summary>
    public event RoutedEventHandler Seleccion
    {
      add { AddHandler(SeleccionEvent, value); }
      remove { RemoveHandler(SeleccionEvent, value); }
    }

    #endregion


  }
}
