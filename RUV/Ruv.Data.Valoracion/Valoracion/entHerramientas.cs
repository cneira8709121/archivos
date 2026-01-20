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
    public class entHerramientas : entidadRUV
    {
        #region tbherramienta_anexo
        
        public List<TBHERRAMIENTA_ANEXO_PER> GetHerramientaPorAnexoVal(int anexoValId)
        {
            List<TBHERRAMIENTA_ANEXO_PER> herramientas = new List<TBHERRAMIENTA_ANEXO_PER>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetHerraPorAnexoPerId", new object[] { anexoValId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBHERRAMIENTA_ANEXO_PER her = EnterpriseLibraryContainer.Current.GetInstance<TBHERRAMIENTA_ANEXO_PER>();
                    her.TBHERRAMIENTAVAL = new TBHERRAMIENTAVAL();
                    her.TBHERRAMIENTAVAL.TBTIPO_HERRAMIENTAVAL = new TBTIPO_HERRAMIENTAVAL();

                    her.ID_VALANEXO_PER = dbDefaults.getInt32(dr, index++).Value;
                    her.ID_HERRAMIENTA = dbDefaults.getInt32(dr, index++).Value;
                    her.DETALLE = dbDefaults.getString(dr, index++);
                    her.FECHA = dbDefaults.getDateTime(dr, index++);
                    her.USAPARADESICION = dbDefaults.getInt16(dr, index++);
                    her.TBHERRAMIENTAVAL.ID = dbDefaults.getInt32(dr, index++).Value;
                    her.TBHERRAMIENTAVAL.NOMBRE = dbDefaults.getString(dr, index++);
                    her.TBHERRAMIENTAVAL.TEXTO = dbDefaults.getString(dr, index++);
                    her.TBHERRAMIENTAVAL.ID_TIPO_HERRAMIENTA = dbDefaults.getInt32(dr, index++);
                    her.TBHERRAMIENTAVAL.TBTIPO_HERRAMIENTAVAL.ID = dbDefaults.getInt32(dr, index++).Value;
                    her.TBHERRAMIENTAVAL.TBTIPO_HERRAMIENTAVAL.NOMBRE = dbDefaults.getString(dr, index++);
                    her.TBHERRAMIENTAVAL.TBTIPO_HERRAMIENTAVAL.TEXTO = dbDefaults.getString(dr, index++);
                    herramientas.Add(her);
                }
            }
            return herramientas;
            /*
            RuvEntities context = new RuvEntities();
            
            herramientas = context.TBHERRAMIENTA_ANEXO_PER.Where(x => x.ID_VALANEXO_PER == anexoValId).ToList();
            return herramientas;*/
        }


        /*public TBHERRAMIENTA_ANEXO_PER Insertar(TBHERRAMIENTA_ANEXO_PER anexo)
        {
            List<object> objetos = ParametrosGuardar(anexo);
            objetos.Add(null);
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_InsertarTbValHerramienta", objetos.ToArray());

            dbRUV.ExecuteNonQuery(cmd);
            int inserto = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_AFECTADAS"));
            if (inserto > 0) {
                anexo.ID = inserto;
                return anexo; 
            }
            else { return anexo; }
        }*/

        public void Insertar(TBHERRAMIENTA_ANEXO_PER anexo, DbTransaction tra)
        {
            if (anexo.TBHERRAMIENTAVAL != null)
            {
                anexo.TBHERRAMIENTAVAL = InsertarHerramientaVal(anexo.TBHERRAMIENTAVAL, tra);
                anexo.ID_HERRAMIENTA = anexo.TBHERRAMIENTAVAL.ID;
            }

            using (var d = new Dao())
            {
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_ID_VAL_ANEXO_PER", OracleType = OracleType.Number, Value = anexo.ID_VALANEXO_PER });
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_ID_HERRAMIENTA", OracleType = OracleType.Number, Value = anexo.ID_HERRAMIENTA });
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_DETALLES", OracleType = OracleType.Clob, Value = anexo.DETALLE == null ? anexo.DETALLE = "" : anexo.DETALLE });
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_FECHA", OracleType = OracleType.DateTime, Value = anexo.FECHA.HasValue ? anexo.FECHA.Value : DateTime.Now });
                d.AddInputParameter (new OracleParameter() { ParameterName = "P_USAPARADESICION", OracleType = OracleType.Number, Value = anexo.USAPARADESICION.HasValue ? (short)(anexo.USAPARADESICION.Value) : 0 });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "P_AFECTADAS", OracleType = OracleType.Number });

                d.ExecuteNonQuery("pkg_valoracion.sp_InsertarTbValHerramienta", tra);
            }

            //List<object> objetos = ParametrosGuardar(anexo);
            //objetos.Add(null);
            //DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_InsertarTbValHerramienta", objetos.ToArray());

            //dbRUV.ExecuteNonQuery(cmd);
            //int inserto = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_AFECTADAS"));           
        }

        public TBHERRAMIENTA_ANEXO_PER Actualizar(TBHERRAMIENTA_ANEXO_PER anexo)
        {

            List<object> objetos = ParametrosGuardar(anexo);
            objetos.Add(null);
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_ActualizarTbValHerramienta", objetos.ToArray());

            dbRUV.ExecuteNonQuery(cmd);
            int afectadas = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_Afectadas"));
            if (afectadas > 0) {
                return anexo;
            }
            else { 
                return null; 
            }
        }
        
        public bool Eliminar(int AnexoId, DbTransaction tra)
        {
            using (Dao d = new Dao())
            {
                d.RefreshParameters();
                d.AddInputParameter(new OracleParameter() { ParameterName = "P_Id", OracleType = System.Data.OracleClient.OracleType.Number, Value = AnexoId });
                d.AddParameter(new OracleParameter() { ParameterName = "P_Afectadas", OracleType = OracleType.Number, Direction = ParameterDirection.Output });
                d.ExecuteNonQuery("pkg_valoracion.sp_EliminarTbHerrAnexo", tra);
                int columnasAfectadas = Convert.ToInt32(d.GetOutputParameter("P_Afectadas"));
                return columnasAfectadas > 0 ? true : false;
            }
                
        }


        private List<object> ParametrosGuardar(TBHERRAMIENTA_ANEXO_PER val_anexo)
        {
            return new List<object>(){
                val_anexo.ID_VALANEXO_PER,
                val_anexo.ID_HERRAMIENTA,
                (!string.IsNullOrEmpty(val_anexo.DETALLE)) ? val_anexo.DETALLE : " ",
                (val_anexo.FECHA.HasValue) ? val_anexo.FECHA.Value : DateTime.Now,
                (val_anexo.USAPARADESICION.HasValue) ? (short)(val_anexo.USAPARADESICION.Value) : 0
            };
        }

        #endregion
        #region tbherramienta_val

        public TBHERRAMIENTAVAL InsertarHerramientaVal(TBHERRAMIENTAVAL herval, DbTransaction tra)
        {
            using (var d = new Dao())
            {    
                d.AddParameter(new OracleParameter() { ParameterName = "P_ID_TIPO_HERR", OracleType = OracleType.Number, Value = herval.ID_TIPO_HERRAMIENTA, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter() { ParameterName = "P_NOMBRE", OracleType = OracleType.VarChar, Value = herval.NOMBRE, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter() { ParameterName = "P_TEXTO", OracleType = OracleType.VarChar, Value = herval.TEXTO, Direction = ParameterDirection.Input });
                d.ExecuteNonQuery("pkg_valoracion.sp_InsertarTbHerrVal", tra);
                herval.ID = int.Parse(d.GetOutputParameter("P_ID").ToString());
                return herval;
            }
        }

        private List<object> ParametrosGuardarHerval(TBHERRAMIENTAVAL herval)
        {
            return new List<object>(){
                herval.ID,
                herval.ID_TIPO_HERRAMIENTA,
                herval.NOMBRE,
                herval.TEXTO
            };
        }


        #endregion

        public List<TBTIPO_HERRAMIENTAVAL> GetTiposHerramientas()
        {
            List<TBTIPO_HERRAMIENTAVAL> herramientas = new List<TBTIPO_HERRAMIENTAVAL>();
            
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetTiposHerramienta", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBTIPO_HERRAMIENTAVAL her = EnterpriseLibraryContainer.Current.GetInstance<TBTIPO_HERRAMIENTAVAL>();

                    her.ID = dbDefaults.getInt32(dr, index++).Value;
                    her.NOMBRE = dbDefaults.getString(dr, index++);
                    herramientas.Add(her);
                }
            }
            /*using(RuvEntities Context = new RuvEntities()){
                herramientas = Context.TBTIPO_HERRAMIENTAVAL.OrderBy(x => x.NOMBRE).ToList();
            }*/
            return herramientas;
        }

        public TBTIPO_HERRAMIENTAVAL GetTiposHerramientasPorId(int tipodId)
        {
            TBTIPO_HERRAMIENTAVAL herramientas = new TBTIPO_HERRAMIENTAVAL();

            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetTipoHerramientaPorId", new object[] { tipodId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    herramientas.ID = dbDefaults.getInt32(dr, index++).Value;
                    herramientas.NOMBRE = dbDefaults.getString(dr, index++);
                }
            }
            /*using(RuvEntities Context = new RuvEntities()){
                herramientas = Context.TBTIPO_HERRAMIENTAVAL.OrderBy(x => x.NOMBRE).ToList();
            }*/
            return herramientas;
        }

        public List<TBHERRAMIENTAVAL> GetHerramientaPorTipoId(int TipoId)
        {
            List<TBHERRAMIENTAVAL> herramientas = new List<TBHERRAMIENTAVAL>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetHerramientasPorTipoId", new object[] { TipoId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBHERRAMIENTAVAL her = EnterpriseLibraryContainer.Current.GetInstance<TBHERRAMIENTAVAL>();

                    her.ID = dbDefaults.getInt32(dr, index++).Value;
                    her.ID_TIPO_HERRAMIENTA = dbDefaults.getInt32(dr, index++).Value;
                    her.NOMBRE = dbDefaults.getString(dr, index++);
                    her.TEXTO = dbDefaults.getString(dr, index++);
                    herramientas.Add(her);
                }
            }
            /*using (RuvEntities Context = new RuvEntities())
            {
                herramientas = Context.TBHERRAMIENTAVAL.Where(h=>h.ID_TIPO_HERRAMIENTA == TipoId).OrderBy(x => x.NOMBRE).ToList();
            }*/
            return herramientas;
        }

        public List<TBHERRAMIENTAVAL> GetHerramientas()
        {
            List<TBHERRAMIENTAVAL> herramientas = new List<TBHERRAMIENTAVAL>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetHerramientas", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    TBHERRAMIENTAVAL her = EnterpriseLibraryContainer.Current.GetInstance<TBHERRAMIENTAVAL>();

                    her.ID = dbDefaults.getInt32(dr, index++).Value;
                    her.ID_TIPO_HERRAMIENTA = dbDefaults.getInt32(dr, index++).Value;
                    her.NOMBRE = dbDefaults.getString(dr, index++);
                    her.TEXTO = dbDefaults.getString(dr, index++);
                    herramientas.Add(her);
                }
            }
            return herramientas;
        }
    }
}
