using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using Ruv.Business.DTO.Notificacion;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.WebSite.Common;
using Ruv.WebSite.DataSources.Notificaciones;
using msg = Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using System.Configuration;

public partial class Notificaciones_EntregaNotificaciones : PaginaBase
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
        if (!Page.IsPostBack) { 
            var isLiderValoracion = RUV.Current.Usuario.RolesUsuario.Contains(eRolesUsuario.LiderNotificaciones);
            this.BtnDescargarEdicto.Visible = isLiderValoracion;
            this.MarcarEdicto.Visible = isLiderValoracion;
            //this.btnMarcarNotificado.Visible = isLiderValoracion;
        }
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

    //protected void BtnDescargarResolucion_Click(object sender, EventArgs e)
    //{
    //    string cError = string.Empty;
    //    var notificacion = new NotificacionService().DetalleNotificacion(this.IdNotificacion, ref cError);
    //    if (notificacion != null)
    //    {
    //        int nIdValoracion = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(notificacion.nIdDeclaracion, ref cError);
    //        string Ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
    //        string NombreFolder = nIdValoracion.ToString();
    //        string NombreArchivo = "Resolucion" + ".pdf";

    //        //Verifica que exista el archivo, y lo regenera en caso de ser necesario
    //        if (!File.Exists(Ruta + NombreFolder + "/" + NombreArchivo))
    //        {
    //            ActosAdminService actosAdminService = new ActosAdminService();
    //            actosAdminService.GenerarDocumentoValoracion(nIdValoracion, true, ref cError);
    //        }

    //        if (File.Exists(Ruta + NombreFolder + "/" + NombreArchivo))
    //        {
    //            Response.Clear();
    //            Response.ContentType = "application/pdf";
    //            Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
    //            Response.WriteFile(Ruta + NombreFolder + "/" + NombreArchivo);
    //            Response.Flush();
    //            Response.End();
    //        }
    //    }
    //}

    protected void BtnDescargarEdicto_Click(object sender, EventArgs e)
    {
        string cError = string.Empty;
        var notificacion = new NotificacionService().DetalleNotificacion(this.IdNotificacion);
        if (notificacion != null)
        {
            int nIdValoracion = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(notificacion.nIdDeclaracion, ref cError);
            string Ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            string NombreFolder = nIdValoracion.ToString();

            var fileHandle = new FileInfo(Ruta + NombreFolder + "/" + "Aviso" + ".pdf");
            if (!fileHandle.Exists)
                fileHandle = new FileInfo(Ruta + NombreFolder + "/" + "Edicto" + ".pdf");
            
            // Verifica que exista el archivo, y lo regenera en caso de ser necesario
            if (!fileHandle.Exists) {
                ActosAdminService actosAdminService = new ActosAdminService();
                actosAdminService.GenerarDocumentoValoracion(nIdValoracion, true, ref cError);
            }

            if (fileHandle.Exists) {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileHandle.Name);
                Response.WriteFile(fileHandle.FullName);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void BtnEdictoPublicado_Click(object sender, EventArgs e)
    {
        string cError = string.Empty;
        var notificacion = new NotificacionService().DetalleNotificacion(this.IdNotificacion);
        if (notificacion != null)
        {
            int nIdValoracion = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(notificacion.nIdDeclaracion, ref cError);
            string Ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            string NombreFolder = nIdValoracion.ToString();

            var fileHandle = new FileInfo(Ruta + NombreFolder + "/" + "Aviso" + ".pdf");
            if (!fileHandle.Exists)
                fileHandle = new FileInfo(Ruta + NombreFolder + "/" + "Edicto" + ".pdf");

            // Verifica que exista el archivo, y lo regenera en caso de ser necesario
            if (!fileHandle.Exists)
            {
                ActosAdminService actosAdminService = new ActosAdminService();
                actosAdminService.GenerarDocumentoValoracion(nIdValoracion, true, ref cError);
            }

            if (fileHandle.Exists)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileHandle.Name);
                Response.WriteFile(fileHandle.FullName);
                Response.Flush();
                Response.End();
            }
        }
    }

   

    protected void btnAtras_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["urlEvio"] == null) Response.Redirect("../NotificacionesEntregadas.aspx");
        else Response.Redirect("../" + Request.QueryString["urlEvio"]);
    }

    #endregion
    protected void btnMarcarNotificado_Click(object sender, EventArgs e)
    {

    }
    protected void MarcarEdicto_Click(object sender, EventArgs e)
    {
        NotificacionService service = new NotificacionService();
        string cError = string.Empty;
        try
        {
            int nIdNotificacion = int.Parse(Request.QueryString["id"]);
            int DiasHabiles = int.Parse(ConfigurationManager.AppSettings["PlazoPlanB"].ToString());
            //edicto publicado esta bien
            var Confirmar = service.ConfirmarPublicacionEdicto(nIdNotificacion, (int)eEstadosNotificacion.EdictoPublicado, DiasHabiles, string.Empty, ref cError);
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