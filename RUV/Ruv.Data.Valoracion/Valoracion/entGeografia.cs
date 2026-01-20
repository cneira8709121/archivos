using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ruv.Data;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entGeografia : entidadRUV
    {
        public DataTable ObtenerGeografia(int? nivel, int? tipo, int? padre)
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_GetGeografia", new object[] { nivel, tipo, padre, null });
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable ObtenerGeografia()
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_GetGeografia", new object[] { null });
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
    }
}
