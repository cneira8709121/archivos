using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Business.DTO.Valoracion;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class SubEtniasB
    {
        public static List<clsSubEtnias> GetSubEtnias(int etniaId)
        {
            List<clsSubEtnias> SubEtnias = new List<clsSubEtnias>();
            entSubEtnias ObjSubEtnia = new entSubEtnias();
            foreach (clsSubEtniasdto data in ObjSubEtnia.GetSubEtnias(etniaId))
            {
                clsSubEtnias view = new clsSubEtnias();
                view.Id = (int?)data.NId;
                view.Nombre = data.cNombre;
                view.EtniaGrupoId = (int)data.NEtniaGrupoId;
                view.Numero = (int)data.NNumero;
                SubEtnias.Add(view);
            }
            return SubEtnias;
        }
    }
}
