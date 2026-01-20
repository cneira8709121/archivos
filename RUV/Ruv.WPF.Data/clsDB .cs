using System.Data;
using System.Data.Common;
using Ruv.Data;

namespace Ruv.WPF.Data
{
    public class clsDB : entidadRUV
    {
        public DataSet ExecuteDataSet(string strCmd, params object[] parametros)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand(strCmd, parametros);
            return dbRUV.ExecuteDataSet(cmd);
        }

        public void ExecutenonQuery(string strCmd, params object[] parametros)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand(strCmd, parametros);
            dbRUV.ExecuteNonQuery(cmd);
        }
    }
}
