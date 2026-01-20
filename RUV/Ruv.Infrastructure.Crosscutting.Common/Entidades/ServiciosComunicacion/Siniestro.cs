using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion
{
    public class Siniestro
    {
        [DataMember]
        public DateTime Fecha;

        [DataMember]
        public string Nombre_hecho;

        [DataMember]
        public int Id_declaracion;

        [DataMember]
        public string Numero_formulario;

        [DataMember]
        public string Localidadcorregimiento;

        [DataMember]
        public string Barriovereda;

        [DataMember]
        public string Departamento;

        [DataMember]
        public string Municipio;
    }
}
