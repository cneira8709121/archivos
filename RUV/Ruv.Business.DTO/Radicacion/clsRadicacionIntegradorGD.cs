using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.Radicacion
{
    public class clsRadicacionIntegradorGD
    {
        public int ID_DECLARACION { get; set; }
        public string NUM_DECLARACION { get; set; }
        public int ID_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string PRIMER_APELIIDO { get; set; }
        public string SEGUNDO_APELLIDO { get; set; }
        public string CEDULA { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO { get; set; }
        public int PAIS { get; set; }
        public int DEPARTAMENTO { get; set; }
        public int MUNICIPIO { get; set; }
        public string DESCRIPCION_ANEXO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public string ARCHIVO { get; set; }
        public string SEGUNDO_NOMBRE { get; set; }
    }
}
