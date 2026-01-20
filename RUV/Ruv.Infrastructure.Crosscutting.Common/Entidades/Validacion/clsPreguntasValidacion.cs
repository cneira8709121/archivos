using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion
{
    [DataContract]
    public class clsPreguntasValidacion
    {
        public clsPreguntasValidacion() { }
        private string pregunta;
        [DataMember]
        public string Pregunta
        {
            get { return pregunta; }
            set { pregunta = value; }
        }
        private List<clsOpcionesPreguntas> opcionesPreguntas;
        [DataMember]
        public List<clsOpcionesPreguntas> OpcionesPreguntas
        {
            get { return opcionesPreguntas; }
            set { opcionesPreguntas = value; }
        }

    }
}
