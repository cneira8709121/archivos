using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data;

namespace Ruv.Data.ActosAdmin 
{
    public class entDocumentosActosAdmin : entidadRUV
    {
        public List<TBPARAMETROS> GetDocumentosPorArea(int Area)
        {
            List<TBPARAMETROS> afectaciones = new List<TBPARAMETROS>();
            using (IDataReader dr = dbRUV.ExecuteReader("PKG_ACTOSADMIN.sp_getDocumentosPorArea", new object[] { Area, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPARAMETROS afect = EnterpriseLibraryContainer.Current.GetInstance<TBPARAMETROS>();
                    afect.ID = dbDefaults.getInt32(dr, index++).Value;
                    afect.NOMBRE = dbDefaults.getString(dr, index++);
                    afectaciones.Add(afect);
                }
            }
            return afectaciones;
        }
    }
}
