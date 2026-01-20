using System;
using System.Web.Security;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WebApp.Common;
using Ruv.WebApp.New_Join_SIRAV.Services;
using System.ServiceModel;
using SIRAV.Entidades.Administracion;



public partial class _Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var message = Request.QSStringField("message");
        //if (!string.IsNullOrEmpty(message))
        //{
        //    loginMessage.Text = message;
        //    loginMessage.Visible = true;
        //}

        //viene desde la captura y debe loguearse y abrir la valoracion
        if (Request.QueryString["Log"] != null && Request.QueryString["Pas"] != null)
        {
            string usuario = Request.QueryString["Log"].ToString();
            string password = Request.QueryString["Pas"].ToString();
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(password))
            {
                int idVal = Convert.ToInt32(Request.QueryString["IdVal"].ToString());
                clsCryptoUtil cifrado = new clsCryptoUtil();
                txtUserName.Value = usuario;
                txtPassword.Value = cifrado.DecryptStringFixed(password);
                LoginButton_Click(new object(), new EventArgs());
                Response.Redirect("Valoracion/Valoracion/Nueva.aspx?id=" + idVal.ToString(), true);
            }
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string usuario = txtUserName.Value;
        string contraseña = txtPassword.Value;

        string validacion = string.Empty;
        string codigo = string.Empty;
        string codigoServicios = string.Empty;

        Administracion objAdmin = new Administracion();

        try
        {
            codigo = objAdmin.Autenticar(usuario, contraseña);
            codigoServicios = objAdmin.Autenticar(Ruv.WebApp.Properties.Resources.UsuarioApp, Ruv.WebApp.Properties.Resources.ClaveApp);
        }
        catch (FaultException<SIRAV.DTO.ExceptionInfo> ex)
        {
            validacion = ex.Detail.Descripcion;
        }

        if (string.IsNullOrEmpty(validacion))
        {
            Session[ConstantesSesion.USUARIO_ID_LOGIN] = codigo;
            Session[ConstantesSesion.USUARIO_APP] = codigoServicios;

            USUARIO _Usuario = objAdmin.UsuarioPorToken(codigo);
            SIRAV.Cliente.Administracion.ClienteUsuario objUsuario = new SIRAV.Cliente.Administracion.ClienteUsuario();
            USUARIO_PROGRAMA usuarioPrograma = objUsuario.ObtenerUsuarioPorPrograma(2, _Usuario.ID, codigoServicios);
            _Usuario.ChangeTracker.State = ObjectState.Added;
            _Usuario.ID = Convert.ToInt32(usuarioPrograma.ID_USUARIO_PROGRAMA);
            Session[ConstantesSesion.USUARIO] = _Usuario;
            string UsuarioCompleto = string.Format("{0} {1} {2} {3}", _Usuario.PRIMER_NOMBRE, _Usuario.SEGUNDO_NOMBRE,
                _Usuario.PRIMER_APELLIDO, _Usuario.SEGUNDO_APELLIDO);
            FormsAuthentication.RedirectFromLoginPage(UsuarioCompleto, false);
        }
        else
        {
            lbMsg.Text = validacion.ToString();
            return;
        }

    }

 }