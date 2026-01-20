using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;

using System.Data.Objects.DataClasses;
using System.Data.Common;

namespace Ruv.Business.Captura.Declaracion
{
    public class HechosVictimizantes
    {
        #region Guardar Datos
        public static void Guardar(clsDeclaracion declaracionView, IPersonaAfectada personaView, int? id_declarante, DbTransaction tran)
        {
            //borrar todos los hechos en RegPersona
            entHechosPersona entBD = new entHechosPersona();
            int idRegPer = (int)personaView.ID;
            entBD.deleteData(idRegPer, tran);
            //Agregar los hechos en RegPersona
            foreach (int hechoView in personaView.HechosVictimizantes)
            {
                Ruv.Data.TBREG_PERSONA_HECHOS hechosData = new TBREG_PERSONA_HECHOS();
                hechosData.PARAM_HECHO = hechoView;
                hechosData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
                hechosData.TBREGISTROS_PERSONAS.ID = idRegPer;

                entBD.setData(hechosData, tran);
            }
        }

        public static void Guardar(clsDeclaracion declaracionView, clsAnexo13_Victima personaView, int? id_declarante, DbTransaction tran)
        {
            //borrar todos los hechos en RegPersona
            entHechosPersona entBD = new entHechosPersona();
            int idRegPer = (int)personaView.ID;
            entBD.deleteData(idRegPer, tran);
            //Agregar los hechos en RegPersona
            foreach (int hechoView in personaView.HechosVictimizantes)
            {
                Ruv.Data.TBREG_PERSONA_HECHOS hechosData = new TBREG_PERSONA_HECHOS();
                hechosData.PARAM_HECHO = hechoView;
                hechosData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
                hechosData.TBREGISTROS_PERSONAS.ID = idRegPer;

                entBD.setData(hechosData, tran);
            }
        }
        #endregion

        #region Obtener Datos
        public static  List<int> Obtener(int personaID)
        {
            entHechosPersona entBD = new entHechosPersona();
            return entBD.getData(personaID);
        }
        #endregion
    }
}
