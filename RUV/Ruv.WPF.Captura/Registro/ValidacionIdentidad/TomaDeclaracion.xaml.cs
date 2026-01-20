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

namespace Ruv.WPF.Captura.Registro
{
    /// <summary>
    /// Lógica de interacción para TomaDeclaracion.xaml
    /// </summary>
    public partial class TomaDeclaracion : Window
    {
        clsValidacionIdentidad ValidacionIdentidad;
        List<clsPersonaIdentidad> PersonasEncontradas;
        public TomaDeclaracion()
        {
            InitializeComponent();
            ValidacionIdentidad = new clsValidacionIdentidad();
            PersonasEncontradas = new List<clsPersonaIdentidad>();
            DataContext = ValidacionIdentidad;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidacionIdentidad.IdTipoDocumento.HasValue && ValidacionIdentidad.IdTipoDocumento == Ruv.Infrastructure.Crosscutting.Common.eTipoDocumento.CedulaCiudadania.GetHashCode())
            {

                var valido = PersonasEncontradas.Where(x => x.Resultado.Equals("VALIDADO")).ToList();
                if (valido.Count > 0)
                {
                    var personaEncontrada = valido.FirstOrDefault();
                    RUV.I.UIPrincipal.PersonaEncontrada = personaEncontrada;
                    this.DialogResult = true;
                }
                else
                    this.DialogResult = false;
            }
            else
            {
                RUV.I.UIPrincipal.PersonaEncontrada = new clsPersonaIdentidad { IdTipoDocumento = ValidacionIdentidad.IdTipoDocumento, 
                    NumeroDocumento= ValidacionIdentidad.NumeroDocumento,
                    PrimerNombre = ValidacionIdentidad.PrimerNombre,
                    SegundoNombre = ValidacionIdentidad.SegundoNombre,
                    PrimerApellido = ValidacionIdentidad.PrimerApellido,
                    SegundoApellido = ValidacionIdentidad.SegundoApellido
                };
                this.DialogResult = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Aqui se debe llamar el servicio
            int idUsuario = RUV.I.Usuario.Id;
            RUV.I.UIPrincipal.BloquearInterfase = "Buscando Persona";
            RUV.I.MultiTarea.EjecutarEnBackground((() =>
            {
                PersonasEncontradas = RUV.I.Red.ServicioGeneral.ValidarIdentidad(ValidacionIdentidad.TipoDeclaracion.Value, idUsuario, ValidacionIdentidad.IdTipoDocumento.Value, ValidacionIdentidad.NumeroDocumento,
                ValidacionIdentidad.PrimerNombre, ValidacionIdentidad.SegundoNombre, ValidacionIdentidad.PrimerApellido, ValidacionIdentidad.SegundoApellido,
                ValidacionIdentidad.Celular, ValidacionIdentidad.Correo).ToList();
            }),
                        (() =>
                        {
                            RUV.I.UIPrincipal.BloquearInterfase = null;
                            dgPersonasEncontradas.ItemsSource = PersonasEncontradas;
                        }));

        }
    }
}
