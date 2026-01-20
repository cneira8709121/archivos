using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsEstadosNotificacion
    {
        [Column(Name = "ID")]
        public int nIdEstado { get; set; }

        [Column(Name = "NOMBRE")]
        public string cNombre { get; set; }
    }
}
