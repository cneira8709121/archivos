using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    [DataContract]
    public class clsSubEtnias
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int EtniaGrupoId { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public int Numero { get; set; }
    }
}
