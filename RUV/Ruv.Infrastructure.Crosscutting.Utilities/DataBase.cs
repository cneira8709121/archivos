using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ruv.Infrastructure.Crosscutting.Utilities
{
    /// <summary>
    /// Class offers several features related to the database useful for the project
    /// </summary>
    public static class DataBase
    {
        #region Utilidades arquitectura compleja

        /// <summary>
        /// Parse a object from a Type to other type
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <param name="targetType">Target type</param>
        /// <returns>Same value with new type</returns>
        /// Fabián A. Becerra
        public static object ConvertValueToType(object value, Type targetType)
        {
            if (value != DBNull.Value && value != null && value.ToString() != "")
            {
                Type valueType = value.GetType();
                if (targetType == valueType || targetType.IsAssignableFrom(valueType))
                    return value;
                else
                {
                    //First, evalue if the target type is a nullable type
                    Type targetTypeTemp = targetType;
                    if (targetTypeTemp.IsNullable())
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
                        try { 
                            return Convert.ChangeType(value, targetTypeTemp); 
                        }
                        catch { 
                            return Convert.ChangeType(value, targetTypeTemp, CultureInfo.InvariantCulture); 
                        }
                    }
                }
            }
            else
                return null;
        }

        /// <summary>
        /// Parse a generic object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// Fabián A. Becerra
        public static T ConvertValueToType<T>(object value)
        {
            object returnValue = ConvertValueToType(value, typeof(T));
            if (returnValue == null)
                return default(T);
            return (T)returnValue;
        }

        /// <summary>
        /// Adjust the length of the object
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string TrimToFieldLength(object value, int maxLength)
        {
            if (value != null)
            {
                string valueString = value.ToString().Trim();
                return valueString.Length > maxLength ? valueString.Substring(0, maxLength).Trim() : valueString;
            }
            else
                return null;
        }

        /// <summary>
        /// Evaluates the DB null value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static object EvaluateDBNullValue(object obj)
        {
            if (obj == null)
                return DBNull.Value;
            else if (obj.ToString().Trim().Length == 0)
                return DBNull.Value;
            else if (obj is string)
                return obj.ToString().Trim();
            else return obj;
        }

        /// <summary>
        /// Evaluates the DB null value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns></returns>
        public static object EvaluateDBNullValue(object obj, int maxLength)
        {
            if (obj == null)
                return DBNull.Value;
            else if (obj.ToString().Trim().Length == 0)
                return DBNull.Value;
            else if (obj is string)
                return Infrastructure.Crosscutting.Utilities.General.TrimToFieldLength(obj, maxLength);
            else return obj;
        }

        #region extensions
        /// <summary>
        /// Class to extend functionality
        /// </summary>

        /// <summary>
        /// Evaluate if the current type is a nullable type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNullable(this Type t)
        {
            return t.Name.ToLower() == "nullable`1";
        }
        #endregion

        #endregion
    }
}
