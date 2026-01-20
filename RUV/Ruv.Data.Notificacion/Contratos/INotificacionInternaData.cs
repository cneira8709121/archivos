using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Ruv.Business.DTO.Notificacion;

namespace Ruv.Data.Notificacion.Contratos
{
    public interface INotificacionInternaData
    {
        IList<clsNotificacionInterna> ObtenerNotificacionesInternas(int nIdUsuario, ref string cError);
        bool GenerarNotificacionInterna(int nIdProceso, int nIdUsuarioGenera, int nTipoProceso, int nIdUsuarioRecibe, string cTexto, string cDescripcion, DbTransaction tra, ref string cError);
        bool MarcarLeido(int nIdNotificacionInterna, DbTransaction tra, ref string cError);
    }
}
