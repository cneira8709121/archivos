using System;
using System.Collections.Generic;
using System.ComponentModel;
using Ruv.Business.DTO.Notificacion;
using Ruv.Infrastructure.Crosscutting.Common;

[DataObject(true)]
public class DataSourcePaqueteNotificacionDetalle : IDataSourceBase
{
    public DataSourcePaqueteNotificacionDetalle() { }

    public int IdPaqueteNotificacion { get; set; }

    public List<clsNotificacion> ObtenerPaqueteNotificaciones(int startRow, int pageSize, string sortColumns)
    {
        startRow = startRow / pageSize;
        startRow++;

        string cError = string.Empty;
        
        NotificacionService service = new NotificacionService();
        return service.ObtenerDetallePaquete(this.IdPaqueteNotificacion, startRow, pageSize, ref cError);
    }

    public int CantidadPaqueteNotificaciones()
    {
        string cError = string.Empty;

        NotificacionService service = new NotificacionService();
        return service.ObtenerDetallePaqueteConteo(this.IdPaqueteNotificacion, ref cError);
    }

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
}