using System.Collections.Generic;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;
using Ruv.WebApp.Presentation.Adapters.Base;
using System;
using Ruv.Infrastructure.Crosscutting.Common;

public class DataSourceCorrecciones : IDataSourceBase
{
    #region Events

    #region Services events

    public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

    #endregion

    #endregion

    public CorreccionesService cService;

    private string error;

    public DataSourceCorrecciones()
    {
        cService = new CorreccionesService();
        error = string.Empty;
    }

    public clsConsultarEstadoDeclaracionSolicitud RequestInfo { get; set; }

    #region Public methods

    #region Services implemantation

    public int VirtualItemCount()
    {
        return cService.ConsultarEstadoDeclaracionConteo(RequestInfo, ref error);
    }

    public IList<object> GetData(int startRow, int maxRows)
    {
        int nPageNumber = startRow / maxRows;
        error = string.Empty;
        clsConsultarEstadoDeclaracionRespuesta EstadoDecla = cService.ConsultarEstadoDeclaracionPaginado(RequestInfo, ++nPageNumber, maxRows, ref error);
        if (!string.IsNullOrWhiteSpace(error))
        {
            OnError(null, new ErrorEventArgs(error));
        }
        IList<object> result = new List<object>();
        if (EstadoDecla != null && EstadoDecla.LstEstadoDeclaracion != null)
            EstadoDecla.LstEstadoDeclaracion.ForEach(x => { result.Add(x); });

        return result;
    }

    public IList<object> GetData(int startRow, int maxRows, string sortColumns)
    {
        throw new NotImplementedException();
    }

    #endregion

    public int VirtualItemCount(int startRow, int maxRows)
    {
        return VirtualItemCount();
    }

    public IList<object> GetDatosCorrecion(int nIdRegistroPersona)
    {
        error = string.Empty;
        var datosCorreccion = cService.CargaDatosCorreccion(nIdRegistroPersona, ref error);
        if (!string.IsNullOrWhiteSpace(error))
        {
            OnError(null, new ErrorEventArgs(error));
        }
        return datosCorreccion == null ? null : datosCorreccion.Select(x => (object)x).ToList();
    }

    #endregion

    void OnError(object sender, ErrorEventArgs e)
    {
        if (ErrorConsulta != null)
        {
            ErrorConsulta(sender, e);
        }
    }
}