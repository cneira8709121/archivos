using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Contexts;
using System.Web.Security;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Collections;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Data;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Xml.Linq;
using SIRAV.Entidades.Administracion;

public class Varios
{
    public Varios()
    {
    }
    
    public static void AgregarSeleccioneUno(ref DropDownList ddl)
    {
        ListItem liPrimero = new ListItem();
        liPrimero.Text = "[Seleccione Uno]";
        liPrimero.Value = ValoresDropDownList.NoSeleccion.GetHashCode().ToString();
        ddl.Items.Add(liPrimero);
    }

    public static void AgregarOtroValor(ref DropDownList ddl)
    {
        ListItem liOtro = new ListItem();
        liOtro.Text = "[Otro]";
        liOtro.Value = ValoresDropDownList.OtroValor.GetHashCode().ToString();
        ddl.Items.Add(liOtro);
    }

    public static int UsuarioId()
    {
        USUARIO usuario = new USUARIO();
        int usuarioId = 0;

        if (HttpContext.Current.Session[ConstantesSesion.USUARIO] != null)
        {
            USUARIO _us = (USUARIO)HttpContext.Current.Session[ConstantesSesion.USUARIO];
            usuarioId = _us.ID;
        }
        return usuarioId;
    }

    public static Guid TokenGuid()
    {
        Guid Token = new Guid();
        if (HttpContext.Current.Session[ConstantesSesion.USUARIO] != null)
        {
            Token = Guid.Parse(HttpContext.Current.Session[ConstantesSesion.USUARIO_ID_LOGIN].ToString());
        }
        return Token;
    }

    public static string Token()
    {
        return TokenGuid().ToString();
    }

    public static Guid TokenGuidApp()
    {
        Guid Token = new Guid();
        if (HttpContext.Current.Session[ConstantesSesion.USUARIO_APP] != null)
        {
            Token = Guid.Parse(HttpContext.Current.Session[ConstantesSesion.USUARIO_APP].ToString());
        }
        return Token;
    }

    public static string TokenApp()
    {
        return TokenGuidApp().ToString();
    }


    public static string Ubica_Puntos_En_NumDocumento(string Documento)
    {
        string doc = string.Empty;
        string NumDoc = Documento;
        int resto = NumDoc.Length;
        NumDoc = NumDoc.Replace(".", "");
        NumDoc = NumDoc.Replace(",", "");
        NumDoc = NumDoc.Replace(";", "");
        NumDoc = NumDoc.Trim();
        if (resto > 3)
        {
            while (resto > 3)
            {
                resto = resto - 3;
                doc = "." + NumDoc.Substring(resto, 3) + doc;
            }
            doc = NumDoc.Substring(0, resto) + doc;
        }
        else
        {
            doc = Documento;
        }

        return doc;
    }

    public static SIRAV.Entidades.Administracion.USUARIO Usuario(HttpContext contexto)                                                                                                                                                                             
    {
        SIRAV.Entidades.Administracion.USUARIO usuario = new SIRAV.Entidades.Administracion.USUARIO();
        if (contexto.Session[ConstantesSesion.USUARIO] != null)
        {
            usuario = (SIRAV.Entidades.Administracion.USUARIO)contexto.Session[ConstantesSesion.USUARIO];
        }
        return usuario;
    }

    public static bool FirmarDocumento(string ArchNoFirmado, string ArchFirmado)
    {
        try
        {
            X509Store objStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            objStore.Open(OpenFlags.ReadOnly);
            X509Certificate2 objCert = null;
            if (objStore.Certificates != null)
            {
                foreach (X509Certificate2 objCertTemp in objStore.Certificates)
                {
                    if (objCertTemp.HasPrivateKey)
                    {
                        if (objCertTemp.SerialNumber.Equals("20F985476427F4964D5E87399CED9662") || objCertTemp.SerialNumber.Equals("4AFE7628FA9E1DA14B3D1DF90FB8D100")
                            || objCertTemp.SerialNumber.Equals("1DBCCF8B4C3C8D8E4D57E06E7FB30B4C"))
                            objCert = objCertTemp;

                    }
                }
            }
            PdfUtilidades.SignHashed(ArchNoFirmado, ArchFirmado, objCert, "Documento Firmado", "Colombia", false);
            return true;
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return false;
        }
    }

    public static void LimpiarSesiones(HttpContext _context)
    {

        _context.Session[ConstantesItems.VALORACION_ANEXO] = null;
        _context.Session[ConstantesItems.VALORACION_ANEXO_ID] = null;
        _context.Session[ConstantesItems.VALORACION_PERSONA_GUARDADA] = null;
        _context.Session[ConstantesItems.VALORACION_PERSONA_ULTIMA] = null;
        _context.Session[ConstantesItems.VALORACION_PERSONA_GRILLA] = null;
        _context.Session[ConstantesItems.VALORADORES] = null;
        _context.Session[ConstantesItems.DECLARACIONES_NO_VAL] = null;
        _context.Session[ConstantesItems.DECLARACIONES_ASIGNADAS] = null;
        _context.Session[ConstantesSesion.USUARIO] = null;
        _context.Session[ConstantesSesion.USUARIO_ID_LOGIN] = null;
        _context.Session[ConstantesItems.HERRAMIENTAS] = null;
        _context.Session[ConstantesItems.GENERALES_DATOS] = null;
        _context.Session[ConstantesItems.ERROR] = null;
    }


    public static void CerrarCession(HttpContext _context)
    {
        LoginService objLogin = new LoginService();

        Ruv.WebApp.New_Join_SIRAV.Services.Administracion objAdmin = new Ruv.WebApp.New_Join_SIRAV.Services.Administracion();
        objAdmin.CerrarSesion(ObtenerUsuarioId());
        objAdmin.CerrarSesion(ObtenerUserservicio());
        LimpiarSesiones(_context);
    }

    public static string ObtenerUsuarioId()
    {
        string token = string.Empty;
        if (HttpContext.Current.Session[ConstantesSesion.USUARIO_ID_LOGIN] != null)
        {
            token = HttpContext.Current.Session[ConstantesSesion.USUARIO_ID_LOGIN].ToString();
        }
        return token;
    }

    public static string ObtenerUserservicio()
    {
        string token = string.Empty;
        if (HttpContext.Current.Session[ConstantesSesion.USUARIO_APP] != null)
        {
            token = HttpContext.Current.Session[ConstantesSesion.USUARIO_APP].ToString();
        }
        return token;
    }


}


