using Ruv.Business.DTO.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Ruv.Data.General
{
    public class entUsuario
    {
        public clsUsuarioBasico ObtenerUsuarioPorId(int IdUsuario, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = "P_ID", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = IdUsuario });
                d.AddParameter(new OracleParameter { ParameterName = "P_RESULT", OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                return ComplexDataAccessImplements.MapFromDataReaderI<clsUsuarioBasico>(d.ExecuteReader("PKG_USUARIOS_EXTERNOS.RUV_us_USUARIO_GetById", ref cError), true).FirstOrDefault();
            }
        }
    }
}
