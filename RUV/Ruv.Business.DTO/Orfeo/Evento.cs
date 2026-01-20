using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.Orfeo
{
    public class Evento
    {
        public int tiporad { get { return 2; } }
        public string numradicado { get; set; }
        public int deprad { get; set; }
        public int codiusu { get; set; }
        public int ttrcodi { get { return 2; } }
    }
}
