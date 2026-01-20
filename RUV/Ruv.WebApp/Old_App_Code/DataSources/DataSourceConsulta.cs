using System.Collections.Generic;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;
using Ruv.WebApp.Presentation.Adapters.Base;
using System;
using Ruv.Infrastructure.Crosscutting.Common;

public class DataSourceConsulta : IDataSourceBase
{
    int startRow = 1;

    public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

    public ConsultarEstadoPersonaService cepService;

    private string error;

    public DataSourceConsulta() {
        cepService = new ConsultarEstadoPersonaService();
        error = string.Empty;
    }

    public clsConsultarEstadoDeclaracionSolicitud RequestInfo { get; set; }

    public int VirtualItemCount()
    {
        return cepService.ConsultarEstadoDeclaracionConteo(RequestInfo, ref error);
    }

    public IList<object> GetData(int startRow, int maxRows)

    {
        if (startRow == 0)
            startRow = 1;
        clsConsultarEstadoDeclaracionRespuesta EstadoDecla = cepService.ConsultarEstadoDeclaracionPaginado(RequestInfo,startRow, maxRows,ref error);
        if (!string.IsNullOrWhiteSpace(error))
        {
            OnError(null, new ErrorEventArgs(error));
        }
        IList<object> result = new List<object>();
        if (EstadoDecla.LstEstadoDeclaracion != null)
            EstadoDecla.LstEstadoDeclaracion.ForEach(x => { result.Add(x); });

        return result;
    }

    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }

    public IList<object> GetData(int startRow, int maxRows, string sortColumns)
    {
        throw new NotImplementedException();
    }
}