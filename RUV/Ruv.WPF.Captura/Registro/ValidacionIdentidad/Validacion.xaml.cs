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
    /// Lógica de interacción para Validacion.xaml
    /// </summary>
    public partial class Validacion : Window
    {
        public clsPersonaIdentidad PersonaEncontrada;
        public bool conPreguntas;
        public Validacion(clsPersonaIdentidad _personaEncontrada, bool _conPreguntas)
        {
            InitializeComponent();
            PersonaEncontrada = _personaEncontrada;
            if (_conPreguntas)
            {
                PreguntasValidacion preguntasValidacion = new PreguntasValidacion(PersonaEncontrada);
                frmValidacion.Navigate(preguntasValidacion);
            }
            else
            {
                EnvioConfirmacion envioConfirmacion = new EnvioConfirmacion(PersonaEncontrada);
                frmValidacion.Navigate(envioConfirmacion);
            }
        }
    }
}
