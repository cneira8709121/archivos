using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO.Notificacion;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using Ruv.Data.Notificacion.Contratos;
using u = Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using Ruv.Business.Notificacion.Contratos;
using Ruv.Data;
using System.Data.Common;
using Ruv.Infrastructure.Crosscutting.Common;
using resx = Ruv.Infrastructure.Crosscutting.Resources;

namespace Ruv.Business.Notificacion
{
    public class NotificacionInternaBusiness : INotificacionInternaBusiness
    {
        public IList<dto::clsNotificacionInterna> ObtenerNotificacionInterna(int nIdUsuario, ref string cError)
        {
            INotificacionInternaData iNotificacionInterna = (INotificacionInternaData)u::Spring.GetService(Objetos.NotificacionInternaData);
            IList<dto::clsNotificacionInterna> listDtoNotificacionInterna = iNotificacionInterna.ObtenerNotificacionesInternas(nIdUsuario, ref cError);
            return listDtoNotificacionInterna;
        }

        public bool MarcarLeido(int nIdNotificacionInterna, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionInternaData iInsertaNotificacionInterna = (INotificacionInternaData)u::Spring.GetService(Objetos.NotificacionInternaData);
                if (iInsertaNotificacionInterna.MarcarLeido(nIdNotificacionInterna, tra, ref cError))
                {
                    tra.Commit();
                    return true;
                }


                tra.Rollback();
                return false;
            }
        }
    }
}
