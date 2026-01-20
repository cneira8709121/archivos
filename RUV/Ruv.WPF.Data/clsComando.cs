using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Oracle.DataAccess.Client;

namespace Ruv.WPF.Data
{
    public class clsComando : IDisposable
    {
        public clsComando(string nombreProcedimiento)
        {
            Inicializar();
            _Comando.CommandText = nombreProcedimiento;
        }

        public void Inicializar()
        {
            _Comando = new OracleCommand();
            _Comando.CommandType = CommandType.StoredProcedure;
        }

        #region DECLARACIONES & PROPIEDADES

        private OracleCommand _Comando;
        public OracleCommand Comando
        {
            get { return _Comando; }
        }

        /// <summary>
        /// Lee o establece el texto del comando. Adicionalmente borra la lista de parámetros.
        /// </summary>
        public string TextoComando
        {
            get { return _Comando.CommandText; }
            set
            {
                _Comando.CommandText = value;
                _Comando.Parameters.Clear();
            }
        }

        public static string Paquete = "PKG_REGISTROOFFLINE.";

        #endregion

        #region TRANSACCIONES

        private OracleTransaction _Transaccion;
        /// <summary>
        /// La transacción que cobija este comando.
        /// </summary>
        public OracleTransaction Transaccion
        {
            get { return _Transaccion; }
            set { _Transaccion = value; }
        }

        /// <summary>
        /// Cobija este comando bajo la transacción iniciada en otro comando.
        /// </summary>
        /// <param name="fuente"></param>
        public void CobijarPorTransaccion(clsComando fuente)
        {
            Transaccion = fuente.Transaccion;
        }

        private clsConeccion oConTransaccion;

        /// <summary>
        /// Iniciar una transacción.
        /// </summary>
        public void IniciarTransaccion()
        {
            oConTransaccion = new clsConeccion();
            _Transaccion = oConTransaccion.Coneccion.BeginTransaction();
        }

        /// <summary>
        /// Postear la transacción.
        /// </summary>
        public void PostearTransaccion()
        {
            _Transaccion.Commit();
            _Transaccion = null;
            oConTransaccion.Cerrar();
        }

        /// <summary>
        /// Abortar la transacción.
        /// </summary>
        public void AbortarTransaccion()
        {
            _Transaccion.Rollback();
            _Transaccion = null;
            oConTransaccion.Cerrar();
        }

        #endregion

        #region MANEJO DE PARÁMETROS

        /// <summary>
        /// Establece el valor de un parámetro.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="valor"></param>
        public void EstablecerParam(string nombre, object valor)
        {
            _Comando.Parameters[nombre].Value = valor;
        }

        /// <summary>
        /// Agrega un parámetro de entrada.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="valor"></param>
        public void AgregarParam(string nombre, object valor)
        {
            if (valor == null || valor == DBNull.Value)
                AgregarParam(nombre, DBNull.Value, true);
            else
                AgregarParam(nombre, valor, true);
        }

        /// <summary>
        /// Agrega un parámetro de entrada.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="valor"></param>
        public void AgregarParamDate(string nombre, object valor)
        {
            if (valor == null || valor == DBNull.Value)
                AgregarParam(nombre, DBNull.Value, true);
            else
                AgregarParam(nombre, valor, true);

            Comando.Parameters[nombre].DbType = DbType.DateTime;
        }

        /// <summary>
        /// Agrega un parámetro de tipo cursor.
        /// </summary>
        /// <param name="nombre"></param>
        public void AgregarParamCursor(string nombre)
        {
            OracleParameter P = new OracleParameter(nombre, OracleDbType.RefCursor);
            P.Direction = ParameterDirection.Output;
            _Comando.Parameters.Add(P);
        }

        /// <summary>
        /// Agrega un parámetro entero de salida.
        /// </summary>
        /// <param name="nombre"></param>
        public void AgregarParamInt32Salida(string nombre)
        {
            OracleParameter P = new OracleParameter(nombre, OracleDbType.Int32);
            P.Direction = ParameterDirection.Output;
            _Comando.Parameters.Add(P);

        }

        /// <summary>
        /// Agrega un parámetro de entrada o salida.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="valor"></param>
        /// <param name="entrada"></param>
        public void AgregarParam(string nombre, object valor, Boolean entrada)
        {
            OracleParameter P = new OracleParameter(nombre, valor);
            P.Direction = entrada ? ParameterDirection.Input : ParameterDirection.Output;
            _Comando.Parameters.Add(P);
        }

        /// <summary>
        /// Retorna una copia de la colección de parámetros
        /// </summary>
        /// <returns></returns>
        public List<OracleParameter> ObtenerCopiaParametros()
        {
            List<OracleParameter> Output = new List<OracleParameter>();
            foreach (OracleParameter item in _Comando.Parameters)
            {
                var NuevoPar = new OracleParameter()
                  {
                      DbType = item.DbType,
                      Direction = item.Direction,
                      IsNullable = item.IsNullable,
                      OracleDbType = item.OracleDbType,
                      ParameterName = item.ParameterName,
                      Value = item.Value
                  };
                Output.Add(NuevoPar);
            }
            return Output;
        }

        /// <summary>
        /// Reemplaza completamente la lista de parámetros por la lista proporcionada.
        /// </summary>
        /// <param name="origen"></param>
        public void EstablecerListaParametros(List<OracleParameter> origen)
        {
            _Comando.Parameters.Clear();
            foreach (OracleParameter item in origen)
            {
                var NuevoPar = new OracleParameter()
                {
                    DbType = item.DbType,
                    Direction = item.Direction,
                    IsNullable = item.IsNullable,
                    OracleDbType = item.OracleDbType,
                    ParameterName = item.ParameterName,
                    Value = item.Value ?? DBNull.Value
                };
                _Comando.Parameters.Add(NuevoPar);
            }
        }

        /// <summary>
        /// Establece los valores de todos los parámetros.
        /// </summary>
        /// <param name="valores"></param>
        public void EstablecerValoresParametros(params object[] valores)
        {
            for (int i = 0; i < valores.Length; i++)
                if (valores[i] == null)
                    _Comando.Parameters[i].Value = DBNull.Value;
                else
                    _Comando.Parameters[i].Value = valores[i];
        }

        /// <summary>
        /// Agrega un parámetro de salida tipo varchar.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        public void AgregarParamVarcharSalida(string nombre)
        {
            OracleParameter P = new OracleParameter(nombre, OracleDbType.Varchar2);
            P.Direction = ParameterDirection.Output;
            P.Size = 3000;
            _Comando.Parameters.Add(P);

        }

        /// <summary>
        /// Retorna el valor de un parámetro tipo entero.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public Int32? LeerParametroComoInt32(string nombre)
        {
            OracleParameter P = _Comando.Parameters[nombre];
            if (P.Value == null || P.Value == DBNull.Value)
                return null;
            else
                return Convert.ToInt32(P.Value);
        }

        public object LeerParametro(string nombre)
        {
            OracleParameter P = _Comando.Parameters[nombre];
            if (P.Value == null || P.Value == DBNull.Value)
                return null;
            else
                return P.Value;
        }

        #endregion

        #region INFERENCIA DE PARÁMETROS

        private Database _oDataBase;
        public Database oDataBase
        {
            get
            {
                if (_oDataBase == null)
                    _oDataBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConexionSipod");
                //_oDataBase = DatabaseFactory.CreateDatabase("ConexionSipod");
                return _oDataBase;
            }
        }

        /// <summary>
        /// Infiere y retorna la colección de parámetros para una Stored Procedure.
        /// </summary>
        /// <param name="nombreProcedimiento"></param>
        /// <returns></returns>
        public List<OracleParameter> InferirParametrosParaSP(string nombreProcedimiento)
        {
            DbCommand oComando = oDataBase.GetStoredProcCommand(nombreProcedimiento);
            oComando.CommandType = CommandType.StoredProcedure;
            oDataBase.DiscoverParameters(oComando);

            List<OracleParameter> Output = new List<OracleParameter>();
            foreach (System.Data.OracleClient.OracleParameter item in oComando.Parameters)
            {
                var NuevoPar = new OracleParameter()
                {
                    DbType = item.DbType,
                    Direction = item.Direction,
                    IsNullable = item.IsNullable,
                    OracleDbType = GetOracleDbType(item.OracleType),
                    ParameterName = item.ParameterName,
                    Value = item.Value
                };
                Output.Add(NuevoPar);
            }
            return Output;
        }

        private static OracleDbType GetOracleDbType(System.Data.OracleClient.OracleType o)
        {
            if (o == System.Data.OracleClient.OracleType.NVarChar) return OracleDbType.Varchar2;
            if (o == System.Data.OracleClient.OracleType.VarChar) return OracleDbType.Varchar2;
            if (o == System.Data.OracleClient.OracleType.DateTime) return OracleDbType.Date;
            if (o == System.Data.OracleClient.OracleType.Int32) return OracleDbType.Int32;
            if (o == System.Data.OracleClient.OracleType.Int16) return OracleDbType.Int16;
            if (o == System.Data.OracleClient.OracleType.Byte) return OracleDbType.Byte;
            if (o == System.Data.OracleClient.OracleType.Double) return OracleDbType.Double;
            if (o == System.Data.OracleClient.OracleType.Blob) return OracleDbType.Blob;
            if (o == System.Data.OracleClient.OracleType.Cursor) return OracleDbType.RefCursor;

            System.Diagnostics.Debugger.Break();
            return OracleDbType.Varchar2;
        }

        #endregion

        #region EJECUCION

        /// <summary>
        /// Ejecuta el comando y devuelve un resultado como dataset.
        /// </summary>
        /// <returns></returns>
        public DataSet EjecutarComoDataSet()
        {
            DataSet Output = new DataSet();
            OracleDataAdapter Adapter = new OracleDataAdapter();
            Adapter.SelectCommand = _Comando;

            if (oConTransaccion != null)
            {
                _Comando.Connection = oConTransaccion.Coneccion;
                _Comando.Transaction = _Transaccion;
                Adapter.Fill(Output);
            }
            else
                using (clsConeccion Con = new clsConeccion())
                {
                    _Comando.Connection = Con.Coneccion;
                    try
                    {
                        Adapter.Fill(Output);
                    }
                    catch
                    {
                    }
                }

            return Output;
        }

        /// <summary>
        /// Ejecuta el comando sin esperar datos de retorno.
        /// </summary>
        public void Ejecutar()
        {
            if (oConTransaccion != null)
            {
                _Comando.Connection = oConTransaccion.Coneccion;
                _Comando.Transaction = _Transaccion;
                _Comando.ExecuteNonQuery();
            }
            else
                using (clsConeccion Con = new clsConeccion())
                {
                    _Comando.Connection = Con.Coneccion;
                    _Comando.ExecuteNonQuery();
                }
        }

        #endregion

        public void Dispose()
        {
            _Comando = null;
        }
    }
}
