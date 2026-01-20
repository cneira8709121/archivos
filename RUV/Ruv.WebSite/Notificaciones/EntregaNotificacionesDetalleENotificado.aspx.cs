using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WebSite.Common;
using Ruv.WebSite.DataSources.Notificaciones;
using msg = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using resx = Ruv.Infrastructure.Crosscutting.Resources;

public partial class Notificaciones_EntregaNotificacionesDetalleENotificado : PaginaBase
{
    protected int IdNotificacion
    {
        get
        {
            return Request.QSIntegerField("id") ?? 0;
        }
    }

    #region Page Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region DataBound Event Handlers
    
    protected void ObjectDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {

        var dataSourceController = e.ObjectInstance as DataSourceNotificacionDetalle;
        dataSourceController.IdNotificacion = this.IdNotificacion;
    }

    #endregion

    #region Action Event Handlers

    protected void BtnDescargarConstanciaEntrega_Click(object sender, EventArgs e)
    {
        string Ruta = HttpContext.Current.Server.MapPath(resx::General.RutaFormatoConstanciaAtencion);
        if (File.Exists(Ruta))
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Ruta));
            Response.WriteFile(Ruta);
            Response.Flush();
            Response.End();
        }
    }

    protected void BtnDescargarResolucion_Click(object sender, EventArgs e)
    {
        string cError = string.Empty;
        var notificacion = new NotificacionService().DetalleNotificacion(this.IdNotificacion);
        if (notificacion != null)
        {
            int nIdValoracion = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(notificacion.nIdDeclaracion, ref cError);
            string Ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            string NombreFolder = nIdValoracion.ToString();
            string NombreArchivo = "Resolucion" + ".pdf";

            //Verifica que exista el archivo, y lo regenera en caso de ser necesario
            if (!File.Exists(Ruta + NombreFolder + "/" + NombreArchivo))
            {
                ActosAdminService actosAdminService = new ActosAdminService();
                actosAdminService.GenerarDocumentoValoracion(nIdValoracion, true, ref cError);
            }

            if (File.Exists(Ruta + NombreFolder + "/" + NombreArchivo))
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
                Response.WriteFile(Ruta + NombreFolder + "/" + NombreArchivo);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        //Subir CA
        if (fuCargarFCA.HasFile)
        {
            try
            {
                string fileName = "ConstanciaAtencion_" + Request.QueryString["id"] + Path.GetExtension(fuCargarFCA.FileName);
                fuCargarFCA.SaveAs(ConfigurationManager.AppSettings["PathArchivosNotificaciones"] + fileName);
            }
            catch (System.Web.HttpException ex)
            {
                RegistroTraza.I.Registrar(ex);
                ModalPopUp.MostrarMensaje(msg::Controles.Error,
                    string.Format(msg::Errores.General, ex.Message));
                return;
            }
        }

        //Finalizar marcando como "Notificado"
        NotificacionService service = new NotificacionService();
        string cError = string.Empty;
        try
        {
            int nIdNotificacion = int.Parse(Request.QueryString["id"]);
            int DiasHabiles = int.Parse(ConfigurationManager.AppSettings["PlazoPlanB"].ToString());
            service.CambiarEstadoNotificacion(nIdNotificacion, (int)eEstadosNotificacion.NotificadoPersonal,DiasHabiles,txtObservacionNotificacion.Text, ref cError);
            if (string.IsNullOrEmpty(cError))
            {
                ModalPopUp.MostrarMensajeYRedirigir(msg::Controles.Exito, msg::Informacion.CambiosGuardados, "NotificacionesEntregadas.aspx");
            }
            else
            {
                ModalPopUp.MostrarMensaje(msg::Controles.Error, string.Format(msg::Errores.General, cError));
                return;
            }
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            ModalPopUp.MostrarMensaje(msg::Controles.Error, string.Format(resx::Globalization.Errores.General, ex.Message));
            return;
        }
    }

    protected void btnAtras_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["urlEvio"] == null) Response.Redirect("../NotificacionesEntregadas.aspx");
        else Response.Redirect("../" + Request.QueryString["urlEvio"]);
    }

    #endregion

    protected void BtnDesfijarEdicto_Click(object sender, EventArgs e)
    {
        NotificacionService service = new NotificacionService();
        string cError = string.Empty;
        try
        {
            int nIdNotificacion = int.Parse(Request.QueryString["id"]);
            int DiasHabiles = int.Parse(ConfigurationManager.AppSettings["PlazoPlanB"].ToString());
            //edicto publicado esta bien
            var Confirmar = service.ConfirmarDesfijarEdicto(nIdNotificacion, (int)eEstadosNotificacion.NotificadoEdicto,txtObservacionNotificacion.Text, ref cError);
            if (string.IsNullOrEmpty(cError) && Confirmar)
                ModalPopUp.MostrarMensaje("Exito", "la accion se realizo satisfactoriamente");
            else
                ModalPopUp.MostrarMensaje("Error", "No se pudo relizar la accion debido al siguiente error :" + cError);

        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            ModalPopUp.MostrarMensaje(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error,
                string.Format(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Errores.General, ex.Message));
            return;
        }
    }
}