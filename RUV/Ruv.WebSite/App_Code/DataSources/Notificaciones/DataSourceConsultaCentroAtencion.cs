using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Business.DTO.Notificacion;
using System.ComponentModel;

/// <summary>
/// Summary description for DataSourceConsultaCentroAtencion
/// </summary>
[DataObject(true)]
public class DataSourceConsultaCentroAtencion : IDataSourceBase
{
    public int? Pais { get; set; }

    public int? Departamento { get; set; }

    public int? Municipio { get; set; }
    
    #region Sorting Properties

    public string SortColumns { get; set; }

    #endregion

    #region Data Functions

    public List<clsDatosCentroAtencion> ConsultaDatosCentroAtencion(int pageIndex, int pageSize, string sortColumns)
    {
        string cError = string.Empty;

        NotificacionService service = new NotificacionService();
        return service.ConsultaCentrosAtencion(Pais, Departamento, Municipio, pageIndex, pageSize, ref cError).ToList();
    }

    public int CantidadNotificaciones()
    {
        
        string cError = string.Empty;

        NotificacionService service = new NotificacionService();
        return service.ConsultaCentrosAtencionConteo(Pais, Departamento, Municipio, ref cError);
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