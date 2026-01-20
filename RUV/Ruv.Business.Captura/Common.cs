using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.Captura
{
    public class Common
    {
        public static short? ShortNull { get { return null; } }

        public static object ThrowException(string message)
        {
            throw new ExceptionRuv(message);
        }

        public static short? ParseIntToShortNullable(int? value)
        {
            if (value.HasValue)
                return (short)value;
            return null;
        }

        internal static short? ParseIntToShortNullable(Ruv.Infrastructure.Crosscutting.Common.eEstadoRegistro eEstadoRegistro)
        {
            throw new NotImplementedException();
        }

        public static int? ParseIntToIntNullable(int? value)
        {
            if (value.HasValue)
                return (int)value;
            return null;
        }

        public static long? ParseIntToLongNullable(Int64? value)
        {
            if (value.HasValue)
                return (long)value;
            return null;
        }

        public static short? ParseStringToShortNullable(string value)
        {
            short result = 0;
            if (!string.IsNullOrWhiteSpace(value))
            {
                Int16.TryParse(value, out result);
            }
            if(result > 0)   
                return result;
            else
                return null;
        }
        public static int ParseStringToIntNullable(string value)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(value))
            {
                Int32.TryParse(value, out result);
            }
            if (result > 0)
                return result;
            else
                return 0;
        }


        public enum eTiposAnexo
        {
            Anexo01 = 1,
            Anexo02,
            Anexo03,
            Anexo04,
            Anexo05,
            Anexo06,
            Anexo07,
            Anexo08,
            Anexo09,
            Anexo10,
            Anexo11,
            Anexo12,
            Anexo13
        }

        public enum eRelacion
        {
            JEFE_HOGAR = 143,
            None = 0
        }
    }
}
