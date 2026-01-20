using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.Captura
{
    public class ExceptionRuv : Exception
    {
        public ExceptionRuv()
            : base()
        {

        }

        public ExceptionRuv(string message)
            : base(message)
        {

        }
    }
}
