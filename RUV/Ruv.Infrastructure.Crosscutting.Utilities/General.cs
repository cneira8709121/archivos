using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Utilities
{
    public static class General
    {
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
    }
}
