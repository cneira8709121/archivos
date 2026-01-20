using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System.Data.Common;
namespace Ruv.Data
{
    /// <summary>
    /// Contenedor de la Clase Genérica de Acceso a Datos
    /// </summary>
    public class entidadRUV 
    {
        public static Database dbRUV;
        public static Database GetInstance()
        {
            //return DatabaseFactory.CreateDatabase("cnBaseDatos");
            //Database basedat = EnterpriseLibraryContainer.Current.GetInstance<Database>("cnBaseDatos");
            return DatabaseFactory.CreateDatabase("cnBaseDatos");
        }
        public entidadRUV()
        {
            dbRUV = GetInstance();
        }



    }
    public static class dbDefaults
    {
        public static Int16? Int16Def = null;
        public static Int32? Int32Def = null;
        public static Int64? Int64Def = null;
        public static String StringDef = null;
        public static DateTime? DateTimeDef = null;
        public static Decimal? DecimalDef = null;

        public static Int16? getInt16(System.Data.IDataReader myReader, int index)
            {
                return (myReader.IsDBNull(index)) ? dbDefaults.Int16Def : myReader.GetInt16(index);
            }
        public static Int32? getInt32(System.Data.IDataReader myReader, int index)
        {
            return (myReader.IsDBNull(index)) ? dbDefaults.Int32Def : myReader.GetInt32(index);
        }
        public static Int64? getInt64(System.Data.IDataReader myReader, int index)
        {
            return (myReader.IsDBNull(index)) ? dbDefaults.Int64Def : myReader.GetInt64(index);
        }
        public static String getString(System.Data.IDataReader myReader, int index)
        {
            return (myReader.IsDBNull(index)) ? dbDefaults.StringDef : myReader.GetString(index);
        }
        public static DateTime? getDateTime(System.Data.IDataReader myReader, int index)
        {
            return (myReader.IsDBNull(index)) ? dbDefaults.DateTimeDef : myReader.GetDateTime(index);
        }
        public static Decimal? getDecimal(System.Data.IDataReader myReader, int index)
        {
            return (myReader.IsDBNull(index)) ? dbDefaults.DecimalDef : myReader.GetDecimal(index);
        }

    }

    
}
