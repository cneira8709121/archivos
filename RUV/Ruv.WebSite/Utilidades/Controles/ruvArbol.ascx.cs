using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Utilidades_Controles_dpsArbol : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        trvArbol.DataBind();
        trvArbol.CollapseAll();
        trvArbol.Nodes[0].Expand();
    }

    public void collapseAll()
    {
        trvArbol.CollapseAll();
    }

    public void expandAll()
    {
        trvArbol.ExpandAll();
    }

    public TreeNodeCollection Nodes
    {
        get { return trvArbol.Nodes; }
        //set{trvFormatos2011.Nodes= value;}
    }

    public void databind()
    {
        trvArbol.DataBind();

    }

    public string datasourceId
    {
        get { return trvArbol.DataSourceID; }
        set { trvArbol.DataSourceID = value; }
    }

    public string skinId
    {
        get { return trvArbol.SkinID; }
        set { trvArbol.SkinID = value; }
    }

    //ImageSet
    public TreeViewImageSet imageSet
    {
        get { return trvArbol.ImageSet; }
        set { trvArbol.ImageSet = value; }
    }

    public Unit width
    {
        get { return trvArbol.Width; }
        set { trvArbol.Width = value; }
    }
}
