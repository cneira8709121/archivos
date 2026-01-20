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
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;

namespace Ruv.WPF.Captura.Registro
{
    /// <summary>
    /// Interaction logic for TomaFirma.xaml
    /// </summary>
    public partial class TomaFirma : Window
    {
        public TomaFirma()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            List<clsFirma> lstFirma = (List<clsFirma>)DataContext;
            if (lstFirma != null)
            {
                bool bFirmado = false;
                foreach (clsFirma f in lstFirma)
                {
                    bFirmado = f.firma != null;
                    if (!bFirmado) break;
                }
                if (bFirmado) Close();
                else MessageBox.Show("Deben guardarse las firmas.");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            Close();
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                List<clsFirma> lstFirma = (List<clsFirma>)e.NewValue;
                lstFirma.ForEach(x =>
                    {
                        if (x.firmaOwner == FirmaOwner.DECLARANTE)
                        {
                            tbiFirmaDeclarante.Visibility = Visibility.Visible;
                            ucFirmaDeclarante.DataContext = x;
                        }
                        if (x.firmaOwner == FirmaOwner.TUTOR)
                        {
                            tbiFirmaTutor.Visibility = Visibility.Visible;
                            ucFirmaTutor.DataContext = x;
                        }
                    });
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ucFirmaDeclarante.DisableCapture();
            ucFirmaTutor.DisableCapture();
        }
    }
}
