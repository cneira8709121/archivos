using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data;
using Ruv.Data;
using Ruv.Data.Valoracion.Valoracion;
using System.Data.Common;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class RegistrosAnteriores
    {
        public RegistrosAnteriores()
        {
        }

        public static List<clsRegistrosAnteriores> GetRegistrosAnteriores()
        {
            List<clsRegistrosAnteriores> Registros = new List<clsRegistrosAnteriores>();
            entRegistrosAnteriores objRegAnt = new entRegistrosAnteriores();
            List<TBREGISTROS_ANTERIORES> RegAnt = objRegAnt.GetRegistrosAnteriores();
            foreach (TBREGISTROS_ANTERIORES data in RegAnt)
            {
                clsRegistrosAnteriores view = new clsRegistrosAnteriores();
                ParseDataToView(ref view, data);
                Registros.Add(view);
            }
            return Registros;
        }

        private static void ParseDataToView(ref clsRegistrosAnteriores view, TBREGISTROS_ANTERIORES data)
        {
            view.Id = data.ID;
            view.Nombre = data.NOMBRE;
        }

        internal static List<clsRegistrosValoracion> GetRegistrosPorValoracion(int IdValoracion)
        {
            List<clsRegistrosValoracion> Registros = new List<clsRegistrosValoracion>();
            entRegistrosAnteriores objRegAnt = new entRegistrosAnteriores();
            List<TBVALORACION_REGISTROS> RegAnt = objRegAnt.GetRegistrosPorValoracion(IdValoracion);
            foreach (TBVALORACION_REGISTROS data in RegAnt)
            {
                clsRegistrosValoracion view = new clsRegistrosValoracion();
                ParseDataToViewRegVal(ref view, data);
                Registros.Add(view);
            }
            return Registros;
        }

        private static void ParseDataToViewRegVal(ref clsRegistrosValoracion view, TBVALORACION_REGISTROS data)
        {
            view.Id = data.ID;
            view.RegistroId = (data.ID_REGISTRO.HasValue) ? data.ID_REGISTRO.Value : 0;
            view.ValoracionId = (data.ID_VALORACION.HasValue) ? data.ID_VALORACION.Value : 0;
            view.RegPersonas = GetPersonasPorRegVal(view.Id);
            view.Preguntas = PreguntasRegistrosAnteriores.GetPreguntasPorRegVal(view.Id);
        }

        private static List<int> GetPersonasPorRegVal(int ValRegId)
        {
            List<int> personas = new List<int>();
            entRegistrosAnteriores objRegAnt = new entRegistrosAnteriores();
            List<TBREGISTROS_PERSONAS> regPer = objRegAnt.GetPersonasPorValRegId(ValRegId);
            foreach (TBREGISTROS_PERSONAS data in regPer)
            {
                personas.Add(data.ID);
            }
            return personas;
        }

        private static void ParseViewToDataRegVal(clsRegistrosValoracion regVal, ref TBVALORACION_REGISTROS data)
        {
            data.ID = regVal.Id;
            data.ID_REGISTRO = regVal.RegistroId;
            data.ID_VALORACION = regVal.ValoracionId;
        }

        internal static List<clsRegistrosValoracion> GetRegistrosPorValoracion(DataTable dtRegitros)
        {
            List<clsRegistrosValoracion> Registros = new List<clsRegistrosValoracion>();
            foreach (DataRow data in dtRegitros.Rows)
            {
                clsRegistrosValoracion view = new clsRegistrosValoracion();
                ParseDataToViewRegVal(ref view, data);
                Registros.Add(view);
            }
            return Registros;
        }

        private static void ParseDataToViewRegVal(ref clsRegistrosValoracion view, DataRow data)
        {
            view.Id = Convert.ToInt32(data["Id"]);
            view.RegistroId = (data["ID_REGISTRO"] != DBNull.Value) ? Convert.ToInt32(data["ID_REGISTRO"]) : 0;
            view.ValoracionId = (data["ID_VALORACION"] != DBNull.Value) ? Convert.ToInt32(data["ID_VALORACION"]) : 0;
            view.RegPersonas = GetPersonasPorRegVal(view.Id);
            view.Preguntas = PreguntasRegistrosAnteriores.GetPreguntasPorRegVal(view.Id);
        }

        

        internal static void Eliminar(clsRegistrosValoracion registroAnterior, DbTransaction transaction) {
            var registroAnteriorData = new entRegistrosAnteriores();
            registroAnteriorData.EliminarRegistroAnteriorValoracion(registroAnterior.Id, transaction);
        }

        internal static void Nuevo(clsRegistrosValoracion registroAnterior, DbTransaction transaction) {
            var registroAnteriorData = new entRegistrosAnteriores();
            var element = new TBVALORACION_REGISTROS { ID_REGISTRO = registroAnterior.RegistroId
                                                     , ID_VALORACION = registroAnterior.ValoracionId };

            var idRegistroAnterior = registroAnteriorData.InsertarRegistroAnterior(element, transaction);

            foreach (int personaId in registroAnterior.RegPersonas) {
                registroAnteriorData.InsertarRegistroAnteriorPersona(idRegistroAnterior, personaId, transaction);
            }

            foreach (int preguntaId in registroAnterior.Preguntas) {
                registroAnteriorData.InsertarRegistroAnteriorPregunta(idRegistroAnterior, preguntaId, transaction);
            }
        }

        internal static void Actualizar(clsRegistrosValoracion registroAnterior, DbTransaction transaction) {
            var registroAnteriorData = new entRegistrosAnteriores();
            var element = new TBVALORACION_REGISTROS { ID = registroAnterior.Id
                                                     , ID_REGISTRO = registroAnterior.RegistroId
                                                     , ID_VALORACION = registroAnterior.ValoracionId };
            
            registroAnteriorData.ActualizarRegistroAnterior(element, transaction);

            foreach (int personaId in registroAnterior.RegPersonas) {
                registroAnteriorData.InsertarRegistroAnteriorPersona(registroAnterior.Id, personaId, transaction);
            }

            foreach (int preguntaId in registroAnterior.Preguntas) {
                registroAnteriorData.InsertarRegistroAnteriorPregunta(registroAnterior.Id, preguntaId, transaction);
            }
        }

    }
}
