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
using System.IO;
using System.Windows.Forms;
using Ruv.WPF.Captura.Infrastructure.Configuracion;

namespace Ruv.WPF.Captura.Opciones
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Window
    {
        public Configuracion()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Configuracion_Loaded);
        }

        void Configuracion_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = RUV.I.Configuraciones;
        }
        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            
            RUV.I.Configuraciones.Grabar();
            //Config.Impresion.ImpresoraPreferida = cbxImpresoras.SelectedItem.ToString();
            //Config.TipoPapel = (eTipoPapel)Enum.Parse(typeof(eTipoPapel),
            //  cbxTipoPapel.SelectedItem.ToString());
            //Config.NumeroCopias = Convert.ToInt32(cbxNumeroCopias.SelectedItem);

            
        }
    }
}
