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
    public class entTipoEncargado : entidadRUV
    {
        public void setData(TBDECLARACION_ENCARGADO objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setTipoEncargado", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objData.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updateData(TBDECLARACION_ENCARGADO objData, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updTipoEncargado", getParametros(objData));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBDECLARACION_ENCARGADO objData)
        {
            return new object[]{     
                                  objData.ID
                                , objData.TBDECLARACIONES.ID
                                , objData.TBENCARGADO.ID 
                                , objData.IDPARAMTIPOENCARGADO 
                                , objData.ENTIDADCOMPETENTE
                                ,null 
            };
        }
    }
}
