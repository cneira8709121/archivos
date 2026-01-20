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
    public class entAnexo9 : entidadRUV
    {
        #region Set y Update
        public void setAnexo9(TBANEXO9 objeAnexo9, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo9", getParametros(objeAnexo9));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo9.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo9(TBANEXO9 objeAnexo9, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo9", getParametros(objeAnexo9));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO9 objeAnexo)
        {
            return new object[]{
                                objeAnexo.ID                             
                              , objeAnexo.TBSINIESTROS_PERSONA.ID
                              , objeAnexo.TBREGISTROS_PERSONAS.ID                
                              , objeAnexo.VICTIMA                        
                              , objeAnexo.AFECTADO                       
                              , objeAnexo.OTRA_AFECTACION                
                              , objeAnexo.DECLARACIONPREV                
                              , objeAnexo.PARAM_ENTIDAD_DENUNCIAPREV     
                              , objeAnexo.FECHA_DENUNCIAPREV             
                              , objeAnexo.ID_PAIS_DENUNCIAPREV   
                              , objeAnexo.ID_DEPARTAMENTO_DENUNCIAPREV   
                              , objeAnexo.ID_MUNICIPIO_DENUNCIAPREV      
                              , objeAnexo.NUMERO_RADICADO_DENUNCIAPREV   
                              , objeAnexo.RECIBIO_ATENCION_MEDICA        
                              , objeAnexo.SOLICITO_AYUDA                 
                              , objeAnexo.ENTIDAD_SOLICITO_AYUDA         
                              , objeAnexo.RECIBIO_AYUDA                  
                              , objeAnexo.TIPO_AYUDA_RECIBIDA            
                              , objeAnexo.ID_DTO_ATENCION_MEDICA         
                              , objeAnexo.ID_MUN_ATENCION_MEDICA         
                              , objeAnexo.ENTIDAD_ATENCION_MEDICA        
                              , objeAnexo.ACTIVO    
                              , null
            };
        }
        #endregion

        #region Obtener
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Siniestro.</param>
        /// <returns></returns>
        public List<TBANEXO9> getData(int ID)
        {
            List<TBANEXO9> registros = new List<TBANEXO9>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos9", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO9 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO9>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

                    int index = 0;

                    #region Common Anexos
                    registro.ID 							= (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID 		= (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID        = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.VICTIMA 						= dbDefaults.getInt16(dataReader, index++);
                    registro.AFECTADO 						= dbDefaults.getInt16(dataReader, index++);
                    registro.OTRA_AFECTACION 				= dbDefaults.getString(dataReader, index++);
                    registro.DECLARACIONPREV 				= dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ENTIDAD_DENUNCIAPREV 	= dbDefaults.getInt32(dataReader, index++);
                    registro.FECHA_DENUNCIAPREV 			= dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_PAIS_DENUNCIAPREV           = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO_DENUNCIAPREV 	= dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_DENUNCIAPREV 		= dbDefaults.getInt64(dataReader, index++);
                    registro.NUMERO_RADICADO_DENUNCIAPREV	= dbDefaults.getString(dataReader, index++);
                    #endregion

                    #region Atención Médica y Ayuda
                    registro.RECIBIO_ATENCION_MEDICA		= dbDefaults.getInt16(dataReader, index++);
                    registro.SOLICITO_AYUDA 				= dbDefaults.getInt16(dataReader, index++);
                    registro.ENTIDAD_SOLICITO_AYUDA 		= dbDefaults.getString(dataReader, index++);
                    registro.RECIBIO_AYUDA 					= dbDefaults.getInt16(dataReader, index++);
                    registro.TIPO_AYUDA_RECIBIDA 			= dbDefaults.getString(dataReader, index++);

                    registro.ID_DTO_ATENCION_MEDICA         = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MUN_ATENCION_MEDICA         = dbDefaults.getInt32(dataReader, index++);
                    registro.ENTIDAD_ATENCION_MEDICA        = dbDefaults.getString(dataReader, index++);
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
