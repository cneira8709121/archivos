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
  /// Lógica de interacción para H01_Encabezado00.xaml
  /// </summary>
  public partial class H01_Pregunta01_08 : UserControl, IEncabezadoImpresion
  {
    public H01_Pregunta01_08()
    {
      InitializeComponent();
    }

    public bool RepiteEnCadaPagina
    {
      get { return false; }
    }

    public int Orden
    {
      get { return 1; }
    }
  }
}
