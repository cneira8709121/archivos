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

namespace Ruv.WPF.Captura.Controles
{
    /// <summary>
    /// Lógica de interacción para MensajesUsuario.xaml
    /// </summary>
    public partial class MensajesUsuario : Window
    {
        public MensajesUsuario()
        {
            InitializeComponent();
        }
        public string Mensaje { get; set; }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
