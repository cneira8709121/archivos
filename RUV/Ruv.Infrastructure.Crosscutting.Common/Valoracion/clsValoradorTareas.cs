using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Valoracion
{
    [DataContract]
    public class clsValoradorTareas
    {
        #region Constructores
        public clsValoradorTareas()
        {
        }
        #endregion

        #region Atributos

        private int valoracionId;
        private int valoradorId;
        private string declarante;
        private string documentoDeclarante;
        private DateTime fechaRadicacion;
        private string numeroFormulario;
        private string hechosVictimizantes;
        private int totalHv;
        private DateTime fechaAsignacion;
        private string estado;
        private string observacion;
        private DateTime fechaActualizacion;
        private int idDeclaracion;
        #endregion

        #region Propiedades
        [DataMember]
        public int ValoracionId
        {
            get { return valoracionId; }
            set { valoracionId = value; }
        }

        [DataMember]
        public int ValoradorId
        {
            get { return valoradorId; }
            set { valoradorId = value; }
        }
        [DataMember]
        public string Declarante
        {
            get { return declarante; }
            set { declarante = value; }
        }
        [DataMember]
        public string DocumentoDeclarante
        {
            get { return documentoDeclarante; }
            set { documentoDeclarante = value; }
        }
        [DataMember]
        public DateTime FechaRadicacion
        {
            get { return fechaRadicacion; }
            set { fechaRadicacion = value; }
        }
        [DataMember]
        public string NumeroFormulario
        {
            get { return numeroFormulario; }
            set { numeroFormulario = value; }
        }
        [DataMember]
        public string HechosVictimizantes
        {
            get { return hechosVictimizantes; }
            set { hechosVictimizantes = value; }
        }
        [DataMember]
        public int TotalHv
        {
            get { return totalHv; }
            set { totalHv = value; }
        }
        [DataMember]
        public DateTime FechaAsignacion
        {
            get { return fechaAsignacion; }
            set { fechaAsignacion = value; }
        }
        [DataMember]
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        [DataMember]
        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }
        [DataMember]
        public DateTime FechaActualizacion
        {
            get { return fechaActualizacion;  }
            set {fechaActualizacion = value; }
        }
        [DataMember]
        public int IdDeclaracion
        {
            get { return idDeclaracion; }
            set { idDeclaracion = value; }
        }
        #endregion
    }
}
