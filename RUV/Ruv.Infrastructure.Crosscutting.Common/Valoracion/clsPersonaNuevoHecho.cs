using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsPersonaNuevoHecho
    {
        private int personaId;
        
        [DataMember]
        public int PersonaId
        {
            get { return personaId; }
            set { personaId = value; }
        }
        private bool victima1;

        [DataMember]
        public bool Victima1
        {
            get { return victima1; }
            set { victima1 = value; }
        }
        private int estadoEnHecho;

        [DataMember]
        public int EstadoEnHecho
        {
            get { return estadoEnHecho; }
            set { estadoEnHecho = value; }
        }
    }
}
