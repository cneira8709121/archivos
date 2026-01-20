using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using dto = Ruv.Business.DTO.Valoracion;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IResumenValoracoinService" in both code and config file together.
[ServiceContract]
public interface IResumenValoracoinService
{
    [OperationContract]
    List<dto::clsResumenValoracion> ObtenerResumenValoracion(int NIdValorador, ref string cError);
}
