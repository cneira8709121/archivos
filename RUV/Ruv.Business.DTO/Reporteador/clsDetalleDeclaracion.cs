using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Reporteador
{
    public class clsDetalleDeclaracion
    {
        [Column(Name = "NUMEROFORMULARIO")]
        public string CNumeroFormulario { get; set; }
        [Column(Name = "ANEXOID")]
        public int nAnexoId { get; set; }
        [Column(Name = "TIPOANEXO")]
        public int nTipoAnexo { get; set; }
        [Column(Name = "ID_SINIESTRO")]
        public int nIdSiniestro { get; set; }
        [Column(Name = "NombreDeclarante")]
        public string CNombresApellidosDeclarante { get; set; }
        [Column(Name = "TipoDocumento")]
        public string CTipoDocumentoDeclarante { get; set; }
        [Column(Name = "DocumentoIdentidad")]
        public string CDocumentoDeclarante { get; set; }
        [Column(Name = "EstadoActualProceso")]
        public string CEstadoActualProceso { get; set; }
        [Column(Name = "IDEstadoProceso")]
        public int nIdEstadoProceso { get; set; }
        [Column(Name = "EstadoValoracion")]
        public string CEstadoValoracion { get; set; }
        [Column(Name = "FechaValoracion")]
        public DateTime? DValoracion { get; set; }
        [Column(Name = "estado")]
        public string CResultadoValoracion { get; set; }
        [Column(Name = "NOMBRE_HECHO_VICTIMIZANTE")]
        public string CHechoVictimizante { get; set; }
        [Column(Name = "FECHAHECHOS")]
        public DateTime? DHecho { get; set; }
        [Column(Name = "NombreVictima")]
        public string CNombresApellidosVictima { get; set; }
        [Column(Name = "TIPODOCUMENTO_VICTIMA")]
        public string CTipoDocumentoVictima { get; set; }
        [Column(Name = "DocumentoVictima")]
        public string CDocumentoVictima { get; set; }
        [Column(Name = "TIPO_VICTIMA")]
        public string CTipoVictima { get; set; }
        [Column(Name = "MARCA")]
        public string CMarca { get; set; }

    }
}
