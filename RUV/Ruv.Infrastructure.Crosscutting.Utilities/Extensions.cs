using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ruv.Infrastructure.Crosscutting.Utilities
{
    public static class Extensions
    {
        public static string Match(this string sToEval, string sPattern)
        {
            return Regex.Match(sToEval, sPattern).Value;
        }

        public static string Description(this Enum value) {
            var attribute = value.GetType().GetMember(value.ToString())[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false).Cast<System.ComponentModel.DescriptionAttribute>().SingleOrDefault();
            if (attribute == null)
                return default(string);

            return attribute.Description;
        }
    }
}
