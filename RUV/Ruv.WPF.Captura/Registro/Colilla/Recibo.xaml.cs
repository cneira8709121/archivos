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
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Registro.Colilla
{
    /// <summary>
    /// Interaction logic for Recibo.xaml
    /// </summary>
    public partial class Recibo : Window
    {
        public Recibo()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            RUV.I.Configuraciones.Impresion.ImprimirColilla((clsDeclaracion)blkColilla.DataContext);
        }
    }
}
