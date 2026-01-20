using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Ruv.Infrastructure.Crosscutting.Resources.DB;
using System.Data.Common;

namespace Ruv.Data.Valoracion.Valoracion
{
   public class clsTipoMotivacion : Contratos.ITipoNotificacion
    {

        public bool InsertaTipoMotivacion(int nidValoracion, string cTipoValoracion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.IdValoracion, OracleType = OracleType.Number, Value = nidValoracion, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.IdTipoMotivacion, OracleType = OracleType.VarChar, Value = cTipoValoracion, Direction = ParameterDirection.Input });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.InsertaTipoMotivacion, tra, ref cError);
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::InsertaTipoMotivacion", ex);
                cError = ex.Message;
                return false;
            }

            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        public string ObtieneTipoMotivacion(int nidValoracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.IdValoracion, OracleType = OracleType.Number, Value = nidValoracion, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.TipoMotivacionId, OracleType = OracleType.VarChar, Direction = ParameterDirection.Output });

            IDataReader dr = null;

            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerTipoMotivacion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::ObtieneTipoMotivacion", ex);
                cError = ex.Message;
                return string.Empty;
            }

            List<string> IdTipoNot = ComplexDataAccessImplements.MapFromDataReaderI<string>(dr, true);
            if (IdTipoNot != null && IdTipoNot.Count > 0)
                return IdTipoNot.FirstOrDefault();

            return string.Empty;
        }
    }
}
