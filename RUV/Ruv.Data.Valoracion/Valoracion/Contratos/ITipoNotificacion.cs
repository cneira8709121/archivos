using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Ruv.Data.Valoracion.Valoracion.Contratos
{
    public interface ITipoNotificacion
    {
        bool InsertaTipoMotivacion(int nidValoracion, string cTipoValoracion, DbTransaction tra, ref string cError);
        string ObtieneTipoMotivacion(int nidValoracion, ref string cError);
    }
}
