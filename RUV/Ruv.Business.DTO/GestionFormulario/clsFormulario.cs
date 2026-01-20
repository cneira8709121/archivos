using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.GestionFormulario
{
    public class clsFormulario
    {
        #region Properties

        [Column(Name = "ID")]
        public uint NId { get; set; }
        [Column(Name = "ID_PAIS")]
        public long? NIdPais { get; set; }
        [Column(Name = "ID_DEPARTAMENTO")]
        public long? NIdDepartamento { get; set; }
        [Column(Name = "ID_MUNICIPIO")]
        public long? NIdMunicipio { get; set; }
        [Column(Name = "ID_ENTIDADMUNICIPIO")]
        public short? NIdEntidad { get; set; }
        [Column(Name = "ID_ESTADOIDFORMULARIO")]
        public ushort NIdEstado { get; set; }
        [Column(Name = "ID_USUARIO")]
        public uint NIdUsuario { get; set; }
        [Column(Name = "NUMEROFORMULARIO")]
        public string CNumeroFormulario { get; set; }
        [Column(Name = "NOMBREPAIS")]
        public string CPais { get; set; }
        [Column(Name = "NOMBREDEPARTAMENTO")]
        public string CDepartamento { get; set; }
        [Column(Name = "NOMBREMUNICIPIO")]
        public string CMunicipio { get; set; }
        [Column(Name = "NOMBREENTIDADMUNICIPIO")]
        public string CEntidad { get; set; }
        [Column(Name = "NOMBREESTADOIDFORMULARIO")]
        public string CEstado { get; set; }
        [Column(Name = "NOMBREUSUARIO")]
        public string CUsuario { get; set; }
        [Column(Name = "DESCARGADO")]
        public bool BDescargado { get; set; }
        [Column(Name = "ACCION")]
        public int Accion { get; set; }
        [Column(Name = "DGENERADO")]
        public DateTime DGenerado { get; set; }
        [Column(Name = "DULTIMAMOD")]
        public DateTime DUltimaModificacion { get; set; }
        [Column(Name = "OBSERVACION")]
        public string CObservacion { get; set; }

        #endregion
    }
}
