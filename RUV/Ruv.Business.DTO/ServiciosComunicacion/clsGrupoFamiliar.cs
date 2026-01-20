using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.ServiciosComunicacion
{
    public class clsGrupoFamiliar
    {
        [Column(Name = "IDDECLARACION")]
        public int IdDeclaracion { get; set; }

        [Column(Name = "IDPERSONA")]
        public int IdPersona { get; set; }

        [Column(Name = "NOMBREPERSONA")]
        public string NombrePersona { get; set; }

        [Column(Name = "FECHANACIMIENTO")]
        public DateTime FechaNacimiento { get; set; }

        [Column(Name = "PARENTESCO")]
        public string Parentesco { get; set; }
    }
}
