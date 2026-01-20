using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common;
/// <summary>
/// Descripción breve de clsSeguridad
/// </summary>
public class clsSeguridad
{
    /// <summary>
    /// Valida las credenciales del usuario.
    /// Retorna verdadero si las credenciales son válidas.
    /// </summary>
    /// <param name="dato"></param>
    /// <returns></returns>
    public bool CredencialesValidas(string dato)
    {
        try
        {
            clsCryptoUtil CU = new clsCryptoUtil();
            string[] InfoUsuario = CU.DecryptStringFixed(dato).Split('\t');

            Ruv.WPF.Server.clsAutenticador AU = new Ruv.WPF.Server.clsAutenticador();
            var Resultado = AU.ValidarCredenciales(InfoUsuario[0], InfoUsuario[1], null);

            if (Resultado.Key == Ruv.Infrastructure.Crosscutting.Common.eCodigoAutenticacion.AutenticacionExitosa)
                Usuario = AU.UsuarioAutenticado;
            else
                Usuario = null;

            return Resultado.Key == Ruv.Infrastructure.Crosscutting.Common.eCodigoAutenticacion.AutenticacionExitosa;
        }
        catch
        {
            return false;
        }
    }

    public bool CredencialesValidas(string dato, ref string cError)
    {
        try
        {
            clsCryptoUtil CU = new clsCryptoUtil();
            string[] InfoUsuario = CU.DecryptStringFixed(dato).Split('\t');

            Ruv.WPF.Server.clsAutenticador AU = new Ruv.WPF.Server.clsAutenticador();
            var Resultado = AU.ValidarCredenciales(InfoUsuario[0], InfoUsuario[1], null);

            if (Resultado.Key == Ruv.Infrastructure.Crosscutting.Common.eCodigoAutenticacion.AutenticacionExitosa)
                Usuario = AU.UsuarioAutenticado;
            else
                Usuario = null;

            return Resultado.Key == Ruv.Infrastructure.Crosscutting.Common.eCodigoAutenticacion.AutenticacionExitosa;
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            cError = ex.Message;
            return false;
        }
    }

    private clsUsuario _Usuario;
    /// <summary>
    /// Usuario autenticado.
    /// </summary>
    public clsUsuario Usuario
    {
        get { return _Usuario; }
        set { _Usuario = value; }
    }

}