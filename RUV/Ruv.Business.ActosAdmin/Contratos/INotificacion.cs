using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ruv.Business.DTO.ActosAdministrativos;
using Ruv.Business.DTO.Orfeo;

namespace Ruv.Business.ActosAdmin.Contratos
{
    public interface INotificacion
    {
        List<clsNotificacionVal> CargaDatosNotificacion(int nIdValoracion, ref string cError);

        void MarcarTipoCodigoActoAdministrativo(int idActoAdministrativo, int valorTipoCodigo);

        int GetIdValoracionByIdDeclaracion(int nIdDeclaracion, ref string cError);

        List<clsOrfeo> ObtenerDatosOrfeoPorIdValoracion(int nIdValoracion, ref string cError);
    }
}
