using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

public partial class Utilidades_Controles_PrincipalMenu : System.Web.UI.UserControl
{
    private int _perfilId;

    public int PerfilId
    {
        get { return _perfilId; }
        set { _perfilId = value; }
    }
    private List<Permisos> items;

    public List<Permisos> Items
    {
        get { return items; }
        set { items = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (items != null && items.Count > 0)
        {
            LoadMenu(items);
        }
    }
    private void LoadMenu(List<Permisos> op)
    {
        NavigationMenu.Items.Clear();
        const int primernivel = 1;
        foreach (Permisos o in op.Where(x => x.Tipo == primernivel))
        {
            MenuItem mi = new MenuItem();
            mi.Text = o.Nombre;
            mi.NavigateUrl = o.Url;
            Children(ref mi, o.Id, op);
            NavigationMenu.Items.Add(mi);
        }
    }

    private void Children(ref MenuItem mi, string opId, List<Permisos> lop)
    {
        foreach (Permisos o in lop)
        {
            if (o.Id != opId && o.Padre == opId && o.Tipo == 2)
            {
                MenuItem m = new MenuItem();
                m.Text = o.Nombre;
                m.NavigateUrl = o.Url;
                mi.ChildItems.Add(m);
            }
        }
    }


}
