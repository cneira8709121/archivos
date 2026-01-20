using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Orfeo
{
    [DataContract]
    public class Evento
    {
        [DataMember]
        public int tiporad { get; set; }
        [DataMember]
        public int deprad { get; set; }
        [DataMember]
        public int codiusu { get; set; }
        [DataMember]
        public int ttrcodi { get; set; }
    }
}
