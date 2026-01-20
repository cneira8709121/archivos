using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.ActosAdministrativos
{
    public class clsNotificacionVal
    {
        [Column(Name = "NUMEROFORMULARIO")]
        public string cNumeroFormulario { get; set; }
        [Column(Name = "NOMBREENTIDAD")]
        public string cNombreEntidad { get; set; }
        [Column(Name = "NOMBREMUNICIPIO")]
        public string cNombreMunicipio { get; set; }
        [Column(Name = "NOMBREDEPARTAMENTO")]
        public string cNombreDepartamento { get; set; }
        [Column(Name = "ENTIDADCOMPLETADECLARACION")]
        public string cEntidadCompletaDeclaracion { get; set; }
        [Column(Name = "NOMBREDECLARANTE")]
        public string cNombreDeclarante { get; set; }
        [Column(Name = "DIRECCION")]
        public string cDireccion { get; set; }
        [Column(Name = "TELEFONO")]
        public string nTelefono { get; set; }
        [Column(Name = "DEPARTAMENTO")]
        public string cDepartamento { get; set; }
        [Column(Name = "MUNICIPIO")]
        public string cMunicipio { get; set; }
        [Column(Name = "TIPODOCUMENTO")]
        public string cTipoDocumento { get; set; }
        [Column(Name = "DOCUMENTOIDENTIDAD")]
        public long nDocumentoIdentidad { get; set; }
        [Column(Name = "FECHAVALORACION")]
        public DateTime dFechaValoracion { get; set; }
        [Column(Name = "FECHAVALORACIONREAL")]
        public DateTime dFechaValoracionReal { get; set; }
        [Column(Name = "FECHARADICACION")]
        public DateTime dFechaRadicacion { get; set; }
        [Column(Name = "HECHOVICTIMIZANTEIN")]
        public string cHechoVictimizanteIn { get; set; }
        [Column(Name = "HECHOVICTIMIZANTENOIN")]
        public string cHechoVictimizanteNoIn { get; set; }
        [Column(Name = "HECHOAGREGADODECLARACION")]
        public string cHechoVictimizanteAgregadoDeclaracion { get; set; }
        [Column(Name = "PRINCIPIOSIN")]
        public string cPrincipioInclusion { get; set; }
        [Column(Name = "PRINCIPIOSNOIN")]
        public string cPrincipioNoInclusion { get; set; }
        [Column(Name = "MOTIVACION")]
        public string cMotivacion { get; set; }
        [Column(Name = "CONSECUTIVO")]
        public string cConsecutivo { get; set; }
        [Column(Name = "FECHAACTOADMINISTRATIVO")]
        public DateTime dFechaActoAdministrativo { get; set; }
        [Column(Name = "CODIGOORFEO")]
        public string cCodigoOrfeo { get; set; }
        [Column(Name = "IDACTOADMIN")]
        public int nIdActoAdmin { get; set; }
        [Column(Name = "VALORADOR")]
        public string cValorador { get; set; }
        [Column(Name = "LIDER")]
        public string cLiderVal { get; set; }
        [Column(Name = "TIPODOCUMENTOVALORACION")]
        public int nTipoDocumentoVal { get; set; }
        [Column(Name = "FECHADECLARACION")]
        public DateTime dFechaDeclaracion { get; set; }
        [Column(Name = "TIPOCODIGOACTO")]
        public int? nTipoCodigoActo { get; set; }
        [Column(Name = "MOTIVACIONINCLUSION")]
        public string cMotivacionInclusion { get; set; }
        [Column(Name = "MOTIVACIONNOINCLUSION")]
        public string cMotivacionNoInclusion { get; set; }
        [Column(Name = "RESUELVEARTICULO1")]
        public string cResuelveArticulo1 { get; set; }
        [Column(Name = "RESUELVEARTICULO2")]
        public string cResuelveArticulo2 { get; set; }
        [Column(Name = "USUARIOPROYECTO")]
        public string cUsuarioProyecto { get; set; }
        [Column(Name = "USUARIOREVISO")]
        public string cUsuarioReviso { get; set; }
        [Column(Name = "NOMBREPUNTO")]
        public string cNombrePunto { get; set; }
        [Column(Name = "DIRECCIONPUNTO")]
        public string cDireccionPunto { get; set; }
    }
}


