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
    public class entLugarAccidente : entidadRUV
    {
        #region Guardar Datos
        public void setLugarAccidente(TBANEXO7_LUGARACCIDENTE objLugar, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo7_LugarAcc", getParametros(objLugar));
            dbRUV.ExecuteNonQuery(cmd, tran);
            objLugar.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updLugarAccidente(TBANEXO7_LUGARACCIDENTE objLugar, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo7_LugarAcc", getParametros(objLugar));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO7_LUGARACCIDENTE objLugar)
        {
            return new object[]{   
                                   objLugar.ID                    
                                  ,objLugar.TBSINIESTROS_PERSONA.ID
                                  ,objLugar.DESCRIPCION
                                  ,null
            };
        }

        /// <summary>
        /// Actualiza ACTIVO = 0 a todos los delitos sexuales del Anexo03
        /// </summary>
        /// <param name="id_anexo">ID Anexo03</param>
        public void delDelitoSexual(int id_anexo, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_delAnexo3_DelitoSexual", id_anexo);
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        #endregion

        #region Obtener Datos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Siniestro.</param>
        /// <returns></returns>
        public TBANEXO7_LUGARACCIDENTE getData(int ID)
        {
            TBANEXO7_LUGARACCIDENTE registro = new TBANEXO7_LUGARACCIDENTE();            
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getLugarAccidenteA7", new object[] { ID, null }))
            {
                
                if (dataReader.Read())
                {
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA(); 

                    int index = 0;                    
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.DESCRIPCION = dbDefaults.getString(dataReader, index++);                                  
                }
            }
            return registro;
        }
        #endregion


    }
}
