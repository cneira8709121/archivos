using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsPreguntasRegAnt
    {
        #region Contructores

        public clsPreguntasRegAnt()
        {
        }

        #endregion

        #region Atributos

        private int id;
        private string pregunta;

        #endregion

        #region Propiedades
        
        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Pregunta
        {
            get { return pregunta; }
            set { pregunta = value; }
        }

         
        #endregion
    }
}
