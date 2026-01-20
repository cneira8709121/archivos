using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Devolucion;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Devolucion
{
    public class clsDevolucion
    {
        [Column(Name = "ID")]
        public int NId { get; set; }
        [Column(Name = "USUARIO")]
        public int NIdUsuario { get; set; }
        [Column(Name = "ESTADODECLANTERIOR")]
        public int? NEstadoDeclaracionAnterior { get; set; }
        [Column(Name = "FECHASOLICITUD")]
        public DateTime? DFechaSolicitud { get; set; }
        [Column(Name = "FECHADEVOLUCION")]
        public DateTime? DFechaDevolucion { get; set; }
        [Column(Name = "FECHAREGISTRO")]
        public DateTime? DFechaRadicacion { get; set; }
        [Column(Name = "ID_DECLARACION")]
        public int NIdDeclaracion { get; set; }
        [Column(Name = "ID_RADICACION")]
        public int NIdRadicacion { get; set; }
        [Column(Name = "PARTEEMOTIVAMOD")]
        public string CParteEmotiva { get; set; }
        [Column(Name = "OBSERVACOINDEVO")]
        public string CObservaciones { get; set; }
        [Column(Name = "PRIMER_NOMBRE")]
        public string CPrimerNombreDeclarante { get; set; }
        [Column(Name = "SEGUNDO_NOMBRE")]
        public string CSegundoNombreDeclarante { get; set; }
        [Column(Name = "PRIMER_APELLIDO")]
        public string CPrimerApellidoDeclarante { get; set; }
        [Column(Name = "SEGUNDO_APELLIDO")]
        public string CSegundoApellidoDeclarante { get; set; }
        [Column(Name = "NRO_FORMULARIO")]
        public string CNumeroFormulario { get; set; }
        [Column(Name = "NROGUIA")]
        public string CNumeroGuia { get; set; }
        [Column(Name = "ID_ENTIDADMUNICIPIO")]
        public int? NIdEntidadMunicipio { get; set; }
        [Column(Name = "DIRECCIONENTIDAD")]
        public string CDireccion { get; set; }
        [Column(Name = "TELEFONOENTIDAD")]
        public int NTelefono { get; set; }
        [Column(Name = "NOMBREFUNCIONARIO")]
        public string CFuncionario { get; set; }
        [Column(Name = "PAIS")]
        public string CPais { get; set; }
        [Column(Name = "DEPARTAMENTO")]
        public string CDepartamento { get; set; }
        [Column(Name = "MUNICIPIO")]
        public string CMunicipio { get; set; }
        [Column(Name = "ENTIDAD")]
        public string CEntidad { get; set; }
        public IList<int> IdsCausales { get; set; }
    }
}
