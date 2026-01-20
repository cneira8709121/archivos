using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using System.ComponentModel;

namespace Ruv.WebApp.DataSources.Notificaciones {
    
    [DataObject(true)]
    public class DataSourceNotificaciones : IDataSourceBase {

        #region Filter Properties

        public string Declaracion { set; get; }

        public int? TipoDocumento { get; set; }

        public string Documento { get; set; }

        public string NombreDeclarante { get; set; }

        public int? PaisNotificacion { get; set; }

        public int? DepartamentoNotificacion { get; set; }

        public int? MunicipioNotificacion { get; set; }

        public string PuntoNotificacion { get; set; }

        public string DireccionCitacion { get; set; }

        #endregion

        #region Sorting Properties

        public string SortColumns { get; set; }

        #endregion

        #region Data Functions
        // ypprieto
        //public List<clsNotificacion> ObtenerNotificaciones(int pageIndex, int pageSize, string sortColumns) {
        //    int? idUsuario = (int?)RUV.Current.Usuario.ID;
            
        //    NotificacionService service = new NotificacionService();
        //    return service.ObtenerNotificaciones(idUsuario, this.Declaracion, this.TipoDocumento, this.Documento, this.NombreDeclarante, this.PaisNotificacion, this.DepartamentoNotificacion, this.MunicipioNotificacion, this.PuntoNotificacion, this.DireccionCitacion, !RUV.Current.Usuario.RolesUsuario.Contains(eRolesUsuario.LiderNotificaciones), sortColumns, pageIndex, pageSize).ToList();
           
        //}

        //public int CantidadNotificaciones() {
        //    int? idUsuario = (int?)RUV.Current.Usuario.ID;

        //    NotificacionService service = new NotificacionService();
        //    return service.ObtenerNotificacionesCantidad(idUsuario, this.Declaracion, this.TipoDocumento, this.Documento, this.NombreDeclarante, this.PaisNotificacion, this.DepartamentoNotificacion, this.MunicipioNotificacion, this.PuntoNotificacion, this.DireccionCitacion, !RUV.Current.Usuario.RolesUsuario.Contains(eRolesUsuario.LiderNotificaciones));
        //}

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