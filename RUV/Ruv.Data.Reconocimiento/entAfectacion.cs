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
    public class entAfectacion : entidadRUV
    {
        #region Guardar Datos

        public void setData(TBAFECTACION objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAfectacionAnexo", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        public void deleteData(int idAnexo, int idTipoAnexo, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_delAfectacionesAnexo", idAnexo, idTipoAnexo);

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBAFECTACION objData)
        {
            return new object[]{       
                                  objData.ID_ANEXO
                                , objData.PARAM_TIPO_HECHO
                                , objData.PARAM_AFECTACION
            };
        }

        #endregion

        #region Obtener Datos

        public List<TBAFECTACION> getData(int tipo_hecho, int id_anexo)
        {
            List<TBAFECTACION> registros = new List<TBAFECTACION>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAfectacionesAnexo", tipo_hecho, id_anexo, null))
            {
                while (dataReader.Read())
                {
                    TBAFECTACION registro = EnterpriseLibraryContainer.Current.GetInstance<TBAFECTACION>();

                    int index = 0;

                    registro.PARAM_TIPO_HECHO = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.ID_ANEXO = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_AFECTACION = (int)dbDefaults.getInt32(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }


        #endregion
    }
}
