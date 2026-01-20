using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsPersona
    {
        #region Constructor
        
        public clsPersona()
        {
        }

        #endregion
        
        #region Atributos

        private int id;
        private string persona;
        private string tipoDocumento;
        private string numeroDocumento;
        private string relacion;
        private int sexo;
        private string _generoNombre;
        private int edad;
        private int etnia;
        private string etniaNombre;
        private bool discapacitado;
        private string hechos;
        
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
        [Column(Name = "Nombre_Persona")]
        public string Persona
        {
            get { return persona; }
            set { persona = value; }
        }

        [DataMember]
        [Column(Name = "TIPO_DOCUMENTO")]
        public string TipoDocumento
        {
            get { return tipoDocumento; }
            set { tipoDocumento = value; }
        }

        [DataMember]
        [Column(Name = "NUMERODOCUMENTO")]
        public string NumeroDocumento
        {
            get { return numeroDocumento; }
            set { numeroDocumento = value; }
        }

        [DataMember]
        [Column(Name = "RELACION")]
        public string Relacion
        {
            get { return relacion; }
            set { relacion = value; }
        }

        [DataMember]
        public int Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        [DataMember]
        [Column(Name = "GENERO")]
        public string GeneroNombre
        {
            get { return _generoNombre; }
            set { _generoNombre = value; }
        }

        [DataMember]
        [Column(Name = "EDAD")]
        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        [DataMember]
        public int Etnia
        {
            get { return etnia; }
            set { etnia = value; }
        }

        [DataMember]
        [Column(Name = "ETNIA")]
        public string EtniaNombre
        {
            get { return etniaNombre; }
            set { etniaNombre = value; }
        }
        
        [DataMember]
        [Column(Name = "Es_Discapacitado")]
        public bool Discapacitado
        {
            get { return discapacitado; }
            set { discapacitado = value; }
        }

        [DataMember]
        [Column(Name = "Hechos")]
        public string Hechos
        {
            get { return hechos; }
            set { hechos = value; }
        }

        #endregion

    }
}
