using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsNotificacion
    {
        [Column(Name = "ID")]
        public int ID { get; set; }

        [Column(Name = "ID_DECLARACION")]
        public string ID_DECLARACION { get; set; }

        [Column(Name = "NUMEROFORMULARIO")]
        public string NumeroFormulario { get; set; } 

        [Column(Name = "ID_VALORACION")]
        public string ID_VALORACION { get; set; }

        [Column(Name = "ID_ESTADONOTIFICACION")]
        public int ID_ESTADONOTIFICACION { get; set; }

        [Column(Name = "CODIGOESTADONOTIFICACION")]
        public int CODIGOESTADONOTIFICACION { get; set; }

        [Column(Name = "ESTADONOTIFICACION")]
        public string ESTADONOTIFICACION { get; set; }

        [Column(Name = "ESTADOCOURIER")]
        public string ESTADOCOURIER { get; set; }

        [Column(Name = "DFIRMA")]
        public DateTime? FECHAFIRMA { get; set; }

        [Column(Name = "UBICACIONNOTIFICACION")]
        public string UBICACIONNOTIFICACION { get; set; }

        [Column(Name = "ID_PAISPUNTO")]
        public int? ID_PAISPUNTO { get; set; }

        [Column(Name = "ID_DEPARTAMENTOPUNTO")]
        public int? ID_DEPARTAMENTOPUNTO { get; set; }

        [Column(Name = "ID_MUNICIPIOPUNTO")]
        public int? ID_MUNICIPIOPUNTO { get; set; }

        [Column(Name = "ID_PUNTOATENCION")]
        public int? ID_PUNTOATENCION { get; set; }

        [Column(Name = "ID_DIRECCIONTERRITORIAL")]
        public int? ID_DIRECCIONTERRITORIAL { get; set; }

        [Column(Name = "ID_UBICACIONNOTIFICACION")]
        public int? ID_UBICACIONNOTIFICACION { get; set; }

        [Column(Name = "DIRECCIONNOTIFICACION")]
        public string DIRECCIONNOTIFICACION { get; set; }

        [Column(Name = "TELEFONONOTIFICACION")]
        public string TELEFONONOTIFICACION { get; set; }

        [Column(Name = "ID_USUARIO")]
        public int ID_USUARIO { get; set; }

        [Column(Name = "ID_PAQUETENOTIFICACION")]
        public int? ID_PAQUETENOTIFICACION { get; set; }

        [Column(Name = "NOMBRECOMPLETO")]
        public string NOMBRECOMPLETO { get; set; }

        [Column(Name = "TIPODOCUMENTO")]
        public string TIPODOCUMENTO { get; set; }

        [Column(Name = "NUMERODOCUMENTO")]
        public string NUMERODOCUMENTO { get; set; }

        [Column(Name = "ESTADOPROCESO")]
        public string ESTADOPROCESO { get; set; }

        [Column(Name = "ID_DEPARTAMENTO")]
        public int ID_DEPARTAMENTO { get; set; }

        [Column(Name = "NOMBREDEPARTAMENTO")]
        public string NOMBREDEPARTAMENTO { get; set; }

        [Column(Name = "NOMBREDEPARTAMENTOALTERNO")]
        public string NombreDepartamentoAlterno { get; set; }

        [Column(Name = "ID_MUNICIPIO")]
        public int ID_MUNICIPIO { get; set; }

        [Column(Name = "NOMBREMUNICIPIO")]
        public string NOMBREMUNICIPIO { get; set; }

        [Column(Name = "NOMBREMUNICIPIOALTERNO")]
        public string NombreMunicipioAlterno { get; set; }

        [Column(Name = "ID_PAIS")]
        public int ID_PAIS { get; set; }

        [Column(Name = "NOMBREPAIS")]
        public string NOMBREPAIS { get; set; }

        [Column(Name = "FECHAESTADOCOURIER")]
        public DateTime? DESTADOCOURIER { get; set; }

        [Column(Name = "FECHAFINAL")]
        public DateTime? FECHAFINAL { get; set; }

        [Column(Name = "APROBADO")]
        public bool Aprobado { get; set; }

        [Column(Name="CODIGOORFEO")]
        public string CodigoOrfeo { get; set; }

        [Column(Name = "IDCODIGOGUIA")]
        public string cIdCodigoGuia { get; set; }

        public string CausalDevolucion { get; set; }

        [Column(Name="ENVIORESOLUCION")]
        public int nEnvioResolucion { get; set; }

        [Column(Name = "ORDENSERVICIO")]
        public string ordenServicio { get; set; }
    }
}
