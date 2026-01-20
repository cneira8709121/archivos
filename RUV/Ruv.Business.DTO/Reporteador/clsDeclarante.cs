using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Reporteador
{
    public class clsDeclarante
    {
        [Column(Name = "NIDREGISTROPRESONA")]
        public int NIdRegistroPresona { get; set; }
        [Column(Name = "id")]
        public int NIdDeclaracion { get; set; }
        [Column(Name = "NUMEROFORMULARIO")]
        public string CNumeroFormulario { get; set; }
        [Column(Name = "ESTADOPROCESO")]
        public string CEstadoProceso { get; set; }
        [Column(Name = "FECHADECLARACION")]
        public DateTime DDeclaracion { get; set; }
        // TODO: jairovg - Falta el país en la consulta
        [Column(Name = "PAIS")]
        public string CPais { get; set; }
        [Column(Name = "departamento")]
        public string CDepartamento { get; set; }
        [Column(Name = "municipio")]
        public string CMunicipio { get; set; }
        [Column(Name = "PRIMERNOMBRE")]
        public string CPrimerNombre { get; set; }
        [Column(Name = "SEGUNDONOMBRE")]
        public string CSegundoNombre { get; set; }
        [Column(Name = "PRIMERAPELLIDO")]
        public string CPrimerApellido { get; set; }
        [Column(Name = "SEGUNDOAPELLIDO")]
        public string CSegundoApellido { get; set; }
        [Column(Name = "TIPODOCUMENTO")]
        public string CTipoDocumento { get; set; }
        [Column(Name = "NUMERODOCUMENTO")]
        public string CNumeroDocumento { get; set; }


    }
}
