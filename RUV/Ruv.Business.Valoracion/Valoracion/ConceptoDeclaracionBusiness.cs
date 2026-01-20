using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ruv.Data;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Business.DTO.General;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class ConceptoDeclaracionBusiness
    {
        public clsConceptoDeclaracion ObtenerConceptoDeclaracion(int idDeclaracion)
        {
            entConceptoDeclaracion entConcep = new entConceptoDeclaracion();
            return entConcep.ObtenerConceptoDeclaracion(idDeclaracion);
        }

        public bool InsertaConceptoDeclaracion(clsConceptoDeclaracion conceptoDeclaracion)
        {
            using (DbTransaction transaction = Dao.InitTransaction())
            {
                entConceptoDeclaracion entConcep = new entConceptoDeclaracion();
                return entConcep.InsertaConceptoDeclaracion(conceptoDeclaracion);
            }
        }
    }
}
