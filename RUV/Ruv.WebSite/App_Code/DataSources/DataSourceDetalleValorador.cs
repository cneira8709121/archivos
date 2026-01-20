using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Business.DTO.GestionValorador;
using Ruv.Infrastructure.Crosscutting.Common;

/// <summary>
/// Summary description for DataSourceDetalleValorador
/// </summary>
public class DataSourceDetalleValorador : IDataSourceBase
{
	public DataSourceDetalleValorador()
	{

        cError = string.Empty;
        ServicioGestionVal = new GestionValoradorService();
    }

    GestionValoradorService ServicioGestionVal;
    string cError;
    public event Error ErrorConsulta;
    public int NIdValorador{ get; set; }
    public DateTime FechaSolicitada { get; set; }

    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }

    public int VirtualItemCount()
    {
        return ServicioGestionVal.DetalleValoradorContador(NIdValorador,FechaSolicitada,ref cError);
    }

    public IList<object> GetData(int startRow, int maxRows)
    {
       
      
        if (startRow == 0)
            startRow = 1;
        List<clsDetalleGestionVal> lstValorador = ServicioGestionVal.DetalleGestionValorador(NIdValorador, FechaSolicitada,startRow, maxRows, ref cError);
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