using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using Ruv.Business.DTO.General;
using Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.General
{
    public class entParametro
    {

        public List<clsParametro> ObtenerParametros(int tipoParametro, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = "pi_TipoParametro", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = tipoParametro });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                return ComplexDataAccessImplements.MapFromDataReaderI<clsParametro>(d.ExecuteReader("PKG_COMMON.sp_ObtenerParametros", ref cError), true);
            }
        }
    }
}
