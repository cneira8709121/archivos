using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common.ActosAdmin;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class ActosAdmin_Nuevo : PaginaBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.UrlCurrenPage = Request.Url.AbsolutePath;
        Master.ValidarPermisoPagina();
        Master.CargarOpcionesporUrl();
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);

        if (!Page.IsPostBack)
        {
            lblFecha.Text = string.Format(lblFecha.Text, DateTime.Now.ToString(ConfigurationManager.AppSettings["Fecha"].ToString()));
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

    protected void ddlOperacion_SelectIndexChange(object sender, EventArgs e)
    {
        if (ddlOperacion.SelectedValue != null)
        {
            ddlDocumento.Items.Clear();
            ddlDocumento.Valor = ddlOperacion.SelectedValue;
            ddlDocumento.Source = Poblar.DocumentosActosAd;
        }
    }

    private void Guardar()
    {
        clsActosAdminstrativos actoadmin = new clsActosAdminstrativos();
        actoadmin.ID = 0;
        actoadmin.DocumentoId = Convert.ToInt32(ddlDocumento.SelectedValue);
        actoadmin.Num_interno = txtNumeroInterno.Text;
        actoadmin.NroFormulario = txtNroFormulario.Text;
        actoadmin.Descripcion = txtDescripcion.Text;
        actoadmin.Dirigido = txtDirigido.Text;
        actoadmin.UsuarioId = Varios.UsuarioId();
        actoadmin.EstadoId = (int)eEstadoActoAdmin.Generado;


        ActosAdminService objActosAdmin = new ActosAdminService();
        string resultado = objActosAdmin.Guardar(actoadmin);
        if (resultado.Contains("chr(13)"))
        {
            mpupError.MensajeTextBox = resultado;
            mpupError.Mostrar();
            return;
        }
        mpopGuardar.Mensaje = string.Format("Se genero con el consecutivo numero: {0}", resultado);
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