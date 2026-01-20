using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.Infrastructure.Crosscutting.Common.ActosAdmin
{
    [DataContract]
    public class clsActosAdminstrativos
    {

        public clsActosAdminstrativos()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        private int _ID;

        [DataMember]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string consecutivo;

        [DataMember]
        public string Consecutivo
        {
            get { return consecutivo; }
            set { consecutivo = value; }
        }
        private DateTime fecha;

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private int documentoId;

        [DataMember]
        public int DocumentoId
        {
            get { return documentoId; }
            set { documentoId = value; }
        }
        private string documento;

        [DataMember]
        public string Documento
        {
            get { return documento; }
            set { documento = value; }
        }
        private int tipoDocumentoId;

        [DataMember]
        public int TipoDocumentoId
        {
            get { return tipoDocumentoId; }
            set { tipoDocumentoId = value; }
        }
        private string tipoDocumento;

        [DataMember]
        public string TipoDocumento
        {
            get { return tipoDocumento; }
            set { tipoDocumento = value; }
        }
        private string num_interno;

        [DataMember]
        public string Num_interno
        {
            get { return num_interno; }
            set { num_interno = value; }
        }
        private int declaracionId;

        [DataMember]
        public int DeclaracionId
        {
            get { return declaracionId; }
            set { declaracionId = value; }
        }
        private string nroFormulario;

        [DataMember]
        public string NroFormulario
        {
            get { return nroFormulario; }
            set { nroFormulario = value; }
        }
        private string descripcion;

        [DataMember]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private string dirigido;

        [DataMember]
        public string Dirigido
        {
            get { return dirigido; }
            set { dirigido = value; }
        }
        private int personaId;

        [DataMember]
        public int PersonaId
        {
            get { return personaId; }
            set { personaId = value; }
        }
        private string persona;

        [DataMember]
        public string Persona
        {
            get { return persona; }
            set { persona = value; }
        }
        private string usuario;

        [DataMember]
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        private int usuarioId;

        [DataMember]
        public int UsuarioId
        {
            get { return usuarioId; }
            set
            {
                usuarioId = value;
            }
        }

        private int estadoId;

        [DataMember]
        public int EstadoId
        {
            get { return estadoId; }
            set
            {
                estadoId = value;
            }
        }
        private string estado;

        [DataMember]
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        protected eEstadoRegistro _EstadoRegistro;

        [DataMember]
        public eEstadoRegistro EstadoRegistro
        {
            get { return _EstadoRegistro; }
            set
            {
                _EstadoRegistro = value;
            }
        }
    }
}
