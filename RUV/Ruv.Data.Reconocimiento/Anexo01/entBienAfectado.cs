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
    public class entBienAfectado : entidadRUV
    {
        #region Guardar Datos
        public void setData(TBBIEN_AFECTADO_A1 objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo1_BienAfectado", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objData.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updData(TBBIEN_AFECTADO_A1 objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo1_BienAfectado", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBBIEN_AFECTADO_A1 objData)
        {
            return new object[]{
                                  objData.ID
                                , objData.TBANEXO1.ID
                                , objData.INMUEBLE
                                , objData.PARAM_TIPOPERTENENCIA
                                , objData.ACTIVO
                                , objData.DESCRIPCION
                                ,null
            };
        }
        #endregion

        #region Obtener Datos

        
        public List<TBBIEN_AFECTADO_A1> getData(int ID)
        {
            List<TBBIEN_AFECTADO_A1> registros = new List<TBBIEN_AFECTADO_A1>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getBienesAfectadosA1", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBBIEN_AFECTADO_A1 registro = EnterpriseLibraryContainer.Current.GetInstance<TBBIEN_AFECTADO_A1>();
                    int index = 0;                    
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);                    
                    //registro.TBANEXO1.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    index++;
                    registro.INMUEBLE = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_TIPOPERTENENCIA = dbDefaults.getInt32(dataReader, index++);
                    registro.ACTIVO = dbDefaults.getInt16(dataReader, index++);
                    registro.DESCRIPCION = dbDefaults.getString(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }
        #endregion
    }
}
