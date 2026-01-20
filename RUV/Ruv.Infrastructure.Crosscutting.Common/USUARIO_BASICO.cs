using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    public class USUARIO_BASICO
    {
        public int ID { get; set; }
        public string IDENTIFICACION { get; set; }
        public string USERNAME { get; set; }
        public int ACTIVO { get; set; }
        public string CLAVE { get; set; }
    }
}
