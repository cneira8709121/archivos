using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    [DataContract]
    public class clsPuntoNotificacion
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string HashId { get; set; }

        public string Type { get { return HashId.StartsWith("PA") ? "Punto de Atención" : "Dirección Territorial"; } }

        [DataMember]
        public int IdMunicipio { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Direccion { get; set; }

    }
}
