using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Business.DTO.Notificacion;
using System.IO;
using System.Configuration;
using Ruv.WebSite.Common;
using Ruv.WebSite.DataSources.Notificaciones;
using msg = Ruv.Infrastructure.Crosscutting.Resources.Globalization;

public partial class Notificaciones_NotificacionDetalleEntregados : PaginaBase
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
        string cError = string.Empty;
        var notificacion = new NotificacionService().DetalleNotificacion(this.IdNotificacion);
        if (notificacion != null)
        {
            int nIdValoracion = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(notificacion.nIdDeclaracion, ref cError);
            string Ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            string NombreFolder = nIdValoracion.ToString();
            string NombreArchivo = "Notificacion" + ".pdf";
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
    
    protected void BtnSubirDNP_Click(object sender, EventArgs e)
    {
        bool result = false;
        if (fuCargarReporte.HasFile)
        {
            string cError = string.Empty;
            NotificacionService service = new NotificacionService();

            try
            {
                fuCargarReporte.SaveAs(ConfigurationManager.AppSettings["PathArchivosNotificaciones"] + fuCargarReporte.FileName);
                result = true;
            }
            catch
            {
                ModalPopUp.MostrarMensaje(msg::Controles.Error, msg::Errores.General);
                return;
            }

            if (result == true)
            {
                try
                {
                    int nIdNotificacion = int.Parse(Request.QueryString["id"]);
                    service.CierraNotificacion(nIdNotificacion, ref cError);
                    ModalPopUp.MostrarMensaje(msg::Controles.Exito, msg::Informacion.CambiosGuardados);
                }
                catch
                {
                    ModalPopUp.MostrarMensaje(msg::Controles.Error, msg::Errores.General);
                    return;
                }
            }
        }
    }

    #endregion
}