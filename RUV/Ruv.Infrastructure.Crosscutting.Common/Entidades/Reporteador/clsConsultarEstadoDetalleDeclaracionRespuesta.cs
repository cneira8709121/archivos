using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador
{
    [DataContract]
    public class clsConsultarEstadoDetalleDeclaracionRespuesta
    {
        [DataMember]
        public List<DetalleDeclaracion> LstDetalleDeclaracion { get; set; }
    }

    [DataContract]
    public class DetalleDeclaracion
	{
        [DataMember]
        public string CNumeroFormulario { get; set; }
        [DataMember]
        public int nAnexoId { get; set; }
        [DataMember]
        public int nTipoAnexo { get; set; }
        [DataMember]
        public int nIdSiniestro { get; set; }
        [DataMember]
        public string CNombresApellidosDeclarante { get; set; }
        [DataMember]
        public string CTipoDocumentoDeclarante { get; set; }
        [DataMember]
        public string CDocumentoDeclarante { get; set; }
        [DataMember]
        public string CEstadoActualProceso { get; set; }
        [DataMember]
        public int nIdEstadoProceso { get; set; }
        [DataMember]
        public string CEstadoValoracion { get; set; }
        [DataMember]
        public DateTime? DValoracion { get; set; }
        [DataMember]
        public string CResultadoValoracion { get; set; }
        [DataMember]
        public DateTime? DHecho { get; set; }
        [DataMember]
        public string CHechoVictimizante { get; set; }
        [DataMember]
        public string CNombresApellidosVictima { get; set; }
        [DataMember]
        public string CTipoDocumentoVictima { get; set; }
        [DataMember]
        public string CDocumentoVictima { get; set; }
        [DataMember]
        public string CEstadoActualProcesotooltip { get; set; }
        [DataMember]
        public string CTipoVictima { get; set; }
        [DataMember]
        public string CMarca { get; set; }
    }
}
