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
    public class entAnexo2 : entidadRUV
    {
        #region Guardar Datos
        public void setAnexo2(TBANEXO2 objeAnexo2, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo2", getParametros(objeAnexo2));
            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo2.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo2(TBANEXO2 objeAnexo2, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo2", getParametros(objeAnexo2));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO2 objeAnexo)
        {
            return new object[]{   
                                   objeAnexo.ID                          
                                  ,objeAnexo.TBSINIESTROS_PERSONA.ID                
                                  ,objeAnexo.TBREGISTROS_PERSONAS.ID               
                                  ,objeAnexo.VICTIMA                     
                                  ,objeAnexo.AFECTADO                    
                                  ,objeAnexo.OTRA_AFECTACION             
                                  ,objeAnexo.DECLARACIONPREV             
                                  ,objeAnexo.PARAM_ENTIDAD_DENUNCIAPREV  
                                  ,objeAnexo.FECHA_DENUNCIAPREV          
                                  ,objeAnexo.ID_PAIS_DENUNCIAPREV
                                  ,objeAnexo.ID_DEPARTAMENTO_DENUNCIAPREV
                                  ,objeAnexo.ID_MUNICIPIO_DENUNCIAPREV   
                                  ,objeAnexo.NUMERO_RADICADO_DENUNCIAPREV
                                  ,objeAnexo.SOLICITA_PROTECCION         
                                  ,objeAnexo.PROTECCION                  
                                  ,objeAnexo.TIPO_PROTECCION             
                                  ,objeAnexo.ENTIDAD_PROTECCION          
                                  ,objeAnexo.FECHA_PROTECCION            
                                  ,objeAnexo.VIGENCIA_PROTECCION         
                                  ,objeAnexo.CONTINUA_AMENAZAS           
                                  ,objeAnexo.ACTIVO                      
                                  ,null // Out
            };
        }
        #endregion

        #region Obtener Datos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Siniestro.</param>
        /// <returns></returns>
        public List<TBANEXO2> getData(int ID)
        {
            List<TBANEXO2> registros = new List<TBANEXO2>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos2", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO2 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO2>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

                    int index = 0;

                    #region Common Anexos
                    registro.ID                             = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID        = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID        = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.VICTIMA                        = dbDefaults.getInt16(dataReader, index++);
                    registro.AFECTADO                       = dbDefaults.getInt16(dataReader, index++);
                    registro.OTRA_AFECTACION                = dbDefaults.getString(dataReader, index++);
                    registro.DECLARACIONPREV                = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ENTIDAD_DENUNCIAPREV     = dbDefaults.getInt32(dataReader, index++);
                    registro.FECHA_DENUNCIAPREV             = dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_PAIS_DENUNCIAPREV           = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO_DENUNCIAPREV   = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_DENUNCIAPREV      = dbDefaults.getInt64(dataReader, index++);
                    registro.NUMERO_RADICADO_DENUNCIAPREV   = dbDefaults.getString(dataReader, index++);
                    #endregion
                    
                    #region Medidas de protección
                    registro.SOLICITA_PROTECCION            = dbDefaults.getInt16(dataReader, index++);
                    registro.PROTECCION                     = dbDefaults.getInt16(dataReader, index++);
                    registro.TIPO_PROTECCION                = dbDefaults.getString(dataReader, index++);
                    registro.ENTIDAD_PROTECCION             = dbDefaults.getString(dataReader, index++);
                    registro.FECHA_PROTECCION               = dbDefaults.getDateTime(dataReader, index++);
                    registro.VIGENCIA_PROTECCION            = dbDefaults.getString(dataReader, index++);
                    registro.CONTINUA_AMENAZAS              = dbDefaults.getInt16(dataReader, index++);
                    #endregion

                    registro.ACTIVO                         = dbDefaults.getInt16(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }
        #endregion
    }
}
