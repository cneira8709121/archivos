using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Orfeo
{
    [DataContract]
    public class Dignatario
    {
        [DataMember]
        public int NTipoRadicado { get; set; }
        [DataMember]
        public string CNombreDeclarante { get; set; }
        [DataMember]
        public string CPrimerApellido { get; set; }
        [DataMember]
        public string CSegundoApellido { get; set; }
        [DataMember]
        public string CCedula { get; set; }
        [DataMember]
        public string CDireccion { get; set; }
        [DataMember]
        public string CTelefono { get; set; }
        [DataMember]
        public string CEntidad { get; set; }
        [DataMember]
        public int NIdDepartamento { get; set; }
        [DataMember]
        public int NIdMunicipio { get; set; }
        [DataMember]
        public string CEmail { get; set; }
    }
}
