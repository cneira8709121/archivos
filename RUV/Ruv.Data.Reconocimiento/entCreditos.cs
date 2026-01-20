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
    public class entCreditos : entidadRUV
    {
        public void setAnexo11_Creditos(TBANEXO11_CREDITOS objeAnexo11_Creditos, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo11_Creditos", getParametros(objeAnexo11_Creditos));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo11_Creditos.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo11_Creditos(TBANEXO11_CREDITOS objeAnexo11_Creditos, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo11_Creditos", getParametros(objeAnexo11_Creditos));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO11_CREDITOS objeAnexo11_Creditos)
        {
            return new object[]{   
                                   objeAnexo11_Creditos.ID
                                  ,objeAnexo11_Creditos.TBANEXO11.ID
                                  ,objeAnexo11_Creditos.PARAM_TIPO_ACREEDOR
                                  ,objeAnexo11_Creditos.NOMBRE_ACREEDOR
                                  ,objeAnexo11_Creditos.FECHA_DEUDA
                                  ,objeAnexo11_Creditos.MONTO_ADEUDADO
                                  ,objeAnexo11_Creditos.ACTIVO
                                  ,null  
    

            };
        }

        #region Obtener
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Siniestro.</param>
        /// <returns></returns>
        public List<TBANEXO11_CREDITOS> getData(int ID)
        {
            List<TBANEXO11_CREDITOS> registros = new List<TBANEXO11_CREDITOS>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getCreditoA11", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO11_CREDITOS registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO11_CREDITOS>();
                    registro.TBANEXO11 = new TBANEXO11();

                    int index = 0;
                                        
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBANEXO11.ID = (int)dbDefaults.getInt32(dataReader, index++);                    
                    registro.PARAM_TIPO_ACREEDOR = dbDefaults.getInt32(dataReader, index++);
                    registro.NOMBRE_ACREEDOR = dbDefaults.getString(dataReader, index++);
                    registro.FECHA_DEUDA = dbDefaults.getDateTime(dataReader, index++);
                    registro.MONTO_ADEUDADO = dbDefaults.getDecimal(dataReader, index++);
                    registro.ACTIVO = dbDefaults.getInt16(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }

        #endregion
    }
}
