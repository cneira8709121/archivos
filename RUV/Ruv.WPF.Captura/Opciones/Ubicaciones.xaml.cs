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
using System.Windows.Forms;
using Ruv.WPF.Captura.Infrastructure.Configuracion;
using System.IO;

namespace Ruv.WPF.Captura.Opciones
{
    /// <summary>
    /// Lógica de interacción para Carptas.xaml
    /// </summary>
    public partial class Ubicaciones : System.Windows.Controls.UserControl
    {
        public Ubicaciones()
        {
            InitializeComponent();
        }

        private void btnExaminarDescargar_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog Carpeta = new FolderBrowserDialog();
            if (Carpeta.ShowDialog() == DialogResult.OK)
            {
                txtCarpeta.Text = Carpeta.SelectedPath;    
            }
        }

        private void btnExaminarCargar_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog Carpeta = new FolderBrowserDialog();
            if (Carpeta.ShowDialog() == DialogResult.OK)
            {
                txtCarpetaCargar.Text = Carpeta.SelectedPath;
            }
        }
    }
}
