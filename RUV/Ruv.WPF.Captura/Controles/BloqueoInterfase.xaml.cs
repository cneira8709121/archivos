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
using System.Windows.Media.Animation;

namespace Ruv.WPF.Captura.Controles
{ 
  /// <summary>
  /// Lógica de interacción para BloqueoInterfase.xaml
  /// </summary>
  public partial class BloqueoInterfase : UserControl
  {
    public BloqueoInterfase()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Texto a mostrar durante el bloqueo de la interfase.
    /// </summary>
    public string TextoBloqueo
    {
      get { return txtMensajeBloqueo.Text; }
      set
      {
        txtMensajeBloqueo.Text = value;
        Storyboard SB = borBloqueoInterfase.Resources["sbBloqueoInterfase"] as Storyboard;
        if (string.IsNullOrWhiteSpace(value))
        {
          SB.Stop();
          this.Visibility = System.Windows.Visibility.Collapsed;
        }
        else
        {
          this.Visibility = System.Windows.Visibility.Visible;
          SB.Begin();
        }
      }
    }

  }
}
