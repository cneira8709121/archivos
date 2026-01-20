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
  public partial class A03_Encabezado02 : UserControl, IEncabezadoImpresion
  {
    public A03_Encabezado02()
    {
      InitializeComponent();
    }

    public bool RepiteEnCadaPagina
    {
      get { return false; }
    }

    public int Orden
    {
      get { return 2; }
    }
  }
}
