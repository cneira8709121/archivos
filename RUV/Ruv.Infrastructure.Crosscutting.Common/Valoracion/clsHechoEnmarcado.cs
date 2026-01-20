using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsHechoEnmarcado
    {
        #region Contructores
        public clsHechoEnmarcado()
        {

        }
        #endregion

        #region Atributos
        private int id;
        private string nombre;
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

        #endregion
    }
}
