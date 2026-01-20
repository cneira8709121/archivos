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
using Ruv.WPF.Captura.Infrastructure.ColaProcesos;
using System.Windows.Forms;

namespace Ruv.WPF.Captura.ListaTareas
{
    /// <summary>
    /// Lógica de interacción para BorrarColaConfirmacion.xaml
    /// </summary>
    public partial class BorrarColaConfirmacion : Window
    {

        public BorrarColaConfirmacion()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(BorrarColaConfirmacion_Loaded);
        }

        void BorrarColaConfirmacion_Loaded(object sender, RoutedEventArgs e)
        {
            dgCola.ItemsSource = RUV.I.ColaProcesos.ListaProcesos.Where(x=> x.Estado ==(int)eEstadoProcesoCola.RequiereRevision ||x.Estado ==(int)eEstadoProcesoCola.PendienteTransmitir);
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //RUV.I.ColaProcesos.PurgarCola();
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            RUV.I.Seguridad.CerrarSesionAsync();
            System.Windows.Application.Current.Shutdown();
        }


    }
}
