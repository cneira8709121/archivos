using Ruv.WPF.Captura.Controles;
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

namespace Ruv.WPF.Captura.Registro.Secciones
{
    /// <summary>
    /// Lógica de interacción para FechaLugar.xaml
    /// </summary>
    public partial class FechaLugar : UserControl
    {
        public FechaLugar()
        {
            InitializeComponent();
            geo01.MunicipioCambio += Geo01_MunicipioCambio;
        }


        public event EventHandler<CambioPaisEventArgs> CambioPais;

        protected void OnCambioPais(long? nuevoPais)
        {
            if(CambioPais != null)
                CambioPais?.Invoke(this, new CambioPaisEventArgs(nuevoPais));
        }

        private void Geo01_MunicipioCambio(object sender, EventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                if (geo01.MunicipioId.HasValue)
                    MostrarMensajeLugar();
            }
        }

        public bool CambiaGeografia { get; set; }
        public string Numero
        {
            get { return steTitulo.Numero; }
            set { steTitulo.Numero = value; }
        }

        public string Texto
        {
            get { return steTitulo.Texto; }
            set { steTitulo.Texto = value; }
        }

        private bool _CajaFechaEsVisible = true;
        /// <summary>
        /// Oculta o muestra la caja de ingreso de fecha.
        /// </summary>
        public bool CajaFechaEsVisible
        {
            get { return _CajaFechaEsVisible; }
            set
            {
                _CajaFechaEsVisible = value;
                if (!value)
                {
                    steFecha.Visibility = System.Windows.Visibility.Collapsed;
                    cifFecha.Visibility = System.Windows.Visibility.Collapsed;
                    txtRedondeoFecha.Visibility = System.Windows.Visibility.Collapsed;
                    // La primera columna ya no tiene ancho.
                    grdMain.ColumnDefinitions[0].Width = new GridLength(0.2d, GridUnitType.Pixel);

                }
            }
        }

        private bool _ComboPaisEsVisible = true;
        /// <summary>
        /// Oculta o muestra el combo de País.
        /// </summary>
        public bool ComboPaisEsVisible
        {
            get { return _ComboPaisEsVisible; }
            set
            {
                _ComboPaisEsVisible = value;
                if (!value)
                {
                    cbPais.Visibility = System.Windows.Visibility.Collapsed;
                    // La Segunda columna ya no tiene ancho.
                    grdMain.ColumnDefinitions[1].Width = new GridLength(0.2d, GridUnitType.Pixel);

                }
            }
        }

        public void MostrarMensajeLugar()
        {
            lblMensajeFechaLugar.Content = "Recuerde que la información de los lugares debe coincidir con la registrada en la narración de hechos";
            MessageBox.Show("Recuerde que la información de los lugares debe coincidir con la registrada en la narración de hechos");
        }

        public void MostrarMensajeFecha()
        {
            lblMensajeFechaLugar.Content = "Recuerde que la información de las fechas debe coincidir con la registrada en la narración de hechos";
            MessageBox.Show("Recuerde que la información de las fechas debe coincidir con la registrada en la narración de hechos");
        }

        private void cifFecha_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RUV.I.Usuario.RolesUsuario.Contains(Ruv.Infrastructure.Crosscutting.Common.eRolesUsuario.TomaEnLinea))
            {
                if (!cifFecha.Fecha.HasValue && !cifFecha.Dia.HasValue && !cifFecha.Mes.HasValue && !cifFecha.Año.HasValue)
                {
                    MostrarMensajeFecha();
                }
            }
        }

        private void cbPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0) {
                var pais = e.AddedItems[0] as Ruv.Infrastructure.Crosscutting.Common.General.clsParametroPais;
                OnCambioPais(pais.Id.Value);
            }
        }
    }
    public class CambioPaisEventArgs : EventArgs
    {
        public long? NuevoPais { get; set; }

        public CambioPaisEventArgs(long? nuevoPais)
        {
            NuevoPais = nuevoPais;
        }
    }
}
