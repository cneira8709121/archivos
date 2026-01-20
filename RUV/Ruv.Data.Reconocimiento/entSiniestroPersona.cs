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
    public class entSiniestroPersona : entidadRUV
    {
        #region Guardar Datos
        public void setData(TBSINIESTROS_PERSONA objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setSiniestrosPersona", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
            int? id = objData.ID;
            objData.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updateData(TBSINIESTROS_PERSONA objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updSiniestrosPersona", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        public int insDataValoracionAnexo(int idValoracion, int idSiniestro, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_insValoracionAnexo", idValoracion, idSiniestro, null);
            dbRUV.ExecuteNonQuery(cmd, tran);
            return Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_IDCREADO"));
        }

        public void insDataValoracionAnexoPersona(int idValanexo, int idRegPersona, int idAnexo, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_insValoracionAnexoPersona", idValanexo, idRegPersona, idAnexo);
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
        
        private object[] getParametros(TBSINIESTROS_PERSONA objData)
        {
            return new object[]{    
                                objData.ID	
                              , objData.PARAM_TIPOHECHO	
                              , objData.TBREGISTROS_PERSONAS.ID
                              , objData.FECHASINIESTRO	
                              , objData.ID_DEPARTAMENTO	
                              , objData.ID_MUNICIPIO	
                              , objData.ID_ENTORNO	
                              , objData.ID_TIPOPOBLADO	
                              , objData.OTRO_ENTORNO	
                              , objData.PARAM_CCVOTAR	
                              , objData.ID_DEPARTAMENTO_VOTAR	
                              , objData.ID_MUNICIPIO_VOTAR	
                              , objData.ID_DPTO_ESTUDIO_HIJOS	
                              , objData.ID_MPIO_ESTUDIO_HIJOS	
                              , objData.INSTITUCION_EDUCATIVA	
                              , objData.PARAM_ENCUESTASISBEN	
                              , objData.ID_DPTO_ENCUESTASISBEN	
                              , objData.ID_MPIO_ENCUESTASISBEN	
                              , objData.NIVEL_SISBEN	
                              , objData.PARAM_FAMILIASACCION	
                              , objData.ID_DPTO_FAMILIASACCION	
                              , objData.ID_MPIO_FAMILIASACCION	
                              , objData.PARAM_ENTIDADCOBRA	
                              , objData.PARAM_SISTEMASALUD	
                              , objData.ID_DPTO_SISTEMASALUD	
                              , objData.ID_MPIO_SISTEMASALUD	
                              , objData.PARAM_TIPOAFILIZACION	
                              , objData.ID_DPTO_TRABAJO	
                              , objData.ID_MPIO_TRABAJO	
                              , objData.NOMBRE_EMPLEADOR	
                              , objData.VICTIMA_DEL_HECHO	
                              , objData.ACTIVO	
                              , objData.PARAM_LOCALIDAD_CORREG	
                              , objData.PARAM_BARRIO_VEREDA	
                              , objData.OTRO_LOCALIDAD_CORREG	
                              , objData.OTRO_BARRIO_VEREDA	
                              , objData.PARAM_TIPO_ENTORNO	
                              , objData.ENTIDADCOBRA
                              //, objData.TBCENSOMASIVORELACION.ID_SINIESTRO_RELACIONADO
                              , null
            };
        }

        #endregion


        #region Obtener Datos
        public List<TBSINIESTROS_PERSONA> getData(int ID_TipoHecho, int ID_DECLARACION)
        {
            List<TBSINIESTROS_PERSONA> registros = new List<TBSINIESTROS_PERSONA>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getSiniestroPersona", new object[] { ID_TipoHecho, ID_DECLARACION, null }))
            {
                                
                while (dataReader.Read())
                {
                    TBSINIESTROS_PERSONA registro = EnterpriseLibraryContainer.Current.GetInstance<TBSINIESTROS_PERSONA>();

                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();                    
                    int index = 0;
                                                         
                    registro.ID	                           = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_TIPOHECHO	           = dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID       = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.FECHASINIESTRO	               = dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_DEPARTAMENTO	           = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO	               = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_ENTORNO	                   = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_TIPOPOBLADO	               = dbDefaults.getInt32(dataReader, index++);
                    registro.OTRO_ENTORNO	               = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_CCVOTAR	               = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_DEPARTAMENTO_VOTAR	       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MUNICIPIO_VOTAR	           = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_DPTO_ESTUDIO_HIJOS	       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MPIO_ESTUDIO_HIJOS	       = dbDefaults.getInt32(dataReader, index++);
                    registro.INSTITUCION_EDUCATIVA	       = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_ENCUESTASISBEN	       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_DPTO_ENCUESTASISBEN	       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MPIO_ENCUESTASISBEN	       = dbDefaults.getInt32(dataReader, index++);
                    registro.NIVEL_SISBEN	               = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_FAMILIASACCION	       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_DPTO_FAMILIASACCION	       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MPIO_FAMILIASACCION	       = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_ENTIDADCOBRA	           = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_SISTEMASALUD	           = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_DPTO_SISTEMASALUD	       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MPIO_SISTEMASALUD	       = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_TIPOAFILIZACION	       = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_DPTO_TRABAJO	           = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MPIO_TRABAJO	           = dbDefaults.getInt32(dataReader, index++);
                    registro.NOMBRE_EMPLEADOR	           = dbDefaults.getString(dataReader, index++);
                    registro.VICTIMA_DEL_HECHO	           = dbDefaults.getInt16(dataReader, index++); 
                    registro.ACTIVO	                       = dbDefaults.getInt16(dataReader, index++); 
                    registro.PARAM_LOCALIDAD_CORREG	       = dbDefaults.getInt32(dataReader, index++); 
                    registro.PARAM_BARRIO_VEREDA	       = dbDefaults.getInt32(dataReader, index++); 
                    registro.OTRO_LOCALIDAD_CORREG	       = dbDefaults.getString(dataReader, index++); 
                    registro.OTRO_BARRIO_VEREDA	           = dbDefaults.getString(dataReader, index++); 
                    registro.PARAM_TIPO_ENTORNO	           = dbDefaults.getInt32(dataReader, index++); 
                    registro.ENTIDADCOBRA                  = dbDefaults.getString(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.CONSECUTIVO_FAMILIA = (short)dbDefaults.getInt32(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }

        public int GetDataValoracionAnexo(int idValoracion, int idSiniestro, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_getValoracionAnexo", idValoracion, idSiniestro, null);
            dbRUV.ExecuteNonQuery(cmd, tran);
            return Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_IDVALANEXO"));
        }
        #endregion

    }
}
