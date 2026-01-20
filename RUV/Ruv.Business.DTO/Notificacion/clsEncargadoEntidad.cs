using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsEncargadoEntidad
    {
        [Column(Name="ID")]
        public int nIdEncargado { get; set; }

        [Column(Name="NOMBRE")]
        public string cNombre { get; set; }

        [Column(Name="CARGO")]
        public string cCargo { get; set; }

        [Column(Name="DIRECCION")]
        public string cDireccion { get; set; }

        [Column(Name="TELEFONO")]
        public string cTelefono { get; set; }
    }
}
