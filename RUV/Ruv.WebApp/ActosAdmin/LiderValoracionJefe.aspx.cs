using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

public partial class ActosAdmin_Notificacion : System.Web.UI.Page {
        
    protected void Page_Load(object sender, EventArgs e) {
        
        if (!Page.IsPostBack) {
            // Diego Alvarez - 15/11/2013 - Se agrega el método HistoricoValoracionCargado el evento OnUserControlPageRendered
            historicoValoracion.OnUserControlPageRendered += new Valoracion_Valoracion_Controles_ValoracionHistoricoPopUp.PageRenderEventHandler(HistoricoValoracionCargado);
            try 
            {
                string errorMessage = string.Empty;
                clsUsuario usuario = HttpContext.Current.Session[ConstantesSesion.USUARIO] as clsUsuario;
                historicoValoracion.nIdValoracion = new ValoracionService().ObtenerIdValoracionporIdDeclaracionServ(int.Parse(Request.QueryString["id"]), ref errorMessage);
            }
            catch 
            {
                historicoValoracion.Visible = false;
            }
        }
    }

    /// <summary>
    /// Diego Alvarez - 15/11/2013 - Método que se ejecuta cuando se termina de cargar el control de historico de valoración
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HistoricoValoracionCargado(object sender, EventArgs e)
    {
        if (this.historicoValoracion.cantidadRegistrosHistorial == 0)
        {
            this.btnConsultarHistorial.Enabled = false;
        }
        // Diego Alvarez - 15/11/2013 - Se quita el método HistoricoValoracionCargado el evento OnUserControlPageRendered para que no se ejecute mas veces
        historicoValoracion.OnUserControlPageRendered -= new Valoracion_Valoracion_Controles_ValoracionHistoricoPopUp.PageRenderEventHandler(HistoricoValoracionCargado);
    }

    protected void ObjectDataResumenVal_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        DatasourceResumenValoracion sinval2 = e.ObjectInstance as DatasourceResumenValoracion;
        sinval2.NIdDeclaracion = int.Parse(Request.QueryString["id"]);       
    }

    protected void btnAprobar_Click(object sender, EventArgs e) {
        string menexito = "Se realizo la accion satisfactoriamente";
        string sesionfallida = "Su Sesion no inicio Correctamente";
        string cError = string.Empty;

        clsUsuario usuario = HttpContext.Current.Session[ConstantesSesion.USUARIO] as clsUsuario;
        if (usuario != null)
        {
            int nIdDeclaracion;
            nIdDeclaracion = int.Parse(Request.QueryString["id"]);
            ILiderValoracionService LVService = new LiderValoracionService();
            bool Exito = LVService.AprobarValoracion(usuario.Id, nIdDeclaracion, ObservacionLiderValoracion.Text, ref cError);

            if (Exito)
            {
                ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Exito, menexito);
                return;
            }

            else
            {
                ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error, string.Format(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Errores.General, cError));
                return;
            }
        }

        else
        {
            ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error, sesionfallida);
            return;
        }
       
            
    }
    protected void btnRechazar_Click(object sender, EventArgs e)
    {
        string menexito = "Se realizo la accion satisfactoriamente";
        string sesionfallida = "Su Sesion no inicio Correctamente";
        string condicion = "Debe Escribir una observacion acerca del rechazo";
        string cError = string.Empty;
        clsUsuario usuario = (HttpContext.Current.Session[ConstantesSesion.USUARIO] as clsUsuario);
        

        if (ObservacionLiderValoracion.Text != null && ObservacionLiderValoracion.Text != string.Empty)
        {
            if (usuario != null)
            {
                int nIdDeclaracion;
                nIdDeclaracion = int.Parse(Request.QueryString["id"]);
                ILiderValoracionService LVService = new LiderValoracionService();
                bool Exito = LVService.RechazarValoracion(usuario.Id, nIdDeclaracion, ObservacionLiderValoracion.Text, ref cError);

                if (Exito)
                {
                    ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Exito, menexito);
                    return;
                }

                else
                {
                    ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error, string.Format(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Errores.General, cError));
                   return;
                }

            }

            else
            {
                ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error, sesionfallida);
                return;
            }
        }

        else 
        {
            ShowMessage(Ruv.Infrastructure.Crosscutting.Resources.Globalization.Controles.Error, condicion);           
            return;
        }
       
    }

    private void ShowMessage(string sTitle, string sMessage)
    {
        mpuMensaje.Titulo = sTitle;
        mpuMensaje.MostrarBotones = true;
        mpuMensaje.MostrarImagen = false;
        mpuMensaje.Mensaje = sMessage;
        mpuMensaje.Mostrar("~/Default.aspx");        
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void btnDescargarDocumento_Click(object sender, EventArgs e)
    {
        int nIdDeclaracion = int.Parse(Request.QueryString["id"]);
        string error = null;
        int valoracionId = new CargaDatosValoracionService().GetIdValoracionByIdDeclaracion(nIdDeclaracion, ref error);
        if (string.IsNullOrEmpty(error))
        {
            string Ruta = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];
            string NombreFolder = valoracionId.ToString();
            string NombreArchivo = valoracionId.ToString() + ".zip";

            //Verifica que exista el archivo, y lo regenera en caso de ser necesario
            if (!File.Exists(Ruta + NombreFolder + "/" + NombreArchivo))
            {
                ActosAdminService actosAdminService = new ActosAdminService();
                
                //1016 - Jefe de registro
                bool firmados = false;
                //Si el usuario tiene permisos de "firma de acto administrativo" se generan nuevamente los documentos firmados
                //if (RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.FirmaActoAdministrativo))
                //{
                //    firmados = true;
                //}

                actosAdminService.GenerarDocumentoValoracion(valoracionId, firmados, ref error);
            }

            if (File.Exists(Ruta + NombreFolder + "/" + NombreArchivo)) {
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
                Response.WriteFile(Ruta + NombreFolder + "/" + NombreArchivo);
                Response.Flush();
                Response.End();
            }
        }
    }
} 

