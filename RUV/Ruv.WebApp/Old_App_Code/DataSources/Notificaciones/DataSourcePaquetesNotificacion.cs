using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using System.ComponentModel;
using Ruv.Business.DTO.Notificacion;

namespace Ruv.WebApp.DataSources.Notificaciones
{

    [DataObject(true)]
    public class DataSourcePaquetesNotificacion : IDataSourceBase
    {

        #region Filter Properties

        public string OrdenServicio { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        #endregion

        #region Sorting Properties

        public string SortColumns { get; set; }

        #endregion

        #region Data Functions

        public List<clsPaqueteNotificacion> ObtenerPaquetes(int startRow, int pageSize, string sortColumns)
        {
            startRow = startRow / pageSize;
            startRow++;

            string cError = string.Empty;

            NotificacionService service = new NotificacionService();
            return service.ObtenerPaquetes(RUV.Current.Usuario.ID, this.OrdenServicio, this.FechaInicio, this.FechaFin, startRow, pageSize, ref cError);
        }

        public int CantidadPaquetes()
        {
            string cError = string.Empty;

            NotificacionService service = new NotificacionService();
            return service.ObtenerPaquetesConteo(RUV.Current.Usuario.ID, this.OrdenServicio, this.FechaInicio, this.FechaFin, ref cError);
        }

        #endregion

        #region IDataSourceBase Implementation

        public event Ruv.Infrastructure.Crosscutting.Common.Error ErrorConsulta;

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

}