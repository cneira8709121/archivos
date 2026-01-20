using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entDeclaracion : entidadRUV
    {
        public DataTable GetvDeclaracionPorId(int ValoracionId)
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getInfoDeclaracion", new object[] { ValoracionId, null });
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public bool EsFueraDeColombia(int declaracionId)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.SP_FUERA_DE_COLOMBIA", new object[] { declaracionId, null });
            dbRUV.ExecuteNonQuery(cmd);
            return Convert.ToBoolean(dbRUV.GetParameterValue(cmd, "P_OUT_FUERA"));
        }
    }
}
