using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Radicacion;
using System.Data.Common;

namespace Ruv.Data.Radicacion.Contratos
{
    public interface IRadicacionDevolucionData
    {
        Int32 RadicarDevolucion(clsRadicacion rad, DbTransaction tra, ref string cError);
    }
}
