using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.GestionValorador;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.GestionValorador
{
    public class clsDetalleGestionVal
    {
        [Column(Name = "FECHAVALORACION")]
        public DateTime? DFechaDeclaracion { get; set; }
        [Column(Name = "DECLARACIONESVALORADAS")]
        public int? NDeclaracionesValoradas { get; set; }
    }
}
