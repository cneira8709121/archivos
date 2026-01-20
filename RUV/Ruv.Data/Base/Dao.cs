using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Ruv.Data
{
    public class Dao : IDisposable
    {
        #region Attributes

        /// <summary>
        /// List of parameters to send to the procedure. Note that the order of the parameters received by the procedure
        /// <remarks>must</remarks> be the same of the order of the list.
        /// </summary>
        private List<DbParameter> _lstParameter = null;
        /// <summary>
        /// Dictionary of the value of each output parameter
        /// </summary>
        private Dictionary<string, object> _dicOutputParameter = null;

        #endregion
        #region Properties

        #region Static

        public static string SConection { get; set; }

        #endregion

        public List<DbParameter> LstParameter { get { return _lstParameter; } }

        #endregion
        #region DAO

        public Dao()
        {
            _lstParameter = new List<DbParameter>();
            _dicOutputParameter = new Dictionary<string, object>();
        }

        #endregion
        #region Public methods

        #region Static

        public static DbTransaction InitTransaction()
        {
            DbTransaction tra = null;
            try
            {
                Database database = CreateDatabase();
                DbConnection connection = null;
                connection = database.CreateConnection();
                connection.Open();
                tra = connection.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
            return tra;
        }

        #endregion

        public void RefreshParameters() {
            _lstParameter.Clear();
        }

        public void AddParameter(DbParameter parameter) {
            _lstParameter.Add(parameter);
        }

        public void AddInputParameter(DbParameter parameter) {
            parameter.Direction = ParameterDirection.Input;
            _lstParameter.Add(parameter);
        }

        public void AddOutputParameter(DbParameter parameter) {
            parameter.Direction = ParameterDirection.Output;
            _lstParameter.Add(parameter);
        }

        public object GetOutputParameter(string sParameterName) {
            object objParameter = null;
            if (_dicOutputParameter.Count > 0) objParameter = _dicOutputParameter[sParameterName];
            return objParameter;
        }

        public IDataReader ExecuteReader(string strSPName, ref string strError) {
            Database db = CreateDatabase();
            using (DbCommand cmd = db.GetStoredProcCommand(strSPName))
            {
                if (_lstParameter != null) cmd.Parameters.AddRange(_lstParameter.ToArray());

                IDataReader dr = null;
                try {
                    dr = db.ExecuteReader(cmd);

                    _dicOutputParameter.Clear();
                    if (_lstParameter != null) {
                        foreach (DbParameter p in cmd.Parameters) {
                            if (p.Direction == ParameterDirection.Output) _dicOutputParameter.Add(p.ParameterName, p.Value);
                        }
                    }
                }
                catch (Exception ex) {
                    strError = ex.Message;
                }
                return dr;
            }
        }

        public IDataReader ExecuteReader(string strSPName) {
            Database db = CreateDatabase();
            using (DbCommand cmd = db.GetStoredProcCommand(strSPName))
            {
                if (_lstParameter != null) cmd.Parameters.AddRange(_lstParameter.ToArray());

                IDataReader dr = null;
                try {
                    dr = db.ExecuteReader(cmd);

                    _dicOutputParameter.Clear();
                    if (_lstParameter != null) {
                        foreach (DbParameter p in cmd.Parameters) {
                            if (p.Direction == ParameterDirection.Output) _dicOutputParameter.Add(p.ParameterName, p.Value);
                        }
                    }
                }
                catch (Exception ex) {
                    throw new DataException(string.Format("Unable to execute procedure '{0}': {1}.", strSPName, ex.Message));
                }
                return dr;
            }
        }

        public bool ExecuteNonQuery(string strSPName, DbTransaction tra, ref string strError) {
            Database db = CreateDatabase();
            using (DbCommand cmd = db.GetStoredProcCommand(strSPName))
            {
                try
                {
                    if (_lstParameter != null) cmd.Parameters.AddRange(_lstParameter.ToArray());

                    //Verificar si viene transacción...
                    if (tra != null)
                    {
                        db.ExecuteNonQuery(cmd, tra);
                    }
                    else
                    {
                        db.ExecuteNonQuery(cmd);
                    }

                    _dicOutputParameter.Clear();
                    if (_lstParameter != null)
                    {
                        foreach (DbParameter par in cmd.Parameters)
                        {
                            if (par.Direction == ParameterDirection.Output) _dicOutputParameter.Add(par.ParameterName, par.Value);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return false;
                }
            }
        }

        public void ExecuteNonQuery(string strSPName, DbTransaction transaction) {
            Database db = CreateDatabase();
            using (DbCommand cmd = db.GetStoredProcCommand(strSPName)) {
                try {
                    if (_lstParameter != null) cmd.Parameters.AddRange(_lstParameter.ToArray());

                    if (transaction != null) {
                        db.ExecuteNonQuery(cmd, transaction);
                    }
                    else {
                        db.ExecuteNonQuery(cmd);
                    }

                    _dicOutputParameter.Clear();
                    if (_lstParameter != null) {
                        foreach (DbParameter par in cmd.Parameters) {
                            if (par.Direction == ParameterDirection.Output) _dicOutputParameter.Add(par.ParameterName, par.Value);
                        }
                    }
                }
                catch (Exception ex) {
                    throw new DataException(string.Format("Unable to execute procedure '{0}': {1}.", strSPName, ex.Message));
                }
            }
        }

        #endregion
        #region Private methods

        #region Static

        private static Database CreateDatabase()
        {
            Database db = null;
            if (SConection == null)
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
            else
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(SConection);

            return db;
        }

        #endregion

        #endregion
        #region IDisposable Implementation

        void IDisposable.Dispose()
        {
            _lstParameter = null;
            _dicOutputParameter = null;
        }

        #endregion
    }
}
