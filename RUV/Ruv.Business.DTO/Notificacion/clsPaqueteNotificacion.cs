using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsPaqueteNotificacion
    {
        [Column(Name = "ID")]
        public int Id { get; set; }

        [Column(Name = "FECHA")]
        public DateTime Fecha { get; set; }

        [Column(Name = "ORDENSERVICIO")]
        public string OrdenServicio { get; set; }

        [Column(Name = "CANTIDAD")]
        public string Cantidad { get; set; }

        [Column(Name = "RESUMEN")]
        public string Resumen { get; set; }

        [Column(Name = "NOMBRE")]
        public string NombreUsuario { get; set; }

    }
}
