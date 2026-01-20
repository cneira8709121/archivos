using System.Windows;
using System.Windows.Controls;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Seguridad
{
  /// <summary>
  /// Lógica de interacción para FondoVacio.xaml
  /// </summary>
  public partial class FondoVacio : Page
  {
    public FondoVacio()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(FondoVacio_Loaded);
    }

    void FondoVacio_Loaded(object sender, RoutedEventArgs e)
    {
      //Sipod.I.InfoGeneral.ListaEntidades(eConjuntosEntidades.DesaparicionForzada)
      //  .ForEach(x =>
      //    System.Diagnostics.Debug.WriteLine(x.Nombre));
    }
  }
}
