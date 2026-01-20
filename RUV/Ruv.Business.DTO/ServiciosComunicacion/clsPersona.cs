using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.ServiciosComunicacion
{
    public class clsPersona
    {
        #region Properties

        [Column(Name = "ID")]
        public int Id { get; set; }

        [Column(Name = "PRIMERNOMBRE")]
        public string PrimerNombre { get; set; }

        [Column(Name = "SEGUNDONOMBRE")]
        public string SegundoNombre { get; set; }

        [Column(Name = "PRIMERAPELLIDO")]
        public string PrimerApellido { get; set; }

        [Column(Name = "SEGUNDOAPELLIDO")]
        public string SegundoApellido { get; set; }

        [Column(Name = "FECHANACIMIENTO")]
        public DateTime? FechaNacimiento { get; set; }

        [Column(Name = "NUMERODOCUMENTO")]
        public string NumeroDocumento { get; set; }

        #endregion
    }
}
