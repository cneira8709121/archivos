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
    public class entDelitosSexuales : entidadRUV
    {
        #region Guardar Datos
        public void setDelitoSexual(TBDELITO_SEXUAL_A3 objDelito, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo3_DelitoSexual", getParametros(objDelito));
            dbRUV.ExecuteNonQuery(cmd, tran);
            objDelito.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updDelitoSexual(TBDELITO_SEXUAL_A3 objDelito, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo3_DelitoSexual", getParametros(objDelito));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBDELITO_SEXUAL_A3 objDelito)
        {
            return new object[]{   
                                   objDelito.ID                    
                                  ,objDelito.TBANEXO3.ID                            
                                  ,objDelito.PARAM_DELITOSEXUAL              
                                  ,objDelito.ACTIVO                
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
        public List<TBDELITO_SEXUAL_A3> getData(int ID)
        {
            List<TBDELITO_SEXUAL_A3> registros = new List<TBDELITO_SEXUAL_A3>();
            
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getDelitoSexualA3", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBDELITO_SEXUAL_A3 registro = EnterpriseLibraryContainer.Current.GetInstance<TBDELITO_SEXUAL_A3>();
                    registro.TBANEXO3 = new TBANEXO3();
                    int index = 0;
                    
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBANEXO3.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_DELITOSEXUAL = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.ACTIVO = dbDefaults.getInt16(dataReader, index++);                    
                    registros.Add(registro);
                }
            }
            return registros;
        }
        #endregion


    }
}
