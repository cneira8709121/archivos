using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    [Serializable]
    [DataContract]
    public class clsUsuario : INotifyPropertyChanged
    {
        public clsUsuario()
        {
            Permisos = new List<ePermisosUsuario>();
            RolesUsuario = new List<eRolesUsuario>();
        }
        private string _usuario;
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Cuenta { get; set; }
        [DataMember]
        public string Nombre
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                ReportarCambioPropiedad("Nombre");
            }
        }
        [DataMember]
        public string Contraseña { get; set; }
        [DataMember]
        public bool Activo { get; set; }
        [DataMember]
        public bool Bloqueado { get; set; }
        [DataMember]
        public int IntentosErrados { get; set; }
        [DataMember]
        public List<ePermisosUsuario> Permisos { get; set; }
        [DataMember]
        public List<eRolesUsuario> RolesUsuario { get; set; }
        [DataMember]
        public byte[] ImagenFirmaDigital { get; set; }
        /// <summary> 
        /// El prefijo que identifica la versión del archivo de parámetros.
        /// </summary>
        [DataMember]
        public string VersionArchivoParametros { get; set; }
        [DataMember]
        public string Cargo { get; set; }
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public string MensajeAutenticacionFallida { get; set; }
        [DataMember]
        public bool UtilizaCertificadoDigital { get; set; }
        [DataMember]
        public int UnidadTerritorialId { get; set; }
        [DataMember]
        public int ID_MUNICIPIO { get; set; }
        [DataMember]
        public int ID_DEPARTAMENTO { get; set; }
        [DataMember]
        public Int32 ID_PAIS { get; set; }
        [DataMember]
        public Int32 ID_ENTIDADMUNICIPIO { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void ReportarCambioPropiedad(string nombrePropiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
                PropertyChanged(this, new PropertyChangedEventArgs("HayParametrosMinimosParaRegistrar"));
            }
        }
    }
}
