using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsObservacionEstado
    {
        #region Contructores

        public clsObservacionEstado()
        {
        }

        #endregion

        #region MyRegion

        private int id;

        private int estadoId;

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
        public int EstadoId
        {
            get { return estadoId; }
            set { estadoId = value; }
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
