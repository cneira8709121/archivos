using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Ruv.WPF.Captura.Controles
{
  /// <summary>
  /// Interaction logic for FocusAdorner.xaml
  /// </summary>
  public partial class FocusAdorner : UserControl
  {
    public FocusAdorner()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(FocusAdorner_Loaded);
    }

    void FocusAdorner_Loaded(object sender, RoutedEventArgs e)
    {
      SB = Resources["sbFocusAdorner"] as Storyboard;
      dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
      dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
    }

    DispatcherTimer dispatcherTimer;
    Storyboard SB;
    FrameworkElement Elemento;

    /// <summary>
    /// Resaltar y pasar el foco a un control.
    /// </summary>
    /// <param name="elemento"></param>
    public void MostrarFoco(FrameworkElement elemento)
    {
      Elemento = elemento;
      Elemento.BringIntoView();
      dispatcherTimer.Start();
    }

    void dispatcherTimer_Tick(object sender, EventArgs e)
    {
      (sender as DispatcherTimer).Stop();

      Width = Elemento.ActualWidth;
      Height = Elemento.ActualHeight;
      Point locationFromWindow = Elemento.TranslatePoint(new Point(0, 0),
             MainWindow);
      Point locationFromScreen;
      try
      {
        locationFromScreen = Elemento.PointToScreen(locationFromWindow);
      }
      catch 
      {
        // El objeto que generó el error de validación
        // ya no está en la interfase.
        return;
      }


      //System.Windows.Media.GeneralTransform GT = Elemento.TransformToVisual(MainWindow);
      //Point Punto = GT.Transform(new Point(0, 0));

      ttPosicion.X = locationFromWindow.X;
      ttPosicion.Y = locationFromWindow.Y;
      stTamaño.CenterX = Elemento.ActualWidth / 2;
      stTamaño.CenterY = Elemento.ActualHeight / 2;

      this.Visibility = System.Windows.Visibility.Visible;
      Elemento.Focus();
      if (Elemento is TextBox)
        (Elemento as TextBox).SelectAll();

      SB.Stop();
      SB.Begin();
    }

    /// <summary>
    /// La ventana principal de la aplicación.
    /// </summary>
    public Window MainWindow
    {
      get { return (Window)GetValue(MainWindowProperty); }
      set { SetValue(MainWindowProperty, value); }
    }

    public static readonly DependencyProperty MainWindowProperty =
        DependencyProperty.Register("MainWindow", typeof(Window),
        typeof(FocusAdorner), new UIPropertyMetadata(null));



  }
}
