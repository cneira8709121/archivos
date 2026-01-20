using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsInfracciones
    {
        #region Contructores

        public clsInfracciones()
        { }

        #endregion

        #region Atributos

        private int id;
        private string nombre;

        #endregion

        #region Propiedades
        
        [DataMember]
        [Column(Name = "ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        [Column(Name = "NOMBRE")]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        #endregion
    }
}
