using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;

/// <summary>
/// Descripción breve de DataSourceTareas
/// </summary>
public class DataSourceTareas : IDataSourceBase
{
	public DataSourceTareas()
	{
        objService = new ValoracionService();
        eConsulta = new clsConsultaValoracion();
	}

    private ValoracionService objService;

    public clsConsultaValoracion eConsulta;

    public event Error ErrorConsulta;

    public int VirtualItemCount()
    {
        string error = string.Empty;

        eConsulta.Filtro = eConsulta.Filtro ?? string.Empty;
        eConsulta.TipoConsulta = Ruv.Infrastructure.Crosscutting.Common.eTipoConsulta.Total;
        objService.ListaTareasValoradorCantidad(ref eConsulta, ref error);
        if (!string.IsNullOrWhiteSpace(error))
        {
            OnError(null, new ErrorEventArgs(error));
        }
        return eConsulta.Total;
    }

    public IList<object> GetData(int startRow, int maxRows)
    {
        throw new NotImplementedException();
    }

    public IList<object> GetData(int startRow, int maxRows, string sortColumns)
    {
        if (string.IsNullOrWhiteSpace(sortColumns))
            sortColumns = "ID";
        string error = string.Empty;

        eConsulta.Filtro = eConsulta.Filtro ?? string.Empty;
        eConsulta.OrdenarPor = sortColumns;
        eConsulta.Pagina = startRow + 1;
        eConsulta.Tamaño = maxRows;
        eConsulta.TipoConsulta = Ruv.Infrastructure.Crosscutting.Common.eTipoConsulta.Listado;
        objService.ListaTareasValorador(ref eConsulta, ref error);
        if (!string.IsNullOrWhiteSpace(error))
        {
            OnError(null, new ErrorEventArgs(error));
        }
        if (eConsulta.Tareas != null)
            return eConsulta.Tareas.Cast<object>().ToList();
        else
            return new List<object>();
    }

    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }
}