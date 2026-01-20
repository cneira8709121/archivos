using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsEntidadMunicipioNotificacion
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "ID")]
        public int Id { get; set; }

        [Column(Name = "ID_ENTIDAD")]
        public int IdEntidad { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "NOMBRE")]
        public string Nombre { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "ID_MUNICIPIO")]
        public int IdMunicipio { get; set; }
        /// <summary>
        ///
        /// </summary>
        [Column(Name = "DIRECCIONENTIDAD")]
        public string DireccionEntidad { get; set; }
    }
}
