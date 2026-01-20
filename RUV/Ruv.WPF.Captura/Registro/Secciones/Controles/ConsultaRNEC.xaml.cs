using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Ruv.WPF.Captura.Infrastructure;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
    /// <summary>
    /// Lógica de interacción para SeleccionAnexo.xaml
    /// </summary>
    public partial class ConsultaRNEC : Window
    {
        #region CONSTRUCTOR

        public List<clsPersonaRNEC> PersonasEncontradas { get; set; }
        public clsPersonaRNEC PersonaSeleccionada { get; set; }
        public ConsultaRNEC()
        {
            InitializeComponent();
            dgPersonasEncontradas.ItemsSource = PersonasEncontradas;
        }

        public ConsultaRNEC(List<clsPersonaRNEC> personasEncontradas)
        {
            InitializeComponent();
            PersonasEncontradas = personasEncontradas;
            dgPersonasEncontradas.ItemsSource = PersonasEncontradas;
        }

        #endregion

        #region ACEPTAR Y CANCELAR


        /// <summary>
        /// Aceptar la selección del usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            var persona = dgPersonasEncontradas.SelectedValue as clsPersonaRNEC;
            if (persona != null)
                PersonaSeleccionada = persona;
            else
            {
                MessageBox.Show("Debe seleccionar al menos una persona de la lista", "Consulta RNEC", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.DialogResult = true;
        }

        

        /// <summary>
        /// Cancelar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region CERRAR ESTA VENTANA

        /// <summary>
        /// lanzar algunos procesos al cerrar esta ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CerrarVentana(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
