using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion
{
    [DataContract]
    public class Persona
    {
        [DataMember]
        public int Id;

        [DataMember]
        public string PrimerNombre;

        [DataMember]
        public string SegundoNombre;

        [DataMember]
        public string PrimerApellido;

        [DataMember]
        public string SegundoApellido;

        [DataMember]
        public DateTime? FechaNacimiento;

        [DataMember]
        public string NumeroDocumento;
    }  
}
