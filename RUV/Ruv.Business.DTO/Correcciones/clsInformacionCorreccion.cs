using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Correcciones
{
    public class clsInformacionCorreccion
    {
        [Column(Name="ID")]
        public int nIdCorreccion { get; set; }

        [Column(Name="ID_REGPERSONA")]
        public int nIdRegPersona { get; set; }

        [Column(Name="ID_USUARIOSOLICITUD")]
        public int nIdUsuarioSolicitante { get; set; }

        [Column(Name="ID_USUARIO")]
        public int nIdUsuarioApruebaRechaza { get; set; }

        [Column(Name="FECHASOLICITUD")]
        public DateTime dFechaSolicitud { get; set; }

        [Column(Name="OBSERVACIONES")]
        public string cObservacione { get; set; }

        [Column(Name="ESTADO")]
        public int nEstado { get; set; }
    }
}
