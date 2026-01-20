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
using Ruv.WPF.Captura.Infrastructure;
using System.Collections.ObjectModel;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Registro.Secciones
{
  /// <summary>
  /// Lógica de interacción para SePresento.xaml
  /// </summary>
  public partial class Anexo06_DatoHomicidio : UserControl
  {
    public Anexo06_DatoHomicidio()
    {
      InitializeComponent();
    }

    public string Numero
    {
      get { return seTitulo.Numero; }
      set { seTitulo.Numero = value; }
    }


  }
}
