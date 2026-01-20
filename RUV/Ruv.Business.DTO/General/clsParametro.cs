using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.General
{
    public class clsParametro
    {
        [Column(Name = "ID")]
        public int Id { get; set; }

        [Column(Name = "NOMBRE")]
        public string Nombre { get; set; }
    }
}
