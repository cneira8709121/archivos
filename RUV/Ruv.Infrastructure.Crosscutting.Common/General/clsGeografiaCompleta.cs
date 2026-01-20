using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    [DataContract]
    public class clsGeografiaCompleta
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public int Tipo { get; set; }

        [DataMember]
        public int Padre { get; set; }
    }
}
