using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Valoracion;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Valoracion
{
    public class clsResumenValoracion
    {
        [Column(Name = "ID")]
        public int? nIdDeclaracion {get; set;}
        [Column(Name = "NUMEROFORMULARIO")]
        public string CNumeroFormulario { get; set; }
        [Column(Name="FECHADECLARACION")]
        public DateTime dFechaDeclaracion { get; set; }
        [Column(Name="NOMBREVALORADOR")]
        public string cNombreValorador { get; set; }
        [Column(Name="NombreDeclarante")]
        public string cNombreDeclarante { get; set; }
        [Column(Name="TipoDocumento")]
        public string cTipoDocumento { get; set; }
        [Column(Name="DocumentoIdentidad")]
        public long? nDocumentoIdentidad { get; set; }
        [Column(Name="EstadoActualProceso")]
        public string cEstadoActualProceso { get; set; }
        [Column(Name = "EstadoValoracion")]
        public string cEstadoValoracion { get; set; }
        [Column(Name="FechaValoracion")]
        public DateTime dFechaValoracion { get; set; }
        [Column(Name="Estado")]
        public string cEstado { get; set; }
        [Column(Name="NOMBRE_HECHO_VICTIMIZANTE")]
        public string cHechoVictimizante { get; set; }
        [Column(Name="INFRACCIONDERECHOHUMAN")]
        public string cInfraccionDerechoHumano { get; set; }
        [Column(Name="NOMBREVICTIMA")]
        public string cNombreVictima { get; set; }
        [Column(Name="TIPODOCUMENTO_VICTIMA")]
        public string cTipodocumentoVictima { get; set; }
        [Column(Name="DOCUMENTOVICTIMA")]
        public string nDocumentoVictima { get; set; }
        [Column(Name="PRINCIPIO")]
        public string cPrincipio { get; set; }

    }
}
