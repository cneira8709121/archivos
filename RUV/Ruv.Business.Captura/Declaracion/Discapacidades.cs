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
    public class Discapacidades
    {
        #region Guardar Datos
        public static void Guardar(IPersonaAfectada personaView, DbTransaction tran)
        {
            entDiscapacidadPersona entDisc = new entDiscapacidadPersona();
            int idRegPersona = (int)personaView.ID;
            entDisc.deleteData(idRegPersona, tran);
            foreach (int discapacidadView in personaView.Discapacidades)
            {
                Ruv.Data.TBDISCAPACIDAD_PERSONA dicapacidadData = new TBDISCAPACIDAD_PERSONA();
                dicapacidadData.PARAM_DISCAPACIDAD = discapacidadView;
                dicapacidadData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
                dicapacidadData.TBREGISTROS_PERSONAS.ID = idRegPersona;

                //Insertar en BD
                entDisc.setData(dicapacidadData, tran);
            }

            entDiscapacidadOtroPersona entDiscOtro = new entDiscapacidadOtroPersona();
            entDiscOtro.deleteData(idRegPersona, tran);
            if (!string.IsNullOrWhiteSpace(personaView.OtraDiscapacidad))
            {
                Ruv.Data.TBDISCAPACIDADOTRO_PERSONA dicapacidadOtroData = new TBDISCAPACIDADOTRO_PERSONA();
                dicapacidadOtroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
                dicapacidadOtroData.TBREGISTROS_PERSONAS.ID = idRegPersona;
                dicapacidadOtroData.PARAM_DISCAPACIDAD = (int)eDiscapacidades.Otra;
                dicapacidadOtroData.OTRO = personaView.OtraDiscapacidad;
                entDiscOtro.setData(dicapacidadOtroData, tran);
            }

        }

        //TODO_Eliminar anexo13
        //public static void Guardar(clsAnexo13_Victima personaView)
        //{
        //    entDiscapacidadPersona entDisc = new entDiscapacidadPersona();
        //    int idRegPersona = (int)personaView.ID;
        //    entDisc.deleteData(idRegPersona);
        //    foreach (int discapacidadView in personaView.Discapacidades)
        //    {
        //        Ruv.Data.TBDISCAPACIDAD_PERSONA dicapacidadData = new TBDISCAPACIDAD_PERSONA();
        //        dicapacidadData.PARAM_DISCAPACIDAD = discapacidadView;
        //        dicapacidadData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
        //        dicapacidadData.TBREGISTROS_PERSONAS.ID = idRegPersona;

        //        //Insertar en BD
        //        entDisc.setData(dicapacidadData);
        //    }

        //    entDiscapacidadOtroPersona entDiscOtro = new entDiscapacidadOtroPersona();
        //    entDiscOtro.deleteData(idRegPersona);
        //    if (!string.IsNullOrWhiteSpace(personaView.OtraDiscapacidad))
        //    {
        //        Ruv.Data.TBDISCAPACIDADOTRO_PERSONA dicapacidadOtroData = new TBDISCAPACIDADOTRO_PERSONA();
        //        dicapacidadOtroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
        //        dicapacidadOtroData.TBREGISTROS_PERSONAS.ID = idRegPersona;
        //        dicapacidadOtroData.PARAM_DISCAPACIDAD = (int)eDiscapacidades.Otra;
        //        dicapacidadOtroData.OTRO = personaView.OtraDiscapacidad;
        //        entDiscOtro.setData(dicapacidadOtroData);
        //    }

        //}
        #endregion

        #region Obtener Datos
        public static List<int> Obtener(int personaID)
        {
            entDiscapacidadPersona entBD = new entDiscapacidadPersona();
            return entBD.getData(personaID);
        }
        public static string ObtenerOtro(int personaID)
        {
            entDiscapacidadOtroPersona entBD = new entDiscapacidadOtroPersona();
            return entBD.getData(personaID);
        }
        #endregion
    }
}
