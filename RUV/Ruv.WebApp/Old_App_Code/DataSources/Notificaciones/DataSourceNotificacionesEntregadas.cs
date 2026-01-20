using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;

namespace Ruv.WebApp.DataSources.Notificaciones
{
    [DataObject(true)]
    public class DataSourceNotificacionesEntregadas : IDataSourceBase
    {

        #region Filter Properties

        public string Declaracion { set; get; }

        public int? TipoDocumento { get; set; }

        public string Documento { get; set; }

        public string NombreDeclarante { get; set; }

        public int? EstadoNotificacion { get; set; }

        public bool BusquedaGlobal {
            get {
                return !string.IsNullOrEmpty(Declaracion) || TipoDocumento.HasValue || !string.IsNullOrEmpty(Documento) || !string.IsNullOrEmpty(NombreDeclarante);
            }
        }
        public bool UsuarioLiderNotificaciones {
            get {
                //ypprieto
                return true;
                //return RUV.Current.Usuario.RolesUsuario.Contains(eRolesUsuario.LiderNotificaciones);
            }
        }

        #endregion

        #region Sorting Properties

        public string SortColumns { get; set; }

        #endregion

        #region Data Functions

        public List<clsNotificacion> ObtenerNotificacionesEntregadas(int startRow, int pageSize, string sortColumns)
        {
            startRow = startRow / pageSize;
            startRow++;

            int idUsuario = RUV.Current.Usuario.ID;

            string errorMessage = string.Empty;
            NotificacionService service = new NotificacionService();
            var response = service.ObtenerNotificacionesEntregadas(idUsuario, this.BusquedaGlobal || this.UsuarioLiderNotificaciones, this.Declaracion, this.TipoDocumento, this.Documento, this.NombreDeclarante, this.EstadoNotificacion, this.SortColumns, startRow, pageSize, ref errorMessage).ToList();

            if (string.IsNullOrEmpty(errorMessage))
                return response;

            ErrorConsulta(this, new ErrorEventArgs(errorMessage));
            return null;
        }

        public int CantidadNotificacionesEntregadas()
        {
            int idUsuario = RUV.Current.Usuario.ID;
            string errorMessage = string.Empty;

            NotificacionService service = new NotificacionService();
            var response = service.ObtenerNotificacionesEntregadasCantidad(idUsuario, this.BusquedaGlobal || this.UsuarioLiderNotificaciones, this.Declaracion, this.TipoDocumento, this.Documento, this.NombreDeclarante, this.EstadoNotificacion, ref errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
                return response;

            ErrorConsulta(this, new ErrorEventArgs(errorMessage));
            return 0;
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