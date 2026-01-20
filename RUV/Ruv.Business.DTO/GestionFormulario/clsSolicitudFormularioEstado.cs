using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.Business.DTO.GestionFormulario
{
    public class clsSolicitudFormularioEstado
    {
        public int NIdUsuario { get; set; }
        public eEstadoFormulario? IdEstado { get; set; }
        public string CNumeroFormulario { get; set; }
        public int? NDesde { get; set; }
        public int? NHasta { get; set; }
        public DateTime? DGenerado { get; set; }
        public long? NIdPais { get; set; }
        public long? NIdDepartamento { get; set; }
        public long? NIdMunicipio { get; set; }
        public short? NIdEntidad { get; set; }
        public int NPagina { get; set; }
        public int NDatosPorPg { get; set; }
    }
}
