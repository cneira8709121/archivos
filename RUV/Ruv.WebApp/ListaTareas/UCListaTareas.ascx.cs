using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class ListaTareas_UCListaTareas : System.Web.UI.UserControl
{
    

    #region Propiedades

    //protected ASP.UCTarea UCTarea;

    private int IntStarRow;

    private int IntPageSize;

    public int IntCantidad;

    //public string FilterEx
    //{
    //    set;
    //    get;
    //}

    public string StrFiltro
    {
        get
        {
            return HFFiltroPor.Value;
        }
        set
        {
            HFFiltroPor.Value = value;
        }
    }

    public string StrOrden 
    {
        get
        {
            return HFOrdenPor.Value;
        }
        set
        {
            HFOrdenPor.Value = value;
        }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
            
        //}

        //IntCantidad = 36;
        //StrOrden = Filtros1.StrOrderCriteria;
        //CargarTareas(string.Empty, StrOrder, IntCantidad);
    }

    //protected void odsTareas_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
    //{
    //    DataSourceListaTareas info = e.ObjectInstance as DataSourceListaTareas;
    //    Session["TotalRegistros"] = info.CantidadTareas();
    //    if (info != null)
    //    {
    //        //info.SortColumns = SortColumns;
    //        info.FilterEx = FilterEx;
    //    }
    //}
    
    //protected void grvTareas_RowCommand(object sender, GridViewCommandEventArgs e)
    //{     
    //    if (e.CommandName == "Select")
    //    {
    //        DataSourceListaTareas info = new DataSourceListaTareas();

    //        string[] arrParametros = e.CommandArgument.ToString().Split(new char[] { '|' });
    //        clsListaTareas clsListaTareas = info.ObtenerTarea(int.Parse(arrParametros[0]), string.IsNullOrEmpty(arrParametros[1]) ? null : (int?)int.Parse(arrParametros[1]));

    //        if (clsListaTareas != null && (clsListaTareas.Tipo == "CORRECCION"))
    //        {
    //            Response.Redirect(string.Format("~/Correcciones/AprobarRechazarCorreccion.aspx?idCorreccion={0}&idRegpersona={1}&urlEvio={2}", clsListaTareas.Correccion,clsListaTareas.Regpersona, this.Request.Url.AbsolutePath));
    //        }

    //        if (clsListaTareas != null && (clsListaTareas.IdAccion == (int)eEstadoDeclaracion.ValoracionPendientePorRevision || clsListaTareas.IdAccion == (int)eEstadoDeclaracion.ValoracionPendientePorFirma))
    //        {
    //            Response.Redirect(string.Format("~/ActosAdmin/LiderValoracionJefe.aspx?id={0}&urlEvio={1}", arrParametros[0], this.Request.Url.AbsolutePath));
    //        }
    //    }
    //}

    protected void filtro_Filtro(object sender, FiltroEventArgs e)
    {
        clsTipoFiltro filtropor = DataSourceGeneral.ObtenerFiltroPorId(e.Filtro.FiltroPor, Proceso.ListaTareas);
        //Filtros filtro = (Filtros)Enum.ToObject(typeof(Filtros), filtropor.Id);

        string filtroT = string.Empty;

        filtroT = filtropor.Nombre;

        switch (filtropor.TipoDato)
        {
            case TypeCode.DateTime:
                if (e.Filtro.Fecha1.HasValue && e.Filtro.Fecha2.HasValue)
                {
                    filtroT = string.Format("{0} BETWEEN TO_DATE('{1}','dd/mm/yyyy') AND TO_DATE('{2}','dd/mm/yyyy')", e.Filtro.Fecha1.Value.ToShortDateString(), e.Filtro.Fecha2.Value.ToShortDateString());
                }
                if (e.Filtro.Fecha1.HasValue && !e.Filtro.Fecha2.HasValue)
                {
                    filtroT = string.Format("{0} = '{1}'", e.Filtro.Fecha1.Value.ToShortDateString());
                }
                break;
            case TypeCode.Int32:
                if (!string.IsNullOrWhiteSpace(e.Filtro.NombreDeclarante) && !string.IsNullOrWhiteSpace(e.Filtro.DocumentoDeclarante))
                {
                    filtroT = string.Format("{0} BETWEEN {1} AND {2}",  e.Filtro.NombreDeclarante, e.Filtro.DocumentoDeclarante);
                }
                if (e.Filtro.Fecha1.HasValue && !e.Filtro.Fecha2.HasValue)
                {
                    filtroT = string.Format("{0} = {1}",  e.Filtro.NombreDeclarante);
                }
                break;
            case TypeCode.String:
                filtroT = string.Format("{0} LIKE '{1}'",  e.Filtro.NombreDeclarante);
                break;
            default:
                break;
        }

        ////ConsultaValoracion.Filtro = filtroT;
        //FilterEx = filtroT;
        //grvTareas.DataBind();
        //Filtros1.StrFilterExpression = filtroT;
        //CargarTareas(filtroT, StrOrder, IntCantidad);

        //Ajuste no muy elegante, para poner las comillas simples en su lugar
        StrFiltro = filtroT.Replace("'","char(39)");
    }

    #endregion

    #region Funciones

    private void CargarTareas(string strFilter, string strOrder, int intCantidad)
    {
        DataSourceListaTareas DSListaTareas = new DataSourceListaTareas(strFilter);
        //int intCantidad = DSListaTareas.CantidadTareas();
        List<clsListaTareas> listaTareas = DSListaTareas.ObtenerListaTareas(1, intCantidad, strOrder);
        pnlTareasPendientes.Controls.Clear();
        //int intIdControl = 0;
        foreach (clsListaTareas tarea in listaTareas)
        {
            var UCTarea = LoadControl("~/ListaTareas/UCTarea.ascx") as Ruv.WebApp.Utilidades.Controles.IUCTarea;
            //UCTarea.ID = "UC" + (intIdControl++).ToString(); //Garantiza que cada uc de tarea tenga un ID único
            UCTarea.ID = "UC" + tarea.Declaracion.ToString();
            UCTarea.Formulario = tarea.Formulario;
            UCTarea.Estado = tarea.Accion;
            UCTarea.Fecha = tarea.Fecha;
            UCTarea.IdDeclaracion = tarea.Declaracion;
            UCTarea.IdCorreccion = (tarea.Correccion == null) ? null : (int?)tarea.Correccion;
            pnlTareasPendientes.Controls.Add(UCTarea as Control);
        }
    }

    //[WebMethod]
    //public static string Adicionar(string controlName)
    //{
    //    return RenderControl(controlName);
    //}

    //public static string RenderControl(string controlName)
    //{
    //    try
    //    {
    //        Page page = new Page();

    //        ASP.UCTarea UCTarea = (ASP.UCTarea)page.LoadControl(controlName);
    //        //UCTarea = (ASP.UCTarea)LoadControl("~/ListaTareas/UCTarea.ascx");
    //        UCTarea.Formulario = "AB00000012";
    //        UCTarea.Estado = "Paila";
    //        UCTarea.Fecha = DateTime.Now;
    //        UCTarea.IdDeclaracion = 123;
    //        UCTarea.IdCorreccion = 0;

    //        UCTarea.EnableViewState = false;

    //        HtmlForm form = new HtmlForm();
    //        form.Controls.Add(UCTarea);
    //        page.Controls.Add(form);

    //        StringWriter textWriter = new StringWriter();
    //        HttpContext.Current.Server.Execute(page, textWriter, false);
    //        return textWriter.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.ToString();
    //    }
    //}

    #endregion
}