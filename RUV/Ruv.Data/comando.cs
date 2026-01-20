using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;


namespace Ruv.Data
{
    public abstract class comandoRuv
    {
        public abstract void ejecutar(Database objBaseDatos, System.Data.IDbTransaction objTransaccion);
        public abstract void retroceder(Database objBaseDatos, System.Data.IDbTransaction objTransaccion);
        public abstract void ejecutar();
        public abstract void retroceder();
    }
}
