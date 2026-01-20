using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;

namespace Ruv.Infrastructure.Crosscutting.Utilities
{
    public class ExcelHelper
    {
        #region Attributes

        /// <summary>
        /// Properties Dictionary to map from excel columns
        /// </summary>
        protected Dictionary<MemberInfo, string> _PropertiesToColumnsNames;

        #endregion

        #region Properties

        /// <summary>
        /// Variable to indicate if working with inheritance
        /// </summary>
        protected bool WorkWithInheredMembers { get { return true; } }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Exports a <see cref="List"/> as an array of <see cref="byte"/>
        /// </summary>
        /// <typeparam name="T">The type of the list to be exported</typeparam>
        /// <param name="lstToExport">The list to export</param>
        /// <returns>An .xlsx file as an array of <see cref="byte"/></returns>
        public byte[] ExportToExcel<T>(List<T> lstToExport) where T : class
        {
            if (lstToExport == null || lstToExport.Count == 0) return null;

            Type objType = typeof(T);
            FindColumnNames(objType);

            DataTable dt = new DataTable();
            dt.TableName = objType.Name;

            foreach (KeyValuePair<MemberInfo, string> mi in _PropertiesToColumnsNames)
            {
                Type t = ((PropertyInfo)mi.Key).PropertyType;
                if (t.IsGenericType &&
                    t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    dt.Columns.Add(mi.Value, Nullable.GetUnderlyingType(t));
                else dt.Columns.Add(mi.Value, t);
            }
            dt.AcceptChanges();

            foreach (T item in lstToExport)
            {
                List<object> lstValues = new List<object>();
                foreach (KeyValuePair<MemberInfo, string> pi in _PropertiesToColumnsNames)
                {
                    lstValues.Add(((PropertyInfo)pi.Key).GetValue(item, null));
                }
                if (lstValues.Count > 0) dt.Rows.Add(lstValues.ToArray());
            }

            if (dt.Rows.Count > 0)
            {
                //Write to the spreadsheet
                using (MemoryStream stream = new MemoryStream())
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);

                        //Save to the memory stream, and return the file
                        wb.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Converts an array of <see cref="byte"/> in a <see cref="List"/>
        /// </summary>
        /// <typeparam name="T">The type of the list to be exported</typeparam>
        /// <param name="bExcel">An .xlsx file as an array of <see cref="byte"/></param>
        /// <returns>>The list of T</returns>
        public List<T> ImportFromExcel<T>(byte[] bExcel) where T : class
        {
            if (bExcel == null) return null;

            using (MemoryStream stream = new MemoryStream(bExcel))
            {
                using (XLWorkbook wb = new XLWorkbook(stream))
                {
                    Type objType = typeof(T);
                    FindColumnNames(objType);

                    List<T> returnList = new List<T>();
                    Dictionary<string, int> schema = new Dictionary<string, int>();

                    IXLWorksheet ws = wb.Worksheets.First();
                    IXLRange range = ws.RangeUsed();

                    IXLTable tbl = range.AsTable();
                    tbl.Fields.ForEach(x => schema.Add(x.Name.ToUpper(), x.Index));

                    int colCount = range.ColumnCount();
                    IXLRangeRows rows = range.RowsUsed();
                    // rowIndex zero is the column names
                    for (int rowIndex = 1; rowIndex < rows.Count(); rowIndex++)
                    {
                        IXLRangeRow row = rows.ElementAt(rowIndex);
                        T objTarget = Activator.CreateInstance<T>();
                        object[] rowData = new object[colCount];
                        int i = 0;
                        row.Cells().ForEach(c => rowData[i++] = c.Value);

                        foreach (MemberInfo member in _PropertiesToColumnsNames.Keys)
                        {
                            string columnName = _PropertiesToColumnsNames[member];
                            if (schema.ContainsKey(columnName.ToUpper()))
                            {
                                PropertyInfo pMember = (PropertyInfo)member;
                                object value = ConvertValueToType(rowData[schema[columnName.ToUpper()]], pMember.PropertyType);
                                pMember.SetValue(objTarget, value, null);
                            }
                        }
                        returnList.Add(objTarget);
                    }

                    return returnList;
                }
            }
        }

        #endregion
        #region Protected

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
        /// Delegates to search criteria.
        /// </summary>
        /// <param name="objMemberInfo">The obj member info.</param>
        /// <param name="objSearch">The obj search.</param>
        /// <returns></returns>
        protected bool DelegateToSearchCriteria(MemberInfo objMemberInfo, Object objSearch)
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
        /// Parse a object from a Type to other type
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <param name="targetType">Target type</param>
        /// <returns>Same value with new type</returns>
        protected object ConvertValueToType(object value, Type targetType)
        {
            if (value != null && value.ToString() != "")
            {
                Type valueType = value.GetType();
                if (targetType == valueType || targetType.IsAssignableFrom(valueType))
                    return value;
                else
                {
                    //First, evalue if the target type is a nullable type
                    Type targetTypeTemp = targetType;
                    if (targetTypeTemp.IsGenericType &&
                        targetTypeTemp.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (value.ToString().Trim().Length == 0)
                            return null;
                        targetTypeTemp = Nullable.GetUnderlyingType(targetType);
                        if (targetTypeTemp == valueType || targetTypeTemp.IsAssignableFrom(valueType))
                            return value;
                    }
                    if (targetTypeTemp.IsEnum)
                        return Enum.Parse(targetTypeTemp, value.ToString(), true);
                    else
                    {
                        try
                        {
                            return Convert.ChangeType(value, targetTypeTemp);
                        }
                        catch
                        {
                            return Convert.ChangeType(value, targetTypeTemp, CultureInfo.InvariantCulture);
                        }
                    }
                }
            }
            else
                return null;
        }

        #endregion

        #endregion
    }
}
