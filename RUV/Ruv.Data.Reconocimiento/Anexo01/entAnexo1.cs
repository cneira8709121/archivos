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
    public class entAnexo1 : entidadRUV
    {
        #region Guardar Datos
        public void setAnexo1(TBANEXO1 objeAnexo1, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo1", getParametros(objeAnexo1));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo1.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo1(TBANEXO1 objeAnexo1, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo1", getParametros(objeAnexo1));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO1 objeAnexo)
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
                                  , objeAnexo.ATENCION_MEDICA              
                                  , objeAnexo.DETALLE_ATENCION_MEDICA      
                                  , objeAnexo.ID_DEPARTAMENTO_ATENCIONMED
                                  , objeAnexo.ID_MUNICIPIO_ATENCIONMED
                                  , objeAnexo.ACTIVO
                                  , null
            };
        }
        #endregion

        #region Obtener Datos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Siniestro.</param>
        /// <returns></returns>
        public List<TBANEXO1> getData(int ID)
        {
            List<TBANEXO1> registros = new List<TBANEXO1>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos1", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO1 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO1>();
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

                    #region Atención Médica
                    registro.ATENCION_MEDICA = dbDefaults.getInt16(dataReader, index++);
                    registro.DETALLE_ATENCION_MEDICA = dbDefaults.getString(dataReader, index++);
                    registro.ID_DEPARTAMENTO_ATENCIONMED = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MUNICIPIO_ATENCIONMED = dbDefaults.getInt32(dataReader, index++);
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