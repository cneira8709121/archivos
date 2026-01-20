using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.IdentidadPersona
{
    public class clsMensajeSMS
    {
        public string Mensaje { get; set; }
        public string Cedula { get; set; }
        public string NombrePersona { get; set; }
        public string Celular { get; set; }
    }
    public class clsMensajeCorreo
    {
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string Cedula { get; set; }
        public string NombrePersona { get; set; }
        public string Correo { get; set; }
    }

    public class clsCodigoValidacion
    {
        public string Cedula { get; set; }
        public string Codigo { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
    }
}
