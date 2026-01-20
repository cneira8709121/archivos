using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using util = Ruv.Infrastructure.Crosscutting.Utilities;
using dto = Ruv.Business.DTO.ActosAdministrativos;
using Ruv.Business.ActosAdmin.Contratos;

[AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Required)]

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CargaDatosValoracionService" in code, svc and config file together.
public class CargaDatosValoracionService : ICargaDatosValoracionService {

    public List<dto::clsNotificacionVal> CargaDatosValoracionNoti(int IdValoracion, ref string cError) {
        INotificacion iNotificacionVal = (INotificacion)new Ruv.Business.ActosAdmin.clsNotificacion();
        return iNotificacionVal.CargaDatosNotificacion(IdValoracion, ref cError);
    }

    public void MarcarTipoCodigoActoAdministrativo(int idActoAdministrativo, int valorTipoCodigo) {
        INotificacion iNotificaciones = new Ruv.Business.ActosAdmin.clsNotificacion();
        iNotificaciones.MarcarTipoCodigoActoAdministrativo(idActoAdministrativo, valorTipoCodigo);
    }

    public int GetIdValoracionByIdDeclaracion(int nIdDeclaracion, ref string cError) {
        INotificacion iNotificacionVal = (INotificacion)new Ruv.Business.ActosAdmin.clsNotificacion();
        return iNotificacionVal.GetIdValoracionByIdDeclaracion(nIdDeclaracion, ref cError);
    }

}
