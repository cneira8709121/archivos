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
    public class entDiscapacidadOtroPersona : entidadRUV
    {

        #region Guardar Datos
        public void setData(TBDISCAPACIDADOTRO_PERSONA objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setDiscapacidadOtroPersona", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        public void deleteData(int idRegPersona, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_delDiscapacidadOtroPersona", idRegPersona);

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBDISCAPACIDADOTRO_PERSONA objData)
        {
            return new object[]{     
                                  objData.TBREGISTROS_PERSONAS.ID
                                , objData.PARAM_DISCAPACIDAD
                                , objData.OTRO  
            };
        }
        #endregion

        #region Obtener Datos
        public string getData(int ID)
        {
            string otro=null;
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getDiscapacidadOtroPersona", new object[] { ID, null }))
            {
                if (dataReader.Read())
                {
                    otro = dbDefaults.getString(dataReader, 0);                    
                }
            }
            return otro;
        }
        #endregion

    }
}
