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
    public class entAnexo3 : entidadRUV
    {
        #region Guardar Datos
        public void setAnexo3(TBANEXO3 objeAnexo3, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo3", getParametros(objeAnexo3));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo3.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo3(TBANEXO3 objeAnexo3, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo3", getParametros(objeAnexo3));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO3 objeAnexo)
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
                                , objeAnexo.PARAM_DELITO_SEXUAL
                                , objeAnexo.SOLICITUD_AYUDA
                                , objeAnexo.DETALLE_SOLICITUD_AYUDA
                                , objeAnexo.AYUDA
                                , objeAnexo.DETALLE_AYUDA
                                , objeAnexo.ATENCION_MEDICA
                                , objeAnexo.ID_DTO_ATENCION_MEDICA
                                , objeAnexo.ID_MUN_ATENCION_MEDICA
                                , objeAnexo.ENTIDAD_ATENCION_MEDICA
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
        public List<TBANEXO3> getData(int ID)
        {
            List<TBANEXO3> registros = new List<TBANEXO3>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos3", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO3 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO3>();
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

                    #region Delito sexual
                    registro.PARAM_DELITO_SEXUAL            = dbDefaults.getInt16(dataReader, index++);
                    #endregion

                    #region Atencion Medica
                    registro.SOLICITUD_AYUDA                = dbDefaults.getInt16(dataReader, index++);
                    registro.DETALLE_SOLICITUD_AYUDA        = dbDefaults.getString(dataReader, index++);
                    registro.AYUDA                          = dbDefaults.getInt16(dataReader, index++);
                    registro.DETALLE_AYUDA                  = dbDefaults.getString(dataReader, index++);

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
