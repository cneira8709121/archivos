using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using Ruv.Business.DTO.Notificacion;
using Ruv.WebApp.Common;
using Ruv.WebApp.DataSources.Notificaciones;

public partial class Notificaciones_NotificacionDetalle : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void ObjectDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        var dataSourceController = e.ObjectInstance as DataSourceNotificacionDetalle;
        dataSourceController.IdNotificacion = Request.QSIntegerField("id") ?? 0;
    }

    protected void dtvDetalleNotificacion_DataBound(object sender, EventArgs e)
    {
        clsNotificacionDetalle dt = (clsNotificacionDetalle)dtvDetalleNotificacion.DataItem;
        btnAprobar.Visible = dt.nIdEstadoNotificacion == 1 && !dt.Aprobado;
        if (dt.nIdEstadoNotificacion == 15)
            BtnDescargarCitacion.Visible = false;
        else 
        {
            BtnDescargarResolucion.Visible = false;
            BtnDescargarAviso.Visible = false;
        }
    }

    #region Eventos

    protected void BtnDescargarCitacion_Click(object sender, EventArgs e) {
        DescargarArchivo("Citacion");
    }

    protected void btnAprobar_Click(object sender, EventArgs e)
    {
        ObjectDataSource.Update();
    }
    protected void BtnDescargarAviso_Click(object sender, EventArgs e)
    {
         DescargarArchivo("Aviso");
    }
    protected void BtnDescargarResolucion_Click(object sender, EventArgs e)
    {
        DescargarArchivo("Resolucion");
    }

    protected void btnAtras_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["urlEvio"] == null) Response.Redirect("../Default.aspx");
        else Response.Redirect("../" + Request.QueryString["urlEvio"]);
    }

    #endregion

    #region Privados
    /// <summary>
    /// Descarga el archivo PDF que se requiere
    /// </summary>
    /// <param name="nombre">Citación, Aviso o Resolución</param>
    private void DescargarArchivo(string nombre) 
    {
        var notificacionDataSource = new DataSourceNotificacionDetalle();
        notificacionDataSource.IdNotificacion = Request.QSIntegerField("id") ?? 0;
        var notificacion = notificacionDataSource.DetalleData();

        int nIdDeclaracion = notificacion.nIdDeclaracion;

        string error = string.Empty;
        var valoracionId = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(nIdDeclaracion, ref error);

        if (string.IsNullOrEmpty(error))
        {
            string Ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            string NombreFolder = valoracionId.ToString();
            string NombreArchivo = nombre + ".pdf";

            //Verifica que exista el archivo, y lo regenera en caso de ser necesario
            if (!File.Exists(Ruta + NombreFolder + "/" + NombreArchivo))
            {
                ActosAdminService actosAdminService = new ActosAdminService();
                actosAdminService.GenerarDocumentoValoracion(valoracionId, true, ref error);
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
    #endregion

}