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
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Interaction logic for Afectacion_Edicion.xaml
  /// </summary>
  public partial class Afectacion_Edicion : UserControl
  {
    public Afectacion_Edicion()
    {
      InitializeComponent();
    }

    /// <summary>
    /// El sub-conjunto de los parámetros a utilizar.
    /// </summary>
    public eGruposParametros Conjunto
    {
      get { return loTiposAfectaciones.Conjunto; }
      set { 
        loTiposAfectaciones.Conjunto = value; }
    }


  }
}
