using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Valoracion
{
    public class clsValoracionHistorico
    {
        [Column(Name = "ID")]
        public int nId { get; set; }

        [Column(Name = "OBSERVACION")]
        public string cObservacion { get; set; }

        [Column(Name = "USUARIO")]
        public string nUsuario { get; set; }

        [Column(Name = "IDVALORACION")]
        public int nIdValoracion { get; set; }

        [Column(Name = "MOTIVACION")]
        public string cValoracion { get; set; }

        [Column(Name = "FECHAACTUALIZACION")]
        public DateTime? dFechaActualizacion { get; set; }
    }
}
