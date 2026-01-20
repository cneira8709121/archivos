using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Objects.DataClasses;
using Ruv.Data.Radicacion;
using System.Data.Common;

namespace Ruv.Business.Captura.Anexos
{
    public class Afectaciones
    {
        public static void Guardar(int id_anexo, int tipoAnexo, clsAnexo_Afectacion afectacionView, DbTransaction tran)
        {
            List<TBAFECTACION> afectacionesData = new List<TBAFECTACION>();
            Afectaciones.ParseViewToData(id_anexo, tipoAnexo, afectacionView, ref afectacionesData);

            entAfectacion entAfect = new entAfectacion();

            //Borrar todas las afectaciones del anexo
            entAfect.deleteData(id_anexo, tipoAnexo, tran);

            //Insertar nuevamente las afectaciones del anexo
            foreach (TBAFECTACION afectacionData in afectacionesData)
                entAfect.setData(afectacionData, tran);
        }

        /// <summary>
        /// Metodo sobrecargado usado para guardar las afectaciones del anexo 13
        /// </summary>
        /// <param name="id_anexo"></param>
        /// <param name="tipoAnexo"></param>
        /// <param name="afectacionView"></param>
        public static void Guardar(int id_anexo, int tipoAnexo, List<int> afectacionView, DbTransaction tran)
        {
            List<TBAFECTACION> afectacionesData = new List<TBAFECTACION>();
            Afectaciones.ParseViewToData(id_anexo, tipoAnexo, afectacionView, ref afectacionesData);

            entAfectacion entAfect = new entAfectacion();

            //Borrar todas las afectaciones del anexo
            entAfect.deleteData(id_anexo, tipoAnexo, tran);

            //Insertar nuevamente las afectaciones del anexo
            foreach (TBAFECTACION afectacionData in afectacionesData)
                entAfect.setData(afectacionData, tran);
        }

        public static List<int> Obtener(int id_anexo, int tipoAnexo)
        {
            List<int> TiposDeAfectacion = new List<int>();
            //Agregar Afectaciones
            entAfectacion entAfec = new entAfectacion();
            List<TBAFECTACION> afectacionesData = entAfec.getData(tipoAnexo, id_anexo);
            foreach (TBAFECTACION afectacionData in afectacionesData)
                TiposDeAfectacion.Add(afectacionData.PARAM_AFECTACION);
            return TiposDeAfectacion;
        }
        
        public static void ParseViewToData(int id_anexo, int tipoAnexo, clsAnexo_Afectacion afectacionView, ref List<TBAFECTACION> afectacionesData)
        {
            afectacionesData = new List<TBAFECTACION>();
            foreach (int tipoAfecatcion in afectacionView.TiposDeAfectacion)
            {
                TBAFECTACION afectacionData = new TBAFECTACION();
                afectacionData.ID_ANEXO = id_anexo;
                afectacionData.PARAM_TIPO_HECHO = tipoAnexo;
                afectacionData.PARAM_AFECTACION = tipoAfecatcion;

                afectacionesData.Add(afectacionData);
            }
        }


        /// <summary>
        /// Metodo sobrecargado utilizado para las afectaciones del anexo 13
        /// </summary>
        /// <param name="id_anexo"></param>
        /// <param name="tipoAnexo"></param>
        /// <param name="afectacionView"></param>
        /// <param name="afectacionesData"></param>
        public static void ParseViewToData(int id_anexo, int tipoAnexo, List<int> afectacionView, ref List<TBAFECTACION> afectacionesData)
        {
            afectacionesData = new List<TBAFECTACION>();
            foreach (int tipoAfectacion in afectacionView)
            {
                TBAFECTACION afectacionData = new TBAFECTACION();
                afectacionData.ID_ANEXO = id_anexo;
                afectacionData.PARAM_TIPO_HECHO = tipoAnexo;
                afectacionData.PARAM_AFECTACION = tipoAfectacion;

                afectacionesData.Add(afectacionData);
            }
        }
    }
}
