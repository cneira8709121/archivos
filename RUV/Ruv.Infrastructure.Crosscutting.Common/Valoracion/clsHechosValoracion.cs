using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsHechosValoracion
    {
        #region Construnctor
        public clsHechosValoracion() {
        }
        #endregion

        #region Atributos

        private int id;
        private int declaracionId;
        private string tipoHecho;
        private int tipoHechoId;
        private int hechoId;
        private string victima1;
        private DateTime fecha;
        private string tipoEntorno;
        private string localidadCorregimiento;
        private string barrioVereda;
        private string departamento;
        private string municipio;
        private int totalPersonas;
        private int valoracionId;
        private DateTime ultimaFechaEdicion;
        private string observaciones;
        List<clsPersonaAnexo> personas;
        private DateTime? fechadespojo;
        private DateTime? fechaabandono;
        private bool muestraabandono;
        private bool muestradespojo;
        

        #endregion

        #region Propiedades

        public bool MuestraAbandono
        {
            get { return muestraabandono; }
            set { muestraabandono = value; }
        }

        public bool MuestraDespojo
        {
            get { return muestradespojo; }
            set { muestradespojo = value; }
        }
        [DataMember]
        [Column(Name = "ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        [Column(Name = "id_declaracion")]
        public int DeclaracionId
        {
            get { return declaracionId; }
            set { declaracionId = value; }
        }

        [DataMember]
        [Column(Name = "TipoHecho")]
        public string TipoHecho
        {
            get { return tipoHecho; }
            set { tipoHecho = value; }
        }

        [DataMember]
        [Column(Name = "TipoHechoId")]
        public int TipoHechoId
        {
            get { return tipoHechoId; }
            set { tipoHechoId = value; }
        }

        [DataMember]
        [Column(Name = "ID_SINIESTRO")]
        public int HechoId
        {
            get { return hechoId; }
            set { hechoId = value; }
        }

        [DataMember]
        [Column(Name = "Victima1")]
        public string Victima1
        {
            get { return victima1; }
            set { victima1 = value; }
        }

        [DataMember]
        [Column(Name = "Fecha")]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        [DataMember]
        [Column(Name = "TipoEntorno")]
        public string TipoEntorno
        {
            get { return tipoEntorno; }
            set { tipoEntorno = value; }
        }

        [DataMember]
        [Column(Name = "LocalidadCorregimiento")]
        public string LocalidadCorregimiento
        {
            get { return localidadCorregimiento; }
            set { localidadCorregimiento = value; }
        }

        [DataMember]
        [Column(Name = "BarrioVereda")]
        public string BarrioVereda
        {
            get { return barrioVereda; }
            set { barrioVereda = value; }
        }

        [DataMember]
        [Column(Name = "Departamento")]
        public string Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }

        [DataMember]
        [Column(Name = "Municipio")]
        public string Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }

        [DataMember]
        [Column(Name = "TotalPersonas")]
        public int TotalPersonas
        {
            get { return totalPersonas; }
            set { totalPersonas = value; }
        }

        [DataMember]
        [Column(Name = "ID_VALORACION")]
        public int ValoracionId
        {
            get { return valoracionId; }
            set { valoracionId = value; }
        }

        [DataMember]
        [Column(Name = "ULTIMA_FECHAEDICION")]
        public DateTime UltimaFechaEdicion
        {
            get { return ultimaFechaEdicion; }
            set { ultimaFechaEdicion = value; }
        }

        [DataMember]
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        [DataMember]
        public List<clsPersonaAnexo> Personas
        {
            get { return personas; }
            set { personas = value; }
        }

        [DataMember]
        [Column(Name = "FechaAbandono")]
        public DateTime? FechaAbandono
        {
            get { return fechaabandono; }
            set { fechaabandono = value; }
        }

        [DataMember]
        [Column(Name = "FechaDespojo")]
        public DateTime? FechaDespojo
        {
            get { return fechadespojo; }
            set { fechadespojo = value; }
        }



        #endregion
    }
}
