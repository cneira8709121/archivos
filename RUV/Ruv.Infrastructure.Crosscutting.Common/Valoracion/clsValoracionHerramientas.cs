using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsValoracionHerramientas
    {
        #region Contructores

        public clsValoracionHerramientas()
        {
        }

        #endregion

        #region Atributos

        private int idAnexo;
        private int herramientaId;
        private string detalle;

        #endregion

        #region Propiedades
        
        [DataMember]
        public int IdAnexo
        {
            get { return idAnexo; }
            set { idAnexo = value; }
        }

        [DataMember]
        public int HerramientaId
        {
            get { return herramientaId; }
            set { herramientaId = value; }
        }

        [DataMember]
        public string Detalle
        {
            get { return detalle; }
            set { detalle = value; }
        }

        #endregion
    }
}
