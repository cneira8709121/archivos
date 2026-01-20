using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using Ionic.Zip;
using Ruv.Infrastructure.Crosscutting.Common;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using System.IO;
using System.ServiceModel.Activation;
using Ruv.Business.DTO.Devolucion;

[AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Required)]
public class PdfHelperService : IPdfHelperService
{
    #region Public methods

    #region Services implementation

    public byte[] GenerateOnePdfFile(string codigo, ref string cError)
    {
        byte[] bPdf = null;
        try
        {
            bPdf = PDFHelper.GenerateOnePdfFile(codigo, HttpContext.Current.Server.MapPath(resx::General.RutaTemplatePdf));
        }
        catch (Exception e)
        {
            cError = string.Format(resx::Globalization.Errores.General, e.Message);
            RegistroTraza.I.Registrar(cError);
        }
        return bPdf;
    }

    public byte[] GenerateManyPdfFilesAsZip(Dictionary<string,bool> codigos, ref string cError)
    {
        byte[] bZipedPdfs = null;
        try
        {
            bZipedPdfs = PDFHelper.GenerateManyPdfFilesAsZip(codigos, HttpContext.Current.Server.MapPath(resx::General.RutaTemplatePdf), HttpContext.Current.Server.MapPath(resx::General.RutaTemplatePdfConnacionales));
        }
        catch (Exception e)
        {
            cError = string.Format(resx::Globalization.Errores.General, e.Message);
            RegistroTraza.I.Registrar(cError);
        }
        return bZipedPdfs;
    }

    public byte[] GeneratePdf(string contenido, ref string cError)
    {
        byte[] bPdf = null;
        try
        {
            bPdf = PDFHelper.GeneratePdf(contenido, HttpContext.Current.Server.MapPath(resx::General.RutaTemplatePdfDevolucion));
        }
        catch (Exception e)
        {
            cError = string.Format(resx::Globalization.Errores.General, e.Message);
            RegistroTraza.I.Registrar(cError);
        }
        return bPdf;
    }

    public byte[] GeneratePdfDevolucion(clsDatosparaDevolucion datosparaDevolucion, ref string cError)
    {
        byte[] bPdf = null;
        try
        {
            bPdf = PDFHelper.GeneratePdfDevolucion(datosparaDevolucion.NIdDeclaracion,             
                                                   datosparaDevolucion.cEntidadMunicipio,
                                                   datosparaDevolucion.cMunicipio,
                                                   datosparaDevolucion.CParteEmotiva,
                                                   datosparaDevolucion.DFechaDevolucion,
                                                   datosparaDevolucion.CNombreDeclarante,
                                                   datosparaDevolucion.cTipoDocumento,
                                                   datosparaDevolucion.nNumeroDocumento,
                                                   datosparaDevolucion.DFechaDeclaracion, 
                                                   HttpContext.Current.Server.MapPath(resx::General.RutaTemplatePdfDevolucion));
        }
        catch (Exception e)
        {
            cError = string.Format(resx::Globalization.Errores.General, e.Message);
        }
        return bPdf;
    }

    public byte[] GenerateOnePdfFileConNacional(string codigo, ref string cError)
    {
        byte[] bPdf = null;
        try
        {
            bPdf = PDFHelper.GenerateOnePdfFile(codigo, HttpContext.Current.Server.MapPath(resx::General.RutaTemplatePdfConnacionales));
        }
        catch (Exception e)
        {
            cError = string.Format(resx::Globalization.Errores.General, e.Message);
            RegistroTraza.I.Registrar(cError);
        }
        return bPdf;
    }

    //public byte[] GenerateManyPdfFilesConNacionalAsZip(IList<string> codigos, ref string cError)
    //{
    //    byte[] bZipedPdfs = null;
    //    try
    //    {
    //        bZipedPdfs = PDFHelper.GenerateManyPdfFilesAsZip(codigos, HttpContext.Current.Server.MapPath(resx::General.RutaTemplatePdfConnacionales));
    //    }
    //    catch (Exception e)
    //    {
    //        cError = string.Format(resx::Globalization.Errores.General, e.Message);
    //    }
    //    return bZipedPdfs;
    //}

    #endregion

    #endregion
}
