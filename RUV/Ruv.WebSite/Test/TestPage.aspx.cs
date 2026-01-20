using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;
using Ruv.Business.DTO.ActosAdministrativos;
using Ruv.Infrastructure.Crosscutting.Common;
using Ionic.Zip;
using Ruv.Business.Correcciones;
using Ruv.Business.DTO.Correcciones;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion;
using Ruv.Business.Notificacion;
using System.Web.Services;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Web.UI.HtmlControls;

public partial class Test_TestPage : System.Web.UI.Page
{
    //protected ASP.UCTarea UCTarea;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        DataSourceListaTareas DSListaTareas = new DataSourceListaTareas();
        int intCantidad = DSListaTareas.CantidadTareas();
        List<clsListaTareas> listaTareas = DSListaTareas.ObtenerListaTareas(1, 24, "");
        //foreach(clsListaTareas tarea in listaTareas)
        //{
        //    UCTarea = (ASP.UCTarea)LoadControl("~/ListaTareas/UCTarea.ascx");
        //    UCTarea.Formulario = tarea.Formulario;
        //    UCTarea.Estado = tarea.Accion;
        //    UCTarea.Fecha = tarea.Fecha;
        //    UCTarea.IdDeclaracion = tarea.Declaracion;
        //    UCTarea.IdCorreccion = (tarea.Correccion == null) ? 0 : (int)tarea.Correccion;
        //    pnlTareas.Controls.Add(UCTarea);
        //}
        //}
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        ActosAdminService service = new ActosAdminService();
        int idValoracion = 798;
        bool firmado = false;
        string cError = string.Empty;
        service.GenerarDocumentoValoracion(idValoracion, firmado, ref cError);
    }

    protected void btnRegenerar_Click(object sender, EventArgs e)
    {
        LiderValoracionService liderValoracionService = new LiderValoracionService();
        string cError = string.Empty;
        liderValoracionService.AprobarValoracion(19389, 2045704, "", ref cError);
    }

    private void cosa()
    {
        int IdValoracion = 706;

        List<byte[]> filesByte = new List<byte[]>();

        ////////Test
        //string nombreArchivo = idActoAdministrativo.ToString() + ".pdf";
        //string nombreCompleto1 = @path + "1_" + nombreArchivo;
        //string nombreCompleto2 = @path + "2_" + nombreArchivo;

        //filesByte.Add(File.ReadAllBytes(nombreCompleto1));
        //filesByte.Add(File.ReadAllBytes(nombreCompleto2));

        //File.WriteAllBytes(nombreCompleto3, PdfUtilidades.MergeFiles(filesByte));

        //return;
        ////////

        CargaDatosValoracionService service = new CargaDatosValoracionService();
        string cError = string.Empty;
        IList<clsNotificacionVal> listclsNotificacionVal = service.CargaDatosValoracionNoti(IdValoracion, ref cError);

        string tipo = string.Empty;
        //Pregunta por el resultado de la valoracion
        if (listclsNotificacionVal.FirstOrDefault().nTipoDocumentoVal == (int)eTipoDocumentoValoracion.Incluido)
        {
            tipo = "Incluido";
        }
        if (listclsNotificacionVal.FirstOrDefault().nTipoDocumentoVal == (int)eTipoDocumentoValoracion.Excluido)
        {
            tipo = "NoIncluido";
        }
        if (listclsNotificacionVal.FirstOrDefault().nTipoDocumentoVal == (int)eTipoDocumentoValoracion.Mixto)
        {
            tipo = "Mixto";
        }

        //Resolucion
        ReportViewer viewerResolucion = new ReportViewer();
        viewerResolucion.LocalReport.ReportPath = Server.MapPath("/Reportes/Valoracion/" + tipo + "/ReporteValoracionResolucion.rdlc");
        viewerResolucion.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
        viewerResolucion.LocalReport.Refresh();
        byte[] bytesResolucion = viewerResolucion.LocalReport.Render("PDF");

        //Aviso
        ReportViewer viewerAviso = new ReportViewer();
        viewerAviso.LocalReport.ReportPath = Server.MapPath("/Reportes/Valoracion/" + tipo + "/ReporteValoracionAviso.rdlc");
        viewerAviso.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
        viewerAviso.LocalReport.Refresh();
        byte[] bytesAviso = viewerAviso.LocalReport.Render("PDF");

        //NotificacionPersonal
        ReportViewer viewerNotificacionPersonal = new ReportViewer();
        viewerNotificacionPersonal.LocalReport.ReportPath = Server.MapPath("/Reportes/Valoracion/" + tipo + "/ReporteValoracionNotificacionPersonal.rdlc");
        viewerNotificacionPersonal.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
        viewerNotificacionPersonal.LocalReport.Refresh();
        byte[] bytesNotificacionPersonal = viewerNotificacionPersonal.LocalReport.Render("PDF");

        //Citacion
        ReportViewer viewerCitacion = new ReportViewer();
        viewerCitacion.LocalReport.ReportPath = Server.MapPath("/Reportes/Valoracion/" + tipo + "/ReporteValoracionCitacion.rdlc");
        viewerCitacion.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
        viewerCitacion.LocalReport.Refresh();
        byte[] bytesCitacion = viewerCitacion.LocalReport.Render("PDF");

        filesByte.Add(bytesResolucion);
        filesByte.Add(bytesAviso);
        filesByte.Add(bytesNotificacionPersonal);
        filesByte.Add(bytesCitacion);

        //int idActoAdministrativo = 685;
        //idActoAdministrativo = listclsNotificacionVal.FirstOrDefault().nIdActoAdmin;

        string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];

        string nombreArchivo = IdValoracion.ToString();

        //string nombreCompleto1 = @path + "1_" + nombreArchivo;
        //string nombreCompleto2 = @path + "2_" + nombreArchivo;
        //string nombreCompleto3 = @path + "3_" + nombreArchivo;
        //string nombreCompleto4 = @path + "4_" + nombreArchivo;

        //FileStream fs1 = new FileStream(nombreCompleto1, FileMode.Create);
        //fs1.Write(bytesResolucion, 0, bytesResolucion.Length);
        //fs1.Close();

        //FileStream fs2 = new FileStream(nombreCompleto2, FileMode.Create);
        //fs2.Write(bytesAviso, 0, bytesAviso.Length);
        //fs2.Close();

        //FileStream fs3 = new FileStream(nombreCompleto3, FileMode.Create);
        //fs3.Write(bytesNotificacionPersonal, 0, bytesNotificacionPersonal.Length);
        //fs3.Close();

        //FileStream fs4 = new FileStream(nombreCompleto4, FileMode.Create);
        //fs4.Write(bytesCitacion, 0, bytesCitacion.Length);
        //fs4.Close();

        //filesByte.Add(File.ReadAllBytes(nombreCompleto1));
        //filesByte.Add(File.ReadAllBytes(nombreCompleto2));
        //filesByte.Add(File.ReadAllBytes(nombreCompleto3));
        //filesByte.Add(File.ReadAllBytes(nombreCompleto4));

        //File.WriteAllBytes(nombreCompleto3, PdfUtilidades.MergeFiles(filesByte));

        using (ZipFile zip = new ZipFile())
        {
            int i = 0;
            foreach (byte[] archivo in filesByte)
            {
                zip.AddEntry((++i).ToString() + ".pdf", archivo);
            }

            zip.Save(path + nombreArchivo + ".zip");
        }
    }

    protected void btnSolicitarCorreccion_Click(object sender, EventArgs e)
    {
        CorreccionesBusiness correccionesBusiness = new CorreccionesBusiness();
        
        IList<clsCorreccion> Correcciones = new List<clsCorreccion>();
        Correcciones.Add(new clsCorreccion(){ Campo=1, Valor="OLGITA"});
        Correcciones.Add(new clsCorreccion() { Campo = 3, Valor = "POSADA" });

        string cError = string.Empty;
        bool respuesta = correccionesBusiness.SolicitarCorreccion(10000118, RUV.Current.Usuario.Id, Correcciones, ref cError);
    }

    protected void btnConsultarCorreccion_Click(object sender, EventArgs e)
    {
        CorreccionesBusiness correccionesBusiness = new CorreccionesBusiness();
        
        int idCorreccion = 4;
        string cError = string.Empty;
        Ruv.Infrastructure.Crosscutting.Common.Entidades.Correcciones.clsCargaDatosCorreccion clsCargaDatosCorreccion = correccionesBusiness.ConsultarCorreccion(idCorreccion, ref cError);
    }

    protected void btnRechazar_Click(object sender, EventArgs e)
    {
        CorreccionesBusiness correccionesBusiness = new CorreccionesBusiness();

        int idCorreccion = 4;
        string strObservaciones = txtObservaciones.Text;
        string cError = string.Empty;
        bool respuesta = correccionesBusiness.RechazarCorreccion(idCorreccion, RUV.Current.Usuario.Id, strObservaciones, ref cError);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Correcciones/AprobarRechazarCorreccion.aspx?idCorreccion={0}&idRegPersona={1}&urlEvio={2}", 4, 10000118, this.Request.Url.AbsolutePath));
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //IServicioComunicacion servicio = new ServicioComunicacion();
        //Persona persona = servicio.Persona();

        NotificacionService notificacionService = new NotificacionService();
        string cError = string.Empty;

        bool updated = notificacionService.ActualizarNotificacion(1, txtNuevaDireccion.Text, ref cError);

    }

    protected void btnNotificaciones_Click(object sender, EventArgs e)
    {
        //NotificacionBusiness notificacionBusiness = new NotificacionBusiness();

        //string cError = string.Empty;
        //grdNotificaciones.DataSource = notificacionBusiness.ObtenerNotificaciones(1, 10, "id", "", 19239, ref cError);
        //grdNotificaciones.DataBind();

        NotificacionService notificacionService = new NotificacionService();
        string cError = string.Empty;

        //grdNotificaciones.DataSource = notificacionService.ObtenerNotificaciones(19239, null, null, null, null, null, null, null, null, null, false, string.Empty, 1, 10, ref cError);
        //grdNotificaciones.DataBind();
    }

    protected void btnGenerarDocumentos_Click(object sender, EventArgs e)
    {
        string cError = string.Empty;
        ControlDocumentosService ctrDoc = new ControlDocumentosService();

        lblComienza.Text = lblComienza.Text + DateTime.Now.ToLongTimeString();

        ctrDoc.GenerarFormularios(uint.Parse(txtNumeroDocumentos.Text),"C", RUV.Current.Usuario.Id, (int)eEstadoFormulario.GENERADO, null, null, null, null, ref cError);

        lblTermina.Text = lblTermina.Text + DateTime.Now.ToLongTimeString();
    }

    [WebMethod]
    public static string Test()
    {
        return DateTime.Now.ToShortDateString();
    }

    protected void odsTareas_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        DataSourceListaTareas info = e.ObjectInstance as DataSourceListaTareas;
        Session["TotalRegistros"] = info.CantidadTareas();
        if (info != null)
        {
            //info.SortColumns = SortColumns;
            //info.FilterEx = FilterEx;
        }
    }

    protected void grvTareas_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    [WebMethod]
    public static string Adicionar(string controlName)
    {
        return RenderControl(controlName);
    }

    public static string RenderControl(string controlName)
    {
        try
        {
            Page page = new Page();

            var UCTarea = page.LoadControl(controlName) as Ruv.WebSite.Utilidades.Controles.IUCTarea;
            //UCTarea = (ASP.UCTarea)LoadControl("~/ListaTareas/UCTarea.ascx");
            UCTarea.Formulario = "AB00000012";
            UCTarea.Estado = "Paila";
            UCTarea.Fecha = DateTime.Now;
            UCTarea.IdDeclaracion = 123;
            UCTarea.IdCorreccion = 0;

            //UCTarea.EnableViewState = false;

            HtmlForm form = new HtmlForm();
            form.Controls.Add(UCTarea as Control);
            page.Controls.Add(form);

            StringWriter textWriter = new StringWriter();
            HttpContext.Current.Server.Execute(page, textWriter, false);
            return textWriter.ToString();
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            return ex.ToString();
        }
    }
}