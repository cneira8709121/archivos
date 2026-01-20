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
using Ruv.WPF.Captura.Utils;

namespace Ruv.WPF.Captura.Controles
{
    /// <summary>
    /// Lógica de interacción para Notificaciones.xaml
    /// </summary>
    public partial class Notificaciones : UserControl
    {
        public Notificaciones()
        {
            InitializeComponent();
        }

        public Notificaciones(string FileName, string Texto)
        {
            clsNotificaciones notificaciones = new clsNotificaciones();
            notificaciones.RutaAbrir = FileName;
            notificaciones.Titulo = Ruv.WPF.Captura.App.Current.Resources["TituloAplicacion"].ToString();
            notificaciones.Informacion = Texto;
            Notificacion = notificaciones;
            InitializeComponent();
        }


        public clsNotificaciones Notificacion
        {
            set
            {
                DataContext = value;
            }
        }

        private void hlkAbrir_Click(object sender, RoutedEventArgs e)
        {
            clsNotificaciones context = DataContext as clsNotificaciones;
            System.Diagnostics.Process.Start(context.RutaAbrir);
        }
    }
}
