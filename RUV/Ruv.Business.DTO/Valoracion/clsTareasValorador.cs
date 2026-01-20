using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Ruv.Business.DTO.Valoracion
{
    public class clsTareasValorador
    {
        #region Constructores
        public clsTareasValorador()
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
        [Column(Name = "ID")]
        public int ValoracionId
        {
            get { return valoracionId; }
            set { valoracionId = value; }
        }

        [Column(Name = "id_valorador")]
        public int ValoradorId
        {
            get { return valoradorId; }
            set { valoradorId = value; }
        }
        [Column(Name = "Declarante")]
        public string Declarante
        {
            get { return declarante; }
            set { declarante = value; }
        }
        [Column(Name = "DocumentoDeclarante")]
        public string DocumentoDeclarante
        {
            get { return documentoDeclarante; }
            set { documentoDeclarante = value; }
        }
        [Column(Name = "FechaRadicacion")]
        public DateTime FechaRadicacion
        {
            get { return fechaRadicacion; }
            set { fechaRadicacion = value; }
        }
        [Column(Name = "Formulario")]
        public string NumeroFormulario
        {
            get { return numeroFormulario; }
            set { numeroFormulario = value; }
        }
        [Column(Name = "Hechos")]
        public string HechosVictimizantes
        {
            get { return hechosVictimizantes; }
            set { hechosVictimizantes = value; }
        }
        [Column(Name = "TotalHV")]
        public int TotalHv
        {
            get { return totalHv; }
            set { totalHv = value; }
        }
        [Column(Name = "FechaAsignacion")]
        public DateTime FechaAsignacion
        {
            get { return fechaAsignacion; }
            set { fechaAsignacion = value; }
        }
        [Column(Name = "Estado")]
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        [Column(Name = "Observacion")]
        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }
        [Column(Name = "FechaActualizacion")]
        public DateTime FechaActualizacion
        {
            get { return fechaActualizacion; }
            set { fechaActualizacion = value; }
        }
        [Column(Name = "IdDeclaracion")]
        public int IdDeclaracion
        {
            get { return idDeclaracion; }
            set { idDeclaracion = value; }
        }
        #endregion
    }
}
