using System;
using System.Globalization;
using System.Web;

namespace Ruv.WebApp.Common
{
    public static class Url
    {

        public static string QSStringField(this HttpRequest request, string key) {
            var value = request.QueryString[key];
            return value != null ? value.ToString() : null;
        }

        public static int? QSIntegerField(this HttpRequest request, string key) {
            var value = request.QueryString[key];
            int integerValue = 0;
            if (value != null && int.TryParse(value, out integerValue))
                return integerValue;

            return null;
        }

        public static DateTime? QSDateField(this HttpRequest request, string key, string format) {
            var value = request.QueryString[key];
            DateTime dateValue = DateTime.MinValue;
            if (value != null && DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dateValue))
                return dateValue;

            return null;
        }

        public static string AppendQueryStringParameter(this string url, string key, string value) { 
            var concatSeparator = url.Contains("?") ? "&" : "?";
            return string.Format("{0}{1}{2}={3}", url, concatSeparator, key, value);
        }

    }
}