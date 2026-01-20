using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ruv.Business.DTO.Valoracion;

namespace Ruv.Business.Valoracion.Contratos
{
    public interface IResumenLiderValoracion
    {
        List<clsResumenValoracion> ObtenerResumenValoracion(int nidDeclaracion, ref string cError);
    }
}
