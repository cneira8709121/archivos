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
using Ruv.Infrastructure.Crosscutting.Common.General;

public partial class Notificaciones_ConsultaDatosCentrosAtencion : PaginaBase
{
    DataSourceConsultaCentroAtencion dataSourceController = new DataSourceConsultaCentroAtencion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            string errorMessage = string.Empty; var service = new GeneralService();
            this.filterPais.DataSource = service.ObtenerPaises(ref errorMessage);
            this.filterPais.DataBind();

            if (Request.QSIntegerField("pais") != null) {
                this.filterPais.SelectedValue = Request.QSIntegerField("pais").Value.ToString();

                this.filterDepartamento.DataSource = service.ObtenerDepartamentosPorPais(Request.QSIntegerField("pais").Value, ref errorMessage);
                this.filterDepartamento.DataBind();

                if (Request.QSIntegerField("departamento") != null) {
                    this.filterDepartamento.SelectedValue = Request.QSIntegerField("departamento").Value.ToString();

                    this.filterMunicipio.DataSource = service.ObtenerMunicipiosPorDepartamento(Request.QSIntegerField("departamento").Value, ref errorMessage);
                    this.filterMunicipio.DataBind();

                    if (Request.QSIntegerField("municipio") != null) {
                        this.filterMunicipio.SelectedValue = Request.QSIntegerField("municipio").Value.ToString();
                    }
                }
            }
            this.filterPais.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));
            this.filterDepartamento.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));
            this.filterMunicipio.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));

            dataSourceController.Pais = Request.QSIntegerField("pais");
            dataSourceController.Departamento = Request.QSIntegerField("departamento");
            dataSourceController.Municipio = Request.QSIntegerField("municipio");
            BindGridView();
        }
    }

    protected void grdConsultaCentrosAtencion_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
    
    }

    private void BindGridView()
    {
        var records = dataSourceController.ConsultaDatosCentroAtencion(GridPager.CurrentPageNumber, GridPager.CurrentPageSize, string.Empty);
        var recordCount = dataSourceController.CantidadNotificaciones();

        grdConsultaCentrosAtencion.PageSize = GridPager.CurrentPageSize;
        grdConsultaCentrosAtencion.DataSource = records;
        grdConsultaCentrosAtencion.DataBind();

        GridPager.TotalPages = recordCount % GridPager.CurrentPageSize == 0 ? recordCount / GridPager.CurrentPageSize : recordCount / GridPager.CurrentPageSize + 1;
    }

    protected void GridPager_PageChanged(object sender, Ruv.WebApp.Utilidades.Controles.GridCustomPager.CustomPageChangeArgs e)
    {
        GridPager.CurrentPageSize = e.CurrentPageSize;
        GridPager.CurrentPageNumber = e.CurrentPageNumber;
        grdConsultaCentrosAtencion.PageSize = e.CurrentPageSize;
        grdConsultaCentrosAtencion.PageIndex = e.CurrentPageNumber;
        BindGridView();
    }

    protected void btnRestaurarFiltros_Click(object sender, EventArgs e)
    {
        this.filterPais.SelectedIndex = 0;
        this.filterDepartamento.SelectedIndex = 0;
        this.filterMunicipio.SelectedIndex = 0;
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        string redirectionUrl = "ConsultaDatosCentrosAtencion.aspx";

        if (!string.IsNullOrEmpty(this.filterPais.SelectedValue))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("pais", this.filterPais.SelectedValue);

        if (!string.IsNullOrEmpty(this.filterDepartamento.SelectedValue) || !string.IsNullOrEmpty(Request.Form[this.filterDepartamento.UniqueID]))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("departamento", !string.IsNullOrEmpty(this.filterDepartamento.SelectedValue) ? this.filterDepartamento.SelectedValue : Request.Form[this.filterDepartamento.UniqueID]);

        if (!string.IsNullOrEmpty(this.filterMunicipio.SelectedValue) || !string.IsNullOrEmpty(Request.Form[this.filterMunicipio.UniqueID]))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("municipio", !string.IsNullOrEmpty(this.filterMunicipio.SelectedValue) ? this.filterMunicipio.SelectedValue : Request.Form[this.filterMunicipio.UniqueID]);

        Response.Redirect(redirectionUrl);
    }
}