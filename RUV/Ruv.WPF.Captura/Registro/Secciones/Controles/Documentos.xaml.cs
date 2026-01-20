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

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
    /// <summary>
    /// Lógica de interacción para Documentos.xaml
    /// </summary>
    public partial class Documentos : Window
    {
        public Documentos()
        {
            InitializeComponent();
        }


        private void nvArchiv_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string pageTitle = ((WebBrowser)sender).InvokeScript;

        }
    }
}
