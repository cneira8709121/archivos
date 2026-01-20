using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;


namespace Ruv.Business.DTO.Notificacion
{
    public class clsDetalleDatosCentrosAtencion
    {
        [Column(Name="DIRECCIONNOTIFICACION")]
        public string cDireccionNotifica { get; set; }
        [Column(Name="TELEFONONOTIFICACION")]
        public string cTelefononotifica { get; set; }
        [Column(Name="ESTADOCOURIER")]
        public string cEstadoCourier { get; set; }
        [Column(Name="FECHAFINAL")]
        public DateTime dFechafinalNotifica { get; set; }
        [Column(Name = "NOMBRE")]
        public string cNombreEstado { get; set; }
        [Column(Name="IDCODIGOGUIA")]
        public string cIdCodigoGuia { get; set; }
    }
}
