using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Web.UI;
using Ruv.Infrastructure.Crosscutting.Common;

/// <summary>
/// Summary description for DataSourceListaTareas
/// </summary>
public class DataSourceListaTareas : IDataSourceBase
{
	public DataSourceListaTareas()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSourceListaTareas(string strFilter)
    {
        FilterEx = strFilter;
    }

    public string SortColumns
    {
        set;
        get;
    }

    public string FilterEx
    {
        set;
        get;
    }

    public List<clsListaTareas> ObtenerListaTareas(int startRow, int pageSize, string sortColumns)
    {
        if (pageSize == 0)
            pageSize = 20;

        //if (sortColumns.Length > 0)
        //    SortColumns = sortColumns;

        //startRow = startRow / pageSize;


        //startRow++;

        int idUsuario = RUV.Current.Usuario.ID;
        string LlaveUsuario = string.Empty;/*RUV.Current.LlaveUsuario;*/

        //Page currentPage = (Page)HttpContext.Current.Handler;
        //currentPage.Master.Attributes

        //Page currentPage = (Page)HttpContext.Current.Handler;
        //object mp = (MasterPage)currentPage.Master;
        //LlaveUsuario = mp.LlaveUsuario;

        GeneralService service = new GeneralService();
        HttpContext.Current.Session[ConstantesListaTareas.ListasTareas] = service.ObtenerListaTareasPaginado(idUsuario, LlaveUsuario, startRow, pageSize, sortColumns, FilterEx);

        //HttpContext.Current.Session[ConstantesListaTareas.ListasTareas] = service.ObtenerListaTareas(idUsuario, LlaveUsuario);

        return (HttpContext.Current.Session[ConstantesListaTareas.ListasTareas] as List<clsListaTareas>);
    }

    public int CantidadTareas()
    {
        int idUsuario = RUV.Current.Usuario.ID;

        GeneralService service = new GeneralService();
        return service.ObtenerListaTareasCantidad(idUsuario);
    }

    public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }

    public int VirtualItemCount()
    {
        //return cepService.ConsultarEstadoDeclaracionConteo(RequestInfo, ref error);

        throw new NotImplementedException();
    }

    public IList<object> GetData(int startRow, int maxRows)
    {
        //clsConsultarEstadoDeclaracionRespuesta EstadoDecla = cepService.ConsultarEstadoDeclaracion(RequestInfo, ref error);
        //if (!string.IsNullOrWhiteSpace(error))
        //{
        //    OnError(null, new ErrorEventArgs(error));
        //}
        //IList<object> result = new List<object>();
        //if (EstadoDecla.LstEstadoDeclaracion != null)
        //    EstadoDecla.LstEstadoDeclaracion.ForEach(x => { result.Add(x); });

        //return result;

        throw new NotImplementedException();
    }

    public IList<object> GetData(int startRow, int maxRows, string sortColumns)
    {
        throw new NotImplementedException();
    }

    public clsListaTareas ObtenerTarea(int idDeclaracion, int? nIdCorreccion)
    {
        clsListaTareas clsListaTareas = null;

        if (HttpContext.Current.Session[ConstantesListaTareas.ListasTareas] != null)
        {
            clsListaTareas = (HttpContext.Current.Session[ConstantesListaTareas.ListasTareas] as List<clsListaTareas>).FirstOrDefault(x => x.Declaracion == idDeclaracion && x.Correccion == nIdCorreccion);
        }

        return clsListaTareas;
    }
}