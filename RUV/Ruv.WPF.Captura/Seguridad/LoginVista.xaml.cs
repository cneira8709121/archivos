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
using System.ComponentModel;
using Microsoft.Win32;
using Ruv.WPF.Captura.Infrastructure;

namespace Ruv.WPF.Captura.Seguridad
{
    public partial class LoginVista : Page
    {

        #region CONSTRUCTOR

        public LoginVista()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += new RoutedEventHandler(LoginVista_Loaded);

        }

        void LoginVista_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> parametros = GetQueryStringParameters();


            string _usuario = string.Empty, _password = string.Empty;          
            if (parametros != null && parametros.Count > 0 && RUV.I.IdDeclaracion == 0) //si viene desde el webapp se loguea y entra directamente a la declaracion
            {
                _usuario = parametros["Log"].ToString();
                _password = System.Uri.UnescapeDataString(parametros["Pas"].ToString());
                RUV.I.IdDeclaracion = Convert.ToInt32(parametros["IdDec"]);
                RUV.I.IdValoracion = Convert.ToInt32(parametros["IdVal"].ToString()); 
                RUV.I.Url = parametros["Url"].ToString();
                clsCryptoUtil cifrado = new clsCryptoUtil();
                _password = cifrado.DecryptStringFixed(_password);
                if (!string.IsNullOrEmpty(_usuario) && !string.IsNullOrEmpty(_password))
                {
                    tbxUsuario.Text = _usuario;
                    tbxContraseña.Password = _password;
                    Ingresar_Click(new object(), new RoutedEventArgs());
                }                
            }
            else
            {
                tbxUsuario.Text = RUV.I.Configuraciones.ConfiguracionGeneral.UsuarioCuentaPreCargada;
                tbxContraseña.Password = RUV.I.Configuraciones.ConfiguracionGeneral.UsuarioClavePreCargarda;
            }
            tbxUsuario.Focus();
            RUV.I.UIPrincipal.MenuEsVisible = false;
            GC.Collect();
        }

        //solo funcionara cuando la captura se invoque desde la WebApp
        public static Dictionary<string, string> GetQueryStringParameters()
        {
            Dictionary<string, string> nameValueTable = new Dictionary<string, string>();
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                string url = string.Empty;
                try
                {                    
                    url = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
                    string queryString = (new Uri(url)).Query;
                    string[] nameValuePairs = queryString.Split('&');
                    foreach (string pair in nameValuePairs)
                    {
                        string[] vars = pair.Split('=');
                        if (!string.IsNullOrEmpty(pair) && !nameValueTable.ContainsKey(vars[0]))
                        {
                            if (vars[0].Contains("?")) vars[0] = vars[0].Replace("?", "");
                            nameValueTable.Add(vars[0], vars[1]);                            
                        }
                    }
                }
                catch (Exception)
                {
                    return new Dictionary<string, string>();
                }                                
            }
            return nameValueTable;
        }

        #endregion

        #region AUTENTICAR

        private void Ingresar_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(
            new Action(() =>
            {
                EstaHabilitado = false;
                Mensaje = "Autenticando";
            }), System.Windows.Threading.DispatcherPriority.Normal, null);

            Usuario = tbxUsuario.Text;
            Contraseña = tbxContraseña.Password;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(IngresarAsync);
            worker.RunWorkerAsync();
        }

        private void IngresarAsync(object sender, DoWorkEventArgs e)
        {
            Autenticado = false;

            var Sec = RUV.I.Seguridad;
            Sec.Autenticado = false;
            Sec.Autenticar(Usuario, Contraseña, ArchivoCertificado);
            if (Sec.Autenticado)
            {
                // Habilitar la cola de procesos.
                RUV.I.ColaProcesos.InicializarColaProcesos();

                this.Dispatcher.Invoke(
                  new Action(() =>
                  {
                      Mensaje = "Ok";
                      if (RUV.I.Usuario != null)
                          RUV.I.UIPrincipal.tblUsuario.Text = RUV.I.Usuario.Nombre;
                  }), System.Windows.Threading.DispatcherPriority.Normal, null);

                System.Threading.Thread.Sleep(1000);

                RUV.I.UIPrincipal.NavegarA("Seguridad/VerificarInformacionGeneral");
            }

            if (Sec.TipoDeError == Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion)
            {
                this.Dispatcher.Invoke(
                new Action(() =>
                {
                    Mensaje = Sec.MensajeEstado;
                    EstaHabilitado = true;
                    tbxUsuario.Focus();
                }), System.Windows.Threading.DispatcherPriority.Normal, null);
            }
        }

        //void worker_AutenticarCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //  if (!Autenticado)
        //  {
        //    EstaHabilitado = true;
        //    tbxUsuario.Focus();
        //  }
        //  else
        //    Mensaje = "";
        //}

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// Verdadero: El usuario está autenticado.
        /// </summary>
        Boolean Autenticado;

        public Boolean EstaHabilitado
        {
            get { return (Boolean)GetValue(EstaHabilitadoProperty); }
            set { SetValue(EstaHabilitadoProperty, value); }
        }

        public static readonly DependencyProperty EstaHabilitadoProperty =
            DependencyProperty.Register("EstaHabilitado", typeof(Boolean),
            typeof(LoginVista), new UIPropertyMetadata(true));

        public string Mensaje
        {
            get { return (string)GetValue(MensajeProperty); }
            set { SetValue(MensajeProperty, value); }
        }

        public static readonly DependencyProperty MensajeProperty =
            DependencyProperty.Register("Mensaje", typeof(string),
            typeof(LoginVista), new UIPropertyMetadata(""));

        public string Usuario { get; set; }
        public string Contraseña { get; set; }

        #endregion

        #region SELECCIÓN DEL TOKEN

        /// <summary>
        /// La ruta al archivo de certificado.
        /// </summary>
        string ArchivoCertificado;

        /// <summary>
        /// Permite seleccionar el archivo de certificado a utilizar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Token_Click(object sender, RoutedEventArgs e)
        {
            var DrivesUSB = RUV.I.Util.ObtenerDiscosExtraibles;
            if (!DrivesUSB.Any())
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                  "Conecte el dispositivo USB que contiene\nel token a utilizar");
                return;
            }

            var USB = DrivesUSB.FirstOrDefault();
            if (USB == null) return;

            OpenFileDialog OFD = new OpenFileDialog()
            {
                Filter = "Certificados (*.cer)|*.cer",
                Multiselect = false,
                InitialDirectory = USB
            };
            var Resultado = OFD.ShowDialog();
            if (!Resultado.HasValue || !Resultado.Value) return;

            if (!DrivesUSB.Any(x => OFD.FileName.StartsWith(x)))
            {
                RUV.I.UIPrincipal.ReportarInformacionDeUsuario(
                  "Sólo puede utilizar un token almacenado\nen su dispositivo USB.");
                return;
            }

            btnToken.Content = "Seleccionado (no desconectar)";
            ArchivoCertificado = OFD.FileName;

            btnIngresar.Focus();
        }

        #endregion

        private void btnRecuperarClave_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Ruv.WPF.Captura.Properties.Settings.Default.UrlRestablecimiento);
        }
    }
}
