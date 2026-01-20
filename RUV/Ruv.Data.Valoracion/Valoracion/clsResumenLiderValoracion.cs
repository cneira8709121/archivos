using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using Ruv.Business.DTO.Valoracion;
using Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class clsResumenLiderValoracion : Contratos.IResumenLiderValoracion
    {
        public List<clsResumenValoracion> ObtenerResumenLiderVal(int nIdDeclaracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDeclaracion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ResumenLiderValoracion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::ObtenerResumenLiderVal", ex);
                cError = ex.Message;
                return null;
            }

           return ComplexDataAccessImplements.MapFromDataReaderI<clsResumenValoracion>(dr, true);            
       
        }

        
    }
}
