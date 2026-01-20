using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador
{
    [DataContract]
    public class clsConsultarEstadoDeclaracionSolicitud
    {
        [DataMember]
        public string CNumeroDocumento { get; set; }
        [DataMember]
        public string CPrimerNombre { get; set; }
        [DataMember]
        public string CPrimerApellido { get; set; }
        [DataMember]
        public string CNumeroFormulario { get; set; }

    }
}
