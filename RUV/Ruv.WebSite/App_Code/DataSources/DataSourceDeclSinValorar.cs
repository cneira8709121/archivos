using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Web.UI;
using Ruv.Infrastructure.Crosscutting.Common;

/// <summary>
/// Descripción breve de DataSourceDeclSinValorar
/// </summary>
public class DataSourceDeclSinValorar : IDataSourceBase
{
    public DataSourceDeclSinValorar()
    {
        objValoracion = new ValoracionService();
        eConsulta = new clsConsultaValoracion();
    }


    public string Filtro { get; set; }

    public event Error ErrorConsulta;

    ValoracionService objValoracion;

    public clsConsultaValoracion eConsulta;

    public int VirtualItemCount()
    {
        string error = string.Empty;
        eConsulta.TipoConsulta = Ruv.Infrastructure.Crosscutting.Common.eTipoConsulta.Total;
        eConsulta.Filtro = eConsulta.Filtro ?? string.Empty;
        objValoracion.ListaDeclaracionesEnValTotal(ref eConsulta, ref error);
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

        eConsulta.Declaraciones = new List<clsDeclaracionValoraracion>();
        eConsulta.Filtro = eConsulta.Filtro ?? string.Empty;
        eConsulta.OrdenarPor = sortColumns;
        eConsulta.Pagina = (startRow == 0) ? 1 : startRow;
        eConsulta.Tamaño = maxRows;
        eConsulta.TipoConsulta = Ruv.Infrastructure.Crosscutting.Common.eTipoConsulta.Listado;
        objValoracion.ListaDeclaracionesEnValPaginada(ref eConsulta, ref error);
        if (!string.IsNullOrWhiteSpace(error))
        {
            OnError(null, new ErrorEventArgs(error));
        }
        return eConsulta.Declaraciones.Cast<object>().ToList(); ;
    }
    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }

}