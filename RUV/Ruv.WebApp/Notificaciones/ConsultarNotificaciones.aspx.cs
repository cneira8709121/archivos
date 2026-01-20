using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.WebApp.Common;
using Ruv.WebApp.DataSources.Notificaciones;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;

public partial class Notificaciones_ConsultarNotificaciones : PaginaBase {

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

                        if (Request.QSStringField("puntoNotificacion") != null)
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

    protected void GridPager_PageChanged(object sender, Ruv.WebApp.Utilidades.Controles.GridCustomPager.CustomPageChangeArgs e) {
        GridPager.CurrentPageSize = e.CurrentPageSize;
        GridPager.CurrentPageNumber = e.CurrentPageNumber;
        grdNotificaciones.PageSize = e.CurrentPageSize;
        grdNotificaciones.PageIndex = e.CurrentPageNumber;
        BindGridView();
    }

    #endregion

    #region Action Event Handlers

    protected void btnFiltrar_Click(object sender, EventArgs e) {
        string redirectionUrl = "ConsultarNotificaciones.aspx";

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

    protected void btnRestaurarFiltros_Click(object sender, EventArgs e) {
        Response.Redirect("ConsultarNotificaciones.aspx");
    }

    protected void btnGenerarPaqueteFiltro_Click(object sender, EventArgs e) {
        var service = new NotificacionService();
        string errorMessage = string.Empty;

        //var package = service.CrearPaqueteNotificacionDesdeFiltro(RUV.Current.Usuario.ID, Request.QSStringField("declaracion"), Request.QSIntegerField("tipoDocumento"), Request.QSStringField("documento"), Request.QSStringField("nombreDeclarante"), Request.QSStringField("direccionCitacion"), Request.QSStringField("ubicacionNotificacion"), RUV.Current.Usuario.RolesUsuario.Contains(eRolesUsuario.PreparadorNotificaciones), ref errorMessage);

        //if (!string.IsNullOrEmpty(errorMessage))
        //    ModalPopUp.MostrarMensaje("Error", errorMessage);
        //else
        //{
        //    grdNotificaciones.DataBind();
        //    lblTextoPaquete.Text = string.Format("Paquete generado exitosamente con código '{0}', incluye {1} notificaciones", package.Id, package.Cantidad);
        //    hlkDetallePaquete.NavigateUrl = string.Format("PaqueteDetalle.aspx?id={0}", package.Id);
        //    mdlPopupExcel.Show();
        //}
    }

    protected void btnGenerarPaquetes_Click(object sender, EventArgs e) {
        var selectedNotificaciones = new List<int>();
        foreach (GridViewRow row in grdNotificaciones.Rows) {
            var checkbox = row.FindControl("chkNotificacion") as CheckBox;
            if (checkbox != null && checkbox.Checked) {
                string rowState = grdNotificaciones.DataKeys[row.RowIndex].Values["NID_ESTADONOTIFICACION"].ToString(), rowApproved = grdNotificaciones.DataKeys[row.RowIndex].Values["Aprobado"].ToString();
                int rowStateValue = 0;
                bool approvedValue = false;
                if (int.TryParse(rowState, out rowStateValue) && (rowStateValue == (int)eEstadosNotificacion.PendienteEnvio || rowStateValue == (int)eEstadosNotificacion.PendienteEnvioresolucion) && bool.TryParse(rowApproved, out approvedValue) && approvedValue)
                {
                    int notificacionSeleccionadaId = int.Parse(grdNotificaciones.DataKeys[row.RowIndex].Value.ToString());
                    selectedNotificaciones.Add(notificacionSeleccionadaId);
                }
                else {
                    ModalPopUp.MostrarMensaje(resx::Controles.Advertencia, "No todas las notificaciones seleccionadas fueron aprobadas");
                    return;
                }
            }
        }

        if (selectedNotificaciones.Count == 0) {
            ModalPopUp.MostrarMensaje(resx::Controles.Advertencia, "Debe seleccionar por lo menos una notificación para generar el paquete");
            return;
        }

        string errorMessage = string.Empty;
        var service = new NotificacionService();
        int? idPaquete = service.IngresaPaquete(selectedNotificaciones, RUV.Current.Usuario.ID, ref errorMessage);
        if (!idPaquete.HasValue || idPaquete == 0)
        {
            ModalPopUp.MostrarMensaje(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error,
                                      string.Format(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Errores.General, errorMessage));
            return;
        }
        ViewState["IDPaquete"] = idPaquete;
        grdNotificaciones.DataBind();
        lblTextoPaquete.Text = string.Format("Paquete generado exitosamente con código '{0}', incluye {1} notificaciones", idPaquete, selectedNotificaciones.Count);
        hlkDetallePaquete.NavigateUrl = string.Format("PaqueteDetalle.aspx?id={0}", idPaquete);
        mdlPopupExcel.Show();
    }

    protected void btnGuardarExcel_Click(object sender, EventArgs e) {
        mdlPopupExcel.Hide();
        ExportarExcel();
        lblTextoPaquete.Text = string.Empty;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Genera el archivo excel con las notificaciones del paquete
    /// </summary>
    private void ExportarExcel() {
        ExcelHelper eh = new ExcelHelper();
        byte[] excel = eh.ExportToExcel<clsNotificacionExcel>(ObtenerNotificacionesPaquete(ViewState["IDPaquete"] as int? ?? 0));

        Response.Clear();
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("Content-Length", excel.Length.ToString());
        Response.AddHeader("Content-Disposition", "attachment;filename=PaqueteEnvioNotificaciones(" + ViewState["IDPaquete"] + ").xlsx");

        Response.BinaryWrite(excel);
        Response.End();
    }

    /// <summary>
    /// Retorna el listado de clsNotificacionExcel que se incluiran en el exel que conforma el paquete de notificaciones
    /// </summary>
    /// <param name="listaIdNotificaciones"></param>
    /// <returns></returns>
    private List<clsNotificacionExcel> ObtenerNotificacionesPaquete(int idPaquete)
    {
        List<clsNotificacionExcel> listaNotificacionesExcel = new List<clsNotificacionExcel>();
        var service = new NotificacionService();
        string errorMessage = string.Empty;
        var notificacionesPaquete = service.ObtenerDetallePaquete(idPaquete, 0, int.MaxValue, ref errorMessage);
        foreach (var notificacion in notificacionesPaquete) {
            clsNotificacionExcel notificacionExcel = new clsNotificacionExcel()
            {
                NombreDeclarante = notificacion.NOMBRECOMPLETO,
                Direccion = notificacion.DIRECCIONNOTIFICACION,
                GeografiaNotificacionExcel = notificacion.NOMBREMUNICIPIO + "-" + notificacion.NOMBREDEPARTAMENTO,
                PesoSobre = "50",
                Referencia = "REGISTRO Y VALORACION",
                RelacionIdNotificacion = notificacion.ID.ToString(),
                RelacionIdCodigoorfeo = string.Format("{0} - {1}", notificacion.CodigoOrfeo, notificacion.NUMERODOCUMENTO)
            };

            listaNotificacionesExcel.Add(notificacionExcel);
        }

        return listaNotificacionesExcel;
    }

    #endregion

    #region Web Methods

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool GuardarDireccionCorrespondencia(string idNotificacion, string idPais, string idDepartamento, string idMunicipio, string direccion) {
        int idNotificacionValue, idPaisValue, idDepartamentoValue, idMunicipioValue;
        if (int.TryParse(idNotificacion, out idNotificacionValue) && int.TryParse(idPais, out idPaisValue) && int.TryParse(idDepartamento, out idDepartamentoValue) && int.TryParse(idMunicipio, out idMunicipioValue)) {
            var service = new NotificacionService();
            var errorMessage = string.Empty;

            var notificacion = service.ObtenerNotificacionPorId(int.Parse(idNotificacion), ref errorMessage);
            if (notificacion == null) {
                return false;
            }

            notificacion.NID_PAIS = idPaisValue;
            notificacion.NID_DEPARTAMENTO = idDepartamentoValue;
            notificacion.NID_MUNICIPIO = idMunicipioValue;
            notificacion.CDIRECCIONNOTIFICACION = direccion;

            /// Diego Alvarez - 06/09/2013 - Se debe eliminar el archivo existente para crear uno nuevo con la información modificada
            EliminarArchivoAlActualizar(idNotificacion);

            return service.ActualizarPuntoNotificacion(notificacion, ref errorMessage);
        }
        return false;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool GuardarPuntoNotificacion(string idNotificacion, string puntoNotificacion, string direccion) { 
        int idNotificacionValue;
        if (int.TryParse(idNotificacion, out idNotificacionValue)) {
            var service = new NotificacionService();
            var swGeografia = new GeneralService();

            var errorMessage = string.Empty;

            var notificacion = service.ObtenerNotificacionPorId(int.Parse(idNotificacion), ref errorMessage);
            if (notificacion == null) {
                return false;
            }

            int idPuntoAtencion, idDireccionTerritorial;
            if (puntoNotificacion.StartsWith("PA") && int.TryParse(puntoNotificacion.Substring(3), out idPuntoAtencion)) {
                notificacion.IdPuntoAtencion = idPuntoAtencion;
                notificacion.IdDireccionTerritorial = null;
            }
            else if (puntoNotificacion.StartsWith("DT") && int.TryParse(puntoNotificacion.Substring(3), out idDireccionTerritorial)) {
                notificacion.IdDireccionTerritorial = idDireccionTerritorial;
                notificacion.IdPuntoAtencion = null;
            }

            EliminarArchivoAlActualizar(idNotificacion);

            if (notificacion.IdPuntoAtencion.HasValue)
                swGeografia.ActualizarDireccionPuntoNotificacion(notificacion.IdPuntoAtencion.Value, (int)ePuntoNotificacion.PuntoAtencion, direccion, ref errorMessage);
            else if (notificacion.IdPuntoAtencion.HasValue)
                swGeografia.ActualizarDireccionPuntoNotificacion(notificacion.IdDireccionTerritorial.Value, (int)ePuntoNotificacion.DireccionTerritorial, direccion, ref errorMessage);                
            
            return service.ActualizarPuntoNotificacion(notificacion, ref errorMessage);
        }
        return false;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static List<clsGeografiaCompleta> ObtenerDepartamentosPorPais(string idPais) {
        int idPaisValue = 0;
        if (int.TryParse(idPais, out idPaisValue)) {
            string errorMessage = string.Empty;
            return new GeneralService().ObtenerDepartamentosPorPais(idPaisValue, ref errorMessage);
        }
        return null;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static List<clsGeografiaCompleta> ObtenerMunicipiosPorDepartamento(string idDepartamento) {
        int idDepartamentoValue = 0;
        if (int.TryParse(idDepartamento, out idDepartamentoValue)) {
            string errorMessage = string.Empty;
            return new GeneralService().ObtenerMunicipiosPorDepartamento(idDepartamentoValue, ref errorMessage);
        }
        return null;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static List<clsPuntoNotificacion> ObtenerPuntosNotificacionPorMunicipio(string idMunicipio)
    {
        int idMunicipioValue = 0;
        if (int.TryParse(idMunicipio, out idMunicipioValue)) {
            return new GeneralService().ObtenerPuntosAtencionyDTPorMunicipio(idMunicipioValue);
        }
        return null;
    }

    /// <summary>
    /// Consulta que retorna la dirección de determinado punto de atención
    /// </summary>
    /// <param name="idPuntoNotificacion">Id del punto de atención</param>
    /// <returns>Dirección punto de atención</returns>
    /// <remarks>ivan.suarez@globant.com 12/09/2013</remarks>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string ObtenerDireccionPorPuntoNotificacion(string idPuntoNotificacion, int tipoPuntoNotificacion)
    {
       int idPuntoNotificacionValue = 0;
        if (int.TryParse(idPuntoNotificacion, out idPuntoNotificacionValue))
        {
            return new GeneralService().ObtenerDireccionPuntoNotificacion(idPuntoNotificacionValue, tipoPuntoNotificacion);
        }
        return string.Empty;
    }

    #endregion Web Methods

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

    private void BindGridView()
    {
        //var records = dataSourceController.ObtenerNotificaciones(GridPager.CurrentPageNumber, GridPager.CurrentPageSize, string.Empty);
        //var recordCount = dataSourceController.CantidadNotificaciones();

        //grdNotificaciones.PageSize = GridPager.CurrentPageSize;
        //grdNotificaciones.DataSource = records;
        //grdNotificaciones.DataBind();

        //GridPager.TotalPages = recordCount % GridPager.CurrentPageSize == 0 ? recordCount / GridPager.CurrentPageSize : recordCount / GridPager.CurrentPageSize + 1;
    }

    private static void EliminarArchivoAlActualizar(string idNotificacion)
    {
        var notificacionDataSource = new DataSourceNotificacionDetalle();
        notificacionDataSource.IdNotificacion = int.Parse(idNotificacion);
        var notificacion = notificacionDataSource.DetalleData();

        int nIdDeclaracion = notificacion.nIdDeclaracion;

        string error = string.Empty;
        var valoracionId = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(nIdDeclaracion, ref error);

        if (string.IsNullOrEmpty(error))
        {
            string Ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            string NombreFolder = valoracionId.ToString();
            string NombreArchivo = "Citacion" + ".pdf";

            //Verifica que exista el archivo y lo elimina
            if (File.Exists(Ruta + NombreFolder + "/" + NombreArchivo))
            {
                File.Delete(Ruta + NombreFolder + "/" + NombreArchivo);
            }
        }
    }

}