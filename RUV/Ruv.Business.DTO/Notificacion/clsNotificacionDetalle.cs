using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsNotificacionDetalle
    {
        [Column(Name="ID")]
        public int nIdNotificacion { get; set; }

        [Column(Name="ID_DECLARACION")]
        public int nIdDeclaracion { get; set; }

        [Column(Name = "NUMEROFORMULARIO")]
        public string NumeroFormulario { get; set; }

        [Column(Name="IDESTADONOTIFICACION")]
        public int nIdEstadoNotificacion { get; set; }

        [Column(Name="ESTADONOTIFICACION")]
        public string cstadoNotificacion { get; set; }

        [Column(Name = "UBICACIONNOTIFICACION")]
        public string CUBICACIONNOTIFICACION { get; set; }

        [Column(Name="DIRECCIONNOTIFICACION")]
        public string cDireccionNotificacion { get; set; }

        [Column(Name = "DEPARTAMENTO")]
        public string cDepartamento { get; set; }

        [Column(Name = "MUNICIPIO")]
        public string cMunicipio { get; set; }

        [Column(Name = "PAIS")]
        public string cPais { get; set; }

        [Column(Name="TELEFONONOTIFICACION")]
        public string cTelefonoNotificacion { get; set; }

        [Column(Name="ID_USUARIO")]
        public int nIdUsuario { get; set; }

        [Column(Name="ID_PAQUETENOTIFICACION")]
        public int nIdPaqueteNotificacion { get; set; }

        [Column(Name="TIPODOCUMENTO")]
        public string cTipoDocumento { get; set; }

        [Column(Name = "DOCUMENTOIDENTIDAD")]
        public string cDocumentoIdentidad{ get; set; }

        [Column(Name="ESTADODECLARACION")]
        public string cEstadoDeclaracion { get; set; }

        [Column(Name="NOMBREDECLARANTE")]
        public string cNombreDeclarante { get; set; }

        [Column(Name = "APROBADO")]
        public bool Aprobado { get; set; }
    }
}
