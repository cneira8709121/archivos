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
using System.Windows.Shapes;

namespace Ruv.WPF.Captura.Opciones
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionImpresora.xaml
    /// </summary>
    public partial class ConfiguracionImpresora : UserControl
    {
        public ConfiguracionImpresora()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Impresoras_Loaded);
        }

        public void Impresoras_Loaded(object sender, RoutedEventArgs e)
        {
            //cbxImpresoras.ItemsSource = Sipod.I.Impresion.ListaImpresoras();
            //cbxImpresoras.SelectedItem = Sipod.I.Impresion.Configuracion.ImpresoraPreferida;

            //cbxTipoPapel.ItemsSource = Enum.GetNames(typeof(eTipoPapel));
            //cbxTipoPapel.SelectedItem = Sipod.I.Impresion.Configuracion.TipoPapel.ToString();

            //int[] ListaCopias = new int[] { 1, 2, 3, 4 };
            //cbxNumeroCopias.ItemsSource = ListaCopias;
            //cbxNumeroCopias.SelectedItem = Sipod.I.Impresion.Configuracion.NumeroCopias;
        }


    }
}
