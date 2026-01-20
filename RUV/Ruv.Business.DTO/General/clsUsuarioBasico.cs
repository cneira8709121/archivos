using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.General
{
    public class clsUsuarioBasico
    {
        [Column(Name = "ID")]
        public int ID { get; set; }
        [Column(Name = "IDENTIFICACION")]
        public string IDENTIFICACION { get; set; }
        [Column(Name = "USERNAME")]
        public string USERNAME { get; set; }
        [Column(Name = "ACTIVO")]
        public int ACTIVO { get; set; }
        [Column(Name = "CLAVE")]
        public string CLAVE { get; set; }
    }
}
