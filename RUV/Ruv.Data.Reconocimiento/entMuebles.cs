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
    public class entMuebles : entidadRUV
    {
        public void setAnexo11_Muebles(TBANEXO11_MUEBLES objeAnexo11_Muebles, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo11_muebles", getParametros(objeAnexo11_Muebles));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo11_Muebles.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo11_Muebles(TBANEXO11_MUEBLES objeAnexo11_Muebles, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo11_muebles", getParametros(objeAnexo11_Muebles));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO11_MUEBLES objeAnexo11_Muebles)
        {
            return new object[]{   
                                   objeAnexo11_Muebles.ID
                                  ,objeAnexo11_Muebles.TBANEXO11.ID
                                  ,objeAnexo11_Muebles.TBREGISTROS_PERSONAS.ID
                                  ,objeAnexo11_Muebles.PARAM_TIPO_MUBLE
                                  ,objeAnexo11_Muebles.DESCRIPCION
                                  ,objeAnexo11_Muebles.PARAM_TIPO_TENENCIA
                                  ,objeAnexo11_Muebles.CANTIDAD
                                  ,objeAnexo11_Muebles.ACTIVO
                                  ,null      


            };
        }

        #region Obtener
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Anexo11.</param>
        /// <returns></returns>
        public List<TBANEXO11_MUEBLES> getData(int ID)
        {
            List<TBANEXO11_MUEBLES> registros = new List<TBANEXO11_MUEBLES>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getMuebleA11", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO11_MUEBLES registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO11_MUEBLES>();
                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
                    registro.TBANEXO11 = new TBANEXO11();

                    int index = 0;

                    #region Common Anexos
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBANEXO11.ID = (int)dbDefaults.getInt32(dataReader, index++);                    
                    //registro.TBREGISTROS_PERSONAS.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    int reg = index++;
                    registro.TBREGISTROS_PERSONAS.ID = (dataReader.IsDBNull(reg)) ? 0 : dbDefaults.getInt32(dataReader, reg).Value;

                    registro.DESCRIPCION = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_TIPO_MUBLE = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_TIPO_TENENCIA = dbDefaults.getInt32(dataReader, index++);
                    registro.CANTIDAD = dbDefaults.getInt16(dataReader, index++);
                    #endregion

                    registro.ACTIVO = dbDefaults.getInt16(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }

        #endregion
    }
}
