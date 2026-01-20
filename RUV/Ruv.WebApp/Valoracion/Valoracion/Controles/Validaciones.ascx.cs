using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class Utilidades_Controles_dpsValidaciones : System.Web.UI.UserControl
{
    [Bindable(true)]
    [DefaultValue(false)]
    [Localizable(true)]
    public new bool Visible
    {
        get { return dvList.Visible; }
        set { dvList.Visible = value; }
    }

    [Bindable(true)]
    [Localizable(true)]
    public ListItemCollection Items
    {
        get { return bll.Items; }
    }

    [Bindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    public string DataSourceID
    {
        get { return bll.DataSourceID; }
        set { bll.DataSourceID = value; }
    }


    [DefaultValue("")]
    public object DataSource
    {
        get { return bll.DataSource; }
        set
        {
            bll.DataSource = value;
        }
    }


    public new void DataBind()
    {
        bll.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}