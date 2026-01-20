using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO;

namespace Ruv.Business.Radicacion.Contratos
{
    public interface IRadicacionDevolucion
    {
        Int32 RadicarDevolucion(dto::Radicacion.clsRadicacion rad, ref string cError);
    }
}
