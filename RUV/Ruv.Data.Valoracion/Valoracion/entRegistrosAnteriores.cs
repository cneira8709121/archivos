using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;
using System.Data.OracleClient;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entRegistrosAnteriores : entidadRUV
    {
        public List<TBREGISTROS_ANTERIORES> GetRegistrosAnteriores()
        {
            List<TBREGISTROS_ANTERIORES> Registros = new List<TBREGISTROS_ANTERIORES>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_getRegistrosAnteriores", new object[] {  null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBREGISTROS_ANTERIORES regitro = EnterpriseLibraryContainer.Current.GetInstance<TBREGISTROS_ANTERIORES>();
                    regitro.ID = dbDefaults.getInt32(dr, index++).Value;
                    regitro.NOMBRE = dbDefaults.getString(dr, index++);
                    regitro.DESCRIPCION = dbDefaults.getString(dr, index++); ;
                    Registros.Add(regitro);
                }
            }
            return Registros;
        }

        public List<TBPARAMETROS> GetPreguntasRegistrosAnteriores()
        {
            List<TBPARAMETROS> Preguntas = new List<TBPARAMETROS>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_getPreguntasRegAnteriores", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPARAMETROS pregunta = EnterpriseLibraryContainer.Current.GetInstance<TBPARAMETROS>();
                    pregunta.ID = dbDefaults.getInt32(dr, index++).Value;
                    pregunta.NOMBRE = dbDefaults.getString(dr, index++);
                    Preguntas.Add(pregunta);
                }
            }
            return Preguntas;
        }


        public List<TBVALORACION_REGISTROS> GetRegistrosPorValoracion(int IdValoracion)
        {
            List<TBVALORACION_REGISTROS> Registros = new List<TBVALORACION_REGISTROS>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_getRegistrosAntPorValId", new object[] { IdValoracion, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBVALORACION_REGISTROS regitro = EnterpriseLibraryContainer.Current.GetInstance<TBVALORACION_REGISTROS>();
                    regitro.ID = dbDefaults.getInt32(dr, index++).Value;
                    regitro.ID_REGISTRO = dbDefaults.getInt32(dr, index++);
                    regitro.ID_VALORACION = dbDefaults.getInt32(dr, index++);
                    Registros.Add(regitro);
                }
            }
            return Registros;
        }

        public List<TBREGISTROS_PERSONAS> GetPersonasPorValRegId(int ValRegId)
        {
            List<TBREGISTROS_PERSONAS> personas = new List<TBREGISTROS_PERSONAS>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_getPersonasPorRegValId", new object[] { ValRegId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBREGISTROS_PERSONAS persona = EnterpriseLibraryContainer.Current.GetInstance<TBREGISTROS_PERSONAS>();
                    persona.ID = dbDefaults.getInt32(dr, index++).Value;
                    personas.Add(persona);
                }
            }
            return personas;
        }

        public List<TBPARAMETROS> GetPreguntasPorValRegId(int ValRegId)
        {
            List<TBPARAMETROS> preguntas = new List<TBPARAMETROS>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_getPreguntasPorRegValId", new object[] { ValRegId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPARAMETROS pregunta = EnterpriseLibraryContainer.Current.GetInstance<TBPARAMETROS>();
                    pregunta.ID = dbDefaults.getInt32(dr, index++).Value;
                    preguntas.Add(pregunta);
                }
            }
            return preguntas;
        }

        private List<object> ParametrosGuardarHerval(TBVALORACION_REGISTROS data)
        {
            return new List<object>(){
                data.ID,
                (data.ID_REGISTRO.HasValue) ? data.ID_REGISTRO.Value : 0,
                (data.ID_VALORACION.HasValue) ? data.ID_VALORACION.Value : 0
            };
        }

        

        public int InsertarRegistroAnterior(TBVALORACION_REGISTROS data, DbTransaction transaction) { 
            using (var d = new Dao()) {
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_RegistroId", OracleType = OracleType.Number, Value = data.ID_REGISTRO });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_ValoracionId", OracleType = OracleType.Number, Value = data.ID_VALORACION });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "p_Id", OracleType = OracleType.Number });
                d.ExecuteNonQuery("PKG_VALORACION.sp_InsertarRegistroAnterior", transaction);
                return Convert.ToInt32(d.GetOutputParameter("p_Id"));
            }
        }

        public void ActualizarRegistroAnterior(TBVALORACION_REGISTROS data, DbTransaction transaction) {
            using (var d = new Dao()) {
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_Id", OracleType = OracleType.Number, Value = data.ID });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_RegistroId", OracleType = OracleType.Number, Value = data.ID_REGISTRO });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_ValoracionId", OracleType = OracleType.Number, Value = data.ID_VALORACION });
                d.ExecuteNonQuery("PKG_VALORACION.sp_ActualizarRegistroAnterior", transaction);
            }
        }

        public void EliminarRegistroAnteriorValoracion(int id, DbTransaction transaction) {
            using (var d = new Dao()) {
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_Id", OracleType = OracleType.Number, Value = id });
                d.ExecuteNonQuery("PKG_VALORACION.sp_EliminarRegistroAnterior", transaction);
            }
        }

        public void InsertarRegistroAnteriorPersona(int registroAnteriorId, int registroPersonaId, DbTransaction transaction) {
            using (var d = new Dao()) {
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_RegistroAnteriorId", OracleType = OracleType.Number, Value = registroAnteriorId });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_RegPersonaId", OracleType = OracleType.Number, Value = registroPersonaId });
                d.ExecuteNonQuery("PKG_VALORACION.sp_InsertarRegistroAntPersona", transaction);
            }
        }

        public void InsertarRegistroAnteriorPregunta(int registroAnteriorId, int preguntaId, DbTransaction transaction) {
            using (var d = new Dao()) {
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_RegistroAnteriorId", OracleType = OracleType.Number, Value = registroAnteriorId });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_PreguntaId", OracleType = OracleType.Number, Value = preguntaId });
                d.ExecuteNonQuery("PKG_VALORACION.sp_InsertarRegistroAntPregunta", transaction);
            }
        }

    }
}
