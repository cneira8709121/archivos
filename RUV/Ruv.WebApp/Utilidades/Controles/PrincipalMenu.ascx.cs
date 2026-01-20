using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using SIRAV.Entidades.Administracion;


public partial class Utilidades_Controles_PrincipalMenu : System.Web.UI.UserControl
{
    private int _perfilId;

    public int PerfilId
    {
        get { return _perfilId; }
        set { _perfilId = value; }
    }
    
    public List<MENU> items
    {
        get
        {
            Ruv.WebApp.New_Join_SIRAV.Services.Administracion dat = new Ruv.WebApp.New_Join_SIRAV.Services.Administracion();
            List<MENU> lstmenu = new List<MENU>();
            SIRAV.Cliente.Administracion.ClienteUsuario objClienteAdmin = new SIRAV.Cliente.Administracion.ClienteUsuario();
            USUARIO usuarioSirav = objClienteAdmin.ObtenerUsuarioPorToken(Session[ConstantesSesion.USUARIO_ID_LOGIN].ToString());
            int usuarioID = usuarioSirav.ID;
            lstmenu = dat.UsuarioMenu(usuarioID);
            return lstmenu;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (items != null && items.Count > 0)
        {
            LoadMenu(items);
        }
    }

    private void LoadMenu(List<MENU> op)
    {
        NavigationMenu.Items.Clear();
        const int primernivel = 2;
        IEnumerable<MENU> _mlevelP = from mn in op
                                     where mn.TIPO.Value == primernivel && mn.ID.StartsWith("12")
                                     orderby mn.POSICION
                                     select mn;

        foreach (MENU m in _mlevelP)
        {
            List<MENU> submenus = new List<MENU>();

            MenuItem mi = new MenuItem();
            mi.Text = m.NOMBRE;
            mi.NavigateUrl = m.URL;
            
            const int segundonivel = 3;
            IEnumerable<MENU> _mlevelS = from mn in op
                                         where mn.TIPO.Value == segundonivel && mn.ID.ToString().StartsWith(m.ID.ToString())
                                         orderby mn.POSICION
                                         select mn;

            List<MenuItem> submenu = CargarSubMenus(_mlevelS.ToList());
            foreach (MenuItem itSubmenu in submenu)
	        {
                mi.ChildItems.Add(itSubmenu);
	        }

            NavigationMenu.Items.Add(mi);
        }
    }

    private List<MenuItem> CargarSubMenus(List<MENU> items)
    {
        List<MenuItem> tblSubMenu = new List<MenuItem>();
        foreach (MENU st in items)
        {
            MenuItem tblRowSubMenu = new MenuItem();
            tblRowSubMenu.Text = st.NOMBRE;
            tblRowSubMenu.NavigateUrl = st.URL;
            tblSubMenu.Add(tblRowSubMenu);
        }
        return tblSubMenu;
    }

    private void Children(ref MenuItem mi, string opId, List<MENU> lop)
    {
        foreach (MENU o in lop)
        {
            List<MENU> submenus = new List<MENU>();

            const int segundonivel = 3;
            IEnumerable<MENU> _mlevelS = from mn in lop
                                         where mn.TIPO.Value == segundonivel && mn.ID.ToString().StartsWith(o.ID.ToString())
                                         orderby mn.POSICION
                                         select mn;

            if (o.TIPO == 3)
            {
                MenuItem m = new MenuItem();
                m.Text = o.NOMBRE;
                m.NavigateUrl = o.URL;
                mi.ChildItems.Add(m);
            }
        }
    }


}
