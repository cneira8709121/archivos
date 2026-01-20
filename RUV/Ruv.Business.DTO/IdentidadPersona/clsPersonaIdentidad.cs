using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.IdentidadPersona
{
    public class clsPersonaIdentidad
    {
        [Column(Name = "TD_ID")]
        public int Identificador { get; set; }
        [Column(Name = "NOM1_VAL")]
        public string PrimerNombre { get; set; }
        [Column(Name = "NOM2_VAL")]
        public string SegundoNombre { get; set; }
        [Column(Name = "APE1_VAL")]
        public string PrimerApellido { get; set; }
        [Column(Name = "APE2_VAL")]
        public string SegundoApellido { get; set; }
        [Column(Name = "VIGENCIA")]
        public string Vigencia { get; set; }
        [Column(Name = "VALIDACION")]
        public int Validacion { get; set; }
        [Column(Name = "RESULTADO")]
        public string Resultado { get; set; }
    }

    public class PreguntaString
    {
        [Column(Name = "QUESTIONS_VAL")]
        public string PreguntaJson { get; set; }
    }
}
