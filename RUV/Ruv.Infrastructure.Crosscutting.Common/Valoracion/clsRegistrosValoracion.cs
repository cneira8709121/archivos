using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsRegistrosValoracion
    {
        public clsRegistrosValoracion()
        {
        }

        private int id;
        private int registroId;
        private int valoracionId;
        private List<int> regPersonas;
        private List<int> preguntas;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        [DataMember]
        public int RegistroId
        {
            get { return registroId; }
            set { registroId = value; }
        }
                
        [DataMember]
        public int ValoracionId
        {
            get { return valoracionId; }
            set { valoracionId = value; }
        }

        [DataMember]
        public List<int> RegPersonas
        {
            get { return regPersonas; }
            set { regPersonas = value; }
        }

        [DataMember]
        public List<int> Preguntas
        {
            get { return preguntas; }
            set { preguntas = value; }
        }

    }
}
