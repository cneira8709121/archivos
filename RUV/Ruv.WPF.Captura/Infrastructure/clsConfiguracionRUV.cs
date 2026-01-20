using System;

namespace Ruv.WPF.Captura.Infrastructure
{
    public class clsConfiguracionRUV
    {

        #region CONSTRUCTOR

        public clsConfiguracionRUV()
        {
            Id = 1;
            PreCargarConfiguracion();
        }

        /// <summary>
        /// Realizar el cargeu del archivo de configuración.
        /// El archivo de configuración es OPCIONAL y siempre se pude
        /// </summary>
        void PreCargarConfiguracion()
        {
            // Tratar de pre-cargar alguna configuración.
            string ArchivoConfiguracion =
              System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "RUVConfig.txt");

            if (!System.IO.File.Exists(ArchivoConfiguracion)) return;

            using (System.IO.StreamReader file = new System.IO.StreamReader(ArchivoConfiguracion))
            {
                string Linea;
                string Propiedad;
                string Valor;
                //bool ValorBol;

                while ((Linea = file.ReadLine()) != null && !string.IsNullOrWhiteSpace(Linea))
                    if (!Linea.Trim().StartsWith("//"))
                    {
                        int Pos = Linea.IndexOf("=");
                        Propiedad = Linea.Substring(0, Pos).ToLower();
                        Valor = Linea.Substring(Pos + 1);

                        switch (Propiedad)
                        {
                            case "predeteccionreddisponible":
                                PreDeteccionRedDisponible =
                                  ObtenerValorBooleano(Valor, PreDeteccionRedDisponible);
                                break;

                            case "usuariocuentaprecargada":
                                UsuarioCuentaPreCargada = Valor;
                                break;

                            case "usuarioclaveprecargarda":
                                UsuarioClavePreCargarda = Valor;
                                break;

                            case "urlservidorpreferido":
                                UrlServidorPreferido = Valor;
                                break;

                            case "omitirvalidacionesalenviar":
                                OmitirValidacionesAlEnviar =
                                  ObtenerValorBooleano(Valor, OmitirValidacionesAlEnviar);
                                break;

                            case "permitirpurgarcolaprocesos":
                                PermitirPurgarColaProcesos =
                                  ObtenerValorBooleano(Valor, PermitirPurgarColaProcesos);
                                break;

                            case "preservarborradordespuesdeenvio":
                                PreservarBorradorDespuesDeEnvio =
                                  ObtenerValorBooleano(Valor, PreservarBorradorDespuesDeEnvio);
                                break;

                            case "transmitirsinusarcoladeprocesos":
                                TransmitirSinUsarColaDeprocesos =
                                  ObtenerValorBooleano(Valor, TransmitirSinUsarColaDeprocesos);
                                break;

                            case "omitirvalidacionfirmadigital":
                                OmitirValidacionFirmaDigital =
                                  ObtenerValorBooleano(Valor, OmitirValidacionFirmaDigital);
                                break;
                        }
                    }
            }
        }

        /// <summary>
        /// Trata de convertir un string en booleano.
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="valorDefecto"></param>
        /// <returns></returns>
        bool ObtenerValorBooleano(string valor, bool valorDefecto)
        {
            bool ValorBol = valorDefecto;
            bool.TryParse(valor, out ValorBol);
            return ValorBol;
        }

        #endregion

        /// <summary>
        /// Persiste los cambios en esta clase.
        /// </summary>
        public void Grabar()
        {
            // Almacenar los cambios en la configuración.
            RUV.I.LocalDB.Save<clsConfiguracionRUV>(this);
            RUV.I.LocalDB.Flush();
        }

        public int Id { get; set; }

        string _RutaArchivoEscaneados;
        /// <summary>
        /// La ruta donde se almacenan los archivo escaneados.
        /// </summary>
        public string RutaArchivosEscaneados
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_RutaArchivoEscaneados))
                {
                    // Establecer la ruta de la carpeta de imágenes.
                    _RutaArchivoEscaneados = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                }
                return _RutaArchivoEscaneados;
            }
            set
            {
                if (_RutaArchivoEscaneados != value)
                {
                    _RutaArchivoEscaneados = value;
                }
            }
        }

        #region OPCIONES DE CONFIGURACIÓN PARA DEPURACIÓN

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        private bool _PreDeteccionRedDisponible = true;
        /// <summary>
        /// True: Se lanza la verificación para la red disponible.
        /// </summary>
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public bool PreDeteccionRedDisponible
        {
            get { return _PreDeteccionRedDisponible; }
            set { _PreDeteccionRedDisponible = value; }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        String _UsuarioCuentaPreCargada = null;
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public String UsuarioCuentaPreCargada
        {
            get { return _UsuarioCuentaPreCargada; }
            set { _UsuarioCuentaPreCargada = value; }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        String _UsuarioClavePreCargarda = null;
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public String UsuarioClavePreCargarda
        {
            get { return _UsuarioClavePreCargarda; }
            set { _UsuarioClavePreCargarda = value; }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        String _UrlServidorPreferido = null;
        /// <summary>
        /// Ruta de acceso preferida para comunicarse a los servicios WCF.
        /// Si se establece este dato, esta será la ruta de comunicación siempre.
        /// </summary>
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public String UrlServidorPreferido
        {
            get { return _UrlServidorPreferido; }
            set { _UrlServidorPreferido = value; }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        bool _OmitirValidacionesAlEnviar = false;
        /// <summary>
        /// True: Antes de enviar la declaración al servidor no verifica que se pasen todas la validaciones
        /// </summary>
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public bool OmitirValidacionesAlEnviar
        {
            get { return _OmitirValidacionesAlEnviar; }
            set { _OmitirValidacionesAlEnviar = value; }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        private bool _PermitirPurgarColaProcesos = false;
        /// <summary>
        /// Verdadero: Presenta un botónn en la interfase que permite borrar todos los procesos
        /// de la cola de transmisión. Default: False.
        /// </summary>
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public bool PermitirPurgarColaProcesos
        {
            get { return _PermitirPurgarColaProcesos; }
            set
            {
                _PermitirPurgarColaProcesos = value;
            }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        private bool _PreservarBorradorDespuesDeEnvio = false;
        /// <summary>
        /// True: Cuando se carga un borrador y se envía se debe conservar y no ser borrado.
        /// Default: False.
        /// </summary>
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public bool PreservarBorradorDespuesDeEnvio
        {
            get { return _PreservarBorradorDespuesDeEnvio; }
            set { _PreservarBorradorDespuesDeEnvio = value; }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        private bool _TransmitirSinUsarColaDeprocesos = false;
        /// <summary>
        /// True: Tansmitir la declaración inmediatamente sin agregarla a la cola de procesos.
        /// Default: False.
        /// </summary>
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public bool TransmitirSinUsarColaDeprocesos
        {
            get { return _TransmitirSinUsarColaDeprocesos; }
            set { _TransmitirSinUsarColaDeprocesos = value; }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        private bool _SiempreEncriptarContraseña = false;
        /// <summary>
        /// True: Cuando se consulte la constraseña del usuario se retorna encriptada.
        /// Default: False.
        /// </summary>
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public bool SiempreEncriptarContraseña
        {
            get { return _SiempreEncriptarContraseña; }
            set { _SiempreEncriptarContraseña = value; }
        }

        [Wintellect.Sterling.Serialization.SterlingIgnore]
        private bool _OmitirValidacionFirmaDigital = false;
        /// <summary>
        /// True: Se omite la validación de la firma digital aunque sea obligatoria.
        /// Default: False.
        /// </summary>
        [Wintellect.Sterling.Serialization.SterlingIgnore]
        public bool OmitirValidacionFirmaDigital
        {
            get { return _OmitirValidacionFirmaDigital; }
            set { _OmitirValidacionFirmaDigital = value; }
        }

        #endregion

    }
}