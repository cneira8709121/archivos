using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    /// <summary>
    /// Unidad básica equivalente a nu depto o mcpio.
    /// </summary>
    [DataContract]
    public class clsParametroPais
    {
        [DataMember]
        public Int64? Id { get; set; }

        /// <summary>
        /// El tipo del parámetro.
        /// </summary>
        [DataMember]
        public string Nombre { get; set; }

        /// <summary>
        /// Codigo Telefonico
        /// </summary>
        [DataMember]
        public Int32? CodigoTelefono { get; set; }

        /// <summary>
        /// Verificacion si el pais tiene representación
        /// </summary>
        [DataMember]
        public bool? TieneRepresentacion { get; set; }

    }

}
