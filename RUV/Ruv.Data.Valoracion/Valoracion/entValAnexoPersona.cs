using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entValAnexoPersona : entidadRUV
    {

        #region New Implementation

        public clsPersonaAnexo GetPorId(int Id)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Id, OracleType = OracleType.Number, Value = Id, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = "p_result", OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                IDataReader dr = d.ExecuteReader("pkg_valoracion.sp_GetValAnexoPersonaPorId");
                List<clsPersonaAnexo> list = ComplexDataAccessImplements.MapFromDataReaderI<clsPersonaAnexo>(dr, true);
                return list.FirstOrDefault();
            }
        }

        public bool Actualizar(clsPersonaAnexo persona, DbTransaction tra)
        {
            using (Dao d = new Dao())
            {
                d.RefreshParameters();

                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = resx::Parametros.Id, OracleType = System.Data.OracleClient.OracleType.Number, Value = persona.Id, Direction = ParameterDirection.Input });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_ID_REGPERSONA", OracleType = System.Data.OracleClient.OracleType.Number, Value = persona.PersonaId, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_ID_OBSERVACION_VAL", OracleType = System.Data.OracleClient.OracleType.Number, Value = persona.ObservacionId, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_ID_ESTADO_VAL", OracleType = System.Data.OracleClient.OracleType.Number, Value = persona.EstadoId, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_ESVICITMA", OracleType = System.Data.OracleClient.OracleType.Number, Value = Convert.ToInt16(persona.Victima), Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_ESAFECTADO", OracleType = System.Data.OracleClient.OracleType.Number, Value = Convert.ToInt16(persona.Afectado), Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_ID_VAL_ANEXO", OracleType = System.Data.OracleClient.OracleType.Number, Value = persona.ValAnexoId, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_OBSERVACION", OracleType = System.Data.OracleClient.OracleType.Clob, Value = persona.Observacion == null ? persona.Observacion = "" : persona.Observacion, Direction = ParameterDirection.Input });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_HECHO_ENMARCADO", OracleType = System.Data.OracleClient.OracleType.Number, Value = persona.HechoEnmarcadoId, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_DECLETO_LEY", OracleType = System.Data.OracleClient.OracleType.VarChar, Value = persona.DecretoLey == null ? persona.DecretoLey = "" : persona.DecretoLey.ToString(), Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_AFECTADAS", OracleType = System.Data.OracleClient.OracleType.Number, Direction = ParameterDirection.Output });

                d.ExecuteNonQuery("pkg_valoracion.sp_ActualizarValAnexoPersona", tra);
                int columnasAfectadas = Convert.ToInt32(d.GetOutputParameter("P_AFECTADAS"));

                return columnasAfectadas > 0 ? true : false;
            }
        }

        #endregion

        #region Old Implementation

        public TBVAL_ANEXO_PERSONA _GetPorId(int Id)
        {
            TBVAL_ANEXO_PERSONA persona = new TBVAL_ANEXO_PERSONA();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetValAnexoPersonaPorId", new object[] { Id, null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    persona.ID = dbDefaults.getInt32(dr, index++).Value;
                    persona.ID_REGPERSONA = dbDefaults.getInt32(dr, index++).Value;
                    persona.ID_OBSERVACION_VAL = dbDefaults.getInt32(dr, index++);
                    persona.ID_ESTADO_VAL = dbDefaults.getInt32(dr, index++);
                    persona.ESVICITMA = dbDefaults.getInt16(dr, index++);
                    persona.ESAFECTADO = dbDefaults.getInt16(dr, index++);
                    persona.ID_VAL_ANEXO = dbDefaults.getInt32(dr, index++);
                    persona.OBSERVACION = dbDefaults.getString(dr, index++);
                }
            }
            return persona;
        }

        public TBVAL_ANEXO_PERSONA ActualizarEF(TBVAL_ANEXO_PERSONA persona)
        {
            using (RuvEntities Context = new RuvEntities())
            {
                //Context.TBVAL_ANEXO_PERSONA.Attach(new TBVAL_ANEXO_PERSONA { ID = persona.ID });
                Context.TBVAL_ANEXO_PERSONA.ApplyCurrentValues(persona);
                Context.SaveChanges();
            }
            return persona;
        }

        public TBVAL_ANEXO_PERSONA _Actualizar(TBVAL_ANEXO_PERSONA persona)
        {
            /*RuvEntities context = new RuvEntities();
            TBVAL_ANEXO_PERSONA tbpersona = context.TBVAL_ANEXO_PERSONA.First(x => x.ID == persona.ID);

            tbpersona.ID_REGPERSONA = persona.ID_REGPERSONA;
            tbpersona.ID_OBSERVACION_VAL = persona.ID_OBSERVACION_VAL;
            tbpersona.ID_ESTADO_VAL = persona.ID_ESTADO_VAL;
            tbpersona.ESVICITMA = persona.ESVICITMA;
            tbpersona.ESAFECTADO = persona.ESAFECTADO;
            tbpersona.ID_VAL_ANEXO = persona.ID_VAL_ANEXO;
            tbpersona.OBSERVACION = persona.OBSERVACION;

            context.SaveChanges();

            return tbpersona;*/
            List<object> objetos = ParametrosGuardar(persona);
            objetos.Add(null);
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_ActualizarValAnexoPersona", objetos.ToArray());

            dbRUV.ExecuteNonQuery(cmd);
            int afectadas = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_AFECTADAS"));
            if (afectadas > 0)
            {
                persona = _GetPorId(persona.ID);
                return persona;
            }
            else
            {
                return null;
            }
        }

        private List<object> ParametrosGuardar(TBVAL_ANEXO_PERSONA persona)
        {
            return new List<object>(){
                persona.ID,
                persona.ID_REGPERSONA,
                persona.ID_OBSERVACION_VAL,
                persona.ID_ESTADO_VAL,
                persona.ESVICITMA,
                persona.ESAFECTADO,
                persona.ID_VAL_ANEXO,
                (!string.IsNullOrEmpty(persona.OBSERVACION)) ? persona.OBSERVACION : " "
            };
        }

        //sp_GetHerramientasPorTipoId

        #endregion

    }
}
