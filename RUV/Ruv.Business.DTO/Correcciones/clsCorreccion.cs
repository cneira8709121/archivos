using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Correcciones
{
    public class clsCorreccion
    {
        [Column(Name = "CAMPO")]
        public int Campo { get; set; }
        [Column(Name = "VALOR")]
        public string Valor { get; set; }
    }
}
