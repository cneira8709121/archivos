using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    [DataContract]
    public class clsEntidadMunicipio
    {
        [DataMember]
        public long? NId { get; set; }
        [DataMember]
        public short? NIdEntidad { get; set; }
        [DataMember]
        public long? NIdMunicipio { get; set; }
        [DataMember]
        public string CNombreEntidad { get; set; }
        [DataMember]
        public string CNombreOtraEntidad { get; set; }
        [DataMember]
        public string CNombreEncargado { get; set; }
    }
}
