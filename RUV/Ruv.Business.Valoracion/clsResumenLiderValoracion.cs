using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using dal = Ruv.Data.Valoracion;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Business.DTO.Valoracion;
namespace Ruv.Business.Valoracion
{
    public class clsResumenLiderValoracion : Contratos.IResumenLiderValoracion
    {
         public List<clsResumenValoracion> ObtenerResumenValoracion(int nidDeclaracion,ref string cError)
        {
            Data.Valoracion.Valoracion.Contratos.IResumenLiderValoracion iResumenValoracion = (Data.Valoracion.Valoracion.Contratos.IResumenLiderValoracion)new Data.Valoracion.Valoracion.clsResumenLiderValoracion();
            return iResumenValoracion.ObtenerResumenLiderVal(nidDeclaracion, ref cError);
        } 
    }
}
