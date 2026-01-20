using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Radicacion
{
    public class clsRadicacion
    {
        [Column(Name = "FECHALLEGADA")]
        public DateTime DLlegada { get; set; }
        [Column(Name = "ID")]
        public int? NId { get; set; }
        [Column(Name = "ID_TIPO_RADICACION")]
        public int? NTipoRadicacion { get; set; }
        [Column(Name = "PARAM_RESULTADO_VALIDACION")]
        public int? NTipoError { get; set; }
        [Column(Name = "PARAM_TIPODOCUMENTO")]
        public int? NTipoDocumento { get; set; }
        public int? NIdUsuarioRadica { get; set; }
        public long? NIdPais { get; set; }
        public long? NIdDepartamento { get; set; }
        public long? NIdMunicipio { get; set; }
        [Column(Name = "ID_ENTIDADMUNICIPIO")]
        public short? NIdEntidad { get; set; }
        [Column(Name = "NRO_FORMULARIO")]
        public string CNumeroFormulario { get; set; }
        [Column(Name = "NUMERODOCUMENTO")]
        public string CNumeroDocumento { get; set; }
        [Column(Name = "PRIMERNOMBRE")]
        public string CPrimerNombre { get; set; }
        [Column(Name = "SEGUNDONOMBRE")]
        public string CSegundoNombre { get; set; }
        [Column(Name = "PRIMERAPELLIDO")]
        public string CPrimerApellido { get; set; }
        [Column(Name = "SEGUNDOAPELLIDO")]
        public string CSegundoApellido { get; set; }
        [Column(Name = "OBSERVACIONES")]
        public string CObservaciones { get; set; }
        [Column(Name = "RUTAIMAGEN")]
        public string CRutaImagen { get; set; }
    }
}
