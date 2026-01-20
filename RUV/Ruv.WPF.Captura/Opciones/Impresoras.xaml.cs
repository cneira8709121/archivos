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
using System.Printing;
using Ruv.WPF.Captura.Infrastructure.Impresion;

namespace Ruv.WPF.Captura.Opciones
{
  /// <summary>
  /// Interaction logic for Impresoras.xaml
  /// </summary>
  public partial class Impresoras : Page
  {
    public Impresoras()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(Impresoras_Loaded);
    }

    void Impresoras_Loaded(object sender, RoutedEventArgs e)
    {
      //  cbxImpresoras.ItemsSource = RUV.I.Configuraciones.Impresion.ListaImpresoras();
      //cbxImpresoras.SelectedItem = RUV.I.Configuraciones.Impresion.Configuracion.ImpresoraPreferida;

      //cbxTipoPapel.ItemsSource = Enum.GetNames(typeof(eTipoPapel));
      //cbxTipoPapel.SelectedItem = RUV.I.Impresion.Configuracion.TipoPapel.ToString();

      //int[] ListaCopias = new int[] { 1, 2, 3, 4 };
      //cbxNumeroCopias.ItemsSource = ListaCopias;
      //cbxNumeroCopias.SelectedItem = RUV.I.Impresion.Configuracion.NumeroCopias;
    }

    private void Aceptar_Click(object sender, RoutedEventArgs e)
    {
      //var Config = RUV.I.Impresion.Configuracion;
      //Config.ImpresoraPreferida = cbxImpresoras.SelectedItem.ToString();
      //Config.TipoPapel = (eTipoPapel)Enum.Parse(typeof(eTipoPapel),
      //  cbxTipoPapel.SelectedItem.ToString());
      //Config.NumeroCopias = Convert.ToInt32(cbxNumeroCopias.SelectedItem);

      //Config.Grabar();

      // Regresar al pantallazo en blanco.
      RUV.I.UIPrincipal.NavegarA();
    }

  }
}
