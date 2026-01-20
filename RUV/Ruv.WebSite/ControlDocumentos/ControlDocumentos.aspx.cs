using System;
using System.Collections.Generic;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Resources;

public partial class ControlDocumentos_ControlDocumentos : PaginaBase
{
    #region Propiedades

    /// <summary>
    /// Contiene la lista de clsFormulario con la cual se alimenta la grilla
    /// </summary>
    private IList<clsFormulario> DocumentosFormulario
    {
        get
        {
            if (Session[ConstatesControlDocumentos.DOCUMENTOS_FORMULARIO] == null)
                Session[ConstatesControlDocumentos.DOCUMENTOS_FORMULARIO] = new List<clsFormulario>();

            return (List<clsFormulario>)Session[ConstatesControlDocumentos.DOCUMENTOS_FORMULARIO];
        }
        set
        {
            Session[ConstatesControlDocumentos.DOCUMENTOS_FORMULARIO] = value;
        }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        // Diego Alvarez - 29/11/2013 - Validar que no sea vacio la cantidad solicitada
        if (txtCantidad.Text == null || txtCantidad.Text == "")
        {
            return;
        }
        if (int.Parse(txtCantidad.Text) > 20)
        {
            ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error, Ruv.Infrastructure.Crosscutting.Resources.Globalization.Errores.MaximoFormularios);
            return;
        }

        mpuMensajeInfo.Titulo = Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Advertencia;
        mpuMensajeInfo.Mensaje = string.Format(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Advertencia.GenerarNumerosFormulario, txtCantidad.Text);
        mpuMensajeInfo.Mostrar();        
    }

    protected void GenerarFormularios(object sender, EventArgs e)
    {
        ControlDocumentosService ObjControlDocumentos = new ControlDocumentosService();
        string cError = string.Empty;

        clsUsuario usuario = (HttpContext.Current.Session[ConstantesSesion.USUARIO] as clsUsuario);

        ObjControlDocumentos.GenerarFormulariosWEB(uint.Parse(txtCantidad.Text),
                                                   txtSerie.Text,
                                                   usuario.Id,
                                                   (int)(eEstadoFormulario.ASIGNADO),
                                                   usuario.ID_ENTIDADMUNICIPIO,
                                                   ref cError);


        if (cError != string.Empty)
        {
            ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error, cError);
            return;
        }

        // Diego Alvarez - 03/10/2013 - Se debe mostrar mensaje al generar documento
        ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Informacion, Ruv.Infrastructure.Crosscutting.Resources.Globalization.Informacion.DocumentosGeneradosCorrectamente);
        //Response.Redirect("ControlDocumentos.aspx");
    }

    protected void grdDocumentos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ExportarPDF")
        {
            string templatePath = HttpContext.Current.Server.MapPath(General.RutaTemplatePdf);

            var code = e.CommandArgument.ToString();
            var fileContent = PDFHelper.GenerateOnePdfFile(code, templatePath);

            //Marca el formulario como descargado
            MarcarDescargado(DocumentosFormulario.FirstOrDefault(d => d.CNumeroFormulario == code).NId);

            DownloadPdfFile(code, fileContent);
        }
    }

    protected void grdDocumentos_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        List<int> list = ViewState["SelectedRecords"] as List<int>;
        if (e.Row.RowType == DataControlRowType.DataRow && list != null)
        {
            var autoId = int.Parse(grdDocumentos.DataKeys[e.Row.RowIndex].Value.ToString());
            if (list.Contains(autoId))
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");
                chk.Checked = true;
            }
        } 
    }

    protected void grdDocumentos_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        UpdateSelectedRecords();

        grdDocumentos.PageIndex = e.NewPageIndex;

        if (DocumentosFormulario != null)
        {
            grdDocumentos.DataSource = DocumentosFormulario;
            grdDocumentos.DataBind();
        }
    }

    protected void btnGenerarPDFs_Click(object sender, EventArgs e)
    {
        UpdateSelectedRecords();
        ControlDocumentosService ObjControlDocumentos = new ControlDocumentosService();
        string cError = string.Empty;
        string templatePath = string.Empty;

        clsUsuario usuario = (HttpContext.Current.Session[ConstantesSesion.USUARIO] as clsUsuario);

        int PaisId = ObjControlDocumentos.ObtenerPaisGeneraFormularioWEB(usuario.ID_ENTIDADMUNICIPIO, ref cError);

        if (string.IsNullOrEmpty(cError))
        {
            if (PaisId == 48)
            {
                templatePath = HttpContext.Current.Server.MapPath("~/templates/GENERA_FUD_V2.1.pdf");
                string outPath = string.Empty;
            }
            else
            {
                templatePath = HttpContext.Current.Server.MapPath("~/templates/GENERA_FUD_CONNACIONALES_V2.1.pdf");
                string outPath = string.Empty;
            }
        }
        else
            ModalPopUp.MostrarMensaje("Error", "No se Pudo Realizar la Accion debido a" + cError);

        IList<string> codigos = new List<string>();

        List<int> list = ViewState["SelectedRecords"] as List<int>;
        if (list != null)
        {
            foreach (int id in list)
            {
                codigos.Add(DocumentosFormulario.FirstOrDefault(d => d.NId == id).CNumeroFormulario);
            }
        } 

        IDictionary<string, byte[]> outCodigos = PDFHelper.GenerateManyPdfFiles(codigos, templatePath);

        if (outCodigos != null && outCodigos.Count() > 0)
        {
            //Marca los formulario como descargados
            foreach (KeyValuePair<string, byte[]> pair in outCodigos)
            {
                MarcarDescargado(DocumentosFormulario.FirstOrDefault(d => d.CNumeroFormulario == pair.Key).NId);
            }

            DownloadZipFile(outCodigos);
        }
    }

    #endregion Eventos

    #region Funciones

    /// <summary>
    /// Muestra un mensaje en un popUp
    /// </summary>
    /// <param name="sTitle"></param>
    /// <param name="sMessage"></param>
    private void ShowMessage(string sTitle, string sMessage)
    {
        mpuMensaje.Titulo = sTitle;
        mpuMensaje.MostrarBotones = true;
        mpuMensaje.MostrarImagen = false;
        mpuMensaje.Mensaje = sMessage;
        mpuMensaje.Mostrar();
    }

    /// <summary>
    /// Actualiza el viewState que controla los rows seleccionados
    /// </summary>
    protected void UpdateSelectedRecords()
    {
        List<int> list = new List<int>();
        if (ViewState["SelectedRecords"] != null)
        {
            list = (List<int>)ViewState["SelectedRecords"];
        }
        foreach (GridViewRow row in grdDocumentos.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            var selectedKey =
            int.Parse(grdDocumentos.DataKeys[row.RowIndex].Value.ToString());
            if (chk.Checked)
            {
                if (!list.Contains(selectedKey))
                {
                    list.Add(selectedKey);
                }
            }
            else
            {
                if (list.Contains(selectedKey))
                {
                    list.Remove(selectedKey);
                }
            }
        }
        ViewState["SelectedRecords"] = list;
    }

    /// <summary>
    /// Descarga el archivo PDF suministrado
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileContent"></param>
    private void DownloadPdfFile(string fileName, byte[] fileContent)
    {
        Response.Clear();
        Response.ContentType = "Application/pdf";
        Response.AddHeader("Content-Length", fileContent.Length.ToString());
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + string.Format("{0}.pdf", fileName) + "\"");
        Response.Flush();
        Response.BinaryWrite(fileContent);

        Response.End();
    }

    /// <summary>
    /// Descarga un archivo ZIP con los archivos PDFs suministrados
    /// </summary>
    /// <param name="fileNames"></param>
    private void DownloadZipFile(IDictionary<string, byte[]> fileNames)
    {
        Response.Clear();
        // no buffering - allows large zip files to download as they are zipped
        Response.BufferOutput = false;
        String ReadmeText = "Dynamic content for a readme file...\n" +
                           DateTime.Now.ToString("G");
        string archiveName = String.Format("FUD-{0}.zip",
                                          DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "attachment; filename=" + archiveName);
        using (ZipFile zip = new ZipFile())
        {
            foreach (KeyValuePair<string, byte[]> pair in fileNames)
            {
                zip.AddEntry(pair.Key + ".pdf", pair.Value);
            }
            
            zip.Save(Response.OutputStream);
        }
        Response.Close();
    }

    /// <summary>
    /// Marca un formulario como ya descargado
    /// </summary>
    /// <param name="id">Identificador unico del formulario</param>
    private void MarcarDescargado(uint id)
    {
        ControlDocumentosService ObjControlDocumentos = new ControlDocumentosService();
        string cError = string.Empty;

        ObjControlDocumentos.MarcarDescargado(id, ref cError);

        if (cError != string.Empty)
        {
            ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error, cError);
            return;
        }
    }

    #endregion Funciones
}