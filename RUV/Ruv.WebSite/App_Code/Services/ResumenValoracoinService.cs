using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.ServiceModel.Activation;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using util = Ruv.Infrastructure.Crosscutting.Utilities;
using dto = Ruv.Business.DTO.Valoracion;
using Ruv.Business.Valoracion.Contratos;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ResumenValoracoinService" in code, svc and config file together.
public class ResumenValoracoinService : IResumenValoracoinService
{
    public List<dto::clsResumenValoracion> ObtenerResumenValoracion(int NIdValorador, ref string cError)
    {
        IResumenLiderValoracion iResumenValoracion = (IResumenLiderValoracion)new Ruv.Business.Valoracion.clsResumenLiderValoracion();
        return iResumenValoracion.ObtenerResumenValoracion(NIdValorador, ref cError);
    }
}
