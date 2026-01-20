using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.Orfeo
{
    public class Radicado
    {
        public int NTipoRadicado { get { return 2; } }
        public int NDepartamentoRadicado { get; set; }
        public int NDepartamentoDestino { get; set; }
        public int NCodigoUsuario { get; set; }
        public int NCodigoUsuarioDestino { get; set; }
        public DateTime DFechaOficial { get { return DateTime.Now; } }
        public string CRadicadoEntrada { get { return string.Empty; } }
        public string CDescanex { get { return string.Empty; } }
        public string CAsunto { get; set; }
        public string CNRoofic { get { return string.Empty; } }
        public string CRutaRadicado { get { return string.Empty; } }
        public string CExpe { get { return string.Empty; } }
        public string CRadicado { get { return "3"; } }
    }
}
