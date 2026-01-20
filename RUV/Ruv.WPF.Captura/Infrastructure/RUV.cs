using System;
using System.Linq;
using System.Windows.Forms;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.WPF.Captura.Infrastructure;
using Ruv.WPF.Captura.Infrastructure.ColaProcesos;
using Ruv.WPF.Captura.Infrastructure.Configuracion;
using Ruv.WPF.Captura.Infrastructure.LocalStorage;
using Wintellect.Sterling;

namespace Ruv.WPF.Captura
{
    /// <summary>
    /// Objeto principal que maneja todas las operaciones requeridas.
    /// </summary>
    public class RUV
    {
        #region CONSTRUCTOR SINGLETON

        private static volatile RUV instance;
        private static object syncRoot = new Object();

        private RUV() { }

        /// <summary>
        /// La instancia única de la aplicación.
        /// </summary>
        public static RUV I
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new RUV();
                        //var Tmp = instance.Configuracion;
                        //if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                        //{
                        //  instance.CargarInformacionGeneral();
                        //}
                    }
                }
                return instance;
            }
        }

        #endregion

        private clsDeclaracion _DeclaracionActual;
        /// <summary>
        /// La declaración que se está editando.
        /// </summary>
        public clsDeclaracion DeclaracionActual
        {
            get { return _DeclaracionActual; }
            set
            {
                _DeclaracionActual = value;
                if (value != null)
                    clsDeclaracion.DeclaracionActual = value;
            }
        }

        private clsLog _Log;
        /// <summary>
        /// Registro de mensajes de estado de la aplicación.
        /// </summary>
        public clsLog Log
        {
            get
            {
                if (_Log == null) _Log = new clsLog();
                return _Log;
            }
        }

        private clsRed _Red;
        /// <summary>
        /// Administración de servicios de red.
        /// </summary>
        public clsRed Red
        {
            get
            {
                if (_Red == null) _Red = new clsRed();
                return _Red;
            }
        }

        private clsSeguridad _Seguridad;
        /// <summary>
        /// Administración de servicios de seguridad.
        /// </summary>
        public clsSeguridad Seguridad
        {
            get
            {
                if (_Seguridad == null) _Seguridad = new clsSeguridad();
                return _Seguridad;
            }
        }

        private clsUsuario _Usuario;
        /// <summary>
        /// El usuario actualmente logueado.
        /// </summary>
        public clsUsuario Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        /// <summary>
        /// Acceso a la base de datos local (en el cliente).
        /// </summary>
        public ISterlingDatabaseInstance LocalDB
        {
            get { return DatabaseService.Current; }
        }

        private MainWindow _UIPrincipal;
        /// <summary>
        /// Acceso al MainWindow.
        /// </summary>
        public MainWindow UIPrincipal
        {
            get { return _UIPrincipal; }
            set { _UIPrincipal = value; }
        }

        private clsInfoGeneral _InfoGeneral;
        /// <summary>
        /// Administra información de caracter general.
        /// </summary>
        public clsInfoGeneral InfoGeneral
        {
            get
            {
                if (_InfoGeneral == null) _InfoGeneral = new clsInfoGeneral();
                return _InfoGeneral;
            }
        }

        private clsMultiTarea _MultiTarea;
        /// <summary>
        /// Facilita la ejecución de procesos en el background.
        /// </summary>
        public clsMultiTarea MultiTarea
        {
            get
            {
                if (_MultiTarea == null) _MultiTarea = new clsMultiTarea();
                return _MultiTarea;
            }
        }

        private Ruv.WPF.Captura.Infrastructure.clsUtil _Util;
        /// <summary>
        /// Rutinas utilitarias varias.
        /// </summary>
        public Ruv.WPF.Captura.Infrastructure.clsUtil Util
        {
            get
            {
                if (_Util == null) _Util = new Ruv.WPF.Captura.Infrastructure.clsUtil();
                return _Util;
            }
        }

        clsValidadorEntidades _ValidadorEntidades;
        /// <summary>
        /// Provee algunas operacione para validar una entidad.
        /// </summary>
        public clsValidadorEntidades ValidadorEntidades
        {
            get
            {
                if (_ValidadorEntidades == null)
                    _ValidadorEntidades = new clsValidadorEntidades();
                return _ValidadorEntidades;
            }
        }


        private clsConfiguracion _configuraciones;
        /// <summary>
        /// Configuraciones Generales de la aplicacion
        /// </summary>
        public clsConfiguracion Configuraciones
        {
            get
            {
                if (_configuraciones == null)
                {
                    clsConfiguracion Config = null;

                    try
                    {
                        Config = RUV.I.LocalDB.Query<clsConfiguracion, int>()
                          .Select(x => x.LazyValue.Value).FirstOrDefault();
                    }
                    catch { }

                    if (Config == null)
                        _configuraciones = new clsConfiguracion();
                    else
                        _configuraciones = Config;
                }
                return _configuraciones;
            }
            set { _configuraciones = value; }
        }


        //private clsImpresion _Impresion;
        ///// <summary>
        ///// Toda la funcionalidad de impresión.
        ///// </summary>
        //public clsImpresion Impresion
        //{
        //    get
        //    {
        //        if (_Impresion == null) _Impresion = new clsImpresion();
        //        return _Impresion;
        //    }
        //}

        //private clsConfiguracionRUV _Configuracion;
        ///// <summary>
        ///// Configuraciones varias de la aplicación.
        ///// </summary>
        //public clsConfiguracionRUV Configuracion
        //{
        //  get
        //  {
        //    if (_Configuracion == null)
        //    {
        //      clsConfiguracionRUV Config = null;

        //      try
        //      {
        //        Config = RUV.I.LocalDB.Query<clsConfiguracionRUV, int>()
        //          .Select(x => x.LazyValue.Value).FirstOrDefault();
        //      }
        //      catch { }

        //      if (Config == null)
        //        _Configuracion = new clsConfiguracionRUV();
        //      else
        //        _Configuracion = Config;
        //    }
        //    return _Configuracion;
        //  }
        //}

        private clsColaProcesos _ColaProcesos;
        /// <summary>
        /// La cola de proceso de transmisión.
        /// </summary>
        public clsColaProcesos ColaProcesos
        {
            get
            {
                if (_ColaProcesos == null)
                {
                    _ColaProcesos = new clsColaProcesos();
                }
                return _ColaProcesos;
            }
        }

        private eModoEjecucion _ModoEjecucion = eModoEjecucion.Desarrollo;
        /// <summary>
        /// Modo de ejecución de la aplicación.
        /// </summary>
        public eModoEjecucion ModoEjecucion
        {
            get { return _ModoEjecucion; }
            set { _ModoEjecucion = value; }
        }


        private NotifyIcon notificaciones;

        public NotifyIcon Notificaciones
        {
            get { return notificaciones; }
            set
            {
                notificaciones = value;

            }
        }

        //se usa para almacenar el id de la Valoracion cuando se abre la captura en modo de edicion desde valoracion
        public int IdValoracion { get; set; }
        //se usa para almacenar el id de la Declaracion cuando se abre la captura en modo de edicion desde valoracion                
        public int IdDeclaracion { get; set; }
        //se usa para almacenar la url del sitio web de ruv
        public string Url { get; set; }
    }
}
