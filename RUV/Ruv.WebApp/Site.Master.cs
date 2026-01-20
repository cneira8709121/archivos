using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Ruv.Infrastructure.Crosscutting.Common;
using SIRAV.Entidades.Administracion;
using Ruv.WebApp.New_Join_SIRAV.Services;

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

    protected void MostrarMensajeSimple(object sender, NotificacionEventArgs e)
    {
        generalPopup.MostrarMensaje("Mensaje", e.CMensaje);
    }

    private void CargarInfoUsuario()
    {
        SIRAV.Entidades.Administracion.USUARIO _us = RUV.Current.Usuario;
        lblUserName.Text = string.Format("User Name: {0}", _us.USERNAME);
        lblFecha.Text = string.Format("Fecha: {0}", DateTime.Now.ToShortDateString());
    }

    private void CargarMenus()
    {
        USUARIO usuario = RUV.Current.Usuario;
    }

    public void ValidarPermisoPagina()
    {
        USUARIO _usuario = RUV.Current.Usuario;
        if (_usuario != null)
        {
            Administracion objmenu = new Administracion();
            Ruv.WebApp.New_Join_SIRAV.Services.Administracion dat = new Ruv.WebApp.New_Join_SIRAV.Services.Administracion();
            if (!dat.permisosPagina(urlCurrenPage, objmenu.UsuarioPorToken(Session[ConstantesSesion.USUARIO_ID_LOGIN].ToString()).ID))
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }

    public void CargarOpcionesporUrl()
    {
        USUARIO _usuario = (USUARIO)Session[ConstantesSesion.USUARIO];

        if (_usuario == null)
        {
            FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
            List<MENU> itemsMenu = new List<MENU>();
            itemsMenu = ItemMenu;
            IEnumerable<MENU> _mlevelP = from mn in itemsMenu
                                         where mn.URL == urlCurrenPage
                                         select mn;
            if (_mlevelP.ToList().Count > 0)
            {
                MENU m = _mlevelP.First();
                IEnumerable<MENU> _mlevelUltimo = from mn in itemsMenu
                                                  where mn.ID.ToString().StartsWith(m.ID.ToString()) && mn.TIPO == (m.TIPO + 1) && mn.TIPO != m.TIPO
                                                  select mn;

                List<MENU> lm = _mlevelUltimo.ToList();
                if (lm.Count > 0)
                {
                    MenuAcciones1.Url = UrlCurrenPage;
                    MenuAcciones1.CargarMenus(lm);
                }
            }
        }
    }

    public List<MENU> ItemMenu
    {
        get
        {
            List<MENU> lstMenu = new List<MENU>();
            Administracion objmenu = new Administracion();
            lstMenu = objmenu.ObtenerMenuUsuario(objmenu.UsuarioPorToken(Session[ConstantesSesion.USUARIO_ID_LOGIN].ToString()).ID);
            return lstMenu;
        }
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
