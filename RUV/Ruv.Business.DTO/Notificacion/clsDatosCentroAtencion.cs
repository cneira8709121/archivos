using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsDatosCentroAtencion
    {
        [Column(Name="IDCENTRO")]
        public int nIdCentro { get; set; }
        [Column(Name="CANTIDADASIGNADA")]
        public int nCantidadNotificaciones { get; set; }
        [Column(Name="NOMBRE")]
        public string cNombreCentroAtencion { get; set; }
        [Column(Name="MUNICIPIO")]
        public string cNombreMunicipio { get; set; }
        [Column(Name="DEPARTAMENTO")]
        public string cNombreDepartamento { get; set; }
        [Column(Name="PAIS")]
        public string cNombrePais { get; set; }
        [Column(Name="tipo")]
        public int nTipo { get; set; }
    }
}
