using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsHerramietasOrganizar
    {
        private int hechoId;

        private int personaId;

        private List<clsHerramientaAnexoPer> herramientas;


        [DataMember]
        public int PersonaId
        {
            get { return personaId; }
            set { personaId = value; }
        }
        [DataMember]
        public List<clsHerramientaAnexoPer> Herramientas
        {
            get { return herramientas; }
            set { herramientas = value; }
        }

    }
}
