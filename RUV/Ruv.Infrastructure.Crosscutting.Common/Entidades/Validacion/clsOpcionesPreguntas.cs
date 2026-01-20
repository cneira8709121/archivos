using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion
{
    [DataContract]
    public class clsOpcionesPreguntas
    {
        public clsOpcionesPreguntas() { }
        private string posibleOpcion;
        [DataMember]
        public string PosibleOpcion
        {
            get { return posibleOpcion; }
            set { posibleOpcion = value; }
        }

        private bool valida;
        [DataMember]
        public bool Valida
        {
            get { return valida; }
            set { valida = value; }
        }

        private string pregunta;
        [DataMember]
        public string Pregunta
        {
            get { return pregunta; }
            set { pregunta = value; }
        }

        private bool respuesta;
        [DataMember]
        public bool Respuesta
        {
            get { return respuesta; }
            set { respuesta = value; }
        }

    }
}
