using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario
{
    [DataContract]
    public class clsFormularioSolicitud
    {
        [DataMember]
        public uint NId { get; set; }
        [DataMember]
        public long? NIdPais { get; set; }
        [DataMember]
        public long? NIdDepartamento { get; set; }
        [DataMember]
        public long? NIdMunicipio { get; set; }
        [DataMember]
        public short? NIdEntidad { get; set; }
        [DataMember]
        public eEstadoFormulario EfId { get; set; }
        [DataMember]
        public uint NIdUsuario { get; set; }
        [DataMember]
        public string CNumeroFormulario { get; set; }
    }

    [DataContract]
    public class clsFormularioSolicitudNoRadicados
    {
        [DataMember]
        public long? NIdPais { get; set; }
        [DataMember]
        public long? NIdDepartamento { get; set; }
        [DataMember]
        public long? NIdMunicipio { get; set; }
        [DataMember]
        public short? NIdEntidad { get; set; }
        [DataMember]
        public string CNumeroFormulario { get; set; }
        [DataMember]
        public eAccionEnFormulario EAccion { get; set; }
    }

    [DataContract]
    public class clsSolicitudFormularioEstado
    {
        [DataMember]
        public int NIdUsuario { get; set; }
        [DataMember]
        public eEstadoFormulario? IdEstado { get; set; }
        [DataMember]
        public string CNumeroFormulario { get; set; }
        [DataMember]
        public int? NDesde { get; set; }
        [DataMember]
        public int? NHasta { get; set; }
        [DataMember]
        public DateTime? DGenerado { get; set; }
        [DataMember]
        public long? NIdPais { get; set; }
        [DataMember]
        public long? NIdDepartamento { get; set; }
        [DataMember]
        public long? NIdMunicipio { get; set; }
        [DataMember]
        public short? NIdEntidad { get; set; }
        [DataMember]
        public int NPagina { get; set; }
        [DataMember]
        public int NDatosPorPg { get; set; }
    }
}
