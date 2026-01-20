using System.Collections.Generic;
using System.IdentityModel.Selectors;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Server
{
  /// <summary>
  /// Descripción breve de clsAutenticador
  /// </summary>
    public class clsAutenticador : UserNamePasswordValidator, System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// De presentarse algún problema con la autenticación se debe generar una excepción.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public override void Validate(string userName, string password)
        {
            var Resultado = ValidarCredenciales(userName, password, null);
            if (Resultado.Key != eCodigoAutenticacion.AutenticacionExitosa)
            {
                throw Ruv.WPF.Server.clsUtilsServer.GetGenericFault(
                  eErrores.Autenticacion, Resultado.Key.ToString(),
                  Resultado.Value);

                //throw new FaultException(
                //  new FaultReason(Resultado.Value),
                //  new FaultCode(eErrores.Autenticacion.ToString(),
                //    new FaultCode(Resultado.Key.ToString())
                //    ));
                //throw new FaultException<clsDefaultFaultContract>(
                //  new clsDefaultFaultContract()
                //  {
                //    Codigo = Resultado.Key,
                //    Mensaje = Resultado.Value
                //  });
            }
        }

        /// <summary>
        /// Valida las credenciales del usuario.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public KeyValuePair<eCodigoAutenticacion, string> ValidarCredenciales(
          string userName, string password, clsInterfaseRed ir)
        {
            UsuarioAutenticado = null;

            userName = userName.ToLower();
            //if (!UsuariosAutenticados.ContainsKey(userName))
            //{
            clsUsuario Usuario = new clsUsuario();
            Usuario.Cuenta = userName;
            Usuario.Contraseña = password;

            // Validarlo contra la base de datos.
            Ruv.WPF.Data.clsAutenticador DL = new Ruv.WPF.Data.clsAutenticador();
            var ObjetoUsuario = DL.ValidarCredenciales(Usuario.Cuenta, Usuario.Contraseña, ir);
            UsuarioAutenticado = ObjetoUsuario;

            //if (DL.ResultadoAutenticacion == eCodigoAutenticacion.AutenticacionExitosa)
            //{
            //  UsuariosAutenticados.Add(userName, ObjetoUsuario);
            //  UsuarioAutenticado = ObjetoUsuario;
            //}

            return new KeyValuePair<eCodigoAutenticacion, string>
              (DL.ResultadoAutenticacion, DL.MensajeAutenticacion);
            //}
            //else if (UsuariosAutenticados[userName].Contraseña != password)
            //{
            //  // Ya autenticado pero está enviando otra contraseña.
            //  UsuarioAutenticado = null;
            //  return new KeyValuePair<eCodigoAutenticacion, string>(
            //    eCodigoAutenticacion.UsuarioClaveDesconocidos, "Usuario o clave desconocidos");
            //}
            //else
            //{
            //  // Usuario previamente autenticado.
            //  UsuarioAutenticado = UsuariosAutenticados[userName];
            //  return new KeyValuePair<eCodigoAutenticacion, string>(
            //    eCodigoAutenticacion.AutenticacionExitosa, "Autenticación exitosa");
            //}
        }

        /// <summary>
        /// Acceso a la instancia del WebServer actual.
        /// </summary>
        System.Web.HttpContext WebServer = System.Web.HttpContext.Current;

        /// <summary>
        /// La lista global de usuarios actualmente autenticados.
        /// </summary>
        Dictionary<string, clsUsuario> UsuariosAutenticados
        {
            get
            {
                Dictionary<string, clsUsuario> Output =
                  WebServer.Application["UsuariosAutenticados"] as Dictionary<string, clsUsuario>;

                if (Output == null)
                {
                    Output = new Dictionary<string, clsUsuario>();
                    WebServer.Application["UsuariosAutenticados"] = Output;
                }
                return Output;
            }
        }

        private clsUsuario _UsuarioAutenticado;
        /// <summary>
        /// El último usuario autenticado.
        /// Retorna null si no lo está.
        /// </summary>
        public clsUsuario  UsuarioAutenticado
        {
            get { return _UsuarioAutenticado; }
            set { _UsuarioAutenticado = value; }
        }


        /// <summary>
        /// Cierra la sesión de un usuario.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        public void CerrarSesion(string nombreUsuario)
        {
            Ruv.WPF.Data.clsAutenticador Auth = new Ruv.WPF.Data.clsAutenticador();
            if (UsuariosAutenticados.ContainsKey(nombreUsuario))
                UsuariosAutenticados.Remove(nombreUsuario);
            Auth.CerrarSesion(nombreUsuario);
        }
    }
}
