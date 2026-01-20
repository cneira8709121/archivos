using Ruv.Infrastructure.Crosscutting.Common.Entidades;
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

namespace Ruv.WPF.Captura.Registro
{
    /// <summary>
    /// Lógica de interacción para NotificacionElectronica.xaml
    /// </summary>
    public partial class NotificacionElectronica : Window
    {
        public clsNotificacionElectronica Notificacion { get; set; }
        public NotificacionElectronica()
        {
            InitializeComponent();
            Notificacion = new clsNotificacionElectronica();
            DataContext = Notificacion;
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                stTieneFormato.Visibility = Visibility.Collapsed;
                stConsentimiento.Visibility = Visibility.Visible;
            }
            else
            {
                Cambio();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var notificacion = DataContext as clsNotificacionElectronica;
            if (notificacion.AutorizaNotificacion.HasValue)
                this.DialogResult = true;
            else
                MessageBox.Show("Indique si otorga el consentimiento para ser notificado electronicamente");
        }

        private void chkTieneFormato_Checked(object sender, RoutedEventArgs e)
        {
            Cambio();
        }

        private void chkTieneFormato_Unchecked(object sender, RoutedEventArgs e)
        {
            Cambio();
        }

        private void Cambio() {
            if (chkTieneFormato.IsChecked.HasValue && chkTieneFormato.IsChecked.Value)
                stConsentimiento.Visibility = Visibility.Visible;
            else
                stConsentimiento.Visibility = Visibility.Collapsed;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
