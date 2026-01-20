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
    public class entHechosPersona : entidadRUV
    {
        #region Guardar Datos

        public void setData(TBREG_PERSONA_HECHOS objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setHechosPersona", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        public void deleteData(int idRegPers, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_delHechosPersona", idRegPers);

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBREG_PERSONA_HECHOS objData)
        {
            return new object[]{       
                                  objData.TBREGISTROS_PERSONAS.ID
                                , objData.PARAM_HECHO
                                , objData.ACTIVO
            };
        }

        #endregion

        #region Obtener Datos

        public List<int> getData(int idRegPers)
        {
            List<int> registros = new List<int>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getHechosPersona", idRegPers, null))
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
