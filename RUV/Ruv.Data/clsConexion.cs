using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;

namespace Ruv.Data
{
    internal class clsConexion : IDisposable
    {

        internal clsConexion()
        {
            _Conecccion = new OracleConnection(
        System.Configuration.ConfigurationManager.ConnectionStrings["cnBaseDatos"].ConnectionString);
            _Conecccion.Open();
        }

        private OracleConnection _Conecccion;

        /// <summary>
        /// Obtener la conexión.
        /// </summary>
        internal OracleConnection Coneccion
        {
            get { return _Conecccion; }
        }

        /// <summary>
        /// Cerrar la conexión.
        /// </summary>
        internal void Cerrar()
        {
            if (_Conecccion != null && _Conecccion.State == System.Data.ConnectionState.Open)
            {
                _Conecccion.Close();
            }
        }

        public void Dispose()
        {
            Cerrar();
        }

    }
}
