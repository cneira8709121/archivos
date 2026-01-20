using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Gestion_Valoracion_GestionValoracion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void ObjectDataSource1_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        DataSourceConsultaValorador SinVal = e.ObjectInstance as DataSourceConsultaValorador;
    }

    protected void gridGestionValorador_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("DetalleValorador.aspx?id={0}&urlEvio={1}", this.gridGestionValorador.SelectedValue, this.Request.Url.AbsolutePath));
    }
}