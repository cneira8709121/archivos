using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Orfeo
{
    [DataContract]
    public class Direccion
    {
        [DataMember]
        public int tipdesrem { get; set; }
        [DataMember]
        public string coddir { get; set; }
        [DataMember]
        public string dirnombre { get; set; }
    }
}
