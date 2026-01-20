using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.IO;
using Ruv.Infrastructure.Crosscutting.Common;
using SIRAV.Entidades.Administracion;

public partial class Valoracion_AsignarValoraciones : PaginaBase
{

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.UrlCurrenPage = Request.Url.AbsolutePath;
        Master.ValidarPermisoPagina();
        Master.CargarOpcionesporUrl();
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);
        txtNombreDeclarante.Visible = true;
        if (!Page.IsPostBack)
        {

            Session["Seleccionados"] = null;
            ListarValoradores();
            //ObtenerDeclaracionesSinValorar();
        }
    }

    void Master_OnOptionClick(object sender, OptionEventArgs e)
    {
        switch (e.ControlName)
        {
            case "Guardar":
                Guardar();
                break;
            case "Atras":
                if (mvAsignar.ActiveViewIndex == 0)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    mvAsignar.ActiveViewIndex = 0;
                }
                break;
            case "Exportar":
                ExportarAExcel();
                break;
            default:
                break;
        }
    }

    private void ExportarAExcel()
    {

        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "<script>alert('El reporte no esta disponible');</script>", false);

    }

    protected void mpGuardar_Ok(object sender, EventArgs e)
    {
        gvDeclSinValorar.DataBind();
        ModalPopUp.Ocultar();
    }



    protected void gvDeclSinValorar_PageIndexChanged(object sender, EventArgs e)
    {

        List<int> sele = GuardarCkecks();
        foreach (GridViewRow row in gvDeclSinValorar.Rows)
        {
            foreach (int va in sele)
            {
                CheckBox chk = row.Cells[10].FindControl("chkSelec") as CheckBox;
                int valor = Convert.ToInt32(gvDeclSinValorar.Rows[row.RowIndex].Cells[0].Text);
                if (valor == va)
                {
                    chk.Checked = true;
                }
            }
        }
    }

    private void MostrarMensage(string mensaje)
    {
        mpGuardar.MostrarImagen = false;
        mpGuardar.MostrarBotones = true;
        mpGuardar.filatextBox = false;
        mpGuardar.Mensaje = mensaje;
        mpGuardar.Mostrar();
    }


    protected void gvDeclSinValorar_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValoracionService objValoracionServ = new ValoracionService();
        int declaracionId = Convert.ToInt32(gvDeclSinValorar.SelectedValue);
        List<clsPersona> personas = objValoracionServ.ListarPersonasPorDeclaracion(declaracionId);
        gvPersonasAnexos.DataSource = personas;
        gvPersonasAnexos.DataBind();
        mvAsignar.ActiveViewIndex = 1;
    }
    protected void dataEmpInfo_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        DataSourceAsignacion info = e.ObjectInstance as DataSourceAsignacion;
        info.SNombreFiltro = new clsFiltro();
        info.SNombreFiltro.NombreDeclarante = txtNombreDeclarante.Text;
        info.SNombreFiltro.DocumentoDeclarante = txtDocumentoDeclarante.Text;
        info.SNombreFiltro.NombreDeclarante = txtNombreDeclarante.Text;
        info.SNombreFiltro.DocumentoDeclarante = txtDocumentoDeclarante.Text;
        info.SNombreFiltro.NumeroFormulario =  txtNumeroFormulario.Text;
        info.SNombreFiltro.Estado = txtEstadoValoracion.Text;
        info.SNombreFiltro.RegimenEspecial = txtRegimenEspecial.Text;
        info.SNombreFiltro.Etnia = txtEtnia.Text;
        info.SNombreFiltro.Genero = txtGenero.Text;
        info.SNombreFiltro.Entidad = txtEntidad.Text;
        info.SNombreFiltro.Municipio = txtMunicipio.Text;
        info.SNombreFiltro.Departamento = txtDepartamento.Text;

        if (txtFecha1.Fecha.ToString() == "1/01/0001 12:00:00 a. m.")
        {
            if (txtFecha2.Fecha.ToString() == "1/01/0001 12:00:00 a. m.")
            {
                info.SNombreFiltro.Fecha1 = null;
                info.SNombreFiltro.Fecha2 = null;
            }
        }
        else
        {
            info.SNombreFiltro.Fecha1 = txtFecha1.Fecha;
            info.SNombreFiltro.Fecha2 = txtFecha2.Fecha;
        }
    }

    protected void gvDeclSinValorar_Sorting(object sender, GridViewSortEventArgs e)
    {
        #region FiltroAntigui
        //if (GridViewSortDirection == SortDirection.Ascending)
        //{
        //    GridViewSortDirection = SortDirection.Descending;
        //    switch (e.SortExpression)
        //    {
        //        case "NombreDeclarante":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderBy(x => x.NombreDeclarante).ToList();
        //            break;
        //        case "DocumentoDeclarante":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderBy(x => x.DocumentoDeclarante).ToList();
        //            break;
        //        case "FechaRadicado":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderBy(x => x.FechaRadicado).ToList();
        //            break;
        //        case "NumeroFormulario":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderBy(x => x.NumeroFormulario).ToList();
        //            break;
        //        case "TotalHv":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderBy(x => x.TotalHV).ToList();
        //            break;
        //        case "Departamento":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderBy(x => x.Departamento).ToList();
        //            break;
        //        case "Municipio":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderBy(x => x.Municipio).ToList();
        //            break;
        //        case "Entidad":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderBy(x => x.Entidad).ToList();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else
        //{
        //    GridViewSortDirection = SortDirection.Ascending;
        //    switch (e.SortExpression)
        //    {
        //        case "NombreDeclarante":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderByDescending(x => x.NombreDeclarante).ToList();
        //            break;
        //        case "DocumentoDeclarante":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderByDescending(x => x.DocumentoDeclarante).ToList();
        //            break;
        //        case "FechaRadicado":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderByDescending(x => x.FechaRadicado).ToList();
        //            break;
        //        case "NumeroFormulario":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderByDescending(x => x.NumeroFormulario).ToList();
        //            break;
        //        case "TotalHv":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderByDescending(x => x.TotalHV).ToList();
        //            break;
        //        case "Departamento":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderByDescending(x => x.Departamento).ToList();
        //            break;
        //        case "Municipio":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderByDescending(x => x.Municipio).ToList();
        //            break;
        //        case "Entidad":
        //            DeclaracionesSinValorar = DeclaracionesSinValorar.OrderByDescending(x => x.Entidad).ToList();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //gvDeclSinValorar.DataSource = DeclaracionesSinValorar;
        //gvDeclSinValorar.DataBind();
        #endregion
    }

    protected void filtro_Filtro(object sender, FiltroEventArgs e)
    {
        //ObtenerDeclaracionesSinValorar();
        string fechas = e.Filtro.Fecha1.ToString() + ";" + e.Filtro.Fecha2.ToString();

        ValoracionService ObjValoracion = new ValoracionService();
        clsTipoFiltro filtropor = DataSourceGeneral.ObtenerFiltroPorId(e.Filtro.FiltroPor, Proceso.Asignacion);
        Filtros filtro = (Filtros)Enum.ToObject(typeof(Filtros), filtropor.Id);

        ViewState["NombreFiltro"] = filtropor.Nombre;
        if (fechas == "01/01/0001 12:00:00 a.m.;01/01/0001 12:00:00 a.m.")
            ViewState["ValorFiltro"] = e.Filtro.DocumentoDeclarante;
        else
            ViewState["ValorFiltro"] = fechas;

        ObjectDataSource1.Select();
        gvDeclSinValorar.DataBind();

        Master.OcultarMensajeGenerico();
    }


    #endregion

    #region Propiedades

    [Obsolete]
    public SortDirection GridViewSortDirection
    {

        get
        {

            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];

        }

        set { ViewState["sortDirection"] = value; }

    }
    private List<clsDeclaracionValoraracion> DeclaracionesSinValorar
    {
        get
        {
            if (Session[ConstantesItems.DECLARACIONES_NO_VAL] == null)
                Session[ConstantesItems.DECLARACIONES_NO_VAL] = new List<clsDeclaracionValoraracion>();

            return (List<clsDeclaracionValoraracion>)Session[ConstantesItems.DECLARACIONES_NO_VAL];
        }
        set
        {
            Session[ConstantesItems.DECLARACIONES_NO_VAL] = value;
        }
    }

    private List<clsValorador> Valoradores
    {
        get
        {
            if (Session[ConstantesItems.VALORADORES] == null)
                Session[ConstantesItems.VALORADORES] = new List<clsValorador>();

            return (List<clsValorador>)Session[ConstantesItems.VALORADORES];
        }
        set
        {
            Session[ConstantesItems.VALORADORES] = value;
        }
    }


    #endregion

    #region Metodos Privados

    private void ListarValoradores()
    {

        Ruv.WebApp.New_Join_SIRAV.Services.Administracion objvaloradores = new Ruv.WebApp.New_Join_SIRAV.Services.Administracion();
        List<SIRAV.Entidades.Administracion.INFORMACION_USUARIO> resultado = objvaloradores.obtenerUsuariosMenu("12030301");

        ddlValorador.DataSource = resultado;
        ddlValorador.DataBind();
    }



    private void Guardar()
    {
        ValoracionService objValoracionServ = new ValoracionService();
        //Ruv.WebApp.New_Join_SIRAV.Services.Administracion objValoracionServ = new Ruv.WebApp.New_Join_SIRAV.Services.Administracion();
        List<int> sele = GuardarCkecks();
        ViewState["Seleccionados"] = null;
        int valoradorId = 0;
        int ValRUsuarioId = 0;
        int asignadorId = Varios.UsuarioId();
        if (ddlValorador.TienenValor)
        {
            valoradorId = Convert.ToInt32(ddlValorador.SelectedValue);
            string tokenApp = Session[ConstantesSesion.USUARIO_APP].ToString();
            SIRAV.Cliente.Administracion.ClienteUsuario objUsuario = new SIRAV.Cliente.Administracion.ClienteUsuario();
            USUARIO_PROGRAMA usuarioPrograma = objUsuario.ObtenerUsuarioPorPrograma(2, valoradorId, tokenApp);
            ValRUsuarioId = Convert.ToInt32(usuarioPrograma.ID_USUARIO_PROGRAMA);
        }
        else
        {
            lblError.Text = "Seleccione el valorador";
            return;
        }
        List<clsValoracion> asignaciones = new List<clsValoracion>();
        foreach (int decla in sele)
        {
            asignaciones.Add(new clsValoracion() { Id = 0, DeclaracionId = decla, EstadoId = (int)eEstadosValoracion.PendientePorValorar, FechaAsignacion = DateTime.Now, ValoradorId = ValRUsuarioId, AsignadorId = asignadorId, ValoradorRId = ValRUsuarioId });
        }

        if (!objValoracionServ.Asignar(asignaciones))
        {
            lblError.Text = "Ocurrio un error guardando";
            return;
        }
        MostrarMensage("Se asignaron correctamente las declaraciones al valorador");
    }


    private List<int> GuardarCkecks()
    {
        List<int> sele = new List<int>();
        if (Session["Seleccionados"] != null)
        {
            sele = (List<int>)Session["Seleccionados"];
        }
        foreach (GridViewRow row in gvDeclSinValorar.Rows)
        {
            if (((CheckBox)row.Cells[10].FindControl("chkSelec")).Checked)
            {
                int valor = Convert.ToInt32(gvDeclSinValorar.Rows[row.RowIndex].Cells[0].Text);
                if (!sele.Contains(valor))
                {
                    sele.Add(valor);
                }
            }
        }
        Session["Seleccionados"] = sele;
        return sele;
    }
    #endregion
    #region Filtros
    public event FiltroHandler Filtro;

    public void btnFiltrar_Click(object sender, EventArgs e)
    {
        gvDeclSinValorar.DataBind();
        //dataEmpInfo_ObjectCreated(sender, new FiltroEventArgs(info.SNombreFiltro()));
    }

    void OnFiltro(object sender, FiltroEventArgs e)
    {
        if (Filtro != null)
        {
            Filtro(sender, e);
        }
    }

    public void LimpiarCampos()
    {
        txtNombreDeclarante.Text    = string.Empty;
        txtDocumentoDeclarante.Text = string.Empty;
        txtNumeroFormulario.Text    = string.Empty;
        txtEstadoValoracion.Text    = string.Empty;
        txtRegimenEspecial.Text     = string.Empty;
        txtEtnia.Text               = string.Empty;
        txtGenero.Text              = string.Empty;
        txtFecha1.Text              = string.Empty;
        txtFecha2.Text              = string.Empty;
        txtEntidad.Text             = string.Empty;
        txtMunicipio.Text           = string.Empty;
        txtDepartamento.Text        = string.Empty; 
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        LimpiarCampos();
        Response.Redirect(Request.Url.AbsolutePath);
    }

    #endregion
}
