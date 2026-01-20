using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsDeclaracionInfoValoracion
    {
        #region Constructor
        
        public clsDeclaracionInfoValoracion()
        {

        }

        #endregion
        #region Atributos
        
        private int declaracionId;
        private string formulario;
        private int valoradorId;
        private string valorador;
        private DateTime fechaRadicado;
        private string unidadTerritorial;
        private string municipio;
        private string departamento;
        private DateTime fechaValoracion;

        #endregion
        #region Propiedades
        
        [DataMember]
        public int DeclaracionId
        {
            get { return declaracionId; }
            set { declaracionId = value; }
        }

        [DataMember]
        public string Formulario
        {
            get { return formulario; }
            set { formulario = value; }
        }

        [DataMember]
        public int ValoradorId
        {
            get { return valoradorId; }
            set { valoradorId = value; }
        }
        [DataMember]
        public string Valorador
        {
            get { return valorador; }
            set { valorador = value; }
        }
        [DataMember]
        public DateTime FechaRadicado
        {
            get { return fechaRadicado; }
            set { fechaRadicado = value; }
        }
        [DataMember]
        public string UnidadTerritorial
        {
            get { return unidadTerritorial; }
            set { unidadTerritorial = value; }
        }
        [DataMember]
        public string Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }
        [DataMember]
        public string Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }
        [DataMember]
        public DateTime FechaValoracion
        {
            get { return fechaValoracion; }
            set { fechaValoracion = value; }
        }
        #endregion
    }
}
