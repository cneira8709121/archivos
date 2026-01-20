using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using u = Ruv.Infrastructure.Crosscutting.Utilities;
using b = Ruv.Business.Notificacion;
using dto = Ruv.Business.DTO.Notificacion;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NotificacionInternaService" in code, svc and config file together.
public class NotificacionInternaService : INotificacionInternaService
{
   public IList<dto::clsNotificacionInterna> ObtenerNotificacionInterna(int nIdUsuario, ref string cError)
    {
        b::Contratos.INotificacionInternaBusiness NotificacionInterna = (b::Contratos.INotificacionInternaBusiness)u::Spring.GetService(Objetos.NotificacionInternaBussiness);
          return NotificacionInterna.ObtenerNotificacionInterna(nIdUsuario,ref cError);
    }

   public bool MarcarLeido(int nIdNotificacionInterna, ref string cError)
   {
       b::Contratos.INotificacionInternaBusiness NotificacionInterna = (b::Contratos.INotificacionInternaBusiness)u::Spring.GetService(Objetos.NotificacionInternaBussiness);
       return NotificacionInterna.MarcarLeido(nIdNotificacionInterna, ref cError);
   }
}
