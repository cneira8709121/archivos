using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Ruv.Data.GestionGlosa.Contratos
{
    public interface IGestionGlosa
    {
        void AsignarGlosa(int? nIdAsignaGlosa, DbTransaction tra, ref string cError);
    }
}
