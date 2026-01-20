using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Site : System.Web.UI.MasterPage
{

    public event OptionHandler OnOptionClick;

    private string urlCurrenPage;

    public string UrlCurrenPage
    {
        get { return urlCurrenPage; }
        set { urlCurrenPage = value; }
    }

    private string idPage;

    public string IdPage
    {
        get { return idPage; }
        set { idPage = value; }
    }

    public string Mensaje
    {
        get { return lblMensaje.Text; }
        set { lblMensaje.Text = value; }
    }

    private string menuId;
    public string MenuId
    {
        get { return menuId; }
        set { menuId = value; }
    }

    private List<Permisos> itemsMenu;

    public List<Permisos> ItemsMenu
    {
        get { return itemsMenu; }
        set { itemsMenu = value; }
    }

    public Utilidades_Controles_dpsModalPopUp MensajeDeError 
    {
        get
        {
            return PopUpError;
        }
    }
    public Utilidades_Controles_dpsModalPopUp PopUpGeneral
    {
        get
        {
            return mpCargando;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.User.Identity.IsAuthenticated)
        {
            FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
            CargarInfoUsuario();
            if (!Page.IsPostBack)
            {
                CargarMenus();
            }
        }
    }

    protected void MostrarMensaje(object sender, NotificacionEventArgs e)
    {
        PopUpGeneral.Mensaje = e.CMensaje;
        PopUpGeneral.MostrarImagen = false;
        PopUpGeneral.MostrarBotones = true;
        PopUpGeneral.VisibleBotonCancelar = false;
        PopUpGeneral.Mostrar();
    }

    protected void MostrarMensajeSimple(object sender, NotificacionEventArgs e) {
        generalPopup.MostrarMensaje("Mensaje", e.CMensaje);
    }

    private void CargarInfoUsuario()
    {
        clsUsuario _us = RUV.Current.Usuario;
        lblUserName.Text = string.Format("User Name: {0}", _us.Cuenta);
        lblFecha.Text = string.Format("Fecha: {0}", DateTime.Now.ToShortDateString());
    }

    private void CargarMenus()
    {
        Menus _menu = new Menus();
        clsUsuario _usuario = (clsUsuario)RUV.Current.Usuario;
        List<Permisos> permisos = new List<Permisos>();
        foreach (ePermisosUsuario q in _usuario.Permisos)
        {
            if (_menu.ObtenerPermisos().Exists(x => x.Id == q.GetHashCode().ToString()))
            {
                permisos.Add(_menu.ObtenerPermisos().First(x => x.Id == q.GetHashCode().ToString()));
            }
        }
        MenuPrincipal1.Items = permisos;
    }

    public void ValidarPermisoPagina()
    {
        Menus _menu = new Menus();
        clsUsuario _usuario = (clsUsuario)Session[ConstantesSesion.USUARIO];
        List<Permisos> permisos = new List<Permisos>();
        foreach (ePermisosUsuario q in _usuario.Permisos)
        {
            if (_menu.ObtenerPermisos().Exists(x => x.Id == q.GetHashCode().ToString()))
            {
                permisos.AddRange(_menu.ObtenerPermisos().Where(x => x.Id == q.GetHashCode().ToString()));
            }
        }

        if (!permisos.Exists(x => x.Url.Equals(this.UrlCurrenPage)))
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    public void CargarOpcionesporUrl()
    {
        Menus _menu = new Menus();
        clsUsuario _usuario = (clsUsuario)Session[ConstantesSesion.USUARIO];
        List<Permisos> permisos = new List<Permisos>();
        Permisos pagina = new Permisos();
        if (!string.IsNullOrWhiteSpace(this.urlCurrenPage))
            pagina = _menu.ObtenerPermisos().First(x => x.Url == this.urlCurrenPage);
        else
            pagina = _menu.ObtenerPermisos().First(x => x.Id == this.idPage);

        permisos.AddRange(_menu.ObtenerPermisos().Where(x => x.Padre == pagina.Id && x.Tipo == pagina.Tipo + 1));
        MenuAcciones1.CargarMenus(permisos);
    }

    public void QuitarMenus(string[] menus)
    {
        MenuAcciones1.QuitarMenu(menus);
    }

    protected void opcionesMenu_OptionClick(object sender, OptionEventArgs e)
    {
        if (OnOptionClick != null)
        {
            OnOptionClick(sender, e);
        }

    }
    public void MostrarMensajeGenerico()
    {
        ScriptManager.RegisterStartupScript(upModal, this.GetType(), Guid.NewGuid().ToString(), "<script>ShowModConsult()</script>", false);
    }
    public void OcultarMensajeGenerico()
    {
        ScriptManager.RegisterStartupScript(upModal, this.GetType(), Guid.NewGuid().ToString(), "<script>HidePopUp()</script>", false);
    }


    protected void lsInicio_LoggedOut(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        Varios.CerrarCession(context);
        Response.Redirect("~/Default.aspx");
    }
}
