using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsAutores
    {

        #region Contructores

        public clsAutores()
        { }

        #endregion

        #region Atributos

        private int id;
        private string nombre;
        private DateTime? fechaCreacion;
        private DateTime? fechaDesmovilizacion;

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

        [DataMember]
        [Column(Name = "FECHA_CREACION")]
        public DateTime? FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }

        [DataMember]
        [Column(Name = "FECHA_DESMOVILIZACION")]
        public DateTime? FechaDesmovilizacion
        {
            get { return fechaDesmovilizacion; }
            set { fechaDesmovilizacion = value; }
        }

        #endregion
    }
}
