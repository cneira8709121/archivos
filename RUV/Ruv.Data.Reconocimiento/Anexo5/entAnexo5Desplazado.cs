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
    public class entAnexo5Desplazado : entidadRUV
    {
        #region Set & Update
        public void setData(TBANEXO5_DESPLAZADOS objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo5_desplazado", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objData.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updData(TBANEXO5_DESPLAZADOS objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo5_desplazado", getParametros(objData));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO5_DESPLAZADOS objData)
        {
            return new object[]{  
                                    objData.ID
                                  , objData.TBANEXO5.ID
                                  , objData.TBREGISTROS_PERSONAS.ID 
                                  , objData.SE_DESPLAZO
                                  , objData.JEFE_HOGAR
                                  , objData.ACTIVO
                                  , null
            };
        }
        #endregion

        #region Obtener
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Anexo5.</param>
        /// <returns>Lista de desplazados del Anexo5</returns>
        public List<TBANEXO5_DESPLAZADOS> getData(int ID)
        {
            List<TBANEXO5_DESPLAZADOS> registros = new List<TBANEXO5_DESPLAZADOS>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexo5_desplazado", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO5_DESPLAZADOS registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO5_DESPLAZADOS>();
                    registro.TBANEXO5 = new TBANEXO5();
                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

                    int index = 0;

                    registro.ID =                       (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBANEXO5.ID =              (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID =  (int)dbDefaults.getInt32(dataReader, index++);
                    registro.SE_DESPLAZO =              dbDefaults.getInt16(dataReader, index++);
                    registro.JEFE_HOGAR =               dbDefaults.getInt16(dataReader, index++);
                    registro.ACTIVO =                   dbDefaults.getInt16(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }
        #endregion
    }
}
