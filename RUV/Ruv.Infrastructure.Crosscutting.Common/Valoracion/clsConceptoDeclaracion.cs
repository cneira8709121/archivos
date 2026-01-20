using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsConceptoDeclaracion
    {
        [DataMember]
        [Column(Name = "ID")]
        public int Id { get; set; }

        [DataMember]
        [Column(Name = "ID_DECLARACION")]
        public int Id_Declaracion { get; set; }

        [DataMember]
        [Column(Name = "ID_CONCEPTO")]
        public int Id_Concepto { get; set; }

        [DataMember]
        [Column(Name = "FECHA")]
        public DateTime Fecha { get; set; }
    }
}
