using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ruv.Business.DTO.Valoracion;

namespace Ruv.Data.Valoracion.Valoracion.Contratos
{
    public interface IResumenLiderValoracion
    {
        List<clsResumenValoracion> ObtenerResumenLiderVal(int nidDeclaracion, ref string cError);
    }
}
