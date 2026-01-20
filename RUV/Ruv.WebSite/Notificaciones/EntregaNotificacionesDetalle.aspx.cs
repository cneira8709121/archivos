using System;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using Ruv.WebSite.Common;
using Ruv.WebSite.DataSources.Notificaciones;

public partial class Notificaciones_EntregaNotificaciones : PaginaBase
{

    protected int IdNotificacion { get { return Request.QSIntegerField("id") ?? 0; } }

    #region Page Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region Databound Event Handlers

    protected void ObjectDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        var dataSourceController = e.ObjectInstance as DataSourceNotificacionDetalle;
        dataSourceController.IdNotificacion = this.IdNotificacion;
    }

    #endregion

    #region Action Event Handlers

    protected void BtnDescargarDNP_Click(object sender, EventArgs e)
    {
        var fileName = "Notificacion.pdf";
        var filePath = ObtenerRutaActoAdministrativo(fileName);
        EnviarArchivoABrowser(filePath, fileName, "application/pdf");
    }

    protected void BtnDescargarResolucion_Click(object sender, EventArgs e)
    {
        var fileName = "Resolucion.pdf";
        var filePath = ObtenerRutaActoAdministrativo(fileName);
        EnviarArchivoABrowser(filePath, fileName, "application/pdf");
    }

    protected void BtnSubirDNP_Click(object sender, EventArgs e)
    {
        string errorMessage = string.Empty;
        try { 
            if (fuCargarReporte.HasFile || !string.IsNullOrEmpty(ObservacionNotificacion.Text)) {
                if (fuCargarReporte.HasFile) {
                    fuCargarReporte.SaveAs(ConfigurationManager.AppSettings["PathArchivosNotificaciones"] + fuCargarReporte.FileName);
                }

                var service = new NotificacionService();
                service.ObservacionNotificacion(this.IdNotificacion, ObservacionNotificacion.Text, ref errorMessage);
                service.CierraNotificacion(this.IdNotificacion, ref errorMessage);

                if (string.IsNullOrEmpty(errorMessage))
                    ModalPopUp.MostrarMensajeYRedirigir("Exito", "Se realizo la accion exitosamente", "NotificacionesEntregadas.aspx");
                else
                    ModalPopUp.MostrarMensaje("Error", "No se pudo realizar la accion:" + errorMessage);
            }
            else {
                ModalPopUp.MostrarMensaje("Error", "Debe seleccionar un archivo, o ingresar comentarios");
            }
        }
        catch (Exception ex) {
            RegistroTraza.I.Registrar(ex);
            ModalPopUp.MostrarMensaje("Error", "No se pudo realizar la accion:" + ex.Message);
        }
    }

    protected void btnAtras_Click(object sender, EventArgs e)
    {
        Response.Redirect("NotificacionesEntregadas.aspx");
    }

    #endregion

    #region Private Methods

    private string ObtenerRutaActoAdministrativo(string nombreArchivo) {
        string errorMessage = string.Empty;
        var notificacion = new NotificacionService().DetalleNotificacion(this.IdNotificacion);
        if (notificacion != null) {
            int idValoracion = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(notificacion.nIdDeclaracion, ref errorMessage);
            string pathArchivosActosAdmin = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            string nombreFolder = idValoracion.ToString();

            // Verifica que exista el archivo, y lo regenera en caso de ser necesario
            var filePath = string.Format("{0}{1}/{2}", pathArchivosActosAdmin, nombreFolder, nombreArchivo);
            if (!File.Exists(filePath)) {
                new ActosAdminService().GenerarDocumentoValoracion(idValoracion, true, ref errorMessage);
            }

            return filePath;
        }
        InvalidOperationException invalidOperationException = new InvalidOperationException("No se pudo obtener información de la notificación");
        RegistroTraza.I.Registrar(invalidOperationException);
        throw invalidOperationException;
    }

    private void EnviarArchivoABrowser(string filePath, string fileName, string contentType) {
        if (File.Exists(filePath))
        {
            Response.Clear();
            Response.ContentType = contentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.WriteFile(filePath);
            Response.Flush();
            Response.End();
        }
    }

    #endregion

}