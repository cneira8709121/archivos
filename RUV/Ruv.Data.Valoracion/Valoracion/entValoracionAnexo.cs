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
    public class entValoracionAnexo : entidadRUV
    {

        #region New Implementation


        public clsHechosValoracion Actualizar(clsHechosValoracion val_anexo, DbTransaction tra)
        {
            using (Dao d = new Dao())
            {
                d.RefreshParameters();
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = resx::Parametros.Id, OracleType = System.Data.OracleClient.OracleType.Number, Value = val_anexo.Id, Direction = ParameterDirection.Input });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_UltimaFechaEdicion", OracleType = System.Data.OracleClient.OracleType.DateTime, Value = val_anexo.UltimaFechaEdicion, Direction = ParameterDirection.Input });
                d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = "P_CantidadAfecadas", OracleType = System.Data.OracleClient.OracleType.Number, Direction = ParameterDirection.Output });

                d.ExecuteNonQuery("pkg_valoracion.sp_ActualizarValAnexo", tra);
                int columnasAfectadas = Convert.ToInt32(d.GetOutputParameter("P_CantidadAfecadas"));

                if (columnasAfectadas > 0) 
                    return GetPorId(val_anexo.Id);
                
                return null;
            }
        }

        public clsHechosValoracion GetPorId(int Id)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Id, OracleType = OracleType.Number, Value = Id, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = "P_Result", OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                IDataReader dr = d.ExecuteReader("pkg_valoracion.sp_GetValAnexoPorId");
                List<clsHechosValoracion> list = ComplexDataAccessImplements.MapFromDataReaderI<clsHechosValoracion>(dr, true);
                return list.FirstOrDefault();
            }
        }

        #endregion

        public TBVALORACION_ANEXO _Actualizar(TBVALORACION_ANEXO val_anexo)
        {
            List<object> objetos = ParametrosGuardar(val_anexo);
            objetos.Add(null);
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_ActualizarValAnexo", objetos.ToArray());

            dbRUV.ExecuteNonQuery(cmd);
            int afectadas = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_CantidadAfecadas"));
            if (afectadas > 0)
            {
                return _GetPorId(val_anexo.ID);
            }
            else { return null; }
        }

        public TBVALORACION_ANEXO ActualizarEF(TBVALORACION_ANEXO val_anexo)
        {
            using (RuvEntities Context = new RuvEntities())
            {
                Context.TBVALORACION_ANEXO.Attach(new TBVALORACION_ANEXO { ID = val_anexo.ID });
                Context.TBVALORACION_ANEXO.ApplyCurrentValues(val_anexo);
                Context.SaveChanges();
                return val_anexo;
            }
        }

        private List<object> ParametrosGuardar(TBVALORACION_ANEXO val_anexo)
        {
            return new List<object>(){
                val_anexo.ID,
                val_anexo.ULTIMA_FECHAEDICION,
            };
        }

        public TBVALORACION_ANEXO _GetPorId(int Id)
        {
            TBVALORACION_ANEXO valanexo = new TBVALORACION_ANEXO();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetValAnexoPorId", new object[] { Id, null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    valanexo.ID = dbDefaults.getInt32(dr, index++).Value;
                    valanexo.ID_VALORACION = dbDefaults.getInt32(dr, index++).Value;
                    valanexo.ULTIMA_FECHAEDICION = dbDefaults.getDateTime(dr, index++);
                    valanexo.TIPO_ANEXO = dbDefaults.getInt16(dr, index++);
                    valanexo.ID_SINIESTRO = dbDefaults.getInt32(dr, index++);

                }
            }
            return valanexo;
        }

        public TBVALORACION_ANEXO GetPorIdEF(int Id)
        {
            using (RuvEntities Context = new RuvEntities())
            {
                return Context.TBVALORACION_ANEXO.First(x => x.ID == Id);
            }
        }

        public int Nuevo(List<object> nhecho)
        {
            nhecho.Add(null);
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_CrearHecho", nhecho.ToArray());

            dbRUV.ExecuteNonQuery(cmd);
            int ValAnexo = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_ValAnexo"));

            int result = ValAnexo;

            return result;
        }

        public bool NuevoAnexo(int valAnexoId, int regPersona, int estadoEnHecho, DateTime? fechaDespojo, DateTime? fechaAbandono, int valorEspecifico = 0, int inmuebleAbandono = default(int), int inmuebleDespojo = 0)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_CrearAnexo", valAnexoId, regPersona, estadoEnHecho, valorEspecifico, inmuebleDespojo, inmuebleAbandono, fechaAbandono, fechaDespojo);

            dbRUV.ExecuteNonQuery(cmd);
            
            return true;
        }
    }
}
