using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common.ActosAdmin;


/// <summary>
/// Descripción breve de DataSourceActosAdmin
/// </summary>
public class DataSourceActosAdmin
{
	public DataSourceActosAdmin()
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

    public List<clsActosAdminstrativos> ObtenerActosAdministrativos(int startRow, int pageSize, string sortColumns)
    {
        pageSize = 20;
        if (sortColumns.Length > 0)
            SortColumns = sortColumns;
        ActosAdminService valSrv = new ActosAdminService();
        HttpContext.Current.Session[ConstantesItems.ACTOS_ADMIN] = valSrv.GetActosAdminPaginado(startRow, pageSize, sortColumns);
        return (HttpContext.Current.Session[ConstantesItems.ACTOS_ADMIN] as List<clsActosAdminstrativos>);

    }

    public int Cantidad()
    {
        ActosAdminService valSrv = new ActosAdminService();

        return valSrv.CantidadActosAdmin();
    }



    public List<clsActosAdminstrativos> ObtenerActosAdministrativosFiltro(string tipoFiltro, string valorFiltro)
    {
        ActosAdminService valSrv = new ActosAdminService();
        HttpContext.Current.Session[ConstantesItems.ACTOS_ADMIN] = valSrv.GetActosAdminFitro(tipoFiltro, valorFiltro);
        return (List<clsActosAdminstrativos>)HttpContext.Current.Session[ConstantesItems.ACTOS_ADMIN];
    }
}