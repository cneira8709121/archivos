using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario
{
    [DataContract]
    public class clsSeparacionFormularioSolicitud
    {
        [DataMember]
        public string CNumeroFormulario { get; set; }
        [DataMember]
        public uint NIdUsuario { get; set; }
    }
}
