using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;


/// <summary>
/// Descripción breve de DataSourceAsignacion
/// </summary>
public class DataSourceAsignacion
{
    public string SNombreFiltro { get; set; }
    public string SValorFiltro { get; set; }

	public DataSourceAsignacion()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//

	}

    public string SortColumns
    {
        set;
        get;
    }
    public List<clsDeclaracionValoraracion> ObtenerDeclaracionesSinValorar(int startRow, int pageSize, string sortColumns)
    {
        pageSize = 20;
        if (sortColumns.Length > 0)
            SortColumns = sortColumns;
        ValoracionService valSrv = new ValoracionService();
        List<clsDeclaracionValoraracion> lstDeclaraciones = valSrv.ListarDeclaracionesSinValorarPaginada(startRow, pageSize, sortColumns, SNombreFiltro, SValorFiltro);
        return lstDeclaraciones;

    }

    public int CantidadSinValorar()
    {
        ValoracionService valSrv = new ValoracionService();

        return valSrv.CantidadDeclaracionesSinValorar(SNombreFiltro, SValorFiltro);
    }

}