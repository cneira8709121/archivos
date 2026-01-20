using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Data.Linq.Mapping;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsValoracion
    {
        #region Constructores

        public clsValoracion()
        {
        }

        #endregion

        #region Atributos

        private int id;
        
        private int declaracionId;

        private DateTime fechaAsignacion;

        private int estadoId;

        private int asignadorId;

        private int valoradorId;

        private int valoradorRId;

        private DateTime fechaValoracion;

        private DateTime fechaRealValoracion;

        private bool esDeclaracion;

        private string observacion;
        
        private List<clsHechosValoracion> hechos;

        private string cidtipomotivo;

        private string motivacion_inclusion;

        private string motivacion_noInclusion;

        private string resuelve_Articulo1;

        private string resuelve_Articulo2;

        private List<int> causalDevolucion;

        private List<clsRegistrosValoracion> registrosAnteriores;

        private List<clsPersona> personasDeclaracion;

        private clsDeclaracion declaracion;

        private List<clsDeclaracionInfoValoracion> basicDeclaracion;

        private int id_EntidadMunicipio;
        
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
        [Column(Name = "ID_DECLARACION")]
        public int DeclaracionId
        {
            get { return declaracionId; }
            set { declaracionId = value; }
        }

        [DataMember]
        [Column(Name = "FECHAASIGNACION")]
        public DateTime FechaAsignacion
        {
            get { return fechaAsignacion; }
            set { fechaAsignacion = value; }
        }

        [DataMember]
        [Column(Name = "ID_ESTADO_VAL")]
        public int EstadoId
        {
            get { return estadoId; }
            set { estadoId = value; }
        }

        [DataMember]
        [Column(Name = "ID_ASIGNADOR")]
        public int AsignadorId
        {
            get { return asignadorId; }
            set { asignadorId = value; }
        }

        [DataMember]
        [Column(Name = "ID_VALORADOR")]
        public int ValoradorId
        {
            get { return valoradorId; }
            set { valoradorId = value; }
        }

        [DataMember]
        [Column(Name = "ID_VALORADOR_RUV")]
        public int ValoradorRId
        {
            get { return valoradorRId; }
            set { valoradorRId = value; }
        }

        [DataMember]
        [Column(Name = "FECHAVALORACION")]
        public DateTime FechaRealValoracion
        {
            get { return fechaRealValoracion; }
            set { fechaRealValoracion = value; }
        }

        [DataMember]
        [Column(Name = "FECHAVALORACIONREAL")]
        public DateTime FechaValoracion
        {
            get { return fechaValoracion; }
            set { fechaValoracion = value; }
        }

        [DataMember]
        [Column(Name = "ESDECLARACION")]
        public bool EsDeclaracion
        {
            get { return esDeclaracion; }
            set { esDeclaracion = value; }
        }

        [DataMember]
        [Column(Name = "OBSERVACION")]
        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }

        [DataMember]
        public List<clsHechosValoracion> Hechos
        {
            get { return hechos; }
            set { hechos = value; }
        }

        [DataMember]
        [Column(Name = "TIPOMOTIVACION")]
        public string cIdTipoMotivo
        {
            get { return cidtipomotivo; }
            set { cidtipomotivo = value; }
        }

        [DataMember]
        [Column(Name = "MOTIVACION_INCLUSION")]
        public string Motivacion_Inclusion
        {
            get { return motivacion_inclusion; }
            set { motivacion_inclusion = value; }
        }

        [DataMember]
        [Column(Name = "MOTIVACION_NOINCLUSION")]
        public string Motivacion_NoInclusion
        {
            get { return motivacion_noInclusion; }
            set { motivacion_noInclusion = value; }
        }

        [DataMember]
        [Column(Name = "RESUELVE_ARTICULO1")]
        public string ResuelveArticulo1
        {
            get { return resuelve_Articulo1; }
            set { resuelve_Articulo1 = value; }
        }

        [DataMember]
        [Column(Name = "RESUELVE_ARTICULO2")]
        public string ResuelveArticulo2
        {
            get { return resuelve_Articulo2; }
            set { resuelve_Articulo2 = value; }
        }

        [DataMember]
        public List<int> CausalDevolucion
        {
            get { return causalDevolucion; }
            set { causalDevolucion = value; }
        }

        [DataMember]
        public List<clsRegistrosValoracion> RegistrosAnteriores
        {
            get { return registrosAnteriores; }
            set { registrosAnteriores = value; }
        }

        [DataMember]
        public List<clsPersona> PersonasDeclaracion
        {
            get { return personasDeclaracion; }
            set { personasDeclaracion = value; }
        }

        public clsDeclaracion Declaracion
        {
            get { return declaracion; }
            set { declaracion = value; }
        }

        public List<clsDeclaracionInfoValoracion> BasicDeclaracion
        {
            get { return basicDeclaracion; }
            set { basicDeclaracion = value; }
        }

        #endregion

    }
}
