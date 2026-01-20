using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using dto = Ruv.Business.DTO.Notificacion;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INotificacionInternaService" in both code and config file together.
[ServiceContract]
public interface INotificacionInternaService
{
	[OperationContract]
    IList<dto::clsNotificacionInterna> ObtenerNotificacionInterna(int nIdUsuario, ref string cError);
    [OperationContract]
    bool MarcarLeido(int nIdNotificacionInterna, ref string cError);
}
