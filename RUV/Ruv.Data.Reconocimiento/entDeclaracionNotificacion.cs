using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Ruv.Data.Reconocimiento
{
    public class entDeclaracionNotificacion : entidadRUV
    {
        public void setDeclaracionNotificacion(int idDeclaracion, byte AutorizaNotificacion, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.SP_SETNOTIFICACIONELECTONICA", new object[] { idDeclaracion, AutorizaNotificacion });
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
    }
}
