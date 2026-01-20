using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using Ruv.Business.DTO.Notificacion;

namespace Ruv.WebSite.DataSources.Notificaciones
{
    [DataObject(true)]
    public class DataSourceNotificacionDetalle : IDataSourceBase
    {

        #region Filter Properties

        public int IdNotificacion { get; set; }

        #endregion

        #region Data Functions

        public clsNotificacionDetalle DetalleData() {
            try {
                return new NotificacionService().DetalleNotificacion(IdNotificacion);
            }
            catch (Exception ex) {
                RegistroTraza.I.Registrar(ex);
                ErrorConsulta(this, new ErrorEventArgs(ex.Message));
                return null;
            }
        }

        public void AprobarNotificacion()
        {
            string errorMessage = string.Empty;

            var response = new NotificacionService().AprobarNotificacion(IdNotificacion, ref errorMessage);

            if (!response || !string.IsNullOrEmpty(errorMessage))
                OnError(this, new ErrorEventArgs(errorMessage));
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

}