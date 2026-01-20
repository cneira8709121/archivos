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
    public class entAnexo10 : entidadRUV
    {
        #region Set & Update
        public void setAnexo10(TBANEXO10 objeAnexo10, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo10", getParametros(objeAnexo10));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo10.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }
        public void updAnexo10(TBANEXO10 objeAnexo10, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo10", getParametros(objeAnexo10));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
        private object[] getParametros(TBANEXO10 objeAnexo)
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
                                  ,objeAnexo.GRUPO_ARMADO_PERTENECIO     
                                  ,objeAnexo.FECHA_DESVINCULACION        
                                  ,objeAnexo.ATENDIDO_ICBF               
                                  ,objeAnexo.FECHA_ATENCION_ICBF         
                                  ,objeAnexo.ATENDIDO_OTRA_ENTIDAD       
                                  ,objeAnexo.FECHA_ATENCION_OTRA         
                                  ,objeAnexo.ENTIDAD_ATENDIO             
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
        public List<TBANEXO10> getData(int ID)
        {
            List<TBANEXO10> registros = new List<TBANEXO10>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos10", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO10 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO10>();
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
                    registro.PARAM_ENTIDAD_DENUNCIAPREV     = dbDefaults.getInt16(dataReader, index++);
                    registro.FECHA_DENUNCIAPREV             = dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_PAIS_DENUNCIAPREV           = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO_DENUNCIAPREV   = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_DENUNCIAPREV      = dbDefaults.getInt64(dataReader, index++);
                    registro.NUMERO_RADICADO_DENUNCIAPREV   = dbDefaults.getString(dataReader, index++);
                    #endregion                                
                                                              
                    #region Información de Desmovilización    
                    registro.GRUPO_ARMADO_PERTENECIO        = dbDefaults.getString(dataReader, index++);
                    registro.FECHA_DESVINCULACION           = dbDefaults.getDateTime(dataReader, index++);
                    registro.ATENDIDO_ICBF                  = dbDefaults.getInt16(dataReader, index++);
                    registro.FECHA_ATENCION_ICBF            = dbDefaults.getDateTime(dataReader, index++);
                    registro.ATENDIDO_OTRA_ENTIDAD          = dbDefaults.getInt16(dataReader, index++);
                    registro.FECHA_ATENCION_OTRA            = dbDefaults.getDateTime(dataReader, index++);
                    registro.ENTIDAD_ATENDIO                = dbDefaults.getString(dataReader, index++);
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
