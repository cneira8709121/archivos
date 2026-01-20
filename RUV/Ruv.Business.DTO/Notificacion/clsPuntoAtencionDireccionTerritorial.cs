using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsPuntoAtencionDireccionTerritorial {
        
        [Column(Name = "ID")]
        public int Id { get; set; }

        [Column(Name = "HASHID")]
        public string HashId { get; set; }
        
        [Column(Name = "NOMBRE")]
        public string Nombre { get; set; }

        [Column(Name = "IDMUNICIPIO")]
        public int IdMunicipio { get; set; }

        [Column(Name = "DIRECCION")]
        public string Direccion { get; set; }
    }
}
