using System;
using System.Web.Security;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WebSite.Common;

public partial class _Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {
        var message = Request.QSStringField("message");
        if (!string.IsNullOrEmpty(message)) {
            loginMessage.Text = message;
            loginMessage.Visible = true;
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e) {
        string usuario = txtUserName.Text;
        string contraseña = txtPassword.Text;

        LoginService objLogin = new LoginService();

        clsCryptoUtil cifrado = new clsCryptoUtil();
        string ContraseñaCifrada = cifrado.EncryptStringFixed(DateTime.Now.ToString("yyyyMMddHHmmss"));

        Ruv.Infrastructure.Crosscutting.Common.clsInterfaseRed ir = new Ruv.Infrastructure.Crosscutting.Common.clsInterfaseRed();
        clsUsuario usuarioaut = objLogin.Authenticate(usuario, contraseña, ir, ContraseñaCifrada);
        if (!string.IsNullOrEmpty(usuarioaut.Contraseña))
            usuarioaut.Contraseña = cifrado.EncryptStringFixed(usuarioaut.Contraseña);

        if (!string.IsNullOrEmpty(usuarioaut.MensajeAutenticacionFallida)) {
            lblError.Text = usuarioaut.MensajeAutenticacionFallida;
            return;
        }

        var sessionCookieValue = cifrado.EncryptStringFixed(string.Format("{0}|{1}", usuario, usuarioaut.Contraseña));
        Response.Cookies.Add(new System.Web.HttpCookie("RUVSessionID", sessionCookieValue));

        Session[ConstantesSesion.USUARIO] = usuarioaut;
        Session[ConstantesSesion.USUARIO_ID_LOGIN] = usuarioaut.Id;
        string UsuarioCompleto = string.Format("{0}", usuarioaut.Nombre);
        FormsAuthentication.RedirectFromLoginPage(UsuarioCompleto, false);
        
    }

    protected void btnRecordar_Click(object sender, EventArgs e) {
       
    }

    protected void btnOlvidoClave_Click(object sender, EventArgs e) {

    }

    protected void btnRegresar_Click(object sender, EventArgs e) {
        mvLogin.ActiveViewIndex = 0;
    }

}