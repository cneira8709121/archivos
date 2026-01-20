using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using System.ComponentModel;
using Ruv.Business.DTO.Feriado;

namespace Ruv.WebSite.DataSources.Feriados
{
    
    [DataObject(true)]
    public class DataSourceFeriados : IDataSourceBase
    {
        #region Filter Properties

        public int Ano { set; get; }

        #endregion

        #region Data Functions

        public List<Feriado> ObtenerNotificaciones()
        {
            string errorMessage = string.Empty;
            FeriadosService service = new FeriadosService();
            return service.ConsultarFestivos(this.Ano, ref errorMessage);
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