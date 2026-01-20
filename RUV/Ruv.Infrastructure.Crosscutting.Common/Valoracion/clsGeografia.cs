using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsGeografia
    {
        #region Cosntructores
        public clsGeografia() { }
        #endregion
        #region Atributos

        private int id;
        private string nombre;
        private int tipo;
        private int padre;

        

        #endregion

        #region Propiedades

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        [DataMember]
        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [DataMember]
        public int Padre
        {
            get { return padre; }
            set { padre = value; }
        }

        #endregion

        
    }
}
