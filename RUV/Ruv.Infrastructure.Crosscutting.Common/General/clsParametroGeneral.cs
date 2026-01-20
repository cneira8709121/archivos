using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    /// <summary>
    /// Unidad básica equivalente a un parámetro de la tabla
    /// TBParametros.
    /// </summary>
    [DataContract]
    public class clsParametroGeneral
    {
        //CAMBIO: int?.
        [DataMember]
        public int? Id { get; set; }
        /// <summary>
        /// El tipo del parámetro.
        /// </summary>
        [DataMember]
        public eTipoParametros Tipo { get; set; }
        /// <summary>
        /// Descripción del parámetro.
        /// </summary>
        [DataMember]
        public string Nombre { get; set; }
        /// <summary>
        /// Verdadero: Este parámetro debe ser tratado como la opción "Otro".
        /// </summary>
        [DataMember]
        public bool EsOtro { get; set; }
        /// <summary>
        /// El número del parámetro, dentro de su tipo.
        /// </summary>
        [DataMember]
        public int Numero { get; set; }
        /// <summary>
        /// El valor de la tabla Extendidos.
        /// </summary>
        [DataMember]
        public string Valor { get; set; }
        /// <summary>
        /// El campo Activo de la tabla extendidos.
        /// </summary>
        [DataMember]
        public bool Activo { get; set; }


    }
}
