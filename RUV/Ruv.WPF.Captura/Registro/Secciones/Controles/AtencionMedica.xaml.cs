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

namespace Ruv.WPF.Captura.Registro.Secciones
{
  /// <summary>
  /// Lógica de interacción para AtencionMedica.xaml
  /// </summary>
  public partial class AtencionMedica : UserControl
  {
    public AtencionMedica()
    {
      InitializeComponent();
    }

    public string Numero
    {
      get { return te01.Numero; }
      set { te01.Numero = value; }
    }
  }
}
