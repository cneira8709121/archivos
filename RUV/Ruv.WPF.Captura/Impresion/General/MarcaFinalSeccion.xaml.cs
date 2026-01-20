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

namespace Ruv.WPF.Captura.Impresion.General
{
  /// <summary>
  /// Lógica de interacción para MarcaFinalSeccion.xaml
  /// </summary>
  public partial class MarcaFinalSeccion : UserControl
  {
    public MarcaFinalSeccion()
    {
      InitializeComponent();
    }

    /// <summary>
    /// El nombre de la sección para poner la marca.
    /// </summary>
    public string NombreSeccion
    {
      set { txtMarca.Text = string.Format("Final: {0} ", value); }
    }

  }
}
