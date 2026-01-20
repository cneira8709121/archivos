using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Ruv.WPF.Server;
using Ruv.Infrastructure.Crosscutting.Common;
using System.ServiceModel.Activation;
using System.IO;


  [AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Required)]
  public class LoginService : ILoginService
  {
    #region AUTENTICAR

    /// <summary>
    /// Devuelve la información básica de autenticación del usuario.
    /// Si el usuario no puede ser autenticado se genera una excepción interna
    /// que imipde que este método se invoque.
    /// </summary>
    /// <param name="cuenta"></param>
    /// <returns></returns>
    public clsUsuario Authenticate(string cuenta, string contraseña, clsInterfaseRed ir, string info)
    {
      clsUsuario Usuario = null;

      // Validación de la hora de autentación.
      // debe estar en un rango de 15 minutos comparado con el servidor.
      if (info == null)
      {
        Usuario = new clsUsuario()
        {
          MensajeAutenticacionFallida = "Autenticación fallida"
        };
        return Usuario;
      }

      /*
       * Se omite esta validación temporalmente por solicitud de Ricardo Daniel
       * para que puedan ingresar desde Toronto.
      clsCryptoUtil Crypto = new clsCryptoUtil();
      string FechaUsuario = Crypto.DecryptStringFixed(info);
      DateTime Fecha = new DateTime(1900, 1, 1);
      DateTime.TryParseExact(FechaUsuario, "yyyyMMddHHmmss", null,
        System.Globalization.DateTimeStyles.None, out Fecha);

      var Diferencia = Math.Abs((DateTime.Now - Fecha).TotalMinutes);
      if (Diferencia > 15)
      {
        Usuario = new clsUsuario()
        {
          MensajeAutenticacionFallida = "Autenticación fallida"
        };
        return Usuario;
      }
      */

      try
      {
        // Si se entra aqui, el usuario fué correctamente autenticado y sólo se devuelven
        Ruv.WPF.Server.clsAutenticador Autenticador = new clsAutenticador();
        var Resultado = Autenticador.ValidarCredenciales(cuenta, contraseña, ir);

        Usuario = new clsUsuario();

        if (Resultado.Key != Ruv.Infrastructure.Crosscutting.Common.eCodigoAutenticacion.AutenticacionExitosa)
        {
          Usuario.MensajeAutenticacionFallida = Resultado.Value;
          return Usuario;
        }

        // los datos del usuario.
        //Usuario = UsuariosAutenticados[cuenta.ToLower()];
        Usuario = Autenticador.UsuarioAutenticado;

        // Agregar la versión del archivo de parámetros.
        Usuario.VersionArchivoParametros =
          System.Configuration.ConfigurationManager.AppSettings["UltimoArchivoParametros"];

      }
      catch (Exception ex)
      {
        //clsLog.Registrar(ex);
        RegistroTraza.I.Registrar(ex);
        Usuario = new clsUsuario() { MensajeAutenticacionFallida = "El servicio no está disponible en este momento." };
      }

      return Usuario;
    }

    /// <summary>
    /// La lista global de usuarios actualmente autenticados.
    /// </summary>
    Dictionary<string, clsUsuario> UsuariosAutenticados
    {
      get
      {
        var WebServer = System.Web.HttpContext.Current;
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

    /// <summary>
    /// Cierra la sesión del usuario.
    /// </summary>
    /// <param name="cuenta"></param>
    public void CerrarSesion(string nombreUsuario, string cuentaUsuario)
    {
      clsSeguridad Seguridad = new clsSeguridad();
      if (!Seguridad.CredencialesValidas(cuentaUsuario)) return;

      clsAutenticador Auth = new clsAutenticador();
      Auth.CerrarSesion(nombreUsuario);
    }

    #endregion
  }
