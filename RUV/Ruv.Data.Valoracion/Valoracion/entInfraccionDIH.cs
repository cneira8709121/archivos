using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class entInfraccionDIH : entidadRUV
    {
        /*public TBINFRACCION_DIH_VALANEXOPER Insertar(TBINFRACCION_DIH_VALANEXOPER infraccion)
        {
            List<object> objetos = ParametrosGuardar(infraccion);
            objetos.Add(null);
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_InsertarTbInfraccionDIH", objetos.ToArray());

            dbRUV.ExecuteNonQuery(cmd);
            int inserto = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_AFECTADAS"));
            if (inserto > 0)
            {
                return infraccion;
            }
            else { return null; }
        }
        private List<object> ParametrosGuardar(TBINFRACCION_DIH_VALANEXOPER infraccion)
        {
            return new List<object>(){
                infraccion.ID_INFRACCIONDIH,
                infraccion.ID_VAL_ANEXO_PER
            };
        }*/

        public bool Eliminar(int IdValAnexo, DbTransaction tra)
        {
            using (var d = new Dao())
            {
                d.AddInputParameter(new OracleParameter() { ParameterName = "P_ID_ValAnexoPer", OracleType = OracleType.Number, Value = IdValAnexo });
                d.AddParameter(new OracleParameter() { ParameterName = "P_AFECTADAS", OracleType = OracleType.Number, Direction = ParameterDirection.Output });
                d.ExecuteNonQuery("pkg_valoracion.sp_EliminaTbInfraccionDIH", tra);
                int afectadas = Convert.ToInt32(d.GetOutputParameter("P_AFECTADAS"));

                return afectadas > 0 ? true : false;
            }
        }

        public List<TBINFRACCION_DIH> GetInfracciones()
        {
            List<TBINFRACCION_DIH> infraccioes = new List<TBINFRACCION_DIH>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetInfracciones", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBINFRACCION_DIH infra = EnterpriseLibraryContainer.Current.GetInstance<TBINFRACCION_DIH>();
                    infra.ID = dbDefaults.getInt32(dr, index++).Value;
                    infra.NOMBRE = dbDefaults.getString(dr, index++);
                    infraccioes.Add(infra);
                }
            }
            return infraccioes;
        }

        public List<TBINFRACCION_DIH> GetInfraccionesPorValAnexoPerId(int ValAnexoPerId)
        {
            List<TBINFRACCION_DIH> infraccioes = new List<TBINFRACCION_DIH>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetInfraccionesValAnexoPer", new object[] { ValAnexoPerId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBINFRACCION_DIH infra = EnterpriseLibraryContainer.Current.GetInstance<TBINFRACCION_DIH>();
                    infra.ID = dbDefaults.getInt32(dr, index++).Value;
                    infra.NOMBRE = dbDefaults.getString(dr, index++);
                    infraccioes.Add(infra);
                }
            }
            return infraccioes;
        }

        public List<TBINFRACCION_DIH> GetInfracciones(int valAnexoPerId)
        {
            List<TBINFRACCION_DIH> infraccioes = new List<TBINFRACCION_DIH>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetInfraccionesPorAnexoPer", new object[] { valAnexoPerId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBINFRACCION_DIH infra = EnterpriseLibraryContainer.Current.GetInstance<TBINFRACCION_DIH>();
                    infra.ID = dbDefaults.getInt32(dr, index++).Value;
                    infra.NOMBRE = dbDefaults.getString(dr, index++);
                    infraccioes.Add(infra);
                }
            }
            return infraccioes;
        }

        public void Insertar(int InfraccionId, int ValAnexoId, DbTransaction tra)
        {
            using (var d = new Dao())
            {
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_ID_INFRACCIONDIH", OracleType = OracleType.Number, Value = InfraccionId });
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_ID_VAL_ANEXO_PER", OracleType = OracleType.Number, Value = ValAnexoId });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "P_AFECTADAS", OracleType = OracleType.Number });
                d.ExecuteNonQuery("pkg_valoracion.sp_InsertarTbInfraccionDIH", tra);
            }
        }
    }
}
