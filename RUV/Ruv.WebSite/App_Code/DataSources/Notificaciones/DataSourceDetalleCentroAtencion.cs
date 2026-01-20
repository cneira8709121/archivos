using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Business.DTO.Notificacion;
using System.ComponentModel;

/// <summary>
/// Summary description for DataSourceDetalleCentroAtencion
/// </summary>
[DataObject(true)]
public class DataSourceDetalleCentroAtencion : IDataSourceBase
{
    //public int nIdCentroatencion { get; set; }

    //public string cNombreCentroAtencion { get; set; }
       
    #region Data Functions

    public List<clsDetalleDatosCentrosAtencion> DetalleDatosCentroAtencion(int nIdCentroatencion, int TipoCentroAtencion, int pageIndex, int pageSize, string sortColumns)
    {
        string cError = string.Empty;

        NotificacionService service = new NotificacionService();
        return service.DetalleCentrosAtencion(nIdCentroatencion, TipoCentroAtencion, pageIndex, pageSize, ref cError).ToList();
    }

    public int CantidadNotificaciones(int nIdCentroatencion, int TipoCentroAtencion)
    {
        string cError = string.Empty;

        NotificacionService service = new NotificacionService();
        return service.DetalleCentrosAtencionConteo(nIdCentroatencion, TipoCentroAtencion, ref cError);
    }

    public List<clsEncargadoEntidad> ObtenerEncargadosPorEntidad(int nIdPuntoAtencion, int nTipoCentro, int pageIndex, int pageSize)
    {
        string cError = string.Empty;

        NotificacionService service = new NotificacionService();

        var record = service.ObtenerEncargadosPorEntidad(nIdPuntoAtencion, nTipoCentro, pageIndex, pageSize, ref cError);

        if (record != null)
            return record.ToList();
        else 
            return new List<clsEncargadoEntidad>();
    }

    public int CantidadEncargados(int nIdCentroatencion, int nTipoCentro)
    {
        string cError = string.Empty;

        NotificacionService service = new NotificacionService();
        return service.ContadorEncargadosPorEntidad(nIdCentroatencion, nTipoCentro, ref cError);
    }

    #endregion

    #region IDataSourceBase Implementation

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
        throw new NotImplementedException();
    }

    public IList<object> GetData(int startRow, int maxRows)
    {
        throw new NotImplementedException();
    }

    public IList<object> GetData(int startRow, int maxRows, string sortColumns)
    {
        throw new NotImplementedException();
    }

    #endregion
}