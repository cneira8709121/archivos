using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.WebSite.Common;
using Ruv.WebSite.DataSources.Notificaciones;

public partial class Notificaciones_PrepararNotificaciones : PaginaBase {
    
    DataSourceNotificaciones dataSourceController = new DataSourceNotificaciones();

    #region Page Event Handlers

    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack) {

            /* Poblar y seleccionar controles */
            string errorMessage = string.Empty; var service = new GeneralService();

            if (Request.QSStringField("declaracion") != null)
                this.filterDeclaracion.Text = Request.QSStringField("declaracion");

            this.filterTipoDocumento.DataSource = service.ObtenerParametros((int)eTipoParametros.TipoDeDocumentoDeIdentidad, ref errorMessage);
            this.filterTipoDocumento.DataBind();
            this.filterTipoDocumento.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));

            if (Request.QSIntegerField("tipoDocumento") != null)
                this.filterTipoDocumento.SelectedValue = Request.QSIntegerField("tipoDocumento").Value.ToString();

            if (Request.QSStringField("documento") != null)
                this.filterDocumento.Text = Request.QSStringField("documento");

            if (Request.QSStringField("nombreDeclarante") != null)
                this.filterNombreDeclarante.Text = Request.QSStringField("nombreDeclarante");

            this.filterPaisNotificacion.DataSource = service.ObtenerPaises(ref errorMessage);
            this.filterPaisNotificacion.DataBind();

            if (Request.QSIntegerField("paisNotificacion") != null) {
                this.filterPaisNotificacion.SelectedValue = Request.QSIntegerField("paisNotificacion").Value.ToString();
                
                this.filterDepartamentoNotificacion.DataSource = service.ObtenerDepartamentosPorPais(Request.QSIntegerField("paisNotificacion").Value, ref errorMessage);
                this.filterDepartamentoNotificacion.DataBind();

                if (Request.QSIntegerField("departamentoNotificacion") != null) {
                    this.filterDepartamentoNotificacion.SelectedValue = Request.QSIntegerField("departamentoNotificacion").Value.ToString();

                    this.filterMunicipioNotificacion.DataSource = service.ObtenerMunicipiosPorDepartamento(Request.QSIntegerField("departamentoNotificacion").Value, ref errorMessage);
                    this.filterMunicipioNotificacion.DataBind();

                    if (Request.QSIntegerField("municipioNotificacion") != null) {
                        this.filterMunicipioNotificacion.SelectedValue = Request.QSIntegerField("municipioNotificacion").Value.ToString();

                        this.filterPuntoNotificacion.DataSource = service.ObtenerPuntosAtencionyDTPorMunicipio(Request.QSIntegerField("municipioNotificacion").Value);
                        this.filterPuntoNotificacion.DataBind();

                        if (Request.QSIntegerField("puntoNotificacion") != null)
                            this.filterPuntoNotificacion.SelectedValue = Request.QSStringField("puntoNotificacion");
                    }    
                }    
            }
            this.filterPaisNotificacion.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));
            this.filterDepartamentoNotificacion.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));
            this.filterMunicipioNotificacion.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));
            this.filterPuntoNotificacion.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));

            if (Request.QSStringField("direccionCitacion") != null)
                this.filterDireccionCitacion.Text = Request.QSStringField("direccionCitacion");

            dataSourceController.Declaracion = Request.QSStringField("declaracion");
            dataSourceController.TipoDocumento = Request.QSIntegerField("tipoDocumento");
            dataSourceController.Documento = Request.QSStringField("documento");
            dataSourceController.NombreDeclarante = Request.QSStringField("nombreDeclarante");
            dataSourceController.PaisNotificacion = Request.QSIntegerField("paisNotificacion");
            dataSourceController.DepartamentoNotificacion = Request.QSIntegerField("departamentoNotificacion");
            dataSourceController.MunicipioNotificacion = Request.QSIntegerField("municipioNotificacion");
            dataSourceController.PuntoNotificacion = Request.QSStringField("puntoNotificacion");
            dataSourceController.DireccionCitacion = Request.QSStringField("direccionCitacion");
            BindGridView();
        }
    }

    #endregion

    #region Action Event Handlers

    protected void btnFiltrar_Click(object sender, EventArgs e) {
        string redirectionUrl = "PrepararNotificaciones.aspx";

        if (!string.IsNullOrWhiteSpace(this.filterDeclaracion.Text))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("declaracion", this.filterDeclaracion.Text.Trim());

        if (!string.IsNullOrEmpty(this.filterTipoDocumento.SelectedValue))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("tipoDocumento", this.filterTipoDocumento.SelectedValue);

        if (!string.IsNullOrWhiteSpace(this.filterDocumento.Text))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("documento", this.filterDocumento.Text.Trim());

        if (!string.IsNullOrWhiteSpace(this.filterNombreDeclarante.Text))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("nombreDeclarante", this.filterNombreDeclarante.Text.Trim());

        if (!string.IsNullOrEmpty(this.filterPaisNotificacion.SelectedValue))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("paisNotificacion", this.filterPaisNotificacion.SelectedValue);

        if (!string.IsNullOrEmpty(this.filterDepartamentoNotificacion.SelectedValue) || !string.IsNullOrEmpty(Request.Form[this.filterDepartamentoNotificacion.UniqueID]))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("departamentoNotificacion", !string.IsNullOrEmpty(this.filterDepartamentoNotificacion.SelectedValue) ? this.filterDepartamentoNotificacion.SelectedValue : Request.Form[this.filterDepartamentoNotificacion.UniqueID]);

        if (!string.IsNullOrEmpty(this.filterMunicipioNotificacion.SelectedValue) || !string.IsNullOrEmpty(Request.Form[this.filterMunicipioNotificacion.UniqueID]))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("municipioNotificacion", !string.IsNullOrEmpty(this.filterMunicipioNotificacion.SelectedValue) ? this.filterMunicipioNotificacion.SelectedValue : Request.Form[this.filterMunicipioNotificacion.UniqueID]);

        if (!string.IsNullOrEmpty(this.filterPuntoNotificacion.SelectedValue) || !string.IsNullOrEmpty(Request.Form[this.filterPuntoNotificacion.UniqueID]))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("puntoNotificacion", !string.IsNullOrEmpty(this.filterPuntoNotificacion.SelectedValue) ? this.filterPuntoNotificacion.SelectedValue : Request.Form[this.filterPuntoNotificacion.UniqueID]);

        if (!string.IsNullOrWhiteSpace(this.filterDireccionCitacion.Text))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("direccionCitacion", this.filterDireccionCitacion.Text.Trim());

        Response.Redirect(redirectionUrl);
    }

    protected void GridPager_PageChanged(object sender, Ruv.WebSite.Utilidades.Controles.GridCustomPager.CustomPageChangeArgs e) {
        GridPager.CurrentPageSize = e.CurrentPageSize;
        GridPager.CurrentPageNumber = e.CurrentPageNumber;
        grdNotificaciones.PageSize = e.CurrentPageSize;
        grdNotificaciones.PageIndex = e.CurrentPageNumber;
        BindGridView();
    }

    protected void btnRestaurarFiltros_Click(object sender, EventArgs e)
    {
        Response.Redirect("PrepararNotificaciones.aspx");
    }

    protected void btnGenerarPaqueteFiltro_Click(object sender, EventArgs e)
    {
        var service = new NotificacionService();
        string errorMessage = string.Empty;

        var package = service.CrearPaqueteNotificacionDesdeFiltro(RUV.Current.Usuario.Id, Request.QSStringField("declaracion"), Request.QSIntegerField("tipoDocumento"), Request.QSStringField("documento"), Request.QSStringField("nombreDeclarante"), Request.QSStringField("direccionCitacion"), Request.QSStringField("ubicacionNotificacion"), RUV.Current.Usuario.RolesUsuario.Contains(eRolesUsuario.PreparadorNotificaciones), ref errorMessage);

        if (!string.IsNullOrEmpty(errorMessage))
            ModalPopUp.MostrarMensaje("Error", errorMessage);
        else
            ModalPopUp.MostrarMensajeYRedirigir("Mensaje", string.Format("Paquete generado exitosamente con código '{0}', incluye {1} notificaciones", package.Id.ToString(), package.Cantidad.ToString()), "ConsultarNotificaciones.aspx");

    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Genera el archivo excel con las notificaciones del paquete
    /// </summary>
    private void ExportarExcel()
    {
        ExcelHelper eh = new ExcelHelper();
        byte[] excel = eh.ExportToExcel<clsNotificacionExcel>(ObtenerNotificacionesPaquete());

        Response.Clear();
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("Content-Length", excel.Length.ToString());
        Response.AddHeader("Content-Disposition", "attachment;filename=PaqueteEnvioNotificaciones({" + ViewState["IDPaquete"] + "}).xlsx");

        Response.BinaryWrite(excel);
        Response.End();
    }

    /// <summary>
    /// Retorna el listado de clsNotificacionExcel que se incluiran en el exel que conforma el paquete de notificaciones
    /// </summary>
    /// <param name="listaIdNotificaciones"></param>
    /// <returns></returns>
    private List<clsNotificacionExcel> ObtenerNotificacionesPaquete()
    {
        return null;
        //List<clsNotificacionExcel> listaNotificacionesExcel = new List<clsNotificacionExcel>();
        //DataSourceNotificaciones info = new DataSourceNotificaciones();

        //foreach (clsNotificacion notificacion in ListaNotificacionesSeleccionadas)
        //{
        //    clsNotificacionExcel notificacionExcel = new clsNotificacionExcel()
        //    {
        //        ADICIONAL_1 = notificacion.NID.ToString(),
        //        ADICIONAL_2 = "",
        //        CIUDAD_DESTINATARIO = notificacion.CNOMBREMUNICIPIO,
        //        DEPARTAMENTO = notificacion.CNOMBREDEPARTAMENTO,
        //        DIRECCION_DESTINATARIO = notificacion.CDIRECCIONNOTIFICACION,
        //        NOMBRE = notificacion.CNOMBRECOMPLETO,
        //        PESO = "50"
        //    };

        //    listaNotificacionesExcel.Add(notificacionExcel);
        //}

        //return listaNotificacionesExcel;
    }

    protected void btnGuardarExcel_Click(object sender, EventArgs e)
    {
        mdlPopupExcel.Hide();

        //ExportarExcel();
    }

    #endregion

    protected void grdNotificaciones_RowDataBound(object sender, GridViewRowEventArgs e) {
        var notificacion = e.Row.DataItem as clsNotificacion;
        if (e.Row.RowType == DataControlRowType.DataRow && notificacion != null) {
            var editarDireccionCorrespondencia = e.Row.FindControlRecursively("imgBtnEditarCorrespondencia") as ImageButton;
            if (editarDireccionCorrespondencia != null) { 
                editarDireccionCorrespondencia.OnClientClick =
                string.Format("return ruv.notificaciones_pendienteenvio.showDireccionCorrespondenciaPopup('{0}', '{1}', '{2}', '{3}', '{4}');", notificacion.NID, notificacion.NID_PAIS, notificacion.NID_DEPARTAMENTO, notificacion.NID_MUNICIPIO, notificacion.CDIRECCIONNOTIFICACION);
            }
            var editarPuntoNotificacion = e.Row.FindControlRecursively("imgBtnEditarPuntoNotificacion") as ImageButton;
            if (editarPuntoNotificacion != null) {
                editarPuntoNotificacion.OnClientClick =
                string.Format("return ruv.notificaciones_pendienteenvio.showPuntoNotificacionPopup('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", notificacion.NID, notificacion.IdPaisPuntoNotificacion, notificacion.IdDepartamentoPuntoNotificacion, notificacion.IdMunicipioPuntoNotificacion, notificacion.IdPuntoAtencion, notificacion.IdDireccionTerritorial);
            }
        }
    }

    private void BindGridView() {
        var records = dataSourceController.ObtenerNotificaciones(GridPager.CurrentPageNumber, GridPager.CurrentPageSize, string.Empty);
        var recordCount = dataSourceController.CantidadNotificaciones();

        grdNotificaciones.PageSize = GridPager.CurrentPageSize;
        grdNotificaciones.DataSource = records;
        grdNotificaciones.DataBind();

        GridPager.TotalPages = recordCount % GridPager.CurrentPageSize == 0 ? recordCount / GridPager.CurrentPageSize : recordCount / GridPager.CurrentPageSize + 1;
    }

}