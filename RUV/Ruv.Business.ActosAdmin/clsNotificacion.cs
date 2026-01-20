using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using dal = Ruv.Data.ActosAdmin;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using Ruv.Business.DTO.ActosAdministrativos;
using Ruv.Business.DTO.Orfeo;


namespace Ruv.Business.ActosAdmin
{
    public class clsNotificacion : Contratos.INotificacion
    {
        public List<clsNotificacionVal> CargaDatosNotificacion(int nIdValoracion, ref string cError)
        {
            Data.ActosAdmin.Contratos.INotificacion iNotificacion = (Data.ActosAdmin.Contratos.INotificacion)new Data.ActosAdmin.clsNotificacion();
            return iNotificacion.CargaDatosNotificacion(nIdValoracion, ref cError);
        }

        public void MarcarTipoCodigoActoAdministrativo(int idActoAdministrativo, int valorTipoCodigo) {
            Data.ActosAdmin.Contratos.INotificacion iNotificaciones = new Data.ActosAdmin.clsNotificacion();
            iNotificaciones.MarcarTipoCodigoActoAdministrativo(idActoAdministrativo, valorTipoCodigo);
        }

        public int GetIdValoracionByIdDeclaracion(int nIdDeclaracion, ref string cError)
        {
            Data.ActosAdmin.Contratos.INotificacion iNotificacion = (Data.ActosAdmin.Contratos.INotificacion)new Data.ActosAdmin.clsNotificacion();
            return iNotificacion.GetIdValoracionByIdDeclaracion(nIdDeclaracion, ref cError);
        }

        public List<clsOrfeo> ObtenerDatosOrfeoPorIdValoracion(int nIdValoracion, ref string cError)
        {
            Data.ActosAdmin.Contratos.INotificacion iNotificacion = (Data.ActosAdmin.Contratos.INotificacion)new Data.ActosAdmin.clsNotificacion();
            return iNotificacion.ObtenerDatosOrfeoPorIdValoracion(nIdValoracion, ref cError);       
        }
    }
}
