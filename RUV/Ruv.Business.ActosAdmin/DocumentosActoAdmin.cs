using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.ActosAdmin;
using Ruv.Data;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.Business.ActosAdmin
{
    public class DocumentosActoAdmin
    {
        internal static List<clsParametroGeneral> GetDocumentosPorArea(int Area)
        {
            List<clsParametroGeneral> listaParametros = new List<clsParametroGeneral>();
            entDocumentosActosAdmin objDocumentos = new entDocumentosActosAdmin();
            List<TBPARAMETROS> Listdata = objDocumentos.GetDocumentosPorArea(Area);
            foreach (TBPARAMETROS data in Listdata)
            {
                clsParametroGeneral view = new clsParametroGeneral();
                view.Id = data.ID;
                view.Nombre = data.NOMBRE;
                listaParametros.Add(view);
            }
            return listaParametros;
        }
    }
}
