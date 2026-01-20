using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Ruv.Infrastructure.Crosscutting.Common.ActosAdmin;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class ActosAdmin_Editar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.UrlCurrenPage = Request.Url.AbsolutePath;
        Master.ValidarPermisoPagina();
        Master.CargarOpcionesporUrl();
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);

        if (!Page.IsPostBack)
        {
            CargarDatos();
        }
    }

    private void CargarDatos()
    {
        if (Session[ConstantesItems.ACTOS_ADMIN] != null)
        {
            int id = Convert.ToInt32(Session[ConstantesItems.ACTOS_ADMIN]);
            ActosAdminService objActoAdmin = new ActosAdminService();
            clsActosAdminstrativos actoAdmin = new clsActosAdminstrativos();
            
            actoAdmin = objActoAdmin.GetActoAdminPorId(id);

            lblFecha.Text = string.Format(lblFecha.Text, actoAdmin.Fecha.ToString(ConfigurationManager.AppSettings["Fecha"].ToString()));
            txtDocumento.Text = actoAdmin.TipoDocumento;
            txtDirigido.Text = actoAdmin.Dirigido;
            txtDescripcion.Text = actoAdmin.Descripcion;
            txtNumeroInterno.Text = actoAdmin.Num_interno;
            txtNroFormulario.Text = actoAdmin.NroFormulario;
        }
    }

    void Master_OnOptionClick(object sender, OptionEventArgs e)
    {
        switch (e.ControlName)
        {
            case "Guardar":
                Guardar();
                break;
            case "Atras":
                Response.Redirect("Default.aspx");
                break;
            default:
                break;
        }
    }

    
    private void Guardar()
    {
        clsActosAdminstrativos actoadmin = new clsActosAdminstrativos();
        if (Session[ConstantesItems.ACTOS_ADMIN] != null)
        {
            int id = Convert.ToInt32(Session[ConstantesItems.ACTOS_ADMIN]);
            ActosAdminService objActoAdmin = new ActosAdminService();
            actoadmin = objActoAdmin.GetActoAdminPorId(id);
        }

        if (actoadmin.EstadoId != (int)eEstadoActoAdmin.Generado)
        {
            mpopGuardar.Mensaje = string.Format("No se puede actualizar la información debido al estado en que se encuentra");
            mpopGuardar.Mostrar();
            return;
        }
        
        actoadmin.Num_interno = txtNumeroInterno.Text;
        actoadmin.NroFormulario = txtNroFormulario.Text;
        actoadmin.Descripcion = txtDescripcion.Text;
        actoadmin.Dirigido = txtDirigido.Text;
        actoadmin.UsuarioId = Varios.UsuarioId();
        actoadmin.EstadoRegistro = eEstadoRegistro.Modificado;
        
        
        ActosAdminService objActosAdmin = new ActosAdminService();
        string resultado = objActosAdmin.Guardar(actoadmin);
        if (resultado.Contains("chr(13)"))
        {
            mpupError.MensajeTextBox = resultado;
            mpupError.Mostrar();
            return;
        }
        mpopGuardar.Mensaje = string.Format("Se actualizo el acto administrativo numero: {0}", resultado);
        mpopGuardar.Mostrar();
    }
    protected void txtNroFormulario_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtNroFormulario.Text))
        {
            ActosAdminService objActosAdmin = new ActosAdminService();
            bool existe = objActosAdmin.ExisteDeclaracion(txtNroFormulario.Text);
            if (!existe)
                lblAdvertenciaNoExiste.Text = "Este numero de formulario no existe.";
            else
                lblAdvertenciaNoExiste.Text = string.Empty;

            ViewState["Existe"] = existe;
        }
    }
    protected void mpopGuardar_Ok(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}