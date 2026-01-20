using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Ruv.Business.DTO.IdentidadPersona
{
    public class FechaExpedicion
    {
        public string Opcion { get; set; }
        public bool Valida { get; set; }
    }

    public class FechaDeNacimiento
    {
        public string Opcion { get; set; }
        public bool Valida { get; set; }
    }

    public class DepartamentoExpedicion
    {
        public string Opcion { get; set; }
        public bool Valida { get; set; }
    }

    public class MunicipioExpedicion
    {
        public string Opcion { get; set; }
        public bool Valida { get; set; }
    }

    public class Preguntas
    {
        public List<FechaExpedicion> FechaExpedicion { get; set; }
        public List<FechaDeNacimiento> FechaDeNacimiento { get; set; }
        public List<DepartamentoExpedicion> DepartamentoExpedicion { get; set; }
        public List<MunicipioExpedicion> MunicipioExpedicion { get; set; }
    } 
    
}
