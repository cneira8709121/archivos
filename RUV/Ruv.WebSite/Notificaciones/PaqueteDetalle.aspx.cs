using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Business.DTO.Notificacion;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Utilities; 

public partial class Notificaciones_PaqueteDetalle : PaginaBase
{
    public int IdPaqueteNotificacion {
        get {
            int id = 0;
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out id)) return id;
            return -1;
        }
    }

    #region Page Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) 
        {
            //btnDescargarExcel.Enabled = false;
            btnDescargarCitaciones.Enabled = false;
            btnAsociarOrdenServicio.Enabled = false;
            btnConfirmar.Enabled = false;
            btnAsociarCodigoGuia.Enabled = false;
            fuCargacodigos.Enabled = false;
            fuCargarReporte.Enabled = false;
            btnCargarReporte.Enabled = false;

            NotificacionService service = new NotificacionService();
            string cError = string.Empty;
            int numeroNotificaciones = service.ObtenerDetallePaqueteConteo(IdPaqueteNotificacion, ref cError);
            List<clsNotificacion> notificaciones = service.ObtenerDetallePaquete(IdPaqueteNotificacion, 1, numeroNotificaciones, ref cError);

            if (string.IsNullOrEmpty(cError) && notificaciones != null && notificaciones.Count > 0) {
                /// Paquete NO tiene OrdenDeServicio
                if (string.IsNullOrEmpty(notificaciones.FirstOrDefault().ordenServicio))
                {
                    ///  Deshabilitar todos excepto asociar orden de servicio;
                    btnAsociarOrdenServicio.Enabled = true;
                    //btnDescargarExcel.Enabled = true;
                }
                /// Paquete tiene OrdenDeServicio pero por lo menos una notificacion no tiene código de guía
                else if (!string.IsNullOrEmpty(notificaciones.FirstOrDefault().ordenServicio) && notificaciones.Any(x => string.IsNullOrEmpty(x.cIdCodigoGuia))) {
                    btnAsociarCodigoGuia.Enabled = true;
                    fuCargacodigos.Enabled = true;
                }
                /// Todas las notificaciones tienen codigos de guía pero por lo menos una no está en estado >= Enviado
                else if (notificaciones.All(x => !string.IsNullOrEmpty(x.cIdCodigoGuia)) && notificaciones.Any(x => x.CODIGOESTADONOTIFICACION < (int)eEstadosNotificacion.Enviado)) {
                    ///  Deshabilitar todos excepto Descargar Citaciones y Confirmar Envío;
                    btnConfirmar.Enabled = true;
                    btnDescargarCitaciones.Enabled = true;
                }
                /// Cuando el estado es enviado y ya se han asignado los códigos guia, se deben poder cargar archivo 4-72
                else if(notificaciones.Any(x => x.CODIGOESTADONOTIFICACION == (int)eEstadosNotificacion.Enviado))
                {
                    ///  Deshabilitar todos excepto cargar estados de envio (pilas, esto es un FileUpload)
                    fuCargarReporte.Enabled = true;
                    btnCargarReporte.Enabled = true;
                }
            }
        }
    }

    #endregion

    #region Databound Event Handlers

    protected void odsPaquete_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        var dataSourceController = e.ObjectInstance as DataSourcePaqueteNotificacion;
        if (dataSourceController != null) {
            dataSourceController.IdPaqueteNotificacion = this.IdPaqueteNotificacion;
        }
    }

    protected void odsNotificacionesPaquete_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        var dataSourceController = e.ObjectInstance as DataSourcePaqueteNotificacionDetalle;
        if (dataSourceController != null) {
            dataSourceController.IdPaqueteNotificacion = this.IdPaqueteNotificacion;
        }
    }

    #endregion

    #region Action Event Handlers

    protected void btnDescargarExcel_Click(object sender, EventArgs e)
    {
        string errorMessage = string.Empty;
        var file = ExportarNotificacionesPaqueteAExcel(ref errorMessage);
        if (file == null || errorMessage != string.Empty) {
            
        }
        else {
            var service = new NotificacionService();
            var infoPaquete = service.ObtenerPaquete(this.IdPaqueteNotificacion, ref errorMessage);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment;filename=PaqueteEnvioNotificaciones(" + (infoPaquete.OrdenServicio != null ? "Orden-" + infoPaquete.OrdenServicio : this.IdPaqueteNotificacion.ToString()) + ").xlsx");
            
            Response.BinaryWrite(file);
            Response.End();
        }
    }

    protected void btnDescargarCitaciones_Click(object sender, EventArgs e)
    {
        string errorMessage = string.Empty;
        var file = ExportarNotificacionesPaqueteCitaciones(ref errorMessage);
        if (file == null || errorMessage != string.Empty) { 
        
        }
        else {
            var service = new NotificacionService();
            var infoPaquete = service.ObtenerPaquete(this.IdPaqueteNotificacion, ref errorMessage);

            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment;filename=PaqueteCitaciones(" + (infoPaquete.OrdenServicio != null ? "Orden-" + infoPaquete.OrdenServicio : this.IdPaqueteNotificacion.ToString()) + ").zip");

            Response.BinaryWrite(file);
            Response.End();
        }
    }

    protected void btnAsociarOrdenServicio_Click(object sender, EventArgs e)
    { 
    
    }

    protected void btnGuardarOrdenServicio_Click(object sender, EventArgs e)
    {
        string errorMessage = string.Empty;
        var service = new NotificacionService();
        if (service.AgregaOrdenServicioService(this.IdPaqueteNotificacion, this.txtOrdenServicio.Text, ref errorMessage))
        {
            Response.Redirect(string.Format("PaqueteDetalle.aspx?id={0}", this.IdPaqueteNotificacion.ToString()));
        }
        else
        { 
        
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaquetesNotificacion.aspx");
    }

    protected void btnCargarReporte_Click(object sender, EventArgs e)
    {
        
        //var truthpath = ConfigurationManager.AppSettings["PathReporteCourier"] + path.ToString();
        //var directory = new DirectoryInfo(truthpath);

        //if (directory.Exists == false)
        //{
        //    directory.Create();
        if (fuCargarReporte.HasFile)
        {
            string fileName = NextAvailableFilename((ConfigurationManager.AppSettings["PathReporteCourier"] + "/" + fuCargarReporte.FileName));
            string cError = string.Empty;
            NotificacionService service = new NotificacionService();

            try
            {
                
                fuCargarReporte.SaveAs(fileName);
            }
            catch (System.Web.HttpException ex)
            {
                RegistroTraza.I.Registrar(ex);
                ModalPopUp.MostrarMensaje("Error", ex.Message);
                return;
            }

            bool bResultado = service.CompararRegistrosCourier(this.IdPaqueteNotificacion, fileName, RUV.Current.Usuario.Id, ref cError);

            if (bResultado && string.IsNullOrEmpty(cError))
            {
                ModalPopUp.MostrarMensajeYRedirigir("Exito", "Se realizaron los cambios con exito", string.Format("PaqueteDetalle.aspx?id={0}", this.IdPaqueteNotificacion));
            }
            else
            {
                ModalPopUp.MostrarMensaje("Mensaje", "No se pudo realizar los cambios debido a : " + cError);
            }
        }
    }

    #endregion

    #region Private Methods

    private byte[] ExportarNotificacionesPaqueteAExcel(ref string cError)
    {
        var service = new NotificacionService();
        var notificacionesPaqueteConteo = service.ObtenerDetallePaqueteConteo(this.IdPaqueteNotificacion, ref cError);
        if (notificacionesPaqueteConteo > 0) {
            var notificacionesPaquete = service.ObtenerDetallePaquete(this.IdPaqueteNotificacion, 1, int.MaxValue, ref cError);
            var notificacionesPaqueteExcel = notificacionesPaquete.Select(x => new clsNotificacionExcel {
                NombreDeclarante = x.NOMBRECOMPLETO
              , GeografiaNotificacionExcel = string.Format("{0}-{1}", string.IsNullOrEmpty(x.NombreMunicipioAlterno) ? x.NOMBREMUNICIPIO : x.NombreMunicipioAlterno, string.IsNullOrEmpty(x.NombreDepartamentoAlterno) ? x.NOMBREDEPARTAMENTO : x.NombreDepartamentoAlterno)
              , Referencia = "REGISTRO Y VALORACION"
              , Direccion = x.DIRECCIONNOTIFICACION
              , PesoSobre = "50"
              , RelacionIdNotificacion = x.ID.ToString()
              , RelacionIdCodigoorfeo = string.Format("{0} - {1}", x.CodigoOrfeo, x.NUMERODOCUMENTO)
            }).ToList();
            ExcelHelper eh = new ExcelHelper();
            return eh.ExportToExcel(notificacionesPaqueteExcel);

        }
        else {
            cError = "No hay datos para exportar";
        }
        return null;
    }

    private byte[] ExportarNotificacionesPaqueteCitaciones(ref string cError)
    {
        var service = new NotificacionService();
        var notificacionesPaqueteConteo = service.ObtenerDetallePaqueteConteo(this.IdPaqueteNotificacion, ref cError);
        if (notificacionesPaqueteConteo > 0) {
            var notificacionesPaquete = service.ObtenerDetallePaquete(this.IdPaqueteNotificacion, 1, int.MaxValue, ref cError);
            
            string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            path += path.EndsWith(Path.DirectorySeparatorChar.ToString()) ? string.Empty : Path.DirectorySeparatorChar.ToString();
            string pathTemplate = path + "{0}" + Path.DirectorySeparatorChar + "{1}";

            var citaciones = new Dictionary<string, FileInfo>();

            foreach (var notificacion in notificacionesPaquete) {
                if (notificacion.nEnvioResolucion == 1)
                {
                    var pathResolucion = string.Format(pathTemplate, notificacion.ID_VALORACION, "Resolucion.pdf");
                    var fileResolucion = new FileInfo(pathResolucion);
                    var pathAviso = string.Format(pathTemplate, notificacion.ID_VALORACION, "Aviso.pdf");
                    var fileAviso = new FileInfo(pathAviso);
                    if (fileResolucion.Exists && fileAviso.Exists)
                    {
                        citaciones.Add(string.Format("Resolucion-{0}.pdf", notificacion.CodigoOrfeo), fileResolucion);
                        citaciones.Add(string.Format("Aviso-{0}.pdf", notificacion.CodigoOrfeo), fileAviso);
                    }
                    else
                    {
                        ActosAdminService actosAdminService = new ActosAdminService();
                        ValoracionService serviceValoracion = new ValoracionService();
                        var nIdValoracion = serviceValoracion.ObtenerIdValoracionporIdDeclaracionServ(int.Parse(notificacion.ID_DECLARACION), ref cError);
                        if (string.IsNullOrEmpty(cError) && nIdValoracion != null)
                        {
                            actosAdminService.GenerarDocumentoValoracion(nIdValoracion, true, ref cError);
                            citaciones.Add(string.Format("Resolucion-{0}.pdf", notificacion.CodigoOrfeo), fileResolucion);
                            citaciones.Add(string.Format("Aviso-{0}.pdf", notificacion.CodigoOrfeo), fileAviso);
                        }

                    }
                }
                else
                {
                    var pathCitacion = string.Format(pathTemplate, notificacion.ID_VALORACION, "Citacion.pdf");
                    var fileCitacion = new FileInfo(pathCitacion);
                    if (fileCitacion.Exists)
                    {
                        citaciones.Add(string.Format("Citacion-{0}.pdf", notificacion.CodigoOrfeo), fileCitacion);
                    }
                    else
                    {
                        ActosAdminService actosAdminService = new ActosAdminService();
                        ValoracionService serviceValoracion = new ValoracionService();
                        var nIdValoracion = serviceValoracion.ObtenerIdValoracionporIdDeclaracionServ(int.Parse(notificacion.ID_DECLARACION), ref cError);
                        if (string.IsNullOrEmpty(cError) && nIdValoracion != null)
                        {
                            actosAdminService.GenerarDocumentoValoracion(nIdValoracion, true, ref cError);
                            citaciones.Add(string.Format("Citacion-{0}.pdf", notificacion.CodigoOrfeo), fileCitacion);
                        }

                    }
                }
            }

            return FileHelper.CompressFiles(citaciones);
        }
        else {
            cError = "No hay datos para exportar";
        }
        return null;
    }

    #endregion

    #region Web Methods

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static IList<clsHistoricoNotificacion> ObtenerHistoricoNotificacion(string idNotificacion) {
        int idNotificacionValue = 0;
        if (int.TryParse(idNotificacion, out idNotificacionValue)) {
            var historico = new NotificacionService().ObtenerHistorico(idNotificacionValue);
            return historico;
        }
        return null;
    }

    #endregion

    protected void btnAsociarCodigoGuia_Click(object sender, EventArgs e)
    {
        if (fuCargacodigos.HasFile)
        {
            string cError = string.Empty;
            NotificacionService service = new NotificacionService();

            try
            {
                fuCargacodigos.SaveAs(ConfigurationManager.AppSettings["PathReporteCourier"] + fuCargacodigos.FileName);
            }
            catch (System.Web.HttpException ex)
            {
                RegistroTraza.I.Registrar(ex);
                ModalPopUp.MostrarMensaje("Error", ex.Message);                
                return;
            }

            bool bResultado = service.AsociarCodigosGuiaNotificacion(this.IdPaqueteNotificacion, ConfigurationManager.AppSettings["PathReporteCourier"] + fuCargacodigos.FileName, RUV.Current.Usuario.Id, ref cError);

            if (bResultado && string.IsNullOrEmpty(cError))
            {
                ModalPopUp.MostrarMensajeYRedirigir("Exito", "Se realizaron los cambios con exito", string.Format("PaqueteDetalle.aspx?id={0}", this.IdPaqueteNotificacion.ToString()));
               // ModalPopUp.MostrarMensaje("Exito", "Se realizaron los cambios con exito");
               // Response.Redirect(string.Format("PaqueteDetalle.aspx?id={0}", this.IdPaqueteNotificacion.ToString()));
            }
            else
            {
                ModalPopUp.MostrarMensaje("Mensaje", "No se pudo realizar los cambios debido a : " + cError);
            }
        }

    }

    private static string numberPattern = " ({0})";

    public static string NextAvailableFilename(string path)
    {
        // Short-cut if already available
        if (!File.Exists(path))
            return path;

        // If path has extension then insert the number pattern just before the extension and return next filename
        if (Path.HasExtension(path))
            return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

        // Otherwise just append the pattern to the path and return next filename
        return GetNextFilename(path + numberPattern);
    }

    private static string GetNextFilename(string pattern)
    {
        string tmp = string.Format(pattern, 1);
        if (tmp == pattern)
        {
            ArgumentException argumentException = new ArgumentException("The pattern must include an index place-holder", "pattern");
            RegistroTraza.I.Registrar(argumentException);
            throw argumentException;
        }
        if (!File.Exists(tmp))
            return tmp; // short-circuit if no matches

        int min = 1, max = 2; // min is inclusive, max is exclusive/untested

        while (File.Exists(string.Format(pattern, max)))
        {
            min = max;
            max *= 2;
        }

        while (max != min + 1)
        {
            int pivot = (max + min) / 2;
            if (File.Exists(string.Format(pattern, pivot)))
                min = pivot;
            else
                max = pivot;
        }

        return string.Format(pattern, max);
    }
    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        string errorMessage = string.Empty;
        NotificacionService service = new NotificacionService();

        bool resp = service.ConfirmarEnvioNotificacion(IdPaqueteNotificacion, ref errorMessage);

        if (!resp)
        {
            ModalPopUp.MostrarMensaje("Mensaje", "No se pudo realizar los cambios debido a : " + errorMessage);
        }
        else
        {
            ModalPopUp.MostrarMensajeYRedirigir("Exito", "Se realizaron los cambios con exito", string.Format("PaqueteDetalle.aspx?id={0}", this.IdPaqueteNotificacion));
        }
    }
}