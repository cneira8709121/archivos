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
    public class entAnexo7 : entidadRUV
    {
        #region Set y Update
        public void setAnexo7(TBANEXO7 objeAnexo7, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo7", getParametros(objeAnexo7));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo7.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo7(TBANEXO7 objeAnexo7, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo7", getParametros(objeAnexo7));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO7 objeAnexo)
        {
            return new object[]{   
                                  objeAnexo.ID
                                , objeAnexo.TBSINIESTROS_PERSONA.ID
                                , objeAnexo.TBREGISTROS_PERSONAS.ID
                                , objeAnexo.VICTIMA
                                , objeAnexo.PARAM_ESTADOVICTIMA
                                , objeAnexo.AFECTADO
                                , objeAnexo.OTRA_AFECTACION
                                , objeAnexo.DECLARACIONPREV
                                , objeAnexo.PARAM_ENTIDAD_DENUNCIAPREV
                                , objeAnexo.FECHA_DENUNCIAPREV
                                , objeAnexo.ID_PAIS_DENUNCIAPREV
                                , objeAnexo.ID_DEPARTAMENTO_DENUNCIAPREV
                                , objeAnexo.ID_MUNICIPIO_DENUNCIAPREV
                                , objeAnexo.NUMERO_RADICADO_DENUNCIAPREV
                                , objeAnexo.PARAM_TIPO_ACCIDENTE
                                , objeAnexo.PARAM_ACTIVIDAD_MOMENTO_HECHO
                                , objeAnexo.QUEDO_ALGUN_HUERFANO
                                , objeAnexo.ID_HUERFANO
                                , objeAnexo.PARAM_HUERFANO_DE
                                , objeAnexo.ATENCION_MEDICA
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
        public List<TBANEXO7> getData(int ID)
        {
            List<TBANEXO7> registros = new List<TBANEXO7>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos7", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO7 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO7>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
                    registro.TBREGISTROS_PERSONAS  = new TBREGISTROS_PERSONAS();

                    int index = 0;

                  	#region Common Anexos
                    registro.ID 							= (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID 		= (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID        = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.VICTIMA 						= dbDefaults.getInt16(dataReader, index++);
                    //PARAM_ESTADOVICTIMA solo para Anexo07
                    registro.PARAM_ESTADOVICTIMA 			= dbDefaults.getInt16(dataReader, index++);
                    registro.AFECTADO 						= dbDefaults.getInt16(dataReader, index++);
                    registro.OTRA_AFECTACION 				= dbDefaults.getString(dataReader, index++);
                    registro.DECLARACIONPREV 				= dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ENTIDAD_DENUNCIAPREV 	= dbDefaults.getInt32(dataReader, index++);
                    registro.FECHA_DENUNCIAPREV 			= dbDefaults.getDateTime(dataReader, index++);
                    registro.ID_PAIS_DENUNCIAPREV           = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO_DENUNCIAPREV 	= dbDefaults.getInt64(dataReader, index++);
                    registro.ID_MUNICIPIO_DENUNCIAPREV 		= dbDefaults.getInt64(dataReader, index++);
                    registro.NUMERO_RADICADO_DENUNCIAPREV 	= dbDefaults.getString(dataReader, index++);
                    #endregion

                    #region Detalles Anexo07
                    registro.PARAM_TIPO_ACCIDENTE 			= dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ACTIVIDAD_MOMENTO_HECHO	= dbDefaults.getInt16(dataReader, index++);
                    registro.QUEDO_ALGUN_HUERFANO 			= dbDefaults.getInt16(dataReader, index++);
                    registro.ID_HUERFANO 				    = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_HUERFANO_DE 				= dbDefaults.getInt16(dataReader, index++);
                    registro.ATENCION_MEDICA                = dbDefaults.getInt16(dataReader, index++);
                    registro.ID_DTO_ATENCION_MEDICA         = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MUN_ATENCION_MEDICA         = dbDefaults.getInt32(dataReader, index++);
                    registro.ENTIDAD_ATENCION_MEDICA        = dbDefaults.getString(dataReader, index++);
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
