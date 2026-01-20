using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.CriticaN
{
    public class clsRespuestaCritica
    {
        public int? NIdCriticaN { get; set; }
        public int? NRespuesta { get; set; }
        public long NIdUsuario { get; set; }
        public long NIdRadicacion { get; set; }
        public string CObservacion { get; set; }
    }
}
