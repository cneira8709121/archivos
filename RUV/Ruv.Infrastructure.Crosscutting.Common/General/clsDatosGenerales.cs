using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    [DataContract]
    public class clsDatosGenerales
    {
        [DataMember]
        public List<clsParametroPais> Paises { get; set; }
        [DataMember]
        public List<clsParametroDepartamento> Departamentos { get; set; }
        [DataMember]
        public List<clsParametroMunicipio> Municipios { get; set; }
        [DataMember]
        public List<clsParametroNacionalidad> Nacionalidades { get; set; }
        [DataMember]
        public List<clsParametroGeneral> Parametros { get; set; }
        [DataMember]
        public List<clsGrupoParamDetalle> GrupoParamDetalle { get; set; }
        [DataMember]
        public List<clsComunidadEtnica> ComunidadesEtnicas { get; set; }
        [DataMember]
        public List<clsGrupoEtnica> GruposEtnicos { get; set; }
        [DataMember]
        public List<clsPoblacion> Poblaciones { get; set; }
        [DataMember]
        public List<clsParametroUT> UnidadesTerritoriales { get; set; }
        [DataMember]
        public List<clsValidaciones> Validaciones { get; set; }
        [DataMember]
        public List<clsEntidadMunicipio> EntidadesMunicipios { get; set; }
        [DataMember]
        public List<clsPreguntaCriticaN> PreguntasCriticaN { get; set; }
        [DataMember]
        public List<clsCausal> Causales { get; set; }
    }
}
