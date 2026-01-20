using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ruv.Data;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entListaTareas : entidadRUV
    {

        public DataTable GetValoracionesPorValorador(int ValoradorId)
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getValoracionesPorValorador", new object[] { ValoradorId, null });
            if (ds != null && ds.Tables.Count > 0)
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
