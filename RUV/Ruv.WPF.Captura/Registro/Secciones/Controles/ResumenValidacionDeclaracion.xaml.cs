using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WPF.Captura.Infrastructure;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
    /// <summary>
    /// Lógica de interacción para ResumenValidacionDeclaracion.xaml
    /// </summary>
    public partial class ResumenValidacionDeclaracion : Window
    {
        #region CONSTRUCTOR

        public ResumenValidacionDeclaracion()
        {
            InitializeComponent();
        }

        public ResumenValidacionDeclaracion(clsDeclaracion declaracion)
        {
            InitializeComponent();
            Declaracion = declaracion;
            this.Loaded += new RoutedEventHandler(ResumenValidacionDeclaracion_Loaded);
        }

        void ResumenValidacionDeclaracion_Loaded(object sender, RoutedEventArgs e)
        {
            ValidarDesplegar();
        }

        #endregion

        #region VALIDAR Y DESPLEGAR ERRORES

        void ValidarDesplegar()
        {
            List<eEstadoValidacion> Requeridas = Ruv.WPF.Captura.Infrastructure.clsUtil.ValidacionesRequeridas();
            int validacionesSaltadas = 0;
            var Resultado = Declaracion.ValidarDeclaracion(Requeridas, ref validacionesSaltadas);
            if (Resultado == null || !Resultado.Any())
            {
                HayErroresPendientes = true;
                this.Close();
                return;
            }

            tvMain.ItemsSource = Resultado;
        }

        #endregion

        #region PROPIEDADES Y VARIABLES

        private bool _HayErroresPendientes;
        /// <summary>
        /// Verdadero: Hay errores pendientes al cerrar.
        /// </summary>
        public bool HayErroresPendientes
        {
            get { return _HayErroresPendientes; }
            set { _HayErroresPendientes = value; }
        }

        /// <summary>
        /// La declaración que se está validando.
        /// </summary>
        clsDeclaracion Declaracion;

        #endregion

        #region CERRAR LA VENTANA

        private void CerrarVentana(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<eEstadoValidacion> Requeridas = Ruv.WPF.Captura.Infrastructure.clsUtil.ValidacionesRequeridas();
            int validacionesSaltadas = 0;
            var Resultado = Declaracion.ValidarDeclaracion(Requeridas, ref validacionesSaltadas);
            HayErroresPendientes = (Resultado != null) || Resultado.Any();
        }

        private void CerrarBoton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region REFRESCAR LA LISTA

        private void Refrescar(object sender, RoutedEventArgs e)
        {
            ValidarDesplegar();
        }

        #endregion

    }
}