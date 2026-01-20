using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class PreguntasRegistrosAnteriores
    {
        public PreguntasRegistrosAnteriores()
        {
        }
        public static List<clsPreguntasRegAnt> GetPreguntasRegAnt()
        {
            List<clsPreguntasRegAnt> Preguntas = new List<clsPreguntasRegAnt>();
            entRegistrosAnteriores objRegAnt = new entRegistrosAnteriores();
            List<TBPARAMETROS> parametros = objRegAnt.GetPreguntasRegistrosAnteriores();
            foreach (TBPARAMETROS data in parametros)
            {
                clsPreguntasRegAnt view = new clsPreguntasRegAnt();
                view.Id = data.ID;
                view.Pregunta = data.NOMBRE;
                Preguntas.Add(view);
            }
            return Preguntas;
        }

        internal static List<int> GetPreguntasPorRegVal(int ValRegId)
        {
            List<int> Preguntas = new List<int>();
            entRegistrosAnteriores objRegAnt = new entRegistrosAnteriores();
            List<TBPARAMETROS> parametros = objRegAnt.GetPreguntasPorValRegId(ValRegId);
            foreach (TBPARAMETROS data in parametros)
            {
                Preguntas.Add(data.ID);
            }
            return Preguntas;
        }

    }
}
