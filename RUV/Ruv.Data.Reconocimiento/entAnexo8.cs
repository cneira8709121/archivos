using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Ruv.Data.Reconocimiento
{
    public class entAnexo8 : entidadRUV
    {
        #region Set y Update
        public void setAnexo8(TBANEXO8 objeAnexo8, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo8", getParametros(objeAnexo8));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo8.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo8(TBANEXO8 objeAnexo8, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo8", getParametros(objeAnexo8));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO8 objeAnexo)
        {
            return new object[]{   
                                   objeAnexo.ID                          
                                  ,objeAnexo.TBSINIESTROS_PERSONA.ID
                                  ,objeAnexo.TBREGISTROS_PERSONAS.ID 
                                  ,objeAnexo.VICTIMA                     
                                  ,objeAnexo.SECUESTRADO                 
                                  ,objeAnexo.AFECTADO                    
                                  ,objeAnexo.OTRA_AFECTACION             
                                  ,objeAnexo.DECLARACIONPREV             
                                  ,objeAnexo.PARAM_ENTIDAD_DENUNCIAPREV  
                                  ,objeAnexo.FECHA_DENUNCIAPREV          
                                  ,objeAnexo.ID_PAIS_DENUNCIAPREV
                                  ,objeAnexo.ID_DEPARTAMENTO_DENUNCIAPREV
                                  ,objeAnexo.ID_MUNICIPIO_DENUNCIAPREV   
                                  ,objeAnexo.NUMERO_RADICADO_DENUNCIAPREV
                                  ,objeAnexo.PARAM_TIPO_SECUESTRO        
                                  ,objeAnexo.PARAM_FINALIDAD_SECUESTRO   
                                  ,objeAnexo.OTRA_FINALIDAD_SECUESTRO    
                                  ,objeAnexo.CONTRAPRESTACION            
                                  ,objeAnexo.QUE_CONTRAPRESTACION        
                                  ,objeAnexo.PARAM_SITUACIONACTUALVICTIMA
                                  ,objeAnexo.PARAM_LIBERACION_VICTIMA    
                                  ,objeAnexo.FECHA_LIBERACION            
                                  ,objeAnexo.ACTIVO                      
                                  ,null

            };
        }
        #endregion

        #region Obtener
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Siniestro.</param>
        /// <returns></returns>
        public List<TBANEXO8> getData(int ID)
        {
            List<TBANEXO8> registros = new List<TBANEXO8>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos8", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO8 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO8>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
                    
                    int index = 0;

                    #region Common Anexos
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID    = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.VICTIMA = dbDefaults.getInt16(dataReader, index++);
                    registro.SECUESTRADO = dbDefaults.getInt16(dataReader, index++);
                    registro.AFECTADO = dbDefaults.getInt16(dataReader, index++);
                    registro.OTRA_AFECTACION = dbDefaults.getString(dataReader, index++);
                    registro.DECLARACIONPREV = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ENTIDAD_DENUNCIAPREV = dbDefaults.getInt32(dataReader, index++);
                    registro.FECHA_DENUNCIAPREV = dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_PAIS_DENUNCIAPREV = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO_DENUNCIAPREV = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_DENUNCIAPREV = dbDefaults.getInt64(dataReader, index++);
                    registro.NUMERO_RADICADO_DENUNCIAPREV = dbDefaults.getString(dataReader, index++);
                    #endregion

                    #region Detalles Secuestro
                    registro.PARAM_TIPO_SECUESTRO = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_FINALIDAD_SECUESTRO = dbDefaults.getInt16(dataReader, index++);
                    registro.OTRA_FINALIDAD_SECUESTRO = dbDefaults.getString(dataReader, index++);
                    registro.CONTRAPRESTACION = dbDefaults.getInt16(dataReader, index++);
                    registro.QUE_CONTRAPRESTACION = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_SITUACIONACTUALVICTIMA = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_LIBERACION_VICTIMA = dbDefaults.getInt16(dataReader, index++);
                    registro.FECHA_LIBERACION = dbDefaults.getDateTime(dataReader, index++);
                    #endregion
                  
                    registro.ACTIVO = dbDefaults.getInt16(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }

        #endregion
    }
}
