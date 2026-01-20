using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsHerramientas
    {
        #region Contructores

        public clsHerramientas()
        {
        }

        #endregion

        #region Atributos

        private int id;
        private string nombre;
        private int tipoId;
        private clsTipoHerramienta tipo;
        

        #endregion

        #region Propiedades

        [DataMember]
        public clsTipoHerramienta Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
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
        public int TipoId
        {
            get { return tipoId; }
            set { tipoId = value; }
        }

        

        #endregion
    }
}
