using System;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsNotificacionExcel
    {
        [Column(Name = "NOMBRE DESTINATARIO")]
        public string NombreDeclarante { get; set; }

        [Column(Name = "DIRECCION")]
        public string Direccion { get; set; }

        [Column(Name = "CIUDAD")]
        public string GeografiaNotificacionExcel { get; set; }
                
        [Column(Name = "PESO")]
        public string PesoSobre { get; set; }

        [Column(Name = "REFERENCIA")]
        public string Referencia { get; set; }

        [Column(Name = "CONTENIDO")]
        public string RelacionIdNotificacion { get; set; }

        [Column(Name = "OBSERVACIONES")]
        public string RelacionIdCodigoorfeo { get; set; }
    }
}
