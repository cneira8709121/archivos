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
    public class entAnexo4 : entidadRUV
    {
        #region Set y Update

        public void setAnexo4(TBANEXO4 objeAnexo4, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo4", getParametros(objeAnexo4));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo4.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo4(TBANEXO4 objeAnexo4, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo4", getParametros(objeAnexo4));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO4 objeAnexo)
        {
            return new object[]{   
                                   objeAnexo.ID                          
                                  ,objeAnexo.TBSINIESTROS_PERSONA.ID                
                                  ,objeAnexo.ID_REGPERSONA               
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
                                  ,objeAnexo.DESAPARECIDA                
                                  ,objeAnexo.PARAM_EVENTO_ANTES_HECHO    
                                  ,objeAnexo.PARAM_EVENTO_DESPUES_HECHO  
                                  ,objeAnexo.ACTIVIDAD_EN_DESAPARICION   
                                  ,objeAnexo.MENOR_DESPROTEGIDO          
                                  ,objeAnexo.ID_MENOR_DESPROTEGIDO// Menor Desprotegido       
                                  ,objeAnexo.BUSQUEDA_VICTIMA            
                                  ,objeAnexo.ENTIDAD_BUSQUEDA            
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
        public List<TBANEXO4> getData(int ID)
        {
            List<TBANEXO4> registros = new List<TBANEXO4>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos4", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO4 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO4>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();

                    int index = 0;

                    #region Common Anexos
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.ID_REGPERSONA = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.VICTIMA = dbDefaults.getInt16(dataReader, index++);
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

                    #region Desaparición
                    registro.DESAPARECIDA = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_EVENTO_ANTES_HECHO = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_EVENTO_DESPUES_HECHO = dbDefaults.getInt32(dataReader, index++);
                    registro.ACTIVIDAD_EN_DESAPARICION = dbDefaults.getString(dataReader, index++);
                    registro.MENOR_DESPROTEGIDO = dbDefaults.getInt16(dataReader, index++);
                    registro.ID_MENOR_DESPROTEGIDO = dbDefaults.getInt32(dataReader, index++); // Menor Desprotegido
                    registro.BUSQUEDA_VICTIMA = dbDefaults.getInt16(dataReader, index++);
                    registro.ENTIDAD_BUSQUEDA = dbDefaults.getString(dataReader, index++);
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
