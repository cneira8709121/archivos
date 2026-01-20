using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Correcciones
{
    public class clsCargaDatosCorreccion
    {
        [Column(Name="PRIMERNOMBRE")]
        public string CPrimerNombre { get; set; }
        [Column(Name = "SEGUNDONOMBRE")]
        public string CSegundoNombre { get; set; }
        [Column(Name = "PRIMERAPELLIDO")]
        public string CPrimerApellido { get; set; }
        [Column(Name = "SEGUNDOAPELLIDO")]
        public string CSegundoApellido { get; set; }
        [Column(Name = "PARAM_TIPODOCUMENTO")]
        public int NTipoDocumento { get; set; }
        [Column(Name = "NUMERODOCUMENTO")]
        public string CNumeroDocumento { get; set; }
        [Column(Name = "FECHANACIMIENTO")]
        public DateTime DNacimiento { get; set; }
        [Column(Name = "PARAM_GENERO")]
        public int NGenero { get; set; }
        [Column(Name = "DISCAPACIDAD")]
        public string CDiscapacidades { get; set; }
        [Column(Name = "PARAM_ETNIAPERTENECE")]
        public int NEtnia { get; set; }
        [Column(Name = "PARAM_MINORIAETNICA")]
        public int NSubEtnia { get; set; }
        [Column(Name = "DIRECCION")]
        public string CDireccion { get; set; }
        [Column(Name = "EMAIL")]
        public string CCorreo { get; set; }
        [Column(Name = "TELEFONO")]
        public string CTelefono { get; set; }
    }
}
