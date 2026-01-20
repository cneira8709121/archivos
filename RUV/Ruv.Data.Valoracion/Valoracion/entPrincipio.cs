using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entPrincipio : entidadRUV
    {
        public bool Eliminar(int IdValAnexoPer, DbTransaction tra)
        {
            using (Dao d = new Dao())
            {
                d.RefreshParameters();
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_ID_ValAnexoPer", OracleType = OracleType.Number, Value = IdValAnexoPer });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "P_AFECTADAS", OracleType = OracleType.Number });
                d.ExecuteNonQuery("pkg_valoracion.sp_EliminaTbPrincipioVal", tra);
                int afectadas = Convert.ToInt32(d.GetOutputParameter("P_AFECTADAS"));
                return afectadas > 0 ? true : false;
            }
        }
        
        public List<TBPRINCIPIO> GetPrincipiosPorEstadoId(int estadoId)
        {
            List<TBPRINCIPIO> principios = new List<TBPRINCIPIO>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetPrincipiosPorEstado", new object[] { estadoId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPRINCIPIO principio = EnterpriseLibraryContainer.Current.GetInstance<TBPRINCIPIO>();
                    principio.ID = dbDefaults.getInt32(dr, index++).Value;
                    principio.NOMBRE = dbDefaults.getString(dr, index++);
                    principio.TEXTO = dbDefaults.getString(dr, index++); ;
                    principio.ID_ESTADO_VAL = dbDefaults.getInt32(dr, index++).Value;
                    principios.Add(principio);
                }
            }
            return principios;
        }

        public void InsertarCausal(int causalId, int valId)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_InsertarTbValoracionPri", new object[] { causalId, valId, null });

            dbRUV.ExecuteNonQuery(cmd);
            int afectadas = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_AFECTADAS"));
        }

        public void Insertar(int principioId, int valAnexoPerId, DbTransaction tra)
        {
            using(Dao d = new Dao())
            {
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_ID_PRINCIPIO", OracleType = OracleType.Number, Value = principioId });
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_ID_VAL_ANEXO_PER", OracleType = OracleType.Number, Value = valAnexoPerId });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "P_AFECTADAS", OracleType = OracleType.Number });
                d.ExecuteNonQuery("pkg_valoracion.sp_InsertarTbPrincipioVal", tra);
                int afectadas = Convert.ToInt32(d.GetOutputParameter("P_AFECTADAS"));
            }
        }

        public List<TBPRINCIPIO> GetPrincipiosPorValAnexoPerId(int valAnexoPerId)
        {
            List<TBPRINCIPIO> principios = new List<TBPRINCIPIO>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetPrincipioPorValAnexoPer", new object[] { valAnexoPerId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPRINCIPIO principio = EnterpriseLibraryContainer.Current.GetInstance<TBPRINCIPIO>();
                    principio.ID = dbDefaults.getInt32(dr, index++).Value;
                    principio.NOMBRE = dbDefaults.getString(dr, index++);
                    principio.TEXTO = dbDefaults.getString(dr, index++); ;
                    principio.ID_ESTADO_VAL = dbDefaults.getInt32(dr, index++).Value;
                    principios.Add(principio);
                }
            }
            return principios;
        }

        public List<TBPRINCIPIO> GetPrincipiosPorValoracion(int valId)
        {
            List<TBPRINCIPIO> principios = new List<TBPRINCIPIO>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetPrincipioPorVal", new object[] { valId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPRINCIPIO principio = EnterpriseLibraryContainer.Current.GetInstance<TBPRINCIPIO>();
                    principio.ID = dbDefaults.getInt32(dr, index++).Value;
                    principio.NOMBRE = dbDefaults.getString(dr, index++);
                    principio.TEXTO = dbDefaults.getString(dr, index++); ;
                    principio.ID_ESTADO_VAL = dbDefaults.getInt32(dr, index++).Value;
                    principios.Add(principio);
                }
            }
            return principios;
        }

        public List<TBPRINCIPIO> GetPrincipios()
        {
            List<TBPRINCIPIO> principios = new List<TBPRINCIPIO>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetPrincipios", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBPRINCIPIO principio = EnterpriseLibraryContainer.Current.GetInstance<TBPRINCIPIO>();
                    principio.ID = dbDefaults.getInt32(dr, index++).Value;
                    principio.NOMBRE = dbDefaults.getString(dr, index++);
                    principio.TEXTO = dbDefaults.getString(dr, index++); ;
                    principio.ID_ESTADO_VAL = dbDefaults.getInt32(dr, index++).Value;
                    principios.Add(principio);
                }
            }
            return principios;
        }
    }
}
