using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsNotificacionInterna
    {
        [Column(Name = "ID")]
        public int ID { get; set; }

        [Column(Name="FECHAGENERADO")]
        public DateTime dFechaGenerado {get; set;}

        [Column(Name="TEXTO")]
        public string cTexto { get; set; }

        [Column(Name="DESCRIPCION")]
        public string cDescripcion { get; set; }
    }
}
