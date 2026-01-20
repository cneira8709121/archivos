using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsPersonaAnexo
    {

        #region Constructor
        
        public clsPersonaAnexo()
        { }

        #endregion

        #region Atributos

        private int id;
        private string persona;
        private int personaId;
        private string tipoDocumento;
        private string numeroDocumento;
        private string relacion;
        private int sexo;
        private string genero;
        private int edad;
        private int etniaid;
        private string etnia;
        private bool discapacitado;
        private bool? fallecida;
        private bool? desaparecida;
        private bool? secuestrado;
        private string estadoPorMina;
        private bool? seDesplazo;
        private bool victima;
        private bool afectado;
        private int? estadoId;
        private string estado;
        private int valAnexoId;
        private string observacion;
        private int? observacionId;
        private int? hechoEnmarcadoId;
        private string decretoLey;
        private List<int> principios;
        private List<int> afectacionesDetectadas;
        private List<int> fuentes;
        private List<clsAutores> autores;
        private List<clsInfracciones> infraccionesDHI;
        List<clsHerramientaAnexoPer> herramietas;

        #endregion

        #region Propiedades

        [DataMember]
        [Column(Name = "ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        [Column(Name = "Persona")]
        public string Persona
        {
            get { return persona; }
            set { persona = value; }
        }

        [DataMember]
        [Column(Name = "id_regpersona")]
        public int PersonaId
        {
            get { return personaId; }
            set { personaId  = value; }
        }

        [DataMember]
        [Column(Name = "TipoDocumento")]
        public string TipoDocumento
        {
            get { return tipoDocumento; }
            set { tipoDocumento = value; }
        }

        [DataMember]
        [Column(Name = "numerodocumento")]
        public string NumeroDocumento
        {
            get { return numeroDocumento; }
            set { numeroDocumento = value; }
        }

        [DataMember]
        [Column(Name = "Relacion")]
        public string Relacion
        {
            get { return relacion; }
            set { relacion = value; }
        }

        [DataMember]
        [Column(Name = "GeneroId")]
        public int Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        [DataMember]
        [Column(Name = "Genero")]
        public string Genero
        {
            get { return genero; }
            set { genero = value; }
        }

        [DataMember]
        [Column(Name = "Edad")]
        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        [DataMember]
        [Column(Name = "EtniaId")]
        public int EtniaId
        {
            get { return etniaid; }
            set { etniaid = value; }
        }

        [DataMember]
        [Column(Name = "Etnia")]
        public string Etnia
        {
            get { return etnia; }
            set { etnia = value; }
        }
        
        [DataMember]
        [Column(Name = "Discapacitado")]
        public bool Discapacitado
        {
            get { return discapacitado; }
            set { discapacitado = value; }
        }

        [DataMember]
        [Column(Name = "Fallecida")]
        public bool? Fallecida
        {
            get { return fallecida; }
            set { fallecida = value; }
        }

        [DataMember]
        [Column(Name = "Desaparecida")]
        public bool? Desaparecida
        {
            get { return desaparecida; }
            set { desaparecida = value; }
        }

        [DataMember]
        [Column(Name = "Secuestrado")]
        public bool? Secuestrado
        {
            get { return secuestrado; }
            set { secuestrado = value; }
        }

        [DataMember]
        [Column(Name = "EstadoPorMina")]
        public string EstadoPorMina
        {
            get { return estadoPorMina; }
            set { estadoPorMina = value; }
        }

        [DataMember]
        [Column(Name = "SeDesplazo")]
        public bool? SeDesplazo
        {
            get { return seDesplazo; }
            set { seDesplazo = value; }
        }

        [DataMember]
        [Column(Name = "esvicitma")]
        public bool Victima
        {
            get { return victima; }
            set { victima = value; }
        }

        [DataMember]
        [Column(Name = "esafectado")]
        public bool Afectado
        {
            get { return afectado; }
            set { afectado = value; }
        }

        [DataMember]
        public List<int> AfectacionesDetectadas
        {
            get { return afectacionesDetectadas; }
            set { afectacionesDetectadas = value; }
        }

        [DataMember]
        [Column(Name = "DecretoLey")]
        public string DecretoLey
        {
            get { return decretoLey; }
            set { decretoLey = value; }
        }

        [DataMember]
        public int? EstadoId
        {
            get { return estadoId; }
            set { estadoId = value; }
        }

        [DataMember]
        [Column(Name = "id_estado_val")]
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        [DataMember]
        [Column(Name = "id_val_anexo")]
        public int ValAnexoId
        {
            get { return valAnexoId; }
            set { valAnexoId = value; }
        }

        [DataMember]
        [Column(Name = "id_hechoEnmarcado_val")]
        public int? ObservacionId
        {
            get { return observacionId; }
            set { observacionId = value; }
        }

        [DataMember]
        [Column(Name = "id_observacion_val")]
        public int? HechoEnmarcadoId
        {
            get { return hechoEnmarcadoId; }
            set { hechoEnmarcadoId = value; }
        }

        [DataMember]
        public List<int> Principios
        {
            get { return principios; }
            set { principios = value; }
        }

        [DataMember]
        [Column(Name = "observacion")]
        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }

        [DataMember]
        public List<int> Fuentes
        {
            get { return fuentes; }
            set { fuentes = value; }
        }

        [DataMember]
        public List<clsAutores> Autores
        {
            get { return autores; }
            set { autores = value; }
        }

        [DataMember]
        public List<clsInfracciones> InfraccionesDHI
        {
            get { return infraccionesDHI; }
            set { infraccionesDHI = value; }
        }

        [DataMember]
        public List<clsHerramientaAnexoPer> Herramietas
        {
            get { return herramietas; }
            set { herramietas = value; }
        }
        #endregion
    }
}
