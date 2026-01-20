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
using System.Windows.Shapes;

namespace Ruv.WPF.Captura.Registro.ValidacionIdentidad
{
    /// <summary>
    /// Lógica de interacción para PreguntasValidacion.xaml
    /// </summary>
    public partial class PreguntasValidacion : Page
    {
        clsPersonaIdentidad PersonaEncontrada;
        public PreguntasValidacion(clsPersonaIdentidad _personaEncontrada)
        {
            InitializeComponent();
            PersonaEncontrada = _personaEncontrada;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbPreguntas.ItemsSource = PersonaEncontrada.PreguntasValidacion;
        }

        private void btnValidar_Click(object sender, RoutedEventArgs e)
        {
            int cantidadPreguntas = 0;
            int cantidadRespuestasOk = 0;
            foreach (var item in PersonaEncontrada.PreguntasValidacion)
            {
                cantidadPreguntas++;
                foreach (var ops in item.OpcionesPreguntas)
                {
                    if (ops.Valida && ops.Respuesta)
                        cantidadRespuestasOk++;
                }
            }
            if (cantidadPreguntas == cantidadRespuestasOk) {
                EnvioConfirmacion envioConfirmacion = new EnvioConfirmacion(PersonaEncontrada);
                this.NavigationService.Navigate(envioConfirmacion);
            }
            else
            {
                RUV.I.UIPrincipal.PersonaEncontrada = null;
                MessageBox.Show("Las validaciones no se completaron");
            }
        }
    }
}
