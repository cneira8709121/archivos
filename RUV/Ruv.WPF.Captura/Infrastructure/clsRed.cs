using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Ruv.WPF.Captura.Infrastructure
{
    /// <summary>
    /// Maneja todo tipo de comunicación con el servidor.
    /// </summary>
    public class clsRed : DependencyObject
    {
        #region VARIABLES

        /// <summary>
        /// Milisegundos de timeout al verificar la conexión a la red.
        /// </summary>
        const int VerificarEstadoTimeout = 10000;

        /// <summary>
        /// Time out al determinar la durta de acceso.
        /// </summary>
        const int VerificarRutaEstadoTimeout = 10000;

        /// <summary>
        /// Minutos a verificar la conexión cuando la última verificación estuvo disponible.
        /// </summary>
        TimeSpan IntervaloVerificacionChequeo = new TimeSpan(0, 0, 10);

        /// <summary>
        /// Sirve para determinar si es la primera vez que se invoca el chequeo de la red.
        /// </summary>
        bool FirstTimeCheck = true;

        #endregion

        #region CONSTRUCTOR

        public clsRed()
        {
            // Esteblecer las direcciones de acceso a los servicios para Producción o Pruebas.
            switch (RUV.I.ModoEjecucion)
            {
                case Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion.Desarrollo:
                    DireccionesValidas = new List<string> { "127.0.0.1" };
                    break;
                case Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion.Pruebas:
                    DireccionesValidas = new List<string> { "190.216.51.20", "oimuariv.dev.globant.com","localhost" };
                    break;
                case Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion.Produccion:
                    DireccionesValidas = new List<string> { "172.20.2.39", "192.168.250.36", "200.119.110.149" };
                    break;
                case Ruv.Infrastructure.Crosscutting.Common.eModoEjecucion.Capacitacion:
                    DireccionesValidas = new List<string> { "172.20.2.126", "192.168.250.126", "200.119.110.153" };
                    break;
            }


            if (RUV.I.Configuraciones.ConfiguracionGeneral.PreDeteccionRedDisponible)
                DeterminarPuertoDeAccesoAsync();

            if (!string.IsNullOrWhiteSpace(RUV.I.Configuraciones.ConfiguracionGeneral.UrlServidorPreferido))
            {
                // establecer manualmente la url de los endpoints
                RaizDireccion = RUV.I.Configuraciones.ConfiguracionGeneral.UrlServidorPreferido;

                using (var SerLogin = new LoginService.LoginServiceClient("ExtremoLogin"))
                {
                    DireccionServicioLogin =
                      CambiarUrlRaiz(SerLogin.Endpoint.ListenUri.AbsoluteUri, RUV.I.Configuraciones.ConfiguracionGeneral.UrlServidorPreferido);
                }

                using (var SerGen = new GeneralService.GeneralServiceClient("ExtremoGeneral"))
                {
                    DireccionServicioGeneral =
                      CambiarUrlRaiz(SerGen.Endpoint.ListenUri.AbsoluteUri, RUV.I.Configuraciones.ConfiguracionGeneral.UrlServidorPreferido);
                }
            }

            VerificarAccesoLoginService();
        }

        private void VerificarAccesoLoginService()
        {
            UrlVerificadora = ServicioLogin.Endpoint.ListenUri.AbsoluteUri;

            if (!string.IsNullOrWhiteSpace(RUV.I.Configuraciones.ConfiguracionGeneral.UrlServidorPreferido))
            {
                UrlVerificadora = CambiarUrlRaiz(UrlVerificadora, RUV.I.Configuraciones.ConfiguracionGeneral.UrlServidorPreferido);
            }

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(TimerChequeo_DoWork);

            TimerChequeo = new DispatcherTimer();
            TimerChequeo.Tick += TimerChequeo_Tick;
            TimerChequeo.Interval = IntervaloVerificacionChequeo;
            VerificarEstadoRed();
        }

        #endregion

        #region UTILS

        /// <summary>
        /// Retorna una nueva dirección con la raiz cambiada.
        /// </summary>
        /// <param name="urlCompletaOriginal"></param>
        /// <param name="nuevaRaiz"></param>
        /// <returns></returns>
        string CambiarUrlRaiz(string urlCompletaOriginal, string nuevaRaiz)
        {
            int Pos = urlCompletaOriginal.LastIndexOf('/');
            return
              string.Format("{0}{1}",
              nuevaRaiz,
              urlCompletaOriginal.Substring(Pos));
        }

        #endregion

        #region VERIFICACION DE ACCESO PRIMERA VEZ

        [System.Diagnostics.DebuggerDisplay("{Direccion} - {HayAcceso} - {RutaFinal}")]
        class clsDominio
        {
            public string Direccion { get; set; }
            public string RutaFinal { get; set; }
            public bool? HayAcceso { get; set; }
        }

        /// <summary>
        /// La lista de las direcciones de acceso en Pruebas:
        /// Nivel Nacional, Colvista, Internet
        /// </summary>
        List<string> DireccionesValidas = null;

        /// <summary>
        /// La dirección raiz a utilizar para conectarse en todos los servicios.
        /// </summary>
        string RaizDireccion = null;
        List<clsDominio> ListaDirecciones;

        List<Thread> Tareas_VerificarDominio;

        /// <summary>
        /// Trata de determinar el puerto de acceso.
        /// </summary>
        public void DeterminarPuertoDeAccesoAsync()
        {
            // Inicializar el servicio de Login.
            string DummyString = ServicioLogin.Endpoint.ListenUri.AbsoluteUri;

            ListaDirecciones = new List<clsDominio>();

            // Agregar la dirección por defecto en app.config
            clsDominio EsteEquipo = new clsDominio { Direccion = null, HayAcceso = null };
            ListaDirecciones.Add(EsteEquipo);

            for (int i = 0; i < DireccionesValidas.Count(); i++)
            {
                var Dominio = new clsDominio
                {
                    Direccion = DireccionesValidas[i],
                    HayAcceso = null
                };
                ListaDirecciones.Add(Dominio);
            }

            // Hacer todas la verificaciones al mismo tiempo.
            Tareas_VerificarDominio = new List<Thread>();
            foreach (var Dominio in ListaDirecciones)
            {
                Thread Tarea = new Thread(VerificarDominio_DoWork);
                Tareas_VerificarDominio.Add(Tarea);
                Tarea.Start(Dominio);
                //BackgroundWorker BW = new BackgroundWorker();
                //BW.DoWork += new DoWorkEventHandler(WorkerPuerto_DoWork);
                //BW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerPuerto_RunWorkerCompleted);
                //BW.RunWorkerAsync(Dominio);
            }

            // Esperar a que las verificaciones terminen.
            foreach (var UnaTarea in Tareas_VerificarDominio)
            {
                UnaTarea.Join();
            }

            VerificarDominio_AnalizarResultados();
        }

        void VerificarDominio_DoWork(object dato)
        {
            clsDominio Dominio = dato as clsDominio;
            string RutaFinal = null;
            Dominio.HayAcceso = DeterminarPuertoDeAcceso(Dominio.Direccion, out RutaFinal);
            Dominio.RutaFinal = RutaFinal;
        }

        /// <summary>
        /// Determinar el dominio a utilizar.
        /// </summary>
        void VerificarDominio_AnalizarResultados()
        {
            var Dominio = ListaDirecciones.FirstOrDefault(x => x.HayAcceso.Value);
            if (Dominio != null)
            {
                RaizDireccion = Dominio.RutaFinal.Substring(0,
                  Dominio.RutaFinal.LastIndexOf('/'));
            }
            else
            {
                RaizDireccion = ListaDirecciones.ElementAt(0).RutaFinal.Substring(0,
                  ListaDirecciones.ElementAt(0).RutaFinal.LastIndexOf('/'));
            }

            foreach (var item in ListaDirecciones.Where(x => x.HayAcceso.Value))
                RUV.I.Log.Registrar("Se encontró acceso a la dirección: {0}", item.Direccion);

            RUV.I.Log.Registrar("Se seleccionó acceso a la dirección: {0}", RaizDireccion);

            DireccionServicioLogin = string.Format("{0}/{1}", RaizDireccion, "LoginService.svc");
            DireccionServicioGeneral = string.Format("{0}/{1}", RaizDireccion, "GeneralService.svc");

            Tareas_VerificarDominio = null;
        }

        ///// <summary>
        ///// Una vez revisados todos los puertos, determinar la dirección disponible.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void WorkerPuerto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //  if (ListaDirecciones.Any(x => !x.HayAcceso.HasValue)) return;

        //  var Dominio = ListaDirecciones.FirstOrDefault(x => x.HayAcceso.Value);
        //  if (Dominio != null)
        //  {
        //    RaizDireccion = Dominio.RutaFinal.Substring(0,
        //      Dominio.RutaFinal.LastIndexOf('/'));
        //  }
        //  else
        //  {
        //    RaizDireccion = ListaDirecciones.ElementAt(0).RutaFinal.Substring(0,
        //      ListaDirecciones.ElementAt(0).RutaFinal.LastIndexOf('/'));
        //  }

        //  foreach (var item in ListaDirecciones.Where(x => x.HayAcceso.Value))
        //  {
        //    Sipod.I.Log.Registrar("Se encontró acceso a la dirección: {0}", item.Direccion);
        //  }

        //  Sipod.I.Log.Registrar("Se seleccionó acceso a la dirección: {0}", RaizDireccion);

        //  DireccionServicioLogin = string.Format("{0}/{1}", RaizDireccion, "LoginService.svc");
        //  DireccionServicioGeneral = string.Format("{0}/{1}", RaizDireccion, "GeneralService.svc");
        //}

        //void WorkerPuerto_DoWork(object sender, DoWorkEventArgs e)
        //{
        //  clsDominio Dominio = e.Argument as clsDominio;
        //  string RutaFinal = null;
        //  Dominio.HayAcceso = DeterminarPuertoDeAcceso(Dominio.Direccion, out RutaFinal);
        //  Dominio.RutaFinal = RutaFinal;
        //}

        /// <summary>
        /// Verificación inicial de acceso a una de las sub-redes.
        /// </summary>
        /// <param name="raizUrl"></param>
        /// <returns></returns>
        public bool DeterminarPuertoDeAcceso(string raizUrl, out string rutaFinal)
        {
            RUV.I.Log.Registrar("Verificando: {0}", raizUrl);

            string UrlRuta = ServicioLogin.Endpoint.ListenUri.AbsoluteUri;
            int Pos1 = UrlRuta.IndexOf("//");
            int Pos2 = UrlRuta.IndexOf('/', 10);
            bool Resultado = false;
            rutaFinal = string.Format("{0}{1}{2}",
              UrlRuta.Substring(0, Pos1 + 2),
              raizUrl,
              UrlRuta.Substring(Pos2));

            // Si la dirección es nula, se asume la configuración del app.config
            if (string.IsNullOrWhiteSpace(raizUrl))
                rutaFinal = ServicioLogin.Endpoint.ListenUri.AbsoluteUri;

            try
            {
                // try accessing the web service directly via it's URL
                HttpWebRequest request = WebRequest.Create(rutaFinal) as HttpWebRequest;
                request.Timeout = VerificarRutaEstadoTimeout;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // DISPONIBLE.
                        Resultado = true;
                    }
                }
            }
            catch { }

            return Resultado;
        }

        #endregion

        #region VERIFICACIÓN DEL ESTADO

        /// <summary>
        /// Timer que cuenta el tiempo de verificación de acceso a la red.
        /// </summary>
        DispatcherTimer TimerChequeo;
        BackgroundWorker worker;

        string UrlVerificadora;

        /// <summary>
        /// Verifica el estado de la red de forma inmediata.
        /// </summary>
        public void VerificarEstadoRed()
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        /// <summary>
        /// Verificación periódica del acceso a la red.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimerChequeo_Tick(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        void TimerChequeo_DoWork(object sender, DoWorkEventArgs e)
        {
            VerificarAccesoUrl(UrlVerificadora);
        }

        /// <summary>
        /// Verifica si hay acceso a la url proporcionada.
        /// </summary>
        /// <param name="url"></param>
        public void VerificarAccesoUrl(string url)
        {
            try
            {
                Dispatcher.BeginInvoke(
                 System.Windows.Threading.DispatcherPriority.Normal,
                 new Action(delegate() { EstadoRed = eEstadoRed.EnProcesoDeVerificacion; }));

                // try accessing the web service directly via it's URL
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = VerificarEstadoTimeout;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                        Dispatcher.BeginInvoke(
                           System.Windows.Threading.DispatcherPriority.Normal,
                           new Action(delegate() { EstadoRed = eEstadoRed.Disponible; }));
                    else
                        Dispatcher.BeginInvoke(
                           System.Windows.Threading.DispatcherPriority.Normal,
                           new Action(delegate() { EstadoRed = eEstadoRed.NoDisponible; }));
                }
            }
            catch (WebException ex)
            {
                RUV.I.Log.Registrar("Prueba de conexión al servicio no satisfactoria: {0}", ex.Message);
                Dispatcher.BeginInvoke(
                   System.Windows.Threading.DispatcherPriority.Normal,
                   new Action(delegate() { EstadoRed = eEstadoRed.NoDisponible; }));
            }
            catch (Exception ex)
            {
                RUV.I.Log.Registrar("Prueba de conexión al servicio no satisfactoria: {0}", ex.Message);
                Dispatcher.BeginInvoke(
                   System.Windows.Threading.DispatcherPriority.Normal,
                   new Action(delegate() { EstadoRed = eEstadoRed.NoDisponible; }));
            }

        }

        #endregion

        #region ESTADO DE LA RED

        /// <summary>
        /// El estado actual de la red.
        /// </summary>
        public eEstadoRed EstadoRed
        {
            get
            {
                eEstadoRed Output = eEstadoRed.EnProcesoDeVerificacion;
                this.Dispatcher.Invoke(
                  new Action(() => Output = (eEstadoRed)GetValue(EstadoRedProperty)),
                  DispatcherPriority.Normal, null);
                return Output;
            }
            set
            {
                this.Dispatcher.Invoke(
                  new Action(() => SetValue(EstadoRedProperty, value)),
                  DispatcherPriority.Normal, null);
            }

        }

        /// <summary>
        /// El último estado de la red.
        /// </summary>
        eEstadoRed EstadoRedAnterior = eEstadoRed.NoDisponible;

        public static readonly DependencyProperty EstadoRedProperty =
            DependencyProperty.Register("EstadoRed", typeof(eEstadoRed),
            typeof(clsRed), new UIPropertyMetadata(eEstadoRed.NoDisponible, EstadoRedChanged));

        /// <summary>
        /// Cuando se presente un cambio de estado a Disponible o NoDisponibles se lanza un evento.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void EstadoRedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            eEstadoRed NuevoEstado = (eEstadoRed)e.NewValue;
            if (NuevoEstado == eEstadoRed.EnProcesoDeVerificacion) return;

            clsRed Red = d as clsRed;
            if (
              Red.FirstTimeCheck ||
              (NuevoEstado != Red.EstadoRedAnterior))
            {
                if (NuevoEstado == eEstadoRed.NoDisponible)
                {
                    RUV.I.Log.Registrar("Se ha perdido la conexión al servicio.");
                    if (Red.TimerChequeo.IsEnabled == false)
                        Red.TimerChequeo.Start();
                }
                else
                {
                    RUV.I.Log.Registrar("Se restableció la conexión al servicio.");
                    Red.TimerChequeo.Stop();
                }
                Red.EstadoRedAnterior = NuevoEstado;
                if (Red.EstadoRedCambio != null)
                    Red.EstadoRedCambio(NuevoEstado);
            }
            Red.FirstTimeCheck = false;
        }

        /// <summary>
        /// Sucede cuando cambia el estado de la red de Disponible a NoDisponible o viceversa.
        /// </summary>
        public event EstadoRedChangedDelegate EstadoRedCambio;

        #endregion

        #region SERVICIOS WCF

        private string DireccionServicioLogin;

        private LoginService.LoginServiceClient _ServicioLogin;
        /// <summary>
        /// WCF Login y Autenticación.
        /// </summary>
        public LoginService.LoginServiceClient ServicioLogin
        {
            get
            {
                if (DireccionServicioLogin == null)
                    _ServicioLogin =
                      new LoginService.LoginServiceClient("ExtremoLogin");
                else
                    _ServicioLogin =
                      new LoginService.LoginServiceClient("ExtremoLogin", DireccionServicioLogin);
                return _ServicioLogin;
            }
        }

        private string DireccionServicioGestionDocumentos;

        private ControlDocumentosService.ControlDocumentosServiceClient _ServicioGestionDocumentos;

        public ControlDocumentosService.ControlDocumentosServiceClient ServicioGestionDocumentos
        {
            get
            {
                if (DireccionServicioGestionDocumentos == null)
                    _ServicioGestionDocumentos =
                      new ControlDocumentosService.ControlDocumentosServiceClient("ExtremoControlDocumentos");
                else
                    _ServicioGestionDocumentos =
                      new ControlDocumentosService.ControlDocumentosServiceClient("ExtremoControlDocumentos", DireccionServicioGestionDocumentos);
                return _ServicioGestionDocumentos;
            }
        }

        private string DireccionServicioPdfHelper;

        private PdfHelperServiceReference.PdfHelperServiceClient _ServicioPdfHelper;

        public PdfHelperServiceReference.PdfHelperServiceClient ServicioPdfHelper
        {
            get
            {
                if (DireccionServicioPdfHelper == null)
                    _ServicioPdfHelper =
                      new PdfHelperServiceReference.PdfHelperServiceClient("ExtremoPdfHelper");
                else
                    _ServicioPdfHelper =
                      new PdfHelperServiceReference.PdfHelperServiceClient("ExtremoPdfHelper", DireccionServicioPdfHelper);
                return _ServicioPdfHelper;
            }
        }

        private string DireccionServicioGeneral;

        private GeneralService.GeneralServiceClient _ServicioGeneral;
        /// <summary>
        /// Acceso al servicio General.
        /// </summary>
        public GeneralService.GeneralServiceClient ServicioGeneral
        {
            get
            {
                if (_ServicioGeneral == null)
                {
                    if (DireccionServicioGeneral != null)
                    {
                        _ServicioGeneral = new GeneralService.GeneralServiceClient("ExtremoGeneral", DireccionServicioGeneral);
                        if (RUV.I.Usuario != null)
                        {
                            _ServicioGeneral.ClientCredentials.UserName.UserName = RUV.I.Usuario.Cuenta;
                            _ServicioGeneral.ClientCredentials.UserName.Password = RUV.I.Usuario.Contraseña;
                        }
                    }
                    else
                    {
                        _ServicioGeneral = new GeneralService.GeneralServiceClient("ExtremoGeneral");
                        if (RUV.I.Usuario != null)
                        {
                            _ServicioGeneral.ClientCredentials.UserName.UserName = RUV.I.Usuario.Cuenta;
                            _ServicioGeneral.ClientCredentials.UserName.Password = RUV.I.Usuario.Contraseña;
                        }
                    }
                }
                return _ServicioGeneral;
            }
        }

        private string DireccionServicioCriticaN;

        private CriticaNServiceReference.CriticaNServiceClient _ServicioCriticaN;

        public CriticaNServiceReference.CriticaNServiceClient ServicioCriticaN
        {
            get
            {
                if (DireccionServicioCriticaN == null)
                    _ServicioCriticaN =
                      new CriticaNServiceReference.CriticaNServiceClient("ExtremoCriticaN");
                else
                    _ServicioCriticaN =
                      new CriticaNServiceReference.CriticaNServiceClient("ExtremoCriticaN", DireccionServicioCriticaN);
                return _ServicioCriticaN;
            }
            set { _ServicioCriticaN = value; }
        }

        private string DireccionServicioRadicacion;

        private RadicacionServiceReference.RadicacionServiceClient _ServicioRadicacion;

        public RadicacionServiceReference.RadicacionServiceClient ServicioRadicacion
        {
            get
            {
                if (DireccionServicioRadicacion == null)
                    _ServicioRadicacion = new RadicacionServiceReference.RadicacionServiceClient("ExtremoRadicacion");
                else
                    _ServicioRadicacion = new RadicacionServiceReference.RadicacionServiceClient("ExtremoRadicacion", DireccionServicioRadicacion);
                return _ServicioRadicacion;
            }
            set { _ServicioRadicacion = value; }
        }

        private string DireccionServicioDevolucion;

        private DevolucionServiceReference.DevolucionServiceClient _ServicioDevolucion;

        public DevolucionServiceReference.DevolucionServiceClient ServicioDevolucion
        {
            get
            {
                if (DireccionServicioDevolucion == null)
                    _ServicioDevolucion = new DevolucionServiceReference.DevolucionServiceClient("ExtremoDevolucion");
                else
                    _ServicioDevolucion = new DevolucionServiceReference.DevolucionServiceClient("ExtremoDevolucion", DireccionServicioDevolucion);
                return _ServicioDevolucion;
            }
            set { _ServicioDevolucion = value; }
        }

        #endregion

        #region INFORMACIÓN DE LA INTERFASE DE RED

        /// <summary>
        /// Retorna información sobre la interfase de red del usuario.
        /// </summary>
        /// <returns></returns>
        public Ruv.Infrastructure.Crosscutting.Common.clsInterfaseRed ObtenerInformacionInterfaseRed()
        {
            return new Ruv.Infrastructure.Crosscutting.Common.clsInterfaseRed();

            //var Resultado = (from OneAdapter in
            //                  (from x in NetworkInterface.GetAllNetworkInterfaces()
            //                   where x.OperationalStatus == OperationalStatus.Up
            //                   select new Ruv.Infrastructure.Crosscutting.Common.clsInterfaseRed
            //                   {
            //                     Mac = x.GetPhysicalAddress().ToString(),
            //                     Dns = x.GetIPProperties().DnsSuffix,
            //                     Ips = x.GetIPProperties().UnicastAddresses
            //                       .Where(z => z.Address.ToString().Count(w => w == '.') == 3)
            //                       .Select(y => y.Address.ToString())
            //                       .FirstOrDefault(),
            //                     PcName = System.Environment.MachineName
            //                   })
            //                where OneAdapter.Ips.Split('.').All(x => x != "0")
            //                select OneAdapter).FirstOrDefault();

            //return Resultado;
        }

        #endregion
    }
}
