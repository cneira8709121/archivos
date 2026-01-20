using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entParametro : entidadRUV
    {
        public List<TBPARAMETROS> GetParametros()
        {
            List<TBPARAMETROS> afectaciones = new List<TBPARAMETROS>();
            using (IDataReader dr = dbRUV.ExecuteReader("PKG_Common.sp_GetParametros", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPARAMETROS afect = EnterpriseLibraryContainer.Current.GetInstance<TBPARAMETROS>();
                    afect.ID = dbDefaults.getInt32(dr, index++).Value;
                    afect.NOMBRE = dbDefaults.getString(dr, index++);
                    afect.ID_TIPOPARAMETRO = dbDefaults.getInt16(dr, index++);
                    afectaciones.Add(afect);
                }
            }
            return afectaciones;
        }

    }
}
