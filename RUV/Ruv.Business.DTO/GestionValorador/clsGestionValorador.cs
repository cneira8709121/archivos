using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.GestionValorador;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.GestionValorador
{
    public class clsGestionValorador
    {
        [Column(Name = "USUARIO")]
        public string CNombreUsuario { get; set; }
        [Column(Name = "ID_VALORADOR")]
        public int? NIdValorador { get; set; }
        [Column(Name = "PROMEDIO DE TIEMPO VALORACION")]
        public float? NPromedioValoracion { get; set; }
        [Column(Name = "VALORACIONDEVUELTA")]
        public int? NVALORACIONDEVUELTA { get; set; }
        [Column(Name = "VALORACIONFINALIZADA")]
        public int? NVALORACIONFINALIZADA { get; set; }
        [Column(Name = "VALORACIONENPROCESO")]
        public int? NVALORACIONENPROCESO { get; set; }
        [Column(Name = "VALORACIONASIGNADA")]
        public int? NVALORACIONASIGNADA { get; set; }
        [Column(Name = "VALORACIONDEVUELTAASIGNACION")]
        public int? NValoracionDevuelAsig { get; set; }
    
           
    }

}
