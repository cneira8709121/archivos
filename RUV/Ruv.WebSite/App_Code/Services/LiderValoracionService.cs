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
using Ruv.Business.Valoracion.Contratos;
using v = Ruv.Business.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Business.DTO.Valoracion;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AprobarValoracionService" in code, svc and config file together.
public class LiderValoracionService : ILiderValoracionService
{
	public bool AprobarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, ref string cError)
	{
        v::Contratos.ILiderValoracion iAprobarValoracion = (v::Contratos.ILiderValoracion)util::Spring.GetService(Objetos.LiderValoracionBusiness);
        var result = iAprobarValoracion.AprobarValoracion(nIdUsuario, nIdDeclaracion, cObservacion, ref cError);

        if (!string.IsNullOrEmpty(cError)) {
            cError = string.Format("No fue posible realizar la aprobación de la valoración: {0}", cError);
            return false;
        }

        // Cuando el lider de valoracion aprueba, ya debe generarse el textico chiquito de quien aprueba el documento
        ActosAdminService actosAdminService = new ActosAdminService();
        CargaDatosValoracionService cargaDatosValoracionService = new CargaDatosValoracionService();

        //Obtiene el id de valoracion a partir del id de declaracion
        int idValoracion = cargaDatosValoracionService.GetIdValoracionByIdDeclaracion(nIdDeclaracion, ref cError);
        if (string.IsNullOrEmpty(cError))
        {
            actosAdminService.GenerarDocumentoValoracion(idValoracion, RUV.Current.Usuario.Permisos.Contains(ePermisosUsuario.FirmaActoAdministrativo), ref cError);
            return result;
        }
        return false;
	}

    public bool RechazarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, ref string cError)
	{
        v::Contratos.ILiderValoracion iRechazarValoracion = (v::Contratos.ILiderValoracion)util::Spring.GetService(Objetos.LiderValoracionBusiness);
        return iRechazarValoracion.RechazarValoracion(nIdUsuario,nIdDeclaracion,cObservacion,ref cError);
	}

    public List<clsValoracionHistorico> consultarValoracionHistorico(int nIdValoracion, ref string cError) 
    {
        v::Contratos.ILiderValoracion iValoracionHistorico = (v::Contratos.ILiderValoracion)util::Spring.GetService(Objetos.LiderValoracionBusiness);
        return iValoracionHistorico.consultarValoracionHistorico(nIdValoracion, ref cError);
    }

    public string consultarMotivacionValoracionHistorico(int nIdValoracion, ref string cError) 
    {
        v::Contratos.ILiderValoracion iValoracionHistorico = (v::Contratos.ILiderValoracion)util::Spring.GetService(Objetos.LiderValoracionBusiness);
        return iValoracionHistorico.consultarMotivacionValoracionHistorico(nIdValoracion, ref cError) as string;
    }
}

