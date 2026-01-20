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
    public class entAnexo5 : entidadRUV
    {
        #region Set & Update
        public void setAnexo5(TBANEXO5 objeAnexo5, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo5", getParametros(objeAnexo5));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo5.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo5(TBANEXO5 objeAnexo5, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo5", getParametros(objeAnexo5));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO5 objeAnexo)
        {
            return new object[]{   
                                  objeAnexo.ID             
                                , objeAnexo.TBSINIESTROS_PERSONA.ID        
                                , objeAnexo.DECLARACIONPREV                
                                , objeAnexo.PARAM_ENTIDAD_DENUNCIAPREV     
                                , objeAnexo.FECHA_DENUNCIAPREV             
                                , objeAnexo.ID_PAIS_DENUNCIAPREV   
                                , objeAnexo.ID_DEPARTAMENTO_DENUNCIAPREV   
                                , objeAnexo.ID_MUNICIPIO_DENUNCIAPREV      
                                , objeAnexo.NUMERO_RADICADO_DENUNCIAPREV   
                                , objeAnexo.OTRA_ENTIDAD_DENUNCIAPREV      
                                , objeAnexo.PARAM_TIPO_DESPLAZAMIENTO      
                                , objeAnexo.TIEMPO_RESIDENCIA_ANOS         
                                , objeAnexo.TIEMPO_RESIDENCIA_MESES        
                                , objeAnexo.TIEMPO_RESIDENCIA_DIAS         
                                , objeAnexo.FECHA_ARRIBO                   
                                , objeAnexo.ID_PAIS_ARRIBO
                                , objeAnexo.ID_DEPARTAMENTO_ARRIBO         
                                , objeAnexo.ID_MUNICIPIO_ARRIBO            
                                , objeAnexo.ID_TIPOPOBLACION_ARRIBO        
                                , objeAnexo.ID_ENTORNO_ARRIBO              
                                , objeAnexo.OTRO_ENTORNO_ARRIBO            
                                , objeAnexo.PARAM_DESEOHOGAR               
                                , objeAnexo.ID_PAIS_REUBICACION            
                                , objeAnexo.ID_DPTO_REUBICACION            
                                , objeAnexo.ID_MUNICIPIO_REUBICACION       
                                , objeAnexo.ID_TIPOPOBLACION_REUBICACION   
                                , objeAnexo.ID_ENTORNO_REUBICACION         
                                , objeAnexo.OTRO_ENTORNO_REUBICACION       
                                , objeAnexo.ACTIVO                         
                                , objeAnexo.DESPLAZAMIENTO_OTRO            
                                , objeAnexo.PARAM_LOCALIDAD_CORREG_ARRI    
                                , objeAnexo.PARAM_BARRIO_VEREDA_ARRI       
                                , objeAnexo.OTRO_LOCALIDAD_CORREG_ARRI     
                                , objeAnexo.OTRO_BARRIO_VEREDA_ARRI        
                                , objeAnexo.PARAM_TIPO_ENTORNO_ARRI        
                                , objeAnexo.PARAM_LOCALIDAD_CORREG_REUB    
                                , objeAnexo.PARAM_BARRIO_VEREDA_REUB       
                                , objeAnexo.OTRO_LOCALIDAD_CORREG_REUB     
                                , objeAnexo.OTRO_BARRIO_VEREDA_REUB        
                                , objeAnexo.PARAM_TIPO_ENTORNO_REUB     
                                , objeAnexo.PARAM_CAUSA_DESPLAZAMIENTO
                                , objeAnexo.PARAM_NUEVO_TIPO_DESPLAZAMIENTO
                                , objeAnexo.ESEXILIO
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
        public List<TBANEXO5> getData(int ID)
        {
            List<TBANEXO5> registros = new List<TBANEXO5>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos5", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO5 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO5>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
                    
                    int index = 0;
                     
                    registro.ID                                 = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID            = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.DECLARACIONPREV                    = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ENTIDAD_DENUNCIAPREV         = dbDefaults.getInt32(dataReader, index++);
                    registro.FECHA_DENUNCIAPREV                 = dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_PAIS_DENUNCIAPREV               = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO_DENUNCIAPREV       = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_DENUNCIAPREV          = dbDefaults.getInt64(dataReader, index++);
                    registro.NUMERO_RADICADO_DENUNCIAPREV       = dbDefaults.getString(dataReader, index++);
                    registro.OTRA_ENTIDAD_DENUNCIAPREV          = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_TIPO_DESPLAZAMIENTO          = dbDefaults.getInt32(dataReader, index++);
                    registro.TIEMPO_RESIDENCIA_ANOS             = dbDefaults.getInt16(dataReader, index++);
                    registro.TIEMPO_RESIDENCIA_MESES            = dbDefaults.getInt16(dataReader, index++);
                    registro.TIEMPO_RESIDENCIA_DIAS             = dbDefaults.getInt16(dataReader, index++);
                    registro.FECHA_ARRIBO                       = dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_PAIS_ARRIBO                     = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO_ARRIBO             = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_ARRIBO                = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_TIPOPOBLACION_ARRIBO            = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_ENTORNO_ARRIBO                  = dbDefaults.getInt32(dataReader, index++);
                    registro.OTRO_ENTORNO_ARRIBO                = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_DESEOHOGAR                   = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_PAIS_REUBICACION                = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DPTO_REUBICACION                = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_REUBICACION           = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_TIPOPOBLACION_REUBICACION       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_ENTORNO_REUBICACION             = dbDefaults.getInt32(dataReader, index++);
                    registro.OTRO_ENTORNO_REUBICACION           = dbDefaults.getString(dataReader, index++);
                    registro.ACTIVO                             = dbDefaults.getInt16(dataReader, index++);
                    registro.DESPLAZAMIENTO_OTRO                = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_LOCALIDAD_CORREG_ARRI        = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_BARRIO_VEREDA_ARRI           = dbDefaults.getInt32(dataReader, index++);
                    registro.OTRO_LOCALIDAD_CORREG_ARRI         = dbDefaults.getString(dataReader, index++);
                    registro.OTRO_BARRIO_VEREDA_ARRI            = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_TIPO_ENTORNO_ARRI            = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_LOCALIDAD_CORREG_REUB        = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_BARRIO_VEREDA_REUB           = dbDefaults.getInt32(dataReader, index++);
                    registro.OTRO_LOCALIDAD_CORREG_REUB         = dbDefaults.getString(dataReader, index++);
                    registro.OTRO_BARRIO_VEREDA_REUB            = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_TIPO_ENTORNO_REUB            = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_CAUSA_DESPLAZAMIENTO         = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_NUEVO_TIPO_DESPLAZAMIENTO    = dbDefaults.getInt32(dataReader, index++);
                    registro.ESEXILIO                           = dbDefaults.getInt32(dataReader, index++);
                    registros.Add(registro);
                }
            }
            return registros;
        }

        #endregion
    }
}
