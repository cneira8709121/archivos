using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador
{
    [DataContract]
    public class clsConsultarEstadoDeclaracionRespuesta
    {
        [DataMember]
        public List<EstadoDeclaracion> LstEstadoDeclaracion { get; set; }
    }

    [DataContract]
    public class EstadoDeclaracion
    {
        [DataMember]
        public int NIdRegistroPresona { get; set; }
        [DataMember]
        public int NIdDeclaracion { get; set; }
        [DataMember]
        public string CNumeroFormulario { get; set; }
        [DataMember]
        public string CEstadoProceso { get; set; }
        [DataMember]
        public DateTime DDeclaracion { get; set; }
        [DataMember]
        public string CPais { get; set; }
        [DataMember]
        public string CDepartamento { get; set; }
        [DataMember]
        public string CMunicipio { get; set; }
        [DataMember]
        public string CNombresApellidos { get; set; }
        [DataMember]
        public string CTipoDocumento { get; set; }
        [DataMember]
        public string CNumeroDocumento { get; set; }
        [DataMember]
        public string CTipoVictima { get; set; }
    }
}
