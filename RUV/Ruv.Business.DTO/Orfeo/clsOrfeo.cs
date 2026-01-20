using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Orfeo
{
    public class clsOrfeo
    {
        [Column(Name ="PRIMERNOMBRE")]
        public string cPrimerNombre { get; set; }
        [Column(Name ="PRIMERAPELLIDO")]
        public string cPrimerApellido { get; set; }
        [Column(Name ="SEGUNDOAPELLIDO")]
        public string cSegundoNombre { get; set; }
        [Column(Name ="NUMERODOCUMENTO")]
        public string cNumeroDocumento { get; set; }
        [Column(Name ="DIRECCIONPERSONA")]
        public string cDireccionPersona { get; set; }
        [Column(Name ="DIRECCIONCORRESPONDENCIA")]
        public string cDireccionCorrespondencia { get; set; }
        [Column(Name ="TELEFONOPERSONA")]
        public string cTelefonoPersona { get; set; }
        [Column(Name ="TELEFONOCORRESPONDENCIA")]
        public string cTelefonoCorrespondecia { get; set; }
        [Column(Name ="DEPARTAMENTOCODAZZIPERSONA")]
        public string cDeparatmentoCodazziPersona { get; set; }
        [Column(Name ="MUNICIPIOCODAZZIPERSONA")]
        public string cMunicipioCodazziPersona { get; set; }
        [Column(Name ="DEPARTAMENTOCODAZZICORREO")]
        public string cDepartamentoCodazziCorreo { get; set; }
        [Column(Name ="MUNICIPIOCODAZZICORREO")]
        public string cMunicipioCodazziCorreo { get; set; }
        [Column(Name ="EMAIL")]
        public string cEmail { get; set; }
        [Column(Name="USUARIO")]
        public int nUsuario { get; set; }
        [Column(Name="USUARIODESTINO")]
        public int nUsuarioDestino { get; set; }
        [Column(Name="ENTIDAD")]
        public string cEntidad { get; set; }
        [Column(Name="DPTOADMIN")]
        public int nDptoAdmin { get; set; }
        [Column(Name="DPTOADMINDESTINO")]
        public int nDptoDestino { get; set; }
        [Column(Name="NOMBREDIRECCION")]
        public string cNombreDireccion { get; set; }
    }
}
