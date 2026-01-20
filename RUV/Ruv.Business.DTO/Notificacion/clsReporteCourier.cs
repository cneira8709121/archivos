using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Notificacion
{
    public class clsReporteCourier
    {
        [Column(Name = "CLIENTE")]
        public string CCliente { get; set; }
        [Column(Name = "ORDEN")]
        public string COrden { get; set; }
        [Column(Name = "FECHA ADMISIÓN")]
        public DateTime DAdmision { get; set; }
        [Column(Name = "ENVÍO")]
        public string CEnvio { get; set; }
        [Column(Name = "PESO")]
        public string CPeso { get; set; }
        [Column(Name = "REMITENTE")]
        public string CNombreRemitente { get; set; }
        [Column(Name = "REGIONAL DESTINO")]
        public string CRegionalDestino { get; set; }
        [Column(Name = "CIUDAD DESTINO")]
        public string CCiudadDestino { get; set; }
        [Column(Name = "DPTO DESTINO")]
        public string CDepartamentoDestino { get; set; }
        [Column(Name = "DIRECCIÓN DESTINATARIO")]
        public string CDireccionDestinatario { get; set; }
        [Column(Name = "DESTINATARIO")]
        public string CNombreDestinatario { get; set; }
        [Column(Name = "TELÉFONO DESTINATARIO")]
        public string CTelefonoDestinatario { get; set; }
        [Column(Name = "FECHA ENTREGA")]
        public DateTime? DEntrega { get; set; }
        [Column(Name = "RECHAZADO")]
        public string CRechazado { get; set; }
        [Column(Name = "ESTADO")]
        public string CEstado { get; set; }
        [Column(Name = "RECIBE")]
        public string CQuienRecibe { get; set; }
        [Column(Name = "IDENTIFICACIÓN")]
        public string CIdentificacion { get; set; }
        [Column(Name = "RADICACION")]
        public string CRadicacion { get; set; }
        [Column(Name = "CAUSAL DE DEVOLUCIÓN")]
        public string CCausalDevolucion { get; set; }
        [Column(Name = "DIGITALIZADO")]
        public string CDigitalizado { get; set; }
        [Column(Name = "CONTENIDO")]
        public string CContenido { get; set; }
        [Column(Name = "REFERENCIA")]
        public string CReferencia { get; set; }
        [Column(Name = "OBSERVACIONES")]
        public string CObservaciones { get; set; }
    }
}
