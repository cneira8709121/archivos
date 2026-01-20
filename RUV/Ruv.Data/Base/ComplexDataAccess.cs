using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.Linq.Mapping;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Ruv.Data
{
    public abstract class ComplexDataAccess
    {
        #region Funciones dinámicas dao

        
        #region Protected members

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        protected DbConnection Connection { get; set; }

        /// <summary>
        /// Properties Dictionary to map from database columns
        /// </summary>
        protected Dictionary<MemberInfo, string> _PropertiesToColumnsNames;

        /// <summary>
        /// Current object type working
        /// </summary>
        protected Type _currentType;

        /// <summary>
        /// Variable to indicate if working with inheritance
        /// </summary>
        protected bool WorkWithInheredMembers { get { return true; } }

        #endregion

        /// <summary>
        /// Delegates to search criteria.
        /// </summary>
        /// <param name="objMemberInfo">The obj member info.</param>
        /// <param name="objSearch">The obj search.</param>
        /// <returns></returns>
        private bool DelegateToSearchCriteria(MemberInfo objMemberInfo, Object objSearch)
        {
            object[] attributes = objMemberInfo.GetCustomAttributes(typeof(ColumnAttribute), WorkWithInheredMembers);
            if (attributes.Length > 0)
            {
                ColumnAttribute dbItemInfo = (ColumnAttribute)attributes[0];
                if (!_PropertiesToColumnsNames.ContainsKey(objMemberInfo))
                    _PropertiesToColumnsNames.Add(objMemberInfo, dbItemInfo.Name);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Finds the column names.
        /// </summary>
        /// <param name="type">The type.</param>
        protected void FindColumnNames(Type type)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            if (!WorkWithInheredMembers)
                bindingFlags = bindingFlags | BindingFlags.DeclaredOnly;
            _PropertiesToColumnsNames = new Dictionary<MemberInfo, string>();
            type.FindMembers(MemberTypes.Property, bindingFlags, DelegateToSearchCriteria, null);

        }

        /// <summary>
        /// Converts the type of the value to.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        protected object ConvertValueToType(object value, Type targetType)
        {
            return Ruv.Infrastructure.Crosscutting.Utilities.DataBase.ConvertValueToType(value, targetType);
        }

        /// <summary>
        /// Maps from data reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="closeReader">if set to <c>true</c> [close reader].</param>
        /// <returns></returns>
        protected List<T> MapFromDataReader<T>(IDataReader reader, bool closeReader)
        {
            Type newType = typeof(T);
            if (_currentType != newType)
            {
                _currentType = newType;
                FindColumnNames(_currentType);
            }

            List<T> returnList = new List<T>();
            object[] values = new object[reader.FieldCount];
            Dictionary<string, int> columnsIds = new Dictionary<string, int>(reader.FieldCount);
            List<string> schemaFieldsName = null;
            while (reader.Read())
            {
                reader.GetValues(values);
                T objTarget = Activator.CreateInstance<T>();
                if (schemaFieldsName == null)
                    using (DataTable dtSchema = reader.GetSchemaTable())
                    {
                        schemaFieldsName = dtSchema.AsEnumerable().Select(x => x.Field<string>("ColumnName").ToUpper()).ToList();
                    }
                //When number of properties of the object are more than 5, use parallel process
                if (_PropertiesToColumnsNames.Count > 5)
                {
                    Parallel.ForEach<MemberInfo>(_PropertiesToColumnsNames.Keys, member =>
                    {
                        string columnName = _PropertiesToColumnsNames[member];
                        if (schemaFieldsName.Contains(columnName.ToUpper()))
                        {
                            int columnId = 0;
                            if (columnsIds.ContainsKey(columnName))
                                columnId = columnsIds[columnName];
                            else
                            {
                                columnId = reader.GetOrdinal(columnName);
                                columnsIds.Add(columnName, columnId);
                            }
                            PropertyInfo pMember = (PropertyInfo)member;
                            object value = ConvertValueToType(values[columnId], pMember.PropertyType);
                            pMember.SetValue(objTarget, value, null);
                        }
                    }
                  );
                }
                else
                {
                    foreach (MemberInfo member in _PropertiesToColumnsNames.Keys)
                    {
                        string columnName = _PropertiesToColumnsNames[member];
                        if (schemaFieldsName.Contains(columnName.ToUpper()))
                        {
                            int columnId = 0;
                            if (columnsIds.ContainsKey(columnName))
                                columnId = columnsIds[columnName];
                            else
                            {
                                columnId = reader.GetOrdinal(columnName);
                                columnsIds.Add(columnName, columnId);
                            }
                            PropertyInfo pMember = (PropertyInfo)member;
                            object value = ConvertValueToType(values[columnId], pMember.PropertyType);
                            pMember.SetValue(objTarget, value, null);
                        }
                    }
                }
                returnList.Add(objTarget);
            }
            if (closeReader)
                reader.Dispose();
            return returnList.Count > 0 ? returnList : null;
        }

        /// <summary>
        /// Maps from data reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        protected List<T> MapFromDataReader<T>(IDataReader reader)
        {
            return MapFromDataReader<T>(reader, true);
        }

        #endregion
    }
}
