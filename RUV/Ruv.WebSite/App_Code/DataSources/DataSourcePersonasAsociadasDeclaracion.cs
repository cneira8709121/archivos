using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Infrastructure.Crosscutting.Common;
using System.ComponentModel;
using Ruv.Business.DTO.Valoracion;


namespace Ruv.WebSite.DataSources
{
    [DataObject(true)]
    public class DataSourcePersonasAsociadasDeclaracion : IDataSourceBase
    {
        public int nIdDeclaracion { get; set; }

        public List<clsCargaPersonasAsociadasDeclaracion> CargaAsociadosPersonaDeclaracion()
        {
            string cError = string.Empty;
            ValoracionService ServiceVal = new ValoracionService();
            return ServiceVal.CargaDatosPersonasAsociadas(nIdDeclaracion, ref cError);
        }

        public int CountAsociadosPersonaDeclaracion()
        {

            string cError = string.Empty;
            ValoracionService ServiceVal = new ValoracionService();
            return ServiceVal.CargaDatosPersonasAsociadasCount(nIdDeclaracion, ref cError);
        }

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