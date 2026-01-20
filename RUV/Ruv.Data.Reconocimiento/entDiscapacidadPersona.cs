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
    public class entDiscapacidadPersona : entidadRUV
    {

        #region Guardar Datos
        public void setData(TBDISCAPACIDAD_PERSONA objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setDiscapacidadPersona", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        public void deleteData(int idRegPersona, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_delDiscapacidadPersona", idRegPersona);

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBDISCAPACIDAD_PERSONA objData)
        {
            return new object[]{     
                                  objData.TBREGISTROS_PERSONAS.ID
                                , objData.PARAM_DISCAPACIDAD
            };
        }
        #endregion

        #region Obtener Datos
        public List<int> getData(int ID)
        {
            List<int> registros = new List<int>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getDiscapacidadPersona", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    int registro = (int)dbDefaults.getInt32(dataReader, 1);

                    registros.Add(registro);
                }
            }
            return registros;
        }
        #endregion

    }
}
