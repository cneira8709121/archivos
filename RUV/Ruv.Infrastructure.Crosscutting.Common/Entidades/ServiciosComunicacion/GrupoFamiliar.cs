using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion
{
    public class GrupoFamiliar
    {
        [DataMember]	
        public int IdDeclaracion;

        [DataMember]	
        public int IdPersona;

        [DataMember]	
        public string NombrePersona;

        [DataMember]	
        public DateTime FechaNacimiento;

        [DataMember]
        public string Parentesco;
    }
}
