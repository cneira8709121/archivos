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
  /// Interaction logic for A08_Encabezado00.xaml
  /// </summary>
  public partial class A08_Encabezado00 : UserControl, IEncabezadoImpresion
  {
    public A08_Encabezado00()
    {
      InitializeComponent();
    }

    public bool RepiteEnCadaPagina
    {
      get { return true; }
    }

    public int Orden
    {
      get { return 0; }
    }
  }
}
