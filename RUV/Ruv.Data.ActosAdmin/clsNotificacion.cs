using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Ruv.Business.DTO.ActosAdministrativos;
using Ruv.Business.DTO.Orfeo;
using System.Data.OracleClient;
namespace Ruv.Data.ActosAdmin
{
   public class clsNotificacion : Contratos.INotificacion
    {
        public List<clsNotificacionVal> CargaDatosNotificacion(int nIdValoracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdValoracion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdValoracion,
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
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.CargaDatosNotificacion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsNotificacionVal> listClsNotificacion = ComplexDataAccessImplements.MapFromDataReaderI<clsNotificacionVal>(dr, true);

            if (listClsNotificacion != null && listClsNotificacion.Count > 0)
                return listClsNotificacion;
            else
                return null;
        }

        public void MarcarTipoCodigoActoAdministrativo(int idActoAdministrativo, int valorTipoCodigo) {
            using (var dao = new Dao()) {
                dao.AddParameter(new OracleParameter { ParameterName = Ruv.Infrastructure.Crosscutting.Resources.DB.Parametros.IdActoAdministrativo, OracleType = OracleType.Number, Value = idActoAdministrativo, Direction = ParameterDirection.Input });
                dao.AddParameter(new OracleParameter { ParameterName = Ruv.Infrastructure.Crosscutting.Resources.DB.Parametros.TipoCodigo, OracleType = OracleType.Number, Value = valorTipoCodigo, Direction = ParameterDirection.Input });
                string databaseError = string.Empty;
                try {
                    dao.ExecuteNonQuery(Ruv.Infrastructure.Crosscutting.Resources.DB.Procedimientos.ActualizarTipoCodigoActoAdministrativo, null, ref databaseError);
                }
                catch (Exception ex) {
                    databaseError = ex.Message;
                    
                }
                if (!string.IsNullOrEmpty(databaseError))
                    throw new DataException(string.Format("No se puede actualizar el tipo de acto administrativo generado: '{0}'", databaseError));
            }
        }

        public int GetIdValoracionByIdDeclaracion(int nIdDeclaracion, ref string cError)
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
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdValoracionOut,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Direction = ParameterDirection.Output
            });

            d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerIdValoracionPorIdDeclaracion, null, ref cError);
            if (string.IsNullOrEmpty(cError)) {
                return (int)(decimal)d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.IdValoracionOut);    
            }
            return 0;
        }

        public List<clsOrfeo> ObtenerDatosOrfeoPorIdValoracion(int nIdValoracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdValoracion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdValoracion,
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
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerDatosOrfeoPorIdValoracion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsOrfeo> DatosOrfeo = ComplexDataAccessImplements.MapFromDataReaderI<clsOrfeo>(dr, true);

            if (DatosOrfeo != null && DatosOrfeo.Count > 0)
                return DatosOrfeo;
            else
                return null;
        }
    }
}
