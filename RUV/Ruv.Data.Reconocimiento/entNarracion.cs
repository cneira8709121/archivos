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
    public class entNarracion : entidadRUV
    {
        #region Guardar Datos
        public void setData(TBNARRACIONES objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setNarracion", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        public void updateData(TBNARRACIONES objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updNarracion", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBNARRACIONES objData)
        {
            return new object[]{     
                                  objData.ID_DECLARACION
                                , objData.NARRACION
            };
        }
        #endregion
        
        #region Obtener Datos
        /// <summary>
        /// Obtener la narracion o descripcion de los hechos.
        /// </summary>
        /// <param name="ID">ID de la declaración</param>
        /// <returns></returns>
        public TBNARRACIONES getData(int ID)
        {
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getNarracion", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBNARRACIONES registro = EnterpriseLibraryContainer.Current.GetInstance<TBNARRACIONES>();
                    int index = 0;
                    registro.ID_DECLARACION = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.NARRACION = dbDefaults.getString(dataReader, index++);

                    return registro;
                }
            }
            return null;
        }
        #endregion
    }
}
