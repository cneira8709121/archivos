using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.WebApp.Common;
using Ruv.WebApp.DataSources.Notificaciones;
using Ruv.Business.DTO.Notificacion;

public partial class Notificaciones_DetallesDatosCentroAtencion : System.Web.UI.Page
{

    DataSourceDetalleCentroAtencion dataSourceController = new DataSourceDetalleCentroAtencion();
    public int nIdCentroAtencion
    {
        get
        {
            int id = 0;
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out id)) return id;
            return -1;
        }
    }

    public int nTipo
    {
        get
        {
            int tipo = 0;
            if (Request.QueryString["tipo"] != null && int.TryParse(Request.QueryString["tipo"], out tipo)) return tipo;
            return -1;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridViewDetalleCentrosAtencion();
            BindGridViewEncargados();
        }
    }

    protected void grdDetalleCentrosAtencion_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    private void BindGridViewDetalleCentrosAtencion()
    {
        var records = dataSourceController.DetalleDatosCentroAtencion(this.nIdCentroAtencion,this.nTipo,GridPager.CurrentPageNumber, GridPager.CurrentPageSize, string.Empty);
        var recordCount = dataSourceController.CantidadNotificaciones(this.nIdCentroAtencion,this.nTipo);

        grdDetalleCentrosAtencion.DataSource = records;
        grdDetalleCentrosAtencion.DataBind();

        GridPager.TotalPages = recordCount % GridPager.CurrentPageSize == 0 ? recordCount / GridPager.CurrentPageSize : recordCount / GridPager.CurrentPageSize + 1;
    }

    private void BindGridViewEncargados()
    {
        var records = dataSourceController.ObtenerEncargadosPorEntidad(this.nIdCentroAtencion, this.nTipo, GridPagerEncargados.CurrentPageNumber, GridPagerEncargados.CurrentPageSize);
        var recordCount = dataSourceController.CantidadEncargados(this.nIdCentroAtencion, this.nTipo);

        grdEncargados.DataSource = records; 
        grdEncargados.DataBind();

        GridPagerEncargados.TotalPages = recordCount % GridPagerEncargados.CurrentPageSize == 0 ? recordCount / GridPagerEncargados.CurrentPageSize : recordCount / GridPagerEncargados.CurrentPageSize + 1;
    }

    protected void GridPager_PageChanged(object sender, Ruv.WebApp.Utilidades.Controles.GridCustomPager.CustomPageChangeArgs e)
    {
        GridPager.CurrentPageSize = e.CurrentPageSize;
        GridPager.CurrentPageNumber = e.CurrentPageNumber;
        grdDetalleCentrosAtencion.PageSize = e.CurrentPageSize;
        grdDetalleCentrosAtencion.PageIndex = e.CurrentPageNumber;
        BindGridViewDetalleCentrosAtencion();
    }

    protected void GridPagerEncargados_PageChanged(object sender, Ruv.WebApp.Utilidades.Controles.GridCustomPager.CustomPageChangeArgs e)
    {
        GridPagerEncargados.CurrentPageSize = e.CurrentPageSize;
        GridPagerEncargados.CurrentPageNumber = e.CurrentPageNumber;
        grdEncargados.PageSize = e.CurrentPageSize;
        grdEncargados.PageIndex = e.CurrentPageNumber;
        BindGridViewEncargados();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConsultaDatosCentrosAtencion.aspx");
    }
}