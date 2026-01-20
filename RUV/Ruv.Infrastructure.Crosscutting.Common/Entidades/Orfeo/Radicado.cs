using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Orfeo
{
    [DataContract]
    public class Radicado
    {
        [DataMember]
        public int NTipoRadicado { get; set; }
        [DataMember]
        public int NDepartamentoRadicado { get; set; }
        [DataMember]
        public int NDepartamentoDestino { get; set; }
        [DataMember]
        public int NCodigoUsuario { get; set; }
        [DataMember]
        public int NCodigoUsuarioDestino { get; set; }
        [DataMember]
        public DateTime DFechaOficial { get; set; }
        [DataMember]
        public string CRadicadoEntrada { get; set; }
        [DataMember]
        public string CDescanex { get; set; }
        [DataMember]
        public string CAsunto { get; set; }
        [DataMember]
        public string CNRoofic { get; set; }
        [DataMember]
        public string CExpe { get; set; }
        [DataMember]
        public string CRadicado { get; set; }
    }
}
