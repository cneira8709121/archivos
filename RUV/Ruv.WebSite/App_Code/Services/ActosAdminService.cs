using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.ActosAdmin;
using Ruv.Business.ActosAdmin;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Business.DTO.ActosAdministrativos;
using Ruv.Business.DTO.Orfeo;
using Ruv.Infrastructure.Crosscutting.Common;
using util = Ruv.Infrastructure.Crosscutting.Utilities;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using Microsoft.Reporting.WebForms;
using System.Web;
using Ionic.Zip;
using Ruv.Business.Orfeo.Services;
using Ruv.Infrastructure.Crosscutting.Utilities;

// NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de clase "ActosAdminService" en el código, en svc y en el archivo de configuración a la vez.
public class ActosAdminService : IActosAdminService {

    private DateTime? _fechaCambioFormato = null;

    public DateTime FechaCambioFormato {
        get {
            if (!_fechaCambioFormato.HasValue) {
                _fechaCambioFormato = DateTime.ParseExact(System.Configuration.ConfigurationManager.AppSettings["FechaCambioFormato"], "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }
            return _fechaCambioFormato ?? new DateTime(2012, 07, 02);
        }
    }

	public List<clsActosAdminstrativos> GetActosAdminPaginado(int Inicio, int Fin, string sortColumns)
	{
        ActosAdminBusiness objActosAdmin = new ActosAdminBusiness();
        return objActosAdmin.GetActosAdministrativosPaginado(Inicio, Fin, sortColumns);
	}

    public int CantidadActosAdmin()
    {
        ActosAdminBusiness objActosAdmin = new ActosAdminBusiness();
        return objActosAdmin.GetCantidadActosAdmin();
    }

    public List<clsParametroGeneral> GetDocumentosPorArea(int Area)
    {
        ActosAdminBusiness objActosAdmin = new ActosAdminBusiness();
        return objActosAdmin.GetDocumentosPorArea(Area);
    }

    public bool ExisteDeclaracion(string formulario)
    {
        ActosAdminBusiness objActosAdmin = new ActosAdminBusiness();
        return objActosAdmin.ExisteDeclaracion(formulario);
    }

    public string Guardar(clsActosAdminstrativos actoadmin)
    {
        try
        {
            ActosAdminBusiness objActosAdmin = new ActosAdminBusiness();
            return objActosAdmin.Guardar(actoadmin);
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return string.Format("{0} chr(13) {1}", ex.Message, ex.StackTrace);
        }
    }

    public clsActosAdminstrativos GetActoAdminPorId(int id)
    {
        ActosAdminBusiness objActosAdmin = new ActosAdminBusiness();
        return objActosAdmin.GetActoAdministrativoPorId(id);
    }

    public List<clsActosAdminstrativos> GetActosAdminFitro(string tipoFiltro, string valorFiltro)
    {
        ActosAdminBusiness objActosAdmin = new ActosAdminBusiness();
        return objActosAdmin.GetActosAdministrativosFiltro(tipoFiltro, valorFiltro);
    }

    /// <summary>
    /// Genera el documento de la valoración (Acto administrativo)
    /// </summary>
    /// <param name="idValoracion">id de la valoración</param>
    /// <param name="firmado">Flag que indica si se debe incluir la firma en los documentos</param>
    public void GenerarDocumentoValoracion(int idValoracion, bool firmado, ref string cError)
    {
        try
        {
            cError = string.Empty;

            List<clsOrfeo> lstOrfeo = ObtenerDatosOrfeoPorIdValoracion(idValoracion, ref cError);
            if (lstOrfeo == null || !string.IsNullOrEmpty(cError)) return;

            clsOrfeo orfData = lstOrfeo.First();
            IManageOrfeo iOrfeo = (IManageOrfeo)util::Spring.GetService(resx::Dependencias.Objetos.OrfeoBusiness);
            iOrfeo.NValorARelacionar = idValoracion;
            string cOrfeo = string.Empty;

            //Busca si ya existe algun codigo relacionado con la valoración
            cOrfeo = iOrfeo.ObtenerCodigoOrfeoPorIdVal(idValoracion, ref cError);

            //Si no hay codigo orfeo relacionado, genera y relaciona uno nuevo
            if (string.IsNullOrEmpty(cOrfeo))
            {
                cOrfeo = iOrfeo.GeneraCodigoOrfeo
                (
                    new Dignatario
                    {
                        CNombreDeclarante = orfData.cPrimerNombre,
                        CPrimerApellido = orfData.cPrimerApellido,
                        CSegundoApellido = orfData.cSegundoNombre,
                        CCedula = orfData.cNumeroDocumento,
                        CDireccion = orfData.cDireccionPersona,
                        CTelefono = orfData.cTelefonoPersona,
                        CEmail = orfData.cEmail,
                        CEntidad = orfData.cEntidad,
                        NIdDepartamento = string.IsNullOrEmpty(orfData.cDeparatmentoCodazziPersona) ? 0 : int.Parse(orfData.cDeparatmentoCodazziPersona),
                        NIdMunicipio = string.IsNullOrEmpty(orfData.cMunicipioCodazziPersona) ? 0 : int.Parse(orfData.cMunicipioCodazziPersona),
                    },
                    new Radicado
                    {
                        CAsunto = "AA - Valoración",
                        NCodigoUsuario = orfData.nUsuario,
                        NCodigoUsuarioDestino = orfData.nUsuarioDestino,
                        NDepartamentoDestino = orfData.nDptoDestino,
                        NDepartamentoRadicado = orfData.nDptoAdmin,
                    },
                    new Direccion
                    {
                        coddpto = string.IsNullOrEmpty(orfData.cDepartamentoCodazziCorreo) ? 0 : int.Parse(orfData.cDepartamentoCodazziCorreo),
                        codmpio = string.IsNullOrEmpty(orfData.cMunicipioCodazziCorreo) ? 0 : int.Parse(orfData.cMunicipioCodazziCorreo),
                        direccion = orfData.cDireccionCorrespondencia,
                        dirtelefono = orfData.cTelefonoCorrespondecia,
                        dirnombre = orfData.cPrimerNombre
                    },
                    new Evento
                    {
                        codiusu = orfData.nUsuario,
                        deprad = orfData.nDptoAdmin,
                    },
                    ref cError
                );
            }

            if (string.IsNullOrEmpty(cOrfeo) || !string.IsNullOrEmpty(cError))
            {
                RegistroTraza.I.Registrar(cError);
                throw new Exception(cError);
            }

            IDictionary<string, byte[]> dicfilesByte = new Dictionary<string, byte[]>();
            CargaDatosValoracionService service = new CargaDatosValoracionService();
            IList<clsNotificacionVal> listclsNotificacionVal = service.CargaDatosValoracionNoti(idValoracion, ref cError);

            //Ocurrio un error al obtener los datos para el acto administrativo
            if (!string.IsNullOrEmpty(cError) || listclsNotificacionVal == null || listclsNotificacionVal.Count <= 0) {
                RegistroTraza.I.Registrar("Error al obtener la información para generar los actos administrativos: " + (string.IsNullOrEmpty(cError) ? "No se encontraron registros" : cError));
                throw new Exception("Error al obtener la información para generar los actos administrativos: " + (string.IsNullOrEmpty(cError) ? "No se encontraron registros" : cError));
            }

            // Determinar el tipo de documento a generar
            bool esCodigoAntiguo = false;
            var controlValues = (from value in listclsNotificacionVal
                                 select new { value.nIdActoAdmin, value.dFechaDeclaracion, value.nTipoCodigoActo, value.nTipoDocumentoVal }).FirstOrDefault();

            if (controlValues.nTipoCodigoActo.HasValue) {
                esCodigoAntiguo = controlValues.nTipoCodigoActo.Value == 0;
            }
            else {
                esCodigoAntiguo = controlValues.dFechaDeclaracion < FechaCambioFormato;
                service.MarcarTipoCodigoActoAdministrativo(idActoAdministrativo: controlValues.nIdActoAdmin, valorTipoCodigo: esCodigoAntiguo ? 0 : 1);
            }
            string tipoDocumento = ((eTipoDocumentoValoracion)controlValues.nTipoDocumentoVal).Description() + (esCodigoAntiguo ? "Antiguo" : string.Empty);


            //Obtiene los valores para el usuario de aprobacion juridica y el usuario de aprobacion tecnica
            string strUsuarioJuridica = System.Configuration.ConfigurationManager.AppSettings["UsuarioJuridica"];
            string strUsuarioTecnica = System.Configuration.ConfigurationManager.AppSettings["UsuarioTecnica"];

            //Resolucion
            ReportViewer viewerResolucion = new ReportViewer();
            viewerResolucion.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipoDocumento + "/ReporteValoracionResolucion.rdlc");
            viewerResolucion.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
            viewerResolucion.LocalReport.SetParameters(new ReportParameter("EsJefeRegistro", firmado.ToString(), true));
            viewerResolucion.LocalReport.SetParameters(new ReportParameter("juridica", strUsuarioJuridica, true));
            viewerResolucion.LocalReport.SetParameters(new ReportParameter("tecnica", strUsuarioTecnica, true));
            //viewerResolucion.LocalReport.SetParameters(new ReportParameter("CodigoAntiguo", codigoAntiguo.ToString(), true));
            viewerResolucion.LocalReport.Refresh();
            byte[] bytesResolucion = viewerResolucion.LocalReport.Render("PDF");

            //Aviso o Edicto
            byte[] bytesAviso = null;
            if (esCodigoAntiguo) {
                ReportViewer viewerEdicto = new ReportViewer();
                viewerEdicto.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipoDocumento + "/Edicto.rdlc");
                viewerEdicto.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
                viewerEdicto.LocalReport.Refresh();
                //Se asigna al arreglo de bytes de "aviso" para no modificar la generación o descarga de los archivos
                bytesAviso = viewerEdicto.LocalReport.Render("PDF");
            }
            else {
                ReportViewer viewerAviso = new ReportViewer();
                viewerAviso.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipoDocumento + "/ReporteValoracionAviso.rdlc");
                viewerAviso.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
                viewerAviso.LocalReport.SetParameters(new ReportParameter("EsJefeRegistro", firmado.ToString(), true));
                viewerAviso.LocalReport.SetParameters(new ReportParameter("juridica", strUsuarioJuridica, true));
                viewerAviso.LocalReport.SetParameters(new ReportParameter("tecnica", strUsuarioTecnica, true));
                viewerAviso.LocalReport.Refresh();
                bytesAviso = viewerAviso.LocalReport.Render("PDF");
            }

            //Notificacion Personal
            ReportViewer viewerNotificacionPersonal = new ReportViewer();
            viewerNotificacionPersonal.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipoDocumento + "/ReporteValoracionNotificacionPersonal.rdlc");
            viewerNotificacionPersonal.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
            viewerNotificacionPersonal.LocalReport.Refresh();
            byte[] bytesNotificacionPersonal = viewerNotificacionPersonal.LocalReport.Render("PDF");

            //Citacion
            ReportViewer viewerCitacion = new ReportViewer();
            viewerCitacion.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipoDocumento + "/ReporteValoracionCitacion.rdlc");
            viewerCitacion.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
            viewerCitacion.LocalReport.SetParameters(new ReportParameter("EsJefeRegistro", firmado.ToString(), true));
            viewerCitacion.LocalReport.SetParameters(new ReportParameter("juridica", strUsuarioJuridica, true));
            viewerCitacion.LocalReport.SetParameters(new ReportParameter("tecnica", strUsuarioTecnica, true));
            viewerCitacion.LocalReport.Refresh();
            byte[] bytesCitacion = viewerCitacion.LocalReport.Render("PDF");

            dicfilesByte.Add("Resolucion", bytesResolucion);
            dicfilesByte.Add((esCodigoAntiguo ? "Edicto" : "Aviso"), bytesAviso);
            dicfilesByte.Add("Notificacion", bytesNotificacionPersonal);
            dicfilesByte.Add("Citacion", bytesCitacion);

            string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];

            string nombreArchivo = idValoracion.ToString();

            string folderName = path + nombreArchivo;
            System.IO.Directory.CreateDirectory(folderName);
            var actosAdministrativosFiles = new List<System.IO.FileInfo>();
            using (ZipFile zip = new ZipFile())
            {
                foreach (KeyValuePair<string, byte[]> keyValuePair in dicfilesByte)
                {
                    var filePath = string.Format("{0}/{1}.pdf", folderName, keyValuePair.Key);
                    System.IO.File.WriteAllBytes(filePath, keyValuePair.Value);
                    zip.AddEntry(keyValuePair.Key + ".pdf", keyValuePair.Value);
                    actosAdministrativosFiles.Add(new System.IO.FileInfo(filePath));
                }
                zip.Save(folderName + "/" + nombreArchivo + ".zip");
            }

            int numeroPaginas = 0;
            var unifiedPDF = PDFHelper.MergePDFFiles(actosAdministrativosFiles, ref numeroPaginas);

            // Cargar archivo a ORFEO
            iOrfeo.CargarArchivoOrfeo(cOrfeo, unifiedPDF, string.Format("ActoAdministrativo-Valoracion-{0}.pdf", cOrfeo), numeroPaginas, "prueba5");
        }
        catch (Exception ex)
        {
            //Ocurrio un error al generar los documentos de actos administrativos
            RegistroTraza.I.Registrar(ex);
            cError = string.Format("{0} - {1}", Errores.ActosAdministrativosError, ex.Message);
        }
    }

    public List<clsOrfeo> ObtenerDatosOrfeoPorIdValoracion(int nIdValoracion, ref string cError)
    {
        clsNotificacion ObjectActoAdmin = new clsNotificacion();
        return ObjectActoAdmin.ObtenerDatosOrfeoPorIdValoracion(nIdValoracion, ref cError);
    }
}
