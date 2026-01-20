using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Business.DTO.GestionValorador;
using Ruv.Infrastructure.Crosscutting.Common;

/// <summary>
/// Summary description for DataSourceConsultaValorador
/// </summary>
public class DataSourceConsultaValorador : IDataSourceBase
{
	public DataSourceConsultaValorador()
	{
        cError = string.Empty;
        ServicioGestionVal = new GestionValoradorService();
	}

    GestionValoradorService ServicioGestionVal;
    string cError;
    public event Error ErrorConsulta;

    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }
    
    public int VirtualItemCount()
    {
        return ServicioGestionVal.ContadorValoradores(ref cError);
    }

    public IList<object> GetData(int startRow, int maxRows)
    {
        if (startRow == 0)
            startRow = 1;
        List<clsGestionValorador> lstValorador = ServicioGestionVal.CargaDatosValorador(startRow,maxRows,ref cError);
        if (!string.IsNullOrWhiteSpace(cError))
        {
            OnError(null, new ErrorEventArgs(cError));
        }

        IList<object> result = new List<object>();
        if (lstValorador == null)
            return result;


        return lstValorador.Cast<object>().ToList();
    }

    public IList<object> GetData(int startRow, int maxRows, string sortColumns)
    {
        throw new NotImplementedException();
    }
}