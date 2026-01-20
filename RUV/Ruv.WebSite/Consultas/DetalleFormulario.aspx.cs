using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Common;

public partial class Consultas_DetalleFormulario : PaginaBase
{
    #region Private mehods

    private void ShowMessage(string sTitle, string sMessage)
    {
        mpuMensaje.Titulo = sTitle;
        mpuMensaje.Mensaje = sMessage;
        mpuMensaje.Mostrar();
    }

    #endregion
    #region Protected methods

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.IdPage = "1022";
        Master.CargarOpcionesporUrl();
       // Master.ValidarPermisoPagina();
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);

        if (Request.QueryString["id"] == null) return;

        dwDetalleDeclaracion.DataSource = null;
        HechosVictimizantesRepeater.DataSource = null;

        dwDetalleDeclaracion.DataBind();
        HechosVictimizantesRepeater.DataBind();

        string cError = string.Empty;
        ConsultarEstadoPersonaService cepService = new ConsultarEstadoPersonaService();

        Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador.clsConsultarEstadoDetalleDeclaracionRespuesta cedRespuesta = cepService.ConsultarEstadoDetalleDeclaracion(int.Parse(Request.QueryString["id"]), ref cError);

        if (cError != string.Empty)
        {
            ShowMessage(Controles.Error, Errores.General);
            return;
        }
        if (cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().nIdEstadoProceso == 10029 || cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().nIdEstadoProceso == 10002)
        {
            cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().CEstadoActualProcesotooltip = "Declaración con estado de valoración en proceso aprobación Acto Administrativo";
        }
        if (cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().nIdEstadoProceso == 10030)
        {
            cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().CEstadoActualProcesotooltip = "Declaración con acto administrativo pendiente de firma";
        }
        if (cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().nIdEstadoProceso == 10016)
        {
            cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().CEstadoActualProcesotooltip = "Solicitud que se encuentra en trámite de verificación datos de distribución y recepción del FUD";
        }
        if (cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().nIdEstadoProceso == 10015)
        {
            cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().CEstadoActualProcesotooltip = "Solicitud que está en proceso de verificación si el FUD reúne los requisitos para tener validez jurídica";
        }
        if (cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().nIdEstadoProceso == 10011)
        {
            cedRespuesta.LstDetalleDeclaracion.FirstOrDefault().CEstadoActualProcesotooltip = "Solicitud que está en proceso de verificación campos faltantes del FUD";
        }

        this.dwDetalleDeclaracion.DataSource = cedRespuesta == null ? null : cedRespuesta.LstDetalleDeclaracion;
        this.dwDetalleDeclaracion.DataBind();
        //ACA
        this.HechosVictimizantesRepeater.DataSource = cedRespuesta == null ? null : cedRespuesta.LstDetalleDeclaracion.GroupBy(x => x.nIdSiniestro);
        this.HechosVictimizantesRepeater.DataBind();
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

    protected void HechosVictimizantesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        GridView grid = e.Item.FindControl("GridViewDetalle") as GridView;
        Label hechoLabel = e.Item.FindControl("HechoVictimizanteLabel") as Label;
        var datasource = (e.Item.DataItem) as IGrouping<int, Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador.DetalleDeclaracion>;
        if (grid != null && datasource != null)
        {
           // hechoLabel.Text = datasource.Key;
            var firstElement = datasource.FirstOrDefault();
            hechoLabel.Text = firstElement != null ? firstElement.CHechoVictimizante : "Sin Hecho Victimizante";
            grid.DataSource = datasource.ToList();
            grid.DataBind();
        }
    }

    protected void GridViewDetalle_PageIndexChanging(object sender, EventArgs e)
    {
    }

    #endregion

    #endregion
}