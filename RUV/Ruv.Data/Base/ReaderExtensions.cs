using System;
using System.Data;

namespace Ruv.Data {

    public static class ReaderExtensions {

        public static int GetInt(this IDataReader reader, string fieldName) {
            var ordinal = reader.GetOrdinal(fieldName);
            return (reader.IsDBNull(ordinal)) ? default(int) : reader.GetInt32(ordinal);
        }

        public static int? GetNullableInt(this IDataReader reader, string fieldName) {
            var ordinal = reader.GetOrdinal(fieldName);
            return (reader.IsDBNull(ordinal)) ? default(int?) : reader.GetInt32(ordinal);
        }

        public static short GetShort(this IDataReader reader, string fieldName) {
            var ordinal = reader.GetOrdinal(fieldName);
            return (reader.IsDBNull(ordinal)) ? default(short) : reader.GetInt16(ordinal);
        }

        public static short? GetNullableShort(this IDataReader reader, string fieldName) {
            var ordinal = reader.GetOrdinal(fieldName);
            return (reader.IsDBNull(ordinal)) ? default(short?) : reader.GetInt16(ordinal);
        }

        public static DateTime GetDateTime(this IDataReader reader, string fieldName) {
            var ordinal = reader.GetOrdinal(fieldName);
            return (reader.IsDBNull(ordinal)) ? default(DateTime) : reader.GetDateTime(ordinal);
        }

        public static DateTime? GetNullableDateTime(this IDataReader reader, string fieldName) {
            var ordinal = reader.GetOrdinal(fieldName);
            return (reader.IsDBNull(ordinal)) ? default(DateTime?) : reader.GetDateTime(ordinal);
        }

        public static string GetString(this IDataReader reader, string fieldName) {
            var ordinal = reader.GetOrdinal(fieldName);
            return (reader.IsDBNull(ordinal)) ? default(string) : reader.GetString(ordinal);
        }

    }
    
}
