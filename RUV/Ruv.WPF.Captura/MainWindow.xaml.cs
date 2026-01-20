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
using Microsoft.Win32;
using System.Drawing.Printing;
using Ruv.WPF.Captura.Controles;
using System.Configuration;
using Ruv.WPF.Captura.Registro;
using Ruv.WPF.Captura.Registro.ValidacionIdentidad;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region CONSTRUCTOR

        public MainWindow()
        {

            InitializeComponent();
            RUV.I.UIPrincipal = this;
            //RUV.I.Impresion.ConfiguracionInicialImpresora();
            RUV.I.Configuraciones.Impresion.ConfiguracionInicialImpresora();
            RUV.I.Configuraciones.Ubicaciones.EstablecerUbicaciones();

            // txtConexion.Text = ConfigurationManager.ConnectionStrings["cnBaseDatos"].ConnectionString;
            // Mostrar la marca del modo de ejecución.
            switch (RUV.I.ModoEjecucion)
            {

                case Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion.Desarrollo:
                    txtModoEjecucion.Text = "DESARROLLO";
                    txtModoEjecucion.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion.Pruebas:
                    txtModoEjecucion.Text = "PRUEBAS";
                    txtModoEjecucion.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion.Produccion:
                    txtModoEjecucion.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion.Capacitacion:
                    txtModoEjecucion.Text = "CAPACITACION";
                    txtModoEjecucion.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;
                default:
                    break;
            }
            this.Closed += new EventHandler(MainWindow_Closed);


        }


        void MainWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                RUV.I.Seguridad.CerrarSesionAsync();
                if (RUV.I.MultiTarea != null) RUV.I.MultiTarea.DetenerBackground();
                Application.Current.Shutdown();
            }
            catch { }
        }

        #endregion

        private void btnLinqAnonimo_Click(object sender, RoutedEventArgs e)
        {
            List<clsDatos> Lista = new List<clsDatos>();
            Lista.Add(new clsDatos { Nombre = "Néstor", Edad = 45 });
            Lista.Add(new clsDatos { Nombre = "Rafael", Edad = 46 });
            Lista.Add(new clsDatos { Nombre = "Ernesto", Edad = 47 });
            Lista.Add(new clsDatos { Nombre = "Mauricio", Edad = 48 });
            Lista.Add(new clsDatos { Nombre = "El Perro", Edad = 49 });

            var Consulta = Lista.Select(x =>
              new
              {
                  TheName = x.Nombre,
                  TheAge = x.Edad
              }).OrderBy(x => x.TheName);

            //lbxDatos.DisplayMemberPath = "TheName";
            //lbxDatos.ItemsSource = Consulta;

            //lbxArchivos.ItemsSource = System.IO.Directory.EnumerateFiles(@"f:\tmp", "*");

            //// List of printers.

            //lbxPrinters.ItemsSource = PrinterSettings.InstalledPrinters;

        }

        private void btnCallWCFService_Click(object sender, RoutedEventArgs e)
        {

        }

        #region NAVEGACIÓN

        /// <summary>
        /// Navega hasta la página indicada.
        /// </summary>
        /// <param name="rutaPagina"></param>
        public void NavegarA(string rutaPagina = "Seguridad/FondoVacio")
        {
            Dispatcher.BeginInvoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(() =>
               {
                   if (string.IsNullOrWhiteSpace(rutaPagina))
                       frmMain.Source = null;
                   else
                       frmMain.Navigate(new Uri(string.Format("{0}.xaml", rutaPagina),
                         UriKind.Relative));
               }));
        }

        /// <summary>
        /// Lanza la navegación hacia una página previamente creada.
        /// </summary>
        /// <param name="objetoPagina"></param>
        public void NavegarA(Page objetoPagina)
        {
            frmMain.Navigate(objetoPagina);
        }

        /// <summary>
        /// Navegación hacia la lista de tareas.
        /// </summary>
        public void NavegarAListaDeTareas()
        {
            NavegarA("ListaTareas/ListaTareasV2");
        }

        /// <summary>
        /// Comando para abrir navegar hacia la lista de tareas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AbrirListaDeTareas_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            NavegarAListaDeTareas();
        }

        /// <summary>
        /// Siempre informa que el el comando se puede ejecutar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComandoGenerico_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        #endregion

        #region USO DEL MENÚ

        enum eOpcionesMenu
        {
            Registro,
            RegistroDeclaracion,
            DigitarDeclaracion,
            ObtenerDeclaracion,
            RadicarDeclaracion,
            LiderRadicacion,
            Valoración,
            ColaDeProcesos,
            CargueDeParametros,
            CerrarSesion,
            Salir,
            Configuracion,
            ListaTareas,
            GestionDocumentos,
            InactivarDocumentos,
            Opciones,
            Test
        }

        /// <summary>
        /// Invocar un elemento del menú.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvocarMenu(object sender, RoutedEventArgs e)
        {
            string OpcionStr = (e.OriginalSource as MenuItem).Tag.ToString();
            eOpcionesMenu Opcion = (eOpcionesMenu)Enum.Parse(typeof(eOpcionesMenu), OpcionStr);
            switch (Opcion)
            {
                case eOpcionesMenu.ListaTareas:
                    NavegarAListaDeTareas();
                    break;

                case eOpcionesMenu.CerrarSesion:
                    tblUsuario.Text = string.Empty;
                    RUV.I.ColaProcesos.DetenerCola();
                    RUV.I.Seguridad.CerrarSesionAsync();
                    RUV.I.IdDeclaracion = -1;
                    NavegarA("Seguridad/LoginVista");
                    break;

                case eOpcionesMenu.Valoración:
                    NavegarA("Tmp/Page1");
                    break;

                case eOpcionesMenu.RegistroDeclaracion:
                    if (RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.TomaVirtual))
                    {
                        if (RUV.I.Red.EstadoRed == eEstadoRed.Disponible)
                        {
                            var tomaDeclaracion = new TomaDeclaracion
                            {
                                Owner = this
                            };
                            tomaDeclaracion.ShowDialog();
                            if (tomaDeclaracion.DialogResult.HasValue && tomaDeclaracion.DialogResult.Value)
                            {
                                int tipoDeclaracion = PersonaEncontrada.TipoDeclaracion.HasValue ? PersonaEncontrada.TipoDeclaracion.Value : 0;
                                bool conPreguntas = false;
                                if (tipoDeclaracion == eTipoTomaDeclaracion.Virtual.GetHashCode())
                                    conPreguntas = true;
                                if (conPreguntas)
                                {
                                    var validacion = new Validacion(PersonaEncontrada, conPreguntas)
                                    {
                                        Owner = this
                                    };
                                    validacion.ShowDialog();
                                    if (validacion.DialogResult.HasValue && validacion.DialogResult.Value)
                                        NavegarA("Registro/RegistroVista");
                                }
                                else
                                    NavegarA("Registro/RegistroVista");
                            }
                            else
                                MessageBox.Show("No se pudo validar la identidad", "Error Validación", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                            NavegarA("Registro/RegistroVista");
                    }
                    else
                        NavegarA("Registro/RegistroVista");
                    break;
                case eOpcionesMenu.GestionDocumentos:
                    NavegarA("GestorFormulario/GeneracionFormulario");
                    break;

                case eOpcionesMenu.InactivarDocumentos:
                    NavegarA("GestorFormulario/InactivacionFormulario");
                    break;

                case eOpcionesMenu.Configuracion:
                    /*var VentanaCI = new Ruv.WPF.Captura.Opciones.ConfiguracionImpresora();
                    VentanaCI.ShowDialog();*/
                    new Ruv.WPF.Captura.Opciones.Configuracion().ShowDialog();
                    break;

                case eOpcionesMenu.RadicarDeclaracion:
                    NavegarA("Registro/Secciones/Radicacion");
                    break;

                case eOpcionesMenu.LiderRadicacion:
                    NavegarA("Radicacion/LiderRadicacion");
                    break;

                case eOpcionesMenu.ColaDeProcesos:
                    NavegarA("ListaTareas/ColaProcesos");
                    break;

                case eOpcionesMenu.ObtenerDeclaracion:
                    ObtenerDeclaracionDesdeServidor();
                    break;

                case eOpcionesMenu.CargueDeParametros:
                    ForzarCargueParametros();
                    break;

                case eOpcionesMenu.Salir:
                    RUV.I.Seguridad.CerrarSesionAsync();
                    Application.Current.Shutdown();
                    break;

                case eOpcionesMenu.Test:
                    NavegarA("Test/TestPage");
                    break;
            }
        }

        public clsPersonaIdentidad PersonaEncontrada { get; set; }

        /// <summary>
        /// Altera la visibilidad del menú de acuerdo a los permisos del usuario.
        /// </summary>
        public Boolean MenuEsVisible
        {
            get { return menuMain.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                    VisualStateManager.GoToElementState(dpMain, "MenuVisible", false);
                else
                    VisualStateManager.GoToElementState(dpMain, "MenuInvisible", false);

                if (value)
                {
                    if (RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.Registrar_Declaraciones) ||
                        RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.ObtenerDeclaracion))
                    {
                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.Registro.ToString(), true);

                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.RegistroDeclaracion.ToString(),
                          RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.Registrar_Declaraciones));

                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.ObtenerDeclaracion.ToString(),
                        RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.ObtenerDeclaracion));
                    }
                    else
                    {
                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.Registro.ToString(), false);
                    }

                    if (RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.OPCIONES_COLA_PROCESOS) ||
                        RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.OPCIONES_CARGUE_PARAMETROS) ||
                        RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.OPCIONES_CONFIGURACION))
                    {
                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.Opciones.ToString(), true);

                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.ColaDeProcesos.ToString(),
                          RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.OPCIONES_COLA_PROCESOS));

                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.CargueDeParametros.ToString(),
                          RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.OPCIONES_CARGUE_PARAMETROS));

                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.Configuracion.ToString(),
                          RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.OPCIONES_CONFIGURACION));
                    }
                    else
                    {
                        EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.Opciones.ToString(), false);
                    }

                    EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.RadicarDeclaracion.ToString(),
                            RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.Radicar_Declaracion));

                    EstablecerVisibilidadMenu(menuMain, eOpcionesMenu.GestionDocumentos.ToString(),
                        RUV.I.Usuario.Permisos.Contains(Ruv.Infrastructure.Crosscutting.Common.ePermisosUsuario.Control_Documentos_WPF));

                    EstablecerVisibilidadMenuItem(eOpcionesMenu.Valoración.ToString(), false);
                }
            }
        }

        /// <summary>
        /// Establece la visibilidad de los MenuItems de acuerdo a los permisos del usuario.
        /// </summary>
        /// <param name="itemTag"></param>
        /// <param name="esVisibile"></param>
        void EstablecerVisibilidadMenuItem(string itemTag, Boolean esVisibile)
        {
            Dispatcher.BeginInvoke(
              System.Windows.Threading.DispatcherPriority.Normal,
              new Action(() =>
              {
                  var Item = menuMain.Items.OfType<MenuItem>().Where(x =>
                    x.Tag != null
                    && x.Tag.ToString() == itemTag)
                    .FirstOrDefault();


                  if (Item != null)
                      Item.Visibility =
                        esVisibile ?
                        System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
              }));

        }

        /// <summary>
        /// Establece la visibilidad de los MenuItems de acuerdo a los permisos del usuario.
        /// </summary>
        /// <param name="itemTag"></param>
        /// <param name="esVisibile"></param>
        void EstablecerVisibilidadMenu(Menu menu, string itemTag, Boolean esVisibile)
        {
            //Dispatcher.BeginInvoke(
            //System.Windows.Threading.DispatcherPriority.Normal,
            //new Action(() =>
            //{

            foreach (var Item in menu.Items)
            {
                if ((Item as MenuItem).Tag != null && (Item as MenuItem).Tag.ToString() == itemTag)
                {
                    (Item as MenuItem).Visibility =
                    esVisibile ?
                    System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
                else
                    EstablecerVisibilidadsubMenu((Item as MenuItem), itemTag, esVisibile);
            }

            //}));

        }

        void EstablecerVisibilidadsubMenu(MenuItem menuItem, string itemTag, Boolean esVisibile)
        {
            //Dispatcher.BeginInvoke(
            //  System.Windows.Threading.DispatcherPriority.Normal,
            //  new Action(() =>
            //  {

            foreach (var Item in menuItem.Items)
            {
                if ((Item as MenuItem).Tag != null && (Item as MenuItem).Tag.ToString() == itemTag)
                {
                    (Item as MenuItem).Visibility =
                    esVisibile ?
                    System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
                else
                    EstablecerVisibilidadsubMenu((Item as MenuItem), itemTag, esVisibile);
            }

            //}));

        }

        #endregion

        #region MESSAGE BOX ESTÁNDAR

        /// <summary>
        /// Presenta un mensaje informando un error del usuario.
        /// </summary>
        /// <param name="mensaje"></param>
        public void ReportarErrorDeUsuario(string mensaje)
        {
            MessageBox.Show(mensaje
              , "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        /// <summary>
        /// Presenta un mensaje informando un error del usuario.
        /// </summary>
        /// <param name="mensaje"></param>
        public void ReportarInformacionDeUsuario(string mensaje)
        {
            MessageBox.Show(mensaje
              , "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        /// <summary>
        /// Presenta un mensaje informando un error del usuario, incluye el formato.
        /// </summary>
        /// <param name="mensaje"></param>
        public void ReportarInformacionDeUsuario(params string[] mensaje)
        {
            ReportarInformacionDeUsuario(string.Format(mensaje[0], mensaje.Skip(1).ToArray()));
        }

        /// <summary>
        /// Solicita confirmación al usuario para alguna acción.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public Boolean UsuarioConfirmar(string mensaje)
        {
            var Resultado =
                    MessageBox.Show(mensaje
              , "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            return Resultado == MessageBoxResult.Yes;
        }


        #endregion

        #region OBTENER EL VALIDADOR ACTUAL (SÓLO HAY UNO PRESENTE SIEMPRE)

        /// <summary>
        /// Regresa acceso al validador actual.
        /// </summary>
        public Ruv.WPF.Captura.Controles.ResumenValidacion ValidadorActual
        {
            get
            {
                clsUIHelper UI = new clsUIHelper();
                var Encontrado = UI.GetChildren(frmMain, CriterioBusquedaValidador, false);
                if (Encontrado == null || !Encontrado.Any())
                    return null;
                else
                    return Encontrado.ElementAt(0).SourceControl as Ruv.WPF.Captura.Controles.ResumenValidacion;
            }
        }

        /// <summary>
        /// Criterio de búsqueda para el validador.
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        FrameworkElementItem CriterioBusquedaValidador(DependencyObject child)
        {
            ResumenValidacion FE = child as Ruv.WPF.Captura.Controles.ResumenValidacion;
            if (FE == null)
                return null;
            else
                return new FrameworkElementItem { SourceControl = FE };
        }

        #endregion

        #region BLOQUEO DE LA INTERFASE

        /// <summary>
        /// Permite bloquear la interfase.
        /// </summary>
        public string BloquearInterfase
        {
            get
            {
                return borBloqueoInterfase.TextoBloqueo;
            }
            set
            {
                borBloqueoInterfase.TextoBloqueo = value;

                //Sipod.I.MultiTarea.PosponerEjecucion(1,
                //new Action(() =>
                //{
                //  var SB = borBloqueoInterfase.Resources["sbBloqueoInterfase"] as System.Windows.Media.Animation.Storyboard;
                //  if (!string.IsNullOrWhiteSpace(value))
                //  {
                //    txtMensajeBloqueo.Text = value;
                //    borBloqueoInterfase.Visibility = System.Windows.Visibility.Visible;
                //    Cursor = System.Windows.Input.Cursors.Wait;
                //    SB.Begin();
                //    App.Current.DoEvents();
                //  }
                //  else
                //  {
                //    txtMensajeBloqueo.Text = null;
                //    SB.Stop();
                //    Cursor = System.Windows.Input.Cursors.Arrow;
                //    borBloqueoInterfase.Visibility = System.Windows.Visibility.Collapsed;
                //    App.Current.DoEvents();
                //  }
                //}));
            }
        }

        public void Notificar(UIElement elemento, string texto, Hardcodet.Wpf.TaskbarNotification.BalloonIcon icono = Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info)
        {
            if (elemento != null)
            {
                Notificaciones.ShowCustomBalloon(elemento, System.Windows.Controls.Primitives.PopupAnimation.Slide, 5000);
            }
            else
            {
                Notificaciones.ShowBalloonTip(Ruv.WPF.Captura.App.Current.Resources["TituloAplicacion"].ToString(), texto, icono);
            }
        }
        
        #endregion

        #region Barra notificación

        /// <summary>
        /// Muestra el mensaje ingresado en la barra de notificaciones
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar en la barra de notificaciones. Para limpiar el mensaje no enviar el parámetro</param>
        public void MensajeNotificacion(string mensaje = null)
        {
            RUV.I.UIPrincipal.Dispatcher.BeginInvoke(
             System.Windows.Threading.DispatcherPriority.Normal,
             new Action(() =>
             {
                 txbNotificacion.Text = mensaje;
             }));
        }

        #endregion

    }
}
