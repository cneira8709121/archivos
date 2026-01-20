using Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion;
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

namespace Ruv.WPF.Captura.Registro.ValidacionIdentidad
{
    /// <summary>
    /// Lógica de interacción para EnvioConfirmacion.xaml
    /// </summary>
    public partial class EnvioConfirmacion : Page
    {
        clsPersonaIdentidad PersonaEncontrada;
        public clsValidacion Validacion { get; set; }
        public EnvioConfirmacion(clsPersonaIdentidad _personaEncontrada)
        {
            InitializeComponent();
            PersonaEncontrada = _personaEncontrada;
            Validacion = new clsValidacion();
            DataContext = Validacion;
        }
        public EnvioConfirmacion()
        {
            InitializeComponent();
        }

        private void btnEnviarCodigo_Click(object sender, RoutedEventArgs e)
        {
            bool enCelular = false;
            if (Validacion.AlCelular)
            {
                if (string.IsNullOrEmpty(PersonaEncontrada.Celular))
                {
                    MessageBox.Show("No puede seleccionar este tipo de envió, ya que no se ingreso el numero de telefono", "No tiene celular", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;

                }
                enCelular = true;
            }
            else
            {
                if (string.IsNullOrEmpty(PersonaEncontrada.Correo))
                {
                    MessageBox.Show("No puede seleccionar este tipo de envió, ya que no se ingreso el correo", "No tiene correo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            bool result = false;
            RUV.I.MultiTarea.EjecutarEnBackground((() =>
            {
                result = RUV.I.Red.ServicioGeneral.EnviarValidacion(PersonaEncontrada.NumeroDocumento, $"{PersonaEncontrada.PrimerNombre} {PersonaEncontrada.PrimerApellido}", PersonaEncontrada.Celular, PersonaEncontrada.Correo, enCelular);
            }),
                        (() =>
                        {
                            RUV.I.UIPrincipal.BloquearInterfase = null;
                            var ws = Window.GetWindow(this);
                            MessageBox.Show(ws, "Se envio el mensaje, indicar el codigo de verificación", "RUV", MessageBoxButton.OK, MessageBoxImage.Information);
                        }));

        }

        private void btnValidar_Click(object sender, RoutedEventArgs e)
        {

            bool enCelular = false;

            if (Validacion.AlCelular)
                enCelular = true;

            var result = RUV.I.Red.ServicioGeneral.ValidarCodigo(PersonaEncontrada.NumeroDocumento, PersonaEncontrada.Celular, PersonaEncontrada.Correo, Validacion.Codigo, enCelular);
            //RUV.I.Red.ServicioGeneral.ValidarCodigoCompleted += ServicioGeneral_ValidarCodigoCompleted;
            if (!result)
            {
                MessageBox.Show("El codigo ingresado es incorrecto", "Error Validacion", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ws = Window.GetWindow(this);
            if (ws != null)
            {
                var parent = (ws as Validacion);
                parent.DialogResult = result;
            }
        }

        private void ServicioGeneral_ValidarCodigoCompleted(object sender, GeneralService.ValidarCodigoCompletedEventArgs e)
        {
            if (!e.Result)
            {
                MessageBox.Show("El codigo ingresado es incorrecto", "Error Validacion", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ws = Window.GetWindow(this);
            if (ws != null)
            {
                var parent = (ws as Validacion);
                parent.DialogResult = e.Result;
            }
        }
    }
}
