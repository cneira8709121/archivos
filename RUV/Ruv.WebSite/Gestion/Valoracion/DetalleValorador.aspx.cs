using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Gestion_Valoracion_DetalleValorador : PaginaBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null) return;
        Master.IdPage = "1031";
        Master.CargarOpcionesporUrl();
       // Master.ValidarPermisoPagina();
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);
    }
    void Master_OnOptionClick(object sender, OptionEventArgs e)
    {
        switch (e.ControlName)
        {
            case "Atras":
                if (Request.QueryString["urlEvio"] == null) return;
                string urlEnvio = Request.QueryString["urlEvio"];
                Response.Redirect(urlEnvio);
                break;
            default:
                break;
        }
    }
    protected void ObjectDataSource2_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        
        DataSourceDetalleValorador SinVal = e.ObjectInstance as DataSourceDetalleValorador;
        SinVal.NIdValorador = int.Parse(Request.QueryString["id"]);
        if (TxtFechaInicial.Text == null || TxtFechaInicial.Text == string.Empty)
            SinVal.FechaSolicitada = DateTime.Now;
        else
        {
            DateTime date;
            if (DateTime.TryParseExact(TxtFechaInicial.Text, "MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
            {
                SinVal.FechaSolicitada = date;
            }
            else
            {
                SinVal.FechaSolicitada = DateTime.Now;
               
            }
        }
    }

    protected void Consultar_Click(object sender, EventArgs e)
    {
        gridDetalleValorador.DataBind();
    }
}