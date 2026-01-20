using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Valoracion_Valoracion_Resumen : PaginaBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.UrlCurrenPage = Request.Url.AbsolutePath;
        Master.CargarOpcionesporUrl();
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["ValId"] != null)
            {
                int valId = Convert.ToInt32(Request.QueryString["ValId"]);
                ValoracionService objValoracion = new ValoracionService();
                Valoracion = objValoracion.getResumenPorId(valId);

                dvInforDeclaracion1.DataSource = Valoracion.Tables[0];
                dvInforDeclaracion1.DataBind();

                dvInforDeclaracion2.DataSource = Valoracion.Tables[0];
                dvInforDeclaracion2.DataBind();

                gvHechos.DataSource = Valoracion.Tables[1];
                gvHechos.DataBind();

                
            }
        }
    }


    void Master_OnOptionClick(object sender, OptionEventArgs e)
    {
        switch (e.ControlName)
        {
            case "Atras":
                Response.Redirect("Default.aspx");
                break;
            default:
                break;
        }
    }
    public DataSet Valoracion
    {
        get
        {
            if (Session[ConstantesItems.VALORACION] == null)
                Session[ConstantesItems.VALORACION] = new DataSet();

            return (DataSet)Session[ConstantesItems.VALORACION];
        }
        set
        {
            Session[ConstantesItems.VALORACION] = value;
        }
    }

    protected void gvHechos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int hecho = Convert.ToInt32(gvHechos.SelectedValue);
        pnlTitPer.Visible = true;
        if (Valoracion.Tables[2].Select(string.Format("id_val_anexo = {0}", hecho)).Count() > 0)
        {
            DataTable dtPersona = Valoracion.Tables[2].Select(string.Format("id_val_anexo = {0}", hecho)).CopyToDataTable();
            dvInforPersona.DataSource = dtPersona;
            dvInforPersona.DataBind();
        }
        else
        {
            dvInforPersona.DataSource = null;
            dvInforPersona.DataBind();
        }
    }
}