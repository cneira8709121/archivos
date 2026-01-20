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
    public class entNacidoViolacion : entidadRUV
    {
        #region Guardar Datos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objNacido"></param>
        public void setNacidoViolacion(TBNACIDO_VIOLACION_A3 objNacido, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo3_NacidoViolacion", getParametros(objNacido));
            dbRUV.ExecuteNonQuery(cmd, tran);
            objNacido.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updNacidoViolacion(TBNACIDO_VIOLACION_A3 objNacido, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo3_NacidoViolacion", getParametros(objNacido));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBNACIDO_VIOLACION_A3 objNacido)
        {
            return new object[]{  
 
                                   objNacido.ID                   
                                  ,objNacido.TBSINIESTROS_PERSONA.ID                    
                                  ,objNacido.TBREGISTROS_PERSONAS.ID            
                                  ,objNacido.ACTIVO             
                                  ,null
            };
        }
                
        /// <summary>
        /// Actualiza el estado ACTIVO = 0 para todos los nacidos por violacion del anexo03.
        /// </summary>
        /// <param name="id_anexo">ID anexo03</param>
        public void deleteData(int id_anexo, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_delAnexo3_NacidoViolacion", id_anexo);
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
        #endregion

        #region Obtener Datos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Siniestro.</param>
        /// <returns></returns>
        public List<TBNACIDO_VIOLACION_A3> getData(int ID)
        {
            List<TBNACIDO_VIOLACION_A3> registros = new List<TBNACIDO_VIOLACION_A3>();
            //TODO: revisar el campo activo en el SP
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getNacidosViolacionA3", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBNACIDO_VIOLACION_A3 registro = EnterpriseLibraryContainer.Current.GetInstance<TBNACIDO_VIOLACION_A3>();
                    int index = 0;

                    registro.ID                             = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
                    registro.TBSINIESTROS_PERSONA.ID        = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
                    registro.TBREGISTROS_PERSONAS.ID        = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.ACTIVO                         = dbDefaults.getInt16(dataReader, index++);
                    registros.Add(registro);
                }
            }
            return registros;
        }
        #endregion

    }
}
