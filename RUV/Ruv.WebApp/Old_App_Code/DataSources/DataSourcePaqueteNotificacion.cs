using System;
using System.Collections.Generic;
using System.ComponentModel;
using Ruv.Business.DTO.Notificacion;
using Ruv.Infrastructure.Crosscutting.Common;

[DataObject(true)]
public class DataSourcePaqueteNotificacion : IDataSourceBase
{
    public DataSourcePaqueteNotificacion() { }

    public int IdPaqueteNotificacion { get; set; }

    public clsPaqueteNotificacion ObtenerPaquete()
    {
        string cError = string.Empty;
        
        NotificacionService service = new NotificacionService();
        return service.ObtenerPaquete(this.IdPaqueteNotificacion, ref cError);
    }

    public int VirtualItemCount() { throw new NotImplementedException(); }

    public IList<object> GetData(int startRow, int maxRows)
    {
        throw new NotImplementedException();
    }

    public IList<object> GetData(int startRow, int maxRows, string sortColumns)
    {
        throw new NotImplementedException();
    }

    #region IDataSourceBase Implementation

    public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

    void OnError(object sender, ErrorEventArgs e) 
    {
        if (ErrorConsulta != null) 
            ErrorConsulta(sender, e);
    }

    #endregion

}