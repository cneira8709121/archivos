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
    public class entEncargado : entidadRUV
    {
        #region Guardar Datos
        public void setData(TBENCARGADO objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setEncargado", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objData.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void insertEncargadoFirma(int idEncargado, byte[] firma, DbTransaction transaction) {
            DbCommand command = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setEncargadoFirma", new object[] { idEncargado, firma });
            dbRUV.ExecuteNonQuery(command, transaction);
        }

        public void updateData(TBENCARGADO objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updEncargado", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        public void updateEncargadoFirma(int idEncargado, byte[] firma, DbTransaction transaction)
        {
            DbCommand command = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updEncargadoFirma", new object[] { idEncargado, firma });
            dbRUV.ExecuteNonQuery(command, transaction);
        }

        private object[] getParametros(TBENCARGADO objData)
        {
            return new object[]{     
                                  objData.ID
                                , objData.IDPARAMTIPODOCUMENTO
                                , objData.NUMERODOCUMENTO 
                                , objData.PRIMERNOMBRE 
                                , objData.SEGUNDONOMBRE  
                                , objData.PRIMERAPELLIDO  
                                , objData.SEGUNDOAPELLIDO  
                                , objData.DIRECCION  
                                , objData.TELEFONO 
                                , objData.CARGO  
                                ,null 
            };
        }

        #endregion

        #region Obtener Datos
        public TBENCARGADO getData(int id_encargado, int id_declaracion)
        {
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getEncargado", new object[] { id_encargado, id_declaracion, null }))
            {
                while (dataReader.Read())
                {
                    TBENCARGADO registro = EnterpriseLibraryContainer.Current.GetInstance<TBENCARGADO>();
                    int index = 0;
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.IDPARAMTIPODOCUMENTO = dbDefaults.getInt16(dataReader, index++);
                    registro.NUMERODOCUMENTO = dbDefaults.getString(dataReader, index++);
                    registro.PRIMERNOMBRE = dbDefaults.getString(dataReader, index++);
                    registro.SEGUNDONOMBRE = dbDefaults.getString(dataReader, index++);
                    registro.PRIMERAPELLIDO = dbDefaults.getString(dataReader, index++);
                    registro.SEGUNDOAPELLIDO = dbDefaults.getString(dataReader, index++);
                    registro.DIRECCION = dbDefaults.getString(dataReader, index++);
                    registro.TELEFONO = dbDefaults.getString(dataReader, index++);
                    registro.CARGO = dbDefaults.getString(dataReader, index++);

                    TBDECLARACION_ENCARGADO declaracion_encargado = new Ruv.Data.TBDECLARACION_ENCARGADO();
                    declaracion_encargado.IDPARAMTIPOENCARGADO = dbDefaults.getInt32(dataReader, index++);
                    declaracion_encargado.ENTIDADCOMPETENTE = dbDefaults.getString(dataReader, index++);
                    registro.TBDECLARACION_ENCARGADO.Add(declaracion_encargado);
                    return registro;
                }
            }
            return null;
        }
        #endregion

    }
}
