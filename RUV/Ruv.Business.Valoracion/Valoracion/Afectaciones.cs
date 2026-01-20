using System.Collections.Generic;
using System.Data.Common;
using Ruv.Data;
using Ruv.Data.Valoracion.Valoracion;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Afectaciones
    {
        #region Afectaciones
        
        
        public static List<int> GetAfectacionesPorPersona(int personaId)
        {
            List<int> afectacionesIds = new List<int>();
            entAfectacion ObjAfectacion = new entAfectacion();
            List<TBPARAMETROS> parametros = ObjAfectacion.GetAfectacionesPorPersonaId(personaId);
            foreach (TBPARAMETROS data in parametros)
            {
                int view = data.ID;
                afectacionesIds.Add(view);
            }
            return afectacionesIds;
        }

        public static void Insertar(int afectacion, int anexoPerId, DbTransaction tra)
        {
            entAfectacion objAfectacion = new entAfectacion();
            objAfectacion.Insertar(afectacion, anexoPerId, tra);
        }

        public static void Eliminar(int anexoPerId, DbTransaction tra)
        {
            entAfectacion objAfectacion = new entAfectacion();
            objAfectacion.Eliminar(anexoPerId, tra);
        }

        #endregion  
    }
}
