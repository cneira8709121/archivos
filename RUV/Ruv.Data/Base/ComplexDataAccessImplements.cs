using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ruv.Data
{
    public class ComplexDataAccessImplements: ComplexDataAccess
    {
         #region Constructor

        //protected List<T> MapFromDataReader<T>(IDataReader reader, bool closeReader) 
        //    :base()
        //{  }


        public static List<T> MapFromDataReaderI<T>(IDataReader reader, bool closeReader)
        {
            ComplexDataAccessImplements cDAI = new ComplexDataAccessImplements();
            List<T> lstT = null;
            try
            {
                 lstT = cDAI.MapFromDataReader<T>(reader, closeReader);
            }
            catch(Exception ex)
            {
                if (closeReader && reader != null) reader.Close();
            }
            return lstT;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ExampleDataAccess"/> class.
        ///// </summary>
        //public ComplexDataAccessImplements() 
        //    :base("ConnectionString")
        //{ }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ExampleDataAccess"/> class.
        ///// </summary>
        ///// <param name="connectionString">The connection string.</param>
        //public ComplexDataAccessImplements(string connectionString)
        //    :base (connectionString)
        //{ }

        #endregion
    }
}
