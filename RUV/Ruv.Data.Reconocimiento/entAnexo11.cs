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
    public class entAnexo11 : entidadRUV
    {
        #region Actualizar
        public void setAnexo11(TBANEXO11 objeAnexo11, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo11", getParametros(objeAnexo11));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo11.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo11(TBANEXO11 objeAnexo11, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo11", getParametros(objeAnexo11));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO11 objeAnexo)
        {
            return new object[]{   
                                   objeAnexo.ID                          
                                  ,objeAnexo.TBSINIESTROS_PERSONA.ID
                                  ,objeAnexo.DECLARACIONPREV             
                                  ,objeAnexo.PARAM_ENTIDAD_DENUNCIAPREV  
                                  ,objeAnexo.FECHA_DENUNCIAPREV          
                                  ,objeAnexo.ID_PAIS_DENUNCIAPREV
                                  ,objeAnexo.ID_DEPARTAMENTO_DENUNCIAPREV
                                  ,objeAnexo.ID_MUNICIPIO_DENUNCIAPREV   
                                  ,objeAnexo.NUMERO_RADICADO_DENUNCIAPREV
                                  ,objeAnexo.PARAM_TIERRA_DESPOJADA
                                  ,objeAnexo.PARAM_TIPO_DESPOJADO
                                  ,objeAnexo.AUTOR_DESPOJADO
                                  ,objeAnexo.PARAM_SITUACION_ACT_TIERRA
                                  ,objeAnexo.PARAM_SOL_PROTECCION
                                  ,objeAnexo.PROTECCION_PORQUE
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
        public List<TBANEXO11> getData(int ID)
        {
            List<TBANEXO11> registros = new List<TBANEXO11>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos11", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO11 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO11>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();

                    int index = 0;

                    registro.ID                               = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID          = (int)dbDefaults.getInt32(dataReader, index++);                    
                    registro.DECLARACIONPREV                  = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ENTIDAD_DENUNCIAPREV       = dbDefaults.getInt32(dataReader, index++);
                    registro.FECHA_DENUNCIAPREV               = dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_PAIS_DENUNCIAPREV             = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO_DENUNCIAPREV     = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_DENUNCIAPREV        = dbDefaults.getInt64(dataReader, index++);
                    registro.NUMERO_RADICADO_DENUNCIAPREV     = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_TIERRA_DESPOJADA           = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_TIPO_DESPOJADO             = dbDefaults.getInt32(dataReader, index++);
                    registro.AUTOR_DESPOJADO                  = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_SITUACION_ACT_TIERRA       = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_SOL_PROTECCION             = dbDefaults.getInt32(dataReader, index++);
                    registro.PROTECCION_PORQUE                = dbDefaults.getString(dataReader, index++);
                    registro.ACTIVO                           = dbDefaults.getInt16(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }

        #endregion
    }
}
