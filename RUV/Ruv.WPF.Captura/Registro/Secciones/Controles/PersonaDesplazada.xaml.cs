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
using Ruv.WPF.Captura.Impresion;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Lógica de interacción para PersonaDesplazada.xaml
  /// </summary>
  public partial class PersonaDesplazada : UserControl
  {
    public PersonaDesplazada()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(PersonaDesplazada_Loaded);
    }

    void PersonaDesplazada_Loaded(object sender, RoutedEventArgs e)
    { }

    /// <summary>
    /// Verdadero: Este elemento está seleccionado.
    /// </summary>
    public bool Seleccionado
    {
      get { return chkSeleccionado.IsChecked.Value; }
      set { chkSeleccionado.IsChecked = value; }
    }

  }
}
