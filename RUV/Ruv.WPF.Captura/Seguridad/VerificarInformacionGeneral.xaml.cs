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
using Ruv.WPF.Captura.ListaTareas;

namespace Ruv.WPF.Captura.Seguridad
{
    /// <summary>
    /// Lógica de interacción para VerificarInformacionGeneral.xaml
    /// </summary>
    public partial class VerificarInformacionGeneral : Page
    {
        public VerificarInformacionGeneral()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(VerificarInformacionGeneral_Loaded);
        }

        void VerificarInformacionGeneral_Loaded(object sender, RoutedEventArgs e)
        {
            RUV.I.MultiTarea.PosponerEjecucion(100,
              new Action(() => Precargar()));
        }

        private void Precargar()
        {
            if (RUV.I.InfoGeneral.HayInformacion())
            {
                // Si la información general existe, continuar al verdadero inicio.
                RUV.I.InfoGeneral.PrecargarParametros();
                RUV.I.UIPrincipal.MenuEsVisible = true;
                // Si hay red, pasar a la lista de tareas.
                if (RUV.I.Red.EstadoRed == eEstadoRed.Disponible)
                {
                    RUV.I.UIPrincipal.NavegarA("ListaTareas/ListaTareasV2");
                    //Sipod.I.UIPrincipal.NavegarA("ListaTareas/ListaTareas");
                }
                else
                    RUV.I.UIPrincipal.NavegarA("Seguridad/FondoVacio");
                return;
            }
            else
            {
                spMain.Visibility = System.Windows.Visibility.Visible;
                ExistenDeclaEnCola();
                RUV.I.InfoGeneral.DescargaInformacionCompleted += InfoGeneral_DescargaInformacionCompleted;
                RUV.I.InfoGeneral.Descargar();
            }
            
        }



        public void ExistenDeclaEnCola()
        {
            int CantidadCola = RUV.I.ColaProcesos.ListaProcesos.Count(x => x.Estado == (int)eEstadoProcesoCola.PendienteTransmitir || x.Estado == (int)eEstadoProcesoCola.RequiereRevision);
            if (CantidadCola > 0)
            {
                BorrarColaConfirmacion colaborrar = new BorrarColaConfirmacion();
                colaborrar.ShowDialog();
            }
        }

        void InfoGeneral_DescargaInformacionCompleted(object sender, EventArgs e)
        {
            RUV.I.InfoGeneral.PrecargarParametros();
            RUV.I.InfoGeneral.DescargaInformacionCompleted -= InfoGeneral_DescargaInformacionCompleted;
            RUV.I.UIPrincipal.MenuEsVisible = true;
            
            RUV.I.UIPrincipal.NavegarA("Seguridad/FondoVacio");
        }


    }
}
