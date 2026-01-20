using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO.Notificacion;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;

namespace Ruv.Business.Notificacion.Contratos
{
    public interface INotificacionInternaBusiness
    {
        IList<dto::clsNotificacionInterna> ObtenerNotificacionInterna(int nIdUsuario, ref string cError);
        bool MarcarLeido(int nIdNotificacionInterna, ref string cError);
    }
}
