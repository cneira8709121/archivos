using System.Windows.Controls;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
  /// <summary>
  /// Interaction logic for DenunciaPrevia_Edicion.xaml
  /// </summary>
  public partial class DenunciaPrevia_Edicion : UserControl
  {
    public DenunciaPrevia_Edicion()
    {
      InitializeComponent();
    }

    /// <summary>
    /// El sub-conjunto de los parámetros a utilizar.
    /// </summary>
    public eGruposParametros Conjunto
    {
      get { return loEntidades.Conjunto; }
      set { loEntidades.Conjunto = value; }
    }
  }
}
