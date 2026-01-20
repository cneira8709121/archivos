using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    /// <summary>
    /// Almacena todos los datos que puede contener una declaración sin valorar, que se muestra en la asignacion de valoraciones.
    /// </summary>
    [DataContract]
    public class clsDeclaracionValoraracion
    {

        #region CONSTRUCTOR

        public clsDeclaracionValoraracion()
        {

        }

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// El código de la declaración.
        /// </summary>
        [DataMember]
        public int? ID { get; set; }


        /// <summary>
        /// Nombre del declarante.
        /// </summary>
        [DataMember]
        public string NombreDeclarante { get; set; }


        /// <summary>
        /// Documento del declarante.
        /// </summary>
        [DataMember]
        public string DocumentoDeclarante { get; set; }


        /// <summary>
        /// Fecha de Radicación
        /// </summary>
        [DataMember]
        public DateTime FechaRadicado { get; set; }


        /// <summary>
        /// Numero de FOrmulario
        /// </summary>
        [DataMember]
        public string NumeroFormulario { get; set; }


        /// <summary>
        /// Tipo de Hecho Victimizante.
        /// </summary>
        [DataMember]
        public string HechoVictimizante { get; set; }


        /// <summary>
        /// TOtal Hv
        /// </summary>
        [DataMember]
        public int TotalHV { get; set; }


        /// <summary>
        /// Departamento
        /// </summary>
        [DataMember]
        public string Departamento { get; set; }


        /// <summary>
        /// Municipio
        /// </summary>
        [DataMember]
        public string Municipio { get; set; }


        /// <summary>
        /// Entidad
        /// </summary>
        [DataMember]
        public string Entidad { get; set; }

        /// <summary>
        /// Valorador
        /// </summary>
        [DataMember]
        public string Valorador { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [DataMember]
        public string Estado { get; set; }

        /// <summary>
        /// Regimen Especial
        /// </summary>
        [DataMember]
        public string RegimenEspecial { get; set; }

        /// <summary>
        /// Genero
        /// </summary>
        [DataMember]
        public string Genero { get; set; }

        /// <summary>
        /// Etnia
        /// </summary>
        [DataMember]
        public string Etnia { get; set; }

        /// <summary>
        /// Fecha de Declaracion
        /// </summary>
        [DataMember]
        public DateTime FechaDeclaracion { get; set; }

        /// <summary>
        /// Fecha Vencimiento
        /// </summary>
        [DataMember]
        public DateTime FechaVencimiento { get; set; }

        #endregion

    }
}
