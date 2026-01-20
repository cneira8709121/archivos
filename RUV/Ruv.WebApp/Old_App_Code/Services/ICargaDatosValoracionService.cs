using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using dto = Ruv.Business.DTO.ActosAdministrativos;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICargaDatosValoracionService" in both code and config file together.
[ServiceContract]
public interface ICargaDatosValoracionService
{
    [OperationContract]
    List<dto::clsNotificacionVal> CargaDatosValoracionNoti(int IdValoracion, ref string cError);

    [OperationContract]
    int GetIdValoracionByIdDeclaracion(int nIdDeclaracion, ref string cError);
}
