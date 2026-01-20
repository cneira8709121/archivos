using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.ServiciosComunicacion
{
    public class clsSiniestro
    {
        [Column(Name = "FECHA")]
        public DateTime Fecha { get; set; }

        [Column(Name = "NOMBRE_HECHO")]
        public string NombreHecho { get; set; }

        [Column(Name = "ID_DECLARACION")]
        public int IdDeclaracion { get; set; }

        [Column(Name = "NUMERO_FORMULARIO")]
        public string NumeroFormulario { get; set; }

        [Column(Name = "LOCALIDADCORREGIMIENTO")]
        public string LocalidadCorregimiento { get; set; }

        [Column(Name = "BARRIOVEREDA")]
        public string BariioVereda { get; set; }

        [Column(Name = "DEPARTAMENTO")]
        public string Departamento { get; set; }

        [Column(Name = "MUNICIPIO")]
        public string Municipio { get; set; }
    }
}
