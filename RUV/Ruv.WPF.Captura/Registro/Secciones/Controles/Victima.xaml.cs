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
  public partial class Victima : UserControl
  {
    public Victima()
    {
      InitializeComponent();
    }

    public string Numero
    {
      get { return seTitulo1.Numero; }
      set
      {
        seTitulo1.Numero = value;
        seTitulo2.Numero = (Convert.ToInt32(value) + 1).ToString();
      }
    }


  }
}
