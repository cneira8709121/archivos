using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    public class clsValidaciones
    {
        /// <summary>
        /// Author: John Henao
        /// Company: Globant
        /// Date: 04/09/2012
        /// Purpose: Create the class for validations
        /// </summary>
        [DataMember]
        public string NombreHoja { get; set; }
        [DataMember]
        public string Propiedad { get; set; }
        [DataMember]
        public eEstadoValidacion Valor { get; set; }	
    }
}
