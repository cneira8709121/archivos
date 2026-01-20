using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Feriado
{
    public class Feriado
    {
        [Column(Name = "ID")]
        public int Id { get; set; }

        [Column(Name = "DIA")]
        public string Dia { get; set; }

        [Column(Name = "MES")]
        public string Mes { get; set; }

        [Column(Name = "ANO")]
        public string Ano { get; set; }

        [Column(Name = "FECHA")]
        public DateTime fecha { get; set; }

        [Column(Name = "NOMBRE")]
        public string Nombre { get; set; }

        [Column(Name = "COMENTARIO")]
        public string Comentario { get; set; }

        [Column(Name = "RECURRENTE")]
        public bool Recurrente { get; set; }
    }
}
