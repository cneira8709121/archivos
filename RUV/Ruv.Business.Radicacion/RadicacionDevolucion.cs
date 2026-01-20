using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO;
using System.Data.Common;

namespace Ruv.Business.Radicacion
{
    public class RadicacionDevolucion : Contratos.IRadicacionDevolucion
    {
        public Int32 RadicarDevolucion(dto::Radicacion.clsRadicacion rad, ref string cError)
        {
            Int32 idGenerado = 0;
            Data.Radicacion.Contratos.IRadicacionDevolucionData iRadicacionDevolucionData = (Data.Radicacion.Contratos.IRadicacionDevolucionData)new Data.Radicacion.RadicacionDevolucionData();
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                idGenerado = iRadicacionDevolucionData.RadicarDevolucion(rad, tra, ref cError);
                if (string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return idGenerado;
                }
                tra.Rollback();
                return idGenerado;
            }
        }
    }
}
