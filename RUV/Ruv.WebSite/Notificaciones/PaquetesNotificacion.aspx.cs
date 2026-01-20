using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.WebSite.Common;
using Ruv.WebSite.DataSources.Notificaciones;

public partial class Notificaciones_PaquetesNotificacion : PaginaBase
{
    #region Page Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            if (Request.QSStringField("ordenServicio") != null)
                this.filterOrdenDeServicio.Text = Request.QSStringField("ordenServicio");

            if (Request.QSDateField("fechaInicio", Dates.ShortQSDateFormat) != null)
                this.filterFechaInicio.Text = Request.QSDateField("fechaInicio", Dates.ShortQSDateFormat).Value.ToString(Dates.ShortDateFormat);

            if (Request.QSDateField("fechaFin", Dates.ShortQSDateFormat) != null)
                this.filterFechaFin.Text = Request.QSDateField("fechaFin", Dates.ShortQSDateFormat).Value.ToString(Dates.ShortDateFormat);
        }
    }

    #endregion

    #region Databound Event Handlers

    protected void odsPaquetes_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        var dataSourceController = e.ObjectInstance as DataSourcePaquetesNotificacion;
        if (dataSourceController != null) {
            dataSourceController.OrdenServicio = Request.QSStringField("ordenServicio");
            dataSourceController.FechaInicio = Request.QSDateField("fechaInicio", Dates.ShortQSDateFormat);
            dataSourceController.FechaFin = Request.QSDateField("fechaFin", Dates.ShortQSDateFormat);
        }
    }

    #endregion

    #region Action Event Handlers

    protected void grdPaquetes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detalle")
            Response.Redirect(string.Format("PaqueteDetalle.aspx?id={0}", int.Parse(e.CommandArgument.ToString()).ToString()));
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        string redirectionUrl = "PaquetesNotificacion.aspx";
        
        if (this.filterOrdenDeServicio.Text.Trim() != string.Empty)
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("ordenServicio", this.filterOrdenDeServicio.Text.Trim());

        DateTime fechaInicioValue = DateTime.MinValue;
        if (this.filterFechaInicio.Text != string.Empty && DateTime.TryParseExact(this.filterFechaInicio.Text.Trim(), Dates.ShortDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out fechaInicioValue))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("fechaInicio", fechaInicioValue.ToString(Dates.ShortQSDateFormat));

        DateTime fechaFinValue = DateTime.MaxValue;
        if (this.filterFechaFin.Text != string.Empty && DateTime.TryParseExact(this.filterFechaFin.Text.Trim(), Dates.ShortDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out fechaFinValue))
            redirectionUrl = redirectionUrl.AppendQueryStringParameter("fechaFin", fechaFinValue.ToString(Dates.ShortQSDateFormat));

        Response.Redirect(redirectionUrl);
    }

    protected void btnRestaurarFiltros_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaquetesNotificacion.aspx");
    }

    #endregion
}