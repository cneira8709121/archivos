using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.Orfeo
{
    public class Direccion
    {
        public int tipdesrem { get { return 2; } }
        public string coddir { get; set; }
        public string numradicado { get; set; }
        public string direccion { get; set; }
        public string dirtelefono { get; set; }
        public string dirnombre { get; set; }
        public int coddpto { get; set; }
        public int codmpio { get; set; }
    }
}
