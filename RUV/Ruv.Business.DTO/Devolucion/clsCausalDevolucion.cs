using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Devolucion
{
    public class clsCausalDevolucion
    {
        [Column (Name="ID")]
        public int nId { get; set; }

        [Column (Name="NOMBRECAUSAL")]
        public string cNombre { get; set; }

        [Column (Name="PARTEEMOTIVA")]
        public string cParteEmotiva { get; set; }

        [Column (Name="TIPO")]
        public int nTipo { get; set; }
    }
}
