using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Valoracion
{
    public class clsSubEtniasdto
    {
        [Column(Name = "ID")]
        public int NId { get; set; }
        [Column(Name = "ETNIAGRUPOID")]
        public int NEtniaGrupoId { get; set; }
        [Column(Name = "NOMBRE")]
        public string cNombre { get; set; }
        [Column(Name = "NUMERO")]
        public int NNumero { get; set; }
    }
}
