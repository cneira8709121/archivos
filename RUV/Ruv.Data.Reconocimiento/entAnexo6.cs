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
    public class entAnexo6 : entidadRUV
    {
        #region Set & Update
        public void setAnexo6(TBANEXO6 objeAnexo6, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo6", getParametros(objeAnexo6));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo6.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo6(TBANEXO6 objeAnexo6, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo6", getParametros(objeAnexo6));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO6 objeAnexo)
        {
            return new object[]{   
                                   objeAnexo.ID                          
                                 , objeAnexo.TBSINIESTROS_PERSONA.ID                          
                                 , objeAnexo.TBREGISTROS_PERSONAS.ID               
                                 , objeAnexo.VICTIMA                     
                                 , objeAnexo.FALLECIDA                   
                                 , objeAnexo.AFECTADO                    
                                 , objeAnexo.OTRA_AFECTACION             
                                 , objeAnexo.DECLARACIONPREV             
                                 , objeAnexo.PARAM_ENTIDAD_DENUNCIAPREV  
                                 , objeAnexo.FECHA_DENUNCIAPREV          
                                 , objeAnexo.ID_PAIS_DENUNCIAPREV
                                 , objeAnexo.ID_DEPARTAMENTO_DENUNCIAPREV
                                 , objeAnexo.ID_MUNICIPIO_DENUNCIAPREV   
                                 , objeAnexo.NUMERO_RADICADO_DENUNCIAPREV
                                 , objeAnexo.QUEDO_ALGUN_HUERFANO        
                                 , objeAnexo.ID_HUERFANO             
                                 , objeAnexo.PARAM_HUERFANO_DE           
                                 , objeAnexo.NUM_PERSONAS_MUERTAS        
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
        public List<TBANEXO6> getData(int ID)
        {
            List<TBANEXO6> registros = new List<TBANEXO6>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos6", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO6 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO6>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
                    registro.TBREGISTROS_PERSONAS  = new TBREGISTROS_PERSONAS();

                    int index = 0;

                    #region Common Anexos
                    registro.ID                             = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID        = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID 		= (int)dbDefaults.getInt32(dataReader, index++);
                    registro.VICTIMA                        = dbDefaults.getInt16(dataReader, index++);
                    // FALLECIDA ese solo para Anexo06
                    registro.FALLECIDA                      = dbDefaults.getInt16(dataReader, index++);
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

                    #region Detalles Homicido - Masacre
                    registro.QUEDO_ALGUN_HUERFANO           = dbDefaults.getInt16(dataReader, index++);
                    registro.ID_HUERFANO                    = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_HUERFANO_DE              = dbDefaults.getInt16(dataReader, index++);
                    registro.NUM_PERSONAS_MUERTAS           = dbDefaults.getInt16(dataReader, index++);
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
