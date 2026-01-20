using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;
using SIRAV.Entidades.Administracion;

public partial class Valoracion_ReasignarValoraciones : PaginaBase
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.UrlCurrenPage = Request.Url.AbsolutePath;
        Master.ValidarPermisoPagina();
        Master.CargarOpcionesporUrl();
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);

        if (!Page.IsPostBack)
        {
            ConsultaValoracion = new clsConsultaValoracion();
            ListarValoradores();
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
                Response.Redirect("~/Default.aspx");
                break;
            default:
                break;
        }
    }


    protected void mpGuardar_Ok(object sender, EventArgs e)
    {
        Response.Redirect("AsignarValoraciones.aspx");
    }

    protected void gvDeclSinValorar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<int> sele = GuardarCkecks();
        foreach (GridViewRow row in gvDeclSinValorar.Rows)
        {
            foreach (int va in sele)
            {
                CheckBox chk = row.Cells[10].FindControl("chkSelec") as CheckBox;
                int valor = Convert.ToInt32(gvDeclSinValorar.DataKeys[row.RowIndex].Value);
                if (valor == va)
                {
                    chk.Checked = true;
                }
            }
        }
    }

    protected void filtro_Filtro(object sender, FiltroEventArgs e)
    {
        clsTipoFiltro filtropor = DataSourceGeneral.ObtenerFiltroPorId(e.Filtro.FiltroPor, Proceso.Reasignacion);
        Filtros filtro = (Filtros)Enum.ToObject(typeof(Filtros), filtropor.Id);

        string filtroT = string.Empty;

        filtroT = filtropor.Nombre;

        switch (filtropor.TipoDato)
        {
            case TypeCode.DateTime:
                if (e.Filtro.Fecha1.HasValue && e.Filtro.Fecha2.HasValue)
                {
                    filtroT = string.Format("{0} BETWEEN TO_DATE('{1}','dd/mm/yyyy') AND TO_DATE('{2}','dd/mm/yyyy')", filtropor.Nombre, e.Filtro.Fecha1.Value.ToShortDateString(), e.Filtro.Fecha2.Value.ToShortDateString());
                }
                if (e.Filtro.Fecha1.HasValue && !e.Filtro.Fecha2.HasValue)
                {
                    filtroT = string.Format("{0} = '{1}'", filtropor.Nombre, e.Filtro.Fecha1.Value.ToShortDateString());
                }
                break;
            case TypeCode.Int32:
                if (!string.IsNullOrWhiteSpace(e.Filtro.Texto1) && !string.IsNullOrWhiteSpace(e.Filtro.Texto2))
                {
                    filtroT = string.Format("{0} BETWEEN {1} AND {2}", filtropor.Nombre, e.Filtro.Texto1, e.Filtro.Texto2);
                }
                if (e.Filtro.Fecha1.HasValue && !e.Filtro.Fecha2.HasValue)
                {
                    filtroT = string.Format("{0} = {1}", filtropor.Nombre, e.Filtro.Texto1);
                }
                break;
            case TypeCode.String:
                filtroT = string.Format("{0} LIKE '%{1}%'", filtropor.Nombre, e.Filtro.Texto1);
                break;
            default:
                break;
        }
        ConsultaValoracion.Filtro = filtroT;
        odtSinValorar.Select();
        gvDeclSinValorar.DataBind();

        Master.OcultarMensajeGenerico();
    }

    protected void gvDeclSinValorar_SelectedIndexChanged(object sender, EventArgs e)
    {
        mpAdvertenciaDeshacer.Mostrar();
    }

    protected void mpAdvertenciaDeshacer_Ok(object sender, EventArgs e)
    {
        int valoracionId = Convert.ToInt32(gvDeclSinValorar.SelectedValue);

        ValoracionService objValoracionServ = new ValoracionService();
        clsValoracion valoracion = objValoracionServ.ValoracionPorId(valoracionId, false);

        valoracion.EstadoId = (int)eEstadosValoracion.ValoracionDevueltaAsignacion;
        valoracion.Observacion = mpAdvertenciaDeshacer.MensajeTextBox;

        if (!objValoracionServ.DeshacerAsignacion(valoracion))
        {
            lblError.Text = "Error al deshacer asignación";
            return;
        }
        Response.Redirect("ReasignarValoraciones.aspx");
    }

    protected void odtSinValorar_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        DataSourceDeclSinValorar SinVal = e.ObjectInstance as DataSourceDeclSinValorar;
        SinVal.eConsulta = ConsultaValoracion;
    }

    #endregion

    #region Propiedades


    private clsConsultaValoracion ConsultaValoracion
    {
        get
        {
            if (Session[ConstantesItems.DECLARACIONES_ASIGNADAS] == null)
                Session[ConstantesItems.DECLARACIONES_ASIGNADAS] = new clsConsultaValoracion();

            return (clsConsultaValoracion)Session[ConstantesItems.DECLARACIONES_ASIGNADAS];
        }
        set
        {
            Session[ConstantesItems.DECLARACIONES_ASIGNADAS] = value;
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
        //ValoracionService objValoracionServ = new ValoracionService();
        //Valoradores = objValoracionServ.ListarValoradoresDisponibles();

        Ruv.WebApp.New_Join_SIRAV.Services.Administracion objvaloradores = new Ruv.WebApp.New_Join_SIRAV.Services.Administracion();
        List<SIRAV.Entidades.Administracion.INFORMACION_USUARIO> Valoradores = objvaloradores.obtenerUsuariosMenu("12030301");

        ddlValorador.DataSource = Valoradores;
        ddlValorador.DataBind();
    }



    private void Guardar()
    {
        ValoracionService objValoracionServ = new ValoracionService();
        List<int> sele = GuardarCkecks();
        int valoradorId = 0;
        int asignadorId = Varios.UsuarioId();
        int ValRUsuarioId = 0;
        if (ddlValorador.TienenValor)
        {
            valoradorId = Convert.ToInt32(ddlValorador.SelectedValue);
            string tokenApp = Session[ConstantesSesion.USUARIO_APP].ToString();
            SIRAV.Cliente.Administracion.ClienteUsuario objUsuario = new SIRAV.Cliente.Administracion.ClienteUsuario();
            USUARIO_PROGRAMA usuarioPrograma = objUsuario.ObtenerUsuarioPorPrograma(2, valoradorId, tokenApp);
            ValRUsuarioId = Convert.ToInt32(usuarioPrograma.ID_USUARIO_PROGRAMA);
        }

        List<clsValoracion> reasignaciones = new List<clsValoracion>();
        foreach (int decla in sele)
        {
            clsValoracion valoracion = objValoracionServ.ValoracionPorId(decla, false);
            valoracion.ValoradorId = ValRUsuarioId;
            valoracion.ValoradorRId = ValRUsuarioId;
            reasignaciones.Add(valoracion);
        }

        if (!objValoracionServ.Reasignar(reasignaciones))
        {
            lblError.Text = "Ocurrio un error guardando";
            return;
        }
        mpGuardar.Mostrar();
    }


    private List<int> GuardarCkecks()
    {
        List<int> sele = new List<int>();
        if (ViewState["Seleccionados"] != null)
        {
            sele = (List<int>)ViewState["Seleccionados"];
        }
        foreach (GridViewRow row in gvDeclSinValorar.Rows)
        {
            int valor = Convert.ToInt32(gvDeclSinValorar.DataKeys[row.RowIndex].Value);
            if (((CheckBox)row.Cells[10].FindControl("chkSelec")).Checked)
            {
                sele.Add(valor);
            }
        }
        ViewState["Seleccionados"] = sele;
        return sele;
    }

    
    #endregion
    
}