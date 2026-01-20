using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Data;
using Ruv.WebApp.Common;
using System.Configuration;

public partial class Valoracion_Default : PaginaBase
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.UrlCurrenPage = Request.Url.AbsolutePath;
        Master.ValidarPermisoPagina();
        Master.CargarOpcionesporUrl();
        Master.QuitarMenus(new string[] { "Valorar", "Resumen" });
        Master.OnOptionClick += new OptionHandler(Master_OnOptionClick);

        if (!Page.IsPostBack)
        {
            var mensaje = Request.QSStringField("errorMessage");
            if(!string.IsNullOrEmpty(mensaje))
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "<script>alert('"+mensaje+"');</script>", false);

            ConsultaValoracion = new clsConsultaValoracion();
            ConsultaValoracion.ValoradorId = Varios.UsuarioId();
        }
    }

    void Master_OnOptionClick(object sender, OptionEventArgs e)
    {
        switch (e.ControlName)
        {
            case "Valorar":
                Valorar();
                break;
            case "Informe":
                Informe();
                break;
            case "Atras":
                Response.Redirect("~/Default.aspx");
                break;
            default:
                break;
        }
    }

    private void Informe()
    {
        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), Guid.NewGuid().ToString(), "<script>alert('El reporte no esta disponible');</script>", false);
    }

    protected void grdValoraciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        Valorar();
    }

    protected void filtro_Filtro(object sender, FiltroEventArgs e)
    {
        clsTipoFiltro filtropor = DataSourceGeneral.ObtenerFiltroPorId(e.Filtro.FiltroPor, Proceso.Valoracion);
        Filtros filtro = (Filtros)Enum.ToObject(typeof(Filtros), filtropor.Id);

        string filtroT = string.Empty;

        filtroT = filtropor.Nombre;

        switch (filtropor.TipoDato)
        {
            case TypeCode.DateTime:
                if (e.Filtro.Fecha1.HasValue && e.Filtro.Fecha2.HasValue)
                {
                    filtroT = string.Format("{0} BETWEEN to_date('{1}', 'dd/mm/yyyy') AND to_date('{2}', 'dd/mm/yyyy')", filtropor.Nombre, e.Filtro.Fecha1.Value.ToShortDateString(), e.Filtro.Fecha2.Value.ToShortDateString());
                }
                if (e.Filtro.Fecha1.HasValue && !e.Filtro.Fecha2.HasValue)
                {
                    filtroT = string.Format("{0} = '{1}'", filtropor.Nombre, e.Filtro.Fecha1.Value.ToShortDateString());
                }
                break;
            //case TypeCode.Int32:
            //    if (!string.IsNullOrWhiteSpace(e.Filtro.Texto1) && !string.IsNullOrWhiteSpace(e.Filtro.Texto2))
            //    {
            //        filtroT = string.Format("{0} BETWEEN {1} AND {2}", filtropor.Nombre, e.Filtro.Texto1, e.Filtro.Texto2);
            //    }
            //    if (e.Filtro.Fecha1.HasValue && !e.Filtro.Fecha2.HasValue)
            //    {
            //        filtroT = string.Format("{0} = {1}", filtropor.Nombre, e.Filtro.Texto1);
            //    }
                break;
            case TypeCode.String:
                //int tamano = e.Filtro.Texto1.Length;

                string filtrosub = string.Empty;
               // if (tamano > 0)
                  //  filtrosub = e.Filtro.Texto1.Remove(0, 1);
                //filtrosub = e.Filtro.Texto1;
                //filtroT = string.Format("{0} LIKE '%{1}%'", filtropor.Nombre, filtrosub);
                filtroT = string.Format("{0} = '{1}'", filtropor.Nombre, filtrosub);
                break;
            default:
                break;
        }

        ConsultaValoracion.Filtro = filtroT;
        grdValoraciones.DataBind();

        Master.OcultarMensajeGenerico();
    }

    protected void grdValoraciones_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.DataItem as clsValoradorTareas).Observacion != null) {
                e.Row.BackColor = System.Drawing.Color.LightYellow;
                e.Row.ToolTip = string.Format("({0}) - {1}", (e.Row.DataItem as clsValoradorTareas).FechaActualizacion.ToShortDateString(), (e.Row.DataItem as clsValoradorTareas).Observacion);
            }
        }
    }

    #endregion

    #region Propiedades

    private List<clsValoradorTareas> ValoracionesPorValorador
    {
        get
        {
            if (Session[ConstantesItems.TAREAS_VALORADOR] == null)
                Session[ConstantesItems.TAREAS_VALORADOR] = new List<clsValoradorTareas>();

            return (List<clsValoradorTareas>)Session[ConstantesItems.TAREAS_VALORADOR];
        }
        set
        {
            Session[ConstantesItems.TAREAS_VALORADOR] = value;
        }
    }

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

    #endregion

    #region Metodos
    [Obsolete("Antigua Forma de Cargar la lista de tareas")]
    private void ObtenerValoraciones()
    {
        int valoradorId = Varios.UsuarioId();
        ValoracionService objValService = new ValoracionService();
        ValoracionesPorValorador = objValService.ListarValoracionesPorValoradorId(valoradorId);
        grdValoraciones.DataSource = ValoracionesPorValorador;
        grdValoraciones.DataBind();
    }

    private void Valorar()
    {
        if (grdValoraciones.SelectedValue != null)
        {
            int valoracionid = Convert.ToInt32(grdValoraciones.SelectedValue);
            ValoracionService objValoracion = new ValoracionService();
            clsValoracion _valoracion = objValoracion.ValoracionPorId(valoracionid, false);
            if (_valoracion.EstadoId != (int)eEstadosValoracion.PendientePorNotificar)
            {
                Response.Redirect("Nueva.aspx?id=" + valoracionid);
            }
            else
            {
                lblError.Text = "El estado es Pendiente por notificar, ya no puede ser valorada de nuevo";
            }
        }
        else
        {
            lblError.Text = "Seleccione una declaracion para valorar";
        }
    }

    protected void odsListaTareas_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    {
        DataSourceTareas LTareas = e.ObjectInstance as DataSourceTareas;
        LTareas.eConsulta = ConsultaValoracion;
    }

    #endregion
}