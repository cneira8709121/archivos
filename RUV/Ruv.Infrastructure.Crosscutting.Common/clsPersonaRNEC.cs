using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common
{

    public class clsPersonaRNEC
    {
        public string fuente { get; set; }
        public string tipo_doc { get; set; }
        public string nuip { get; set; }
        public string nom1 { get; set; }
        public string nom2 { get; set; }
        public string ape1 { get; set; }
        public string ape2 { get; set; }
        public string pais_exp { get; set; }
        public string depto_exp { get; set; }
        public string mun_exp { get; set; }
        public string f_exp { get; set; }
        public string estado_cedula { get; set; }
        public string num_resol { get; set; }
        public string ano_resol { get; set; }
        public decimal documento_cancelado { get; set; }
        public string observacion { get; set; }
        public string genero { get; set; }
        public string fechaNacimiento { get; set; }
        public string f_consulta { get; set; }
    }
}
