using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion {

    public class clsHistoricoNotificacion {

        [Column(Name = "ID")]
        public int Id { get; set; }

        [Column(Name = "ID_NOTIFICACION")]
        public int IdNotificacion { get; set; }

        [Column(Name = "ID_PAIS")]
        public int IdPais { get; set; }

        [Column(Name = "PAIS")]
        public string Pais { get; set; }

        [Column(Name = "ID_DEPARTAMENTO")]
        public int IdDepartamento { get; set; }

        [Column(Name = "DEPARTAMENTO")]
        public string Departamento { get; set; }

        [Column(Name = "ID_MUNICIPIO")]
        public int IdMunicipio { get; set; }

        [Column(Name = "MUNICIPIO")]
        public string Municipio { get; set; }

        public string Destino { get { return string.Format("{0} - {1} - {2}", Pais, Departamento, Municipio); } }

        [Column(Name = "DIRECCIONNOTIFICACION")]
        public string DireccionNotificacion { get; set; }

        [Column(Name = "TELEFONONOTIFICACION")]
        public string TelefonoNotificacion { get; set; }

        [Column(Name = "ID_PAQUETENOTIFICACION")]
        public int? IdPaqueteNotificacion { get; set; }

        [Column(Name = "ORDENSERVICIO")]
        public string OrdenDeservicioPaquete { get; set; }

        public string Paquete { get { return IdPaqueteNotificacion.HasValue ? (!string.IsNullOrEmpty(OrdenDeservicioPaquete) ? OrdenDeservicioPaquete : string.Format("ID: {0}", IdPaqueteNotificacion.Value)) : string.Empty; } }

        [Column(Name = "ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column(Name = "USUARIO")]
        public string Usuario { get; set; }

        [Column(Name = "ESTADO")]
        public string Estado { get; set; }

        [Column(Name = "ESTADOCOURIER")]
        public string EstadoCourier { get; set; }

        [Column(Name = "FECHAESTADOCOURIER")]
        public DateTime? FechaEstadoCourier { get; set; }

        public string EstadoYFechaCourier { get { return EstadoCourier + (FechaEstadoCourier.HasValue ? " (" + FechaEstadoCourier.Value.ToString("dd/MM/yyyy") + ")" : string.Empty); } }

        [Column(Name = "FECHAFINAL")]
        public DateTime? FechaFinal { get; set; }

        public string FechaFinalString { get { return FechaFinal.HasValue ? FechaFinal.Value.ToString("dd/MM/yyyy") : string.Empty; } }

        [Column(Name = "OBSERVACIONNOTIFICACION")]
        public string Observaciones { get; set; }

        [Column(Name = "ID_PUNTOATENCION")]
        public int? IdPuntoAtencion { get; set; }

        [Column(Name = "PUNTOATENCION")]
        public string PuntoAtencion { get; set; }

        [Column(Name = "ID_DIRECCIONTERRITORIAL")]
        public int? IdDireccionTerritorial { get; set; }

        [Column(Name = "DIRECCIONTERRITORIAL")]
        public string DireccionTerritorial { get; set; }

        public string AtencionNotificacion { get { return PuntoAtencion ?? DireccionTerritorial; } }

        [Column(Name = "APROBADO")]
        public int Aprobado { get; set; }

        public string AprobadoString { get { return Aprobado == 1 ? "Si" : "No"; } }

        [Column(Name = "IDCODIGOGUIA")]
        public string CodigoGuia { get; set; }

        [Column(Name = "FECHAMODIFICACION")]
        public DateTime FechaModificacion { get; set; }

        public string FechaModificacionString { get { return FechaModificacion.ToString("dd/MM/yyyy hh:mm:ss tt"); } }
   
    }
}
