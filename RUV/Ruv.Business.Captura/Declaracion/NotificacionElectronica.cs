using Ruv.Data.Reconocimiento;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Ruv.Business.Captura.Declaracion
{
    public class NotificacionElectronica
    {
        public static void Guardar(clsDeclaracion declaracionView, DbTransaction tran)
        {
            entDeclaracionNotificacion notificacion = new entDeclaracionNotificacion();

            byte autorizacion = 0;
            if(declaracionView.NotificacionElectronica != null)
            {
                if (declaracionView.NotificacionElectronica.AutorizaNotificacion.HasValue)
                {
                    autorizacion = Convert.ToByte(declaracionView.NotificacionElectronica.AutorizaNotificacion);
                }
            }
            notificacion.setDeclaracionNotificacion((int)declaracionView.ID, autorizacion, tran);
        }
    }
}
