using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Ruv.WPF.Captura.Infrastructure
{
    public class clsSeguridad : clsBase
    {

        #region PROPIEDADES

        private Boolean _Autenticado;

        /// <summary>
        /// Verdadero: El usuario se encuentra autenticado.
        /// </summary>
        public Boolean Autenticado
        {
            get { return _Autenticado; }
            set
            {
                _Autenticado = value;
                CambioEnPropiedad("Autenticado");
            }
        }

        #endregion

        private clsCryptoUtil _Crypto;
        /// <summary>
        /// Utilidades de criptografía.
        /// </summary>
        public clsCryptoUtil Crypto
        {
            get
            {
                if (_Crypto == null) _Crypto = new clsCryptoUtil();
                return _Crypto;
            }
            set
            {
                _Crypto = value;
            }
        }

        /// <summary>
        /// La llave que identifica al usuario contra el servicio.
        /// </summary>
        public string LlaveUsuario
        {
            get
            {
                return Crypto.EncryptStringFixed(string.Format(
                  "{0}\t{1}",
                  RUV.I.Usuario.Cuenta,
                  Crypto.DecryptStringFixed(RUV.I.Usuario.Contraseña)));
            }
        }

        /// <summary>
        /// Valida las credenciales del usuario desde el formulario de login.
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Contraseña"></param>
        public void Autenticar(string Usuario, string Contraseña, string archivoCertificado)
        {
            while (RUV.I.Red.EstadoRed == eEstadoRed.EnProcesoDeVerificacion)
            {
                // Esperar 1.5 segundos mientras se verifica el estado de la red.
                System.Threading.Thread.Sleep(1500);
            }
            string version = string.Empty;
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }

            switch (RUV.I.Red.EstadoRed)
            {
                case eEstadoRed.Disponible:
                    if (!Autenticar_Online(Usuario, Contraseña, archivoCertificado, version))
                        Autenticar_Offline(Usuario, Contraseña, archivoCertificado);
                    break;

                case eEstadoRed.NoDisponible:
                    Autenticar_Offline(Usuario, Contraseña, archivoCertificado);
                    break;
            }
        }

        /// <summary>
        /// Validar las credenciales fuera de línea.
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Contraseña"></param>
        private void Autenticar_Offline(string Usuario, string Contraseña, string archivoCertificado)
        {
            Ruv.Infrastructure.Crosscutting.Common.clsUsuario Usu = null;
            Autenticado = false;

            try
            {
                var Busqueda = RUV.I.LocalDB.Query<Ruv.Infrastructure.Crosscutting.Common.clsUsuario, string>()
                  .Where(x => x.Key != null)
                  .FirstOrDefault(x => x.LazyValue.Value.Cuenta == Usuario);

                if (Busqueda != null) Usu = Busqueda.LazyValue.Value;
            }
            catch { }

            TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Ninguno;

            if (Usu == null)
            {
                TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion;
                MensajeEstado = "Usuario y/o clave desconocidos";
                return;
            }

            if (!Usu.Activo)
            {
                TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion;
                MensajeEstado = "Usuario no activo";
                return;
            }

            if (Usu.Bloqueado)
            {
                TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion;
                MensajeEstado = "Usuario bloqueado";
                return;
            }

            if (Usu.Contraseña != Crypto.EncryptStringFixed(Contraseña))
            {
                TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion;
                MensajeEstado = "Usuario/clave desconocidos";
                if ((++Usu.IntentosErrados) >= 3)
                    Usu.Bloqueado = true;
                RUV.I.LocalDB.Save<Ruv.Infrastructure.Crosscutting.Common.clsUsuario>(Usu);
                RUV.I.LocalDB.Flush();
                return;
            }

            if (!Usu.Permisos.Any())
            {
                TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion;
                MensajeEstado = "No tiene permisos para utilizar esta aplicación";
                return;
            }

            // Verificar la firma digital.
            var ResultadoFirma = ValidarFirmaDigital(archivoCertificado, Usu);
            if (!string.IsNullOrWhiteSpace(ResultadoFirma))
            {
                MensajeEstado = ResultadoFirma;
                TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion;
                return;
            }

            // Usuario correctamente autenticado, dejarle seguir.
            Autenticado = true;
            RUV.I.Usuario = Usu;
            RUV.I.Usuario.Activo = true;
            RUV.I.Usuario.Bloqueado = false;
            RUV.I.Usuario.IntentosErrados = 0;
            RUV.I.LocalDB.Save<Ruv.Infrastructure.Crosscutting.Common.clsUsuario>(RUV.I.Usuario);
            RUV.I.LocalDB.Flush();
        }

        /// <summary>
        /// Validar las credenciales en línea.
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Contraseña"></param>
        private Boolean Autenticar_Online(string Usuario, string Contraseña, string archivoCertificado, string version)
        {
            try
            {
                var Cliente = RUV.I.Red.ServicioLogin;
                RUV.I.Usuario = null;
                RUV.I.Usuario = Cliente.Authenticate(Usuario, Contraseña,
                  RUV.I.Red.ObtenerInformacionInterfaseRed(),
                  version);

                if (!string.IsNullOrWhiteSpace(RUV.I.Usuario.MensajeAutenticacionFallida))
                {
                    MensajeEstado = RUV.I.Usuario.MensajeAutenticacionFallida;
                    TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion;
                    OnlineOperation = true;
                    return OnlineOperation;
                }

                // Verificar la firma digital.
                var ResultadoFirma = ValidarFirmaDigital(archivoCertificado, RUV.I.Usuario);
                if (!string.IsNullOrWhiteSpace(ResultadoFirma))
                {
                    MensajeEstado = ResultadoFirma;
                    TipoDeError = Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion;
                    OnlineOperation = true;
                    return OnlineOperation;
                }

                // Autenticado correctamente, grabar localmente las credenciales del usuario.
                Autenticado = true;
                RUV.I.Usuario.Activo = true;
                RUV.I.Usuario.Bloqueado = false;
                RUV.I.Usuario.IntentosErrados = 0;
                RUV.I.Usuario.Contraseña = Crypto.EncryptStringFixed(Contraseña);
                RUV.I.Usuario.NumeroDocumento = Crypto.EncryptStringFixed(RUV.I.Usuario.NumeroDocumento);
                RUV.I.LocalDB.Save<Ruv.Infrastructure.Crosscutting.Common.clsUsuario>(RUV.I.Usuario);
                RUV.I.LocalDB.Flush();

                OnlineOperation = true;
            }
            catch (System.ServiceModel.Security.MessageSecurityException se)
            {
                // TODO: este código es obsoleto.
                var FE = se.InnerException as System.ServiceModel.FaultException;
                var FC = FE.Code;

                Ruv.Infrastructure.Crosscutting.Common.eErrores TDE = Ruv.Infrastructure.Crosscutting.Common.eErrores.Ninguno;
                Enum.TryParse<Ruv.Infrastructure.Crosscutting.Common.eErrores>(FC.Name, out TDE);
                TipoDeError = TDE;

                if (TipoDeError == Ruv.Infrastructure.Crosscutting.Common.eErrores.NoDeterminado)
                    throw new Exception("Error no determinado");

                if (TipoDeError == Ruv.Infrastructure.Crosscutting.Common.eErrores.Autenticacion)
                    MensajeEstado = FE.Message;

                OnlineOperation = true;
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                // Se asumió que había conexión, pero en medio del camino esta se perdió.
                RUV.I.Log.Registrar("Pérdida de conexión mientras se autenticaba." + ex.Message);
                OnlineOperation = false;
            }
            catch (Exception ex)
            {
                RUV.I.Log.Registrar("Autenticar", ex);
                throw ex;
            }

            return OnlineOperation;
        }

        /// <summary>
        /// Retorna un mensaje de errir si el usuario no pasa la validación de la firma digital, o nulo si la pasa.
        /// Retorna nulo si el usuario no requiere validación de la firma digital.
        /// </summary>
        /// <param name="archivoCertificado"></param>
        /// <returns></returns>
        string ValidarFirmaDigital(string archivoCertificado, Ruv.Infrastructure.Crosscutting.Common.clsUsuario usuario)
        {
            // Si no está obligado a utilizarla, no hacer verificación.
            if (!usuario.UtilizaCertificadoDigital) return null;
            var CedulaCertificado = ObtenerCedulaDeCertificado(archivoCertificado);
            if (CedulaCertificado == null
              || (CedulaCertificado != usuario.NumeroDocumento
              && Crypto.EncryptStringFixed(CedulaCertificado) != usuario.NumeroDocumento))
                return "No se superó la identificación del token";
            else
                return null;
        }

        /// <summary>
        /// Trata de cerrar la sesión en el servidor.
        /// Si no hay comunicación, igual la sesión se cierra localmente.
        /// Esta operación es asíncrona.
        /// </summary>
        public void CerrarSesionAsync()
        {
            BackgroundWorker BW = new BackgroundWorker();
            BW.DoWork += CerrarSesionAsync_DoWork;
            BW.RunWorkerAsync();
        }

        /// <summary>
        /// Realiza el cierre asíncrono de la cuenta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CerrarSesionAsync_DoWork(object sender, DoWorkEventArgs e)
        {
            EstaOcupado = true;
            //LoginService.LoginServiceClient Servicio = new LoginService.LoginServiceClient("MiExtremo");
            try
            {
                RUV.I.Red.ServicioLogin.CerrarSesion(RUV.I.Usuario.Cuenta, RUV.I.Seguridad.LlaveUsuario);
                RUV.I.Log.Registrar("Se cerró la sesión del usuario: {0}", RUV.I.Usuario.Cuenta);
            }
            catch (Exception ex)
            {
                RUV.I.Log.Registrar("CerrarSesionAsync_DoWork", ex);
            }

            EstaOcupado = false;
        }

        /// <summary>
        /// Para la ruta de un archivo .cer emitido por certicámara,
        /// retorna la cédula allí ingresada.
        /// </summary>
        /// <param name="archivoCertificado"></param>
        /// <returns></returns>
        string ObtenerCedulaDeCertificado(string archivoCertificado)
        {
            if (!System.IO.File.Exists(archivoCertificado)) return null;

            string Resultado = null;

            // Abrir el archivo de certificado.
            X509Certificate2 CertUsuario = new X509Certificate2(archivoCertificado);

            // Proviene de Certicámara?
            if (!CertUsuario.Issuer.Contains("O=Sociedad Cameral de Certificación Digital - Certicámara S.A."))
                return null;

            // Tratar de extraer la cédula.
            var PartesCedula = CertUsuario.Subject.Split(',');
            var Seccion = PartesCedula.FirstOrDefault(x =>
              x.Contains("OID.1.3.6.1.4.1.23267.2.2")
              || x.Contains("OID.1.3.6.1.4.1.4710.1.3.1"));

            if (Seccion != null)
            {
                Resultado = Seccion.Split('=')[1];
            }

            return Resultado;
        }

        /// <summary>
        /// Retorna la hora del sistema como una cadena encriptada.
        /// </summary>
        string ObtenerFirmaDeTiempo
        {
            get
            {
                return RUV.I.Seguridad.Crypto.EncryptStringFixed
                  (DateTime.Now.ToString("yyyyMMddHHmmss"));
            }
        }

    }
}
