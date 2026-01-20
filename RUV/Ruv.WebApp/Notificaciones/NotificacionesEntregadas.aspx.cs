using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WebApp.Common;
using Ruv.WebApp.DataSources.Notificaciones;
using System.Configuration;

public partial class Notificaciones_NotificacionesEntregadas : PaginaBase
{

    #region Page Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QSStringField("estadoNotificacion") != null)
                this.filterEstado.Text = Request.QSStringField("estadoNotificacion");

            if (Request.QSStringField("declaracion") != null)
                this.filterDeclaracion.Text = Request.QSStringField("declaracion");

            if (Request.QSIntegerField("tipoDocumento") != null)
                this.filterTipoDocumento.SelectedValue = Request.QSIntegerField("tipoDocumento").Value.ToString();

            if (Request.QSStringField("documento") != null)
                this.filterDocumento.Text = Request.QSStringField("documento");

            if (Request.QSStringField("nombreDeclarante") != null)
                this.filterNombreDeclarante.Text = Request.QSStringField("nombreDeclarante");

            string errorMessage = string.Empty;

            this.filterEstado.DataSource = new NotificacionService().ObtenerEstadosDeNotificacion(ref errorMessage);
            this.filterEstado.DataBind();
            this.filterEstado.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));

            this.filterTipoDocumento.DataSource = new GeneralService().ObtenerParametros((int)eTipoParametros.TipoDeDocumentoDeIdentidad, ref errorMessage);
            this.filterTipoDocumento.DataBind();
            this.filterTipoDocumento.Items.Insert(0, new ListItem("-- Seleccione uno --", string.Empty));
        }
    }

    #endregion

    #region Databound Event Handlers

    protected void NotificacionesEntregadas_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        var dataSourceController = e.ObjectInstance as DataSourceNotificacionesEntregadas;
        if (dataSourceController != null)
        {
            dataSourceController.Declaracion = Request.QSStringField("declaracion");
            dataSourceController.TipoDocumento = Request.QSIntegerField("tipoDocumento");
            dataSourceController.Documento = Request.QSStringField("documento");
            dataSourceController.NombreDeclarante = Request.QSStringField("nombreDeclarante");
            dataSourceController.EstadoNotificacion = Request.QSIntegerField("estadoNotificacion");
        }
    }

    protected void grdNotificaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idNotificacion = 0, idComando = 0;
        if (int.TryParse(e.CommandArgument.ToString(), out idNotificacion) && int.TryParse(e.CommandName, out idComando))
        {
            switch (idComando)
            {
                case (int)eEstadosNotificacion.NotificacionEntregada:
                    Response.Redirect(string.Format("EntregaNotificacionesDetalle.aspx?id={0}", idNotificacion.ToString()));
                    break;

                case (int)eEstadosNotificacion.PendientePublicacion:
                    Response.Redirect(string.Format("PendientePublicacionEdicto.aspx?id={0}", idNotificacion.ToString()));
                    break;
                case (int)eEstadosNotificacion.PendienteDespublicacion:
                    Response.Redirect(string.Format( "EntregaNotificacionesDetalleENotificado.aspx?id={0}", idNotificacion.ToString()));
                    break;
                case (int)eEstadosNotificacion.NotificacionRechazada:
                    Response.Redirect(string.Format("FueraTerminosNotificacionesDetalle.aspx?id={0}&urlEvio={1}", idNotificacion.ToString(), this.Request.Url.AbsolutePath));
                    break;
                case (int)eEstadosNotificacion.NotificacionEstadoPorValidar:
                    Response.Redirect(string.Format("FueraTerminosNotificacionesDetalle.aspx?id={0}&urlEvio={1}", idNotificacion.ToString(), this.Request.Url.AbsolutePath));
                    break;

                case (int)eEstadosNotificacion.NotificadoPersonal:
                //case (int)eEstadosNotificacion.NotificadoEdicto:
                //    Response.Redirect(string.Format("EntregaNotificacionesDetalleENotificado.aspx?id={0}&urlEvio={1}", idNotificacion.ToString(), this.Request.Url.AbsolutePath));
                    break;
            }
        }
    }

    #endregion

    #region Action Event Handlers

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        string redirectionUrl = "NotificacionesEntregadas.aspx";

        if (!string.IsNullOrWhiteSpace(this.filterDeclaracion.Text))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("declaracion", this.filterDeclaracion.Text.Trim());

        if (!string.IsNullOrEmpty(this.filterTipoDocumento.SelectedValue))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("tipoDocumento", this.filterTipoDocumento.SelectedValue);

        if (!string.IsNullOrWhiteSpace(this.filterDocumento.Text))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("documento", this.filterDocumento.Text.Trim());

        if (!string.IsNullOrWhiteSpace(this.filterNombreDeclarante.Text))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("nombreDeclarante", this.filterNombreDeclarante.Text.Trim());

        if (!string.IsNullOrEmpty(this.filterEstado.SelectedValue))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("estadoNotificacion", this.filterEstado.SelectedValue);

        Response.Redirect(redirectionUrl);
    }

    protected void btnRestaurarFiltros_Click(object sender, EventArgs e)
    {
        Response.Redirect("NotificacionesEntregadas.aspx");
    }

    #endregion

}