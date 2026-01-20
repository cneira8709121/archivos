using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Devolucion;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Devolucion
{
    public class clsDatosparaDevolucion
    {
        [Column(Name = "ID_DECLARACION")]
        public int NIdDeclaracion { get; set; }
        [Column(Name = "NOMBREENTIDAD")]
        public string cEntidadMunicipio { get; set; }
        [Column(Name = "NOMBREMUNICIPIO")]
        public string cMunicipio { get; set; }
        [Column(Name = "PARTEEMOTIVAMOD")]
        public string CParteEmotiva { get; set; }  
        [Column(Name = "SYSDATE")]
        public DateTime? DFechaDevolucion { get; set; }
        [Column(Name = "NOMBREDECLARANTE")]
        public string CNombreDeclarante { get; set; }
        [Column(Name = "TIPODOCUMENTO")]
        public string cTipoDocumento { get; set; }
        [Column(Name = "NUMERODOCUMENTO")]
        public int nNumeroDocumento { get; set; }
        [Column(Name = "FECHADECLARACION")]
        public DateTime? DFechaDeclaracion { get; set; }
        [Column(Name="CONSECUTIVO")]
        public string cNumeroActoAdmin { get; set; }
        

    }
}
