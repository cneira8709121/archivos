using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    [DataContract]
    public class clsParametroNacionalidad
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nacionalidad { get; set; }
        [DataMember]
        public string CodNacionalidad { get; set; }
    }
}
