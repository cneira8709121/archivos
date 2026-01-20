using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using System.ComponentModel;
using System.Reflection;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Devolucion
{
    [DataContract]
    public partial class clsDevolucion : INotifyPropertyChanged
    {
        #region Attributes

        private DateTime? _dRadicacion;
        private DateTime? _dSolicitudDevolucion;
        private int? _nId;
        private int? _nIdUsuario;
        private int? _nIdRadicacion;
        private int? _nIdDeclaracion;
        private int? _nIdEntidadMunicipio;
        private string _cPais;
        private string _cDepartamento;
        private string _cMunicipio;
        private string _cEntidad;
        private string _cNumeroFud;
        private string _cDeclarante;
        private string _cDireccion;
        private int _nTelefono;
        private string _cFuncionario;
        private string _cNumeroGuia;
        private string _cObservaciones;
        private string _cParteEmotivaModificada;
        private List<int> _lstCausalesDevolucion = new List<int>();

        #endregion Attributes
        #region Properties
        
        /// <summary>
        /// Fecha de la radicación obtenida desde la BD
        /// </summary>
        [DataMember]
        public DateTime? DRadicacion
        {
            get
            {
                return _dRadicacion;
            }
            set
            {
                if (value == _dRadicacion) return;
                _dRadicacion = value;
            }
        }
        
        /// <summary>
        /// La fecha de la solicitud de la devolución obtenida desde BD
        /// </summary>
        [DataMember]
        public DateTime? DSolicitudDevolucion
        {
            get
            {
                return _dSolicitudDevolucion;
            }
            set
            {
                if (value == _dSolicitudDevolucion) return;
                _dSolicitudDevolucion = value;
            }
        }

        /// <summary>
        /// Id de la devolución
        /// </summary>
        [DataMember]
        public int? NId
        {
            get
            {
                return _nId;
            }
            set
            {
                if (value == _nId) return;
                _nId = value;
            }
        }

        /// <summary>
        /// Id del usuario que ha solicitado la devolución o que la está actualizando
        /// </summary>
        [DataMember]
        public int? NIdUsuario
        {
            get
            {
                return _nIdUsuario;
            }
            set
            {
                if (value == _nIdUsuario) return;
                _nIdUsuario = value;
            }
        }
        
        /// <summary>
        /// Identificador de la radicación.
        /// </summary>
        [DataMember]
        public int? NIdRadicacion
        {
            get
            {
                return _nIdRadicacion;
            }
            set
            {
                if (value == _nIdRadicacion) return;
                _nIdRadicacion = value;
            }
        }

        /// <summary>
        /// Identificador de la declaracion.
        /// </summary>
        [DataMember]
        public int? NIdDeclaracion
        {
            get
            {
                return _nIdDeclaracion;
            }
            set
            {
                if (value == _nIdDeclaracion) return;
                _nIdDeclaracion = value;
            }
        }

        /// <summary>
        /// Id de la entidad municipio donde está la radicación
        /// </summary>
        [DataMember]
        public int? NIdEntidadMunicipio
        {
            get
            {
                return _nIdEntidadMunicipio;
            }
            set
            {
                if (value == _nIdEntidadMunicipio) return;
                _nIdEntidadMunicipio = value;
            }
        }

        /// <summary>
        /// País donde está la entidad
        /// </summary>
        [DataMember]
        public string CPais
        {
            get
            {
                return _cPais;
            }
            set
            {
                if (value == _cPais) return;
                _cPais= value;
            }
        }

        /// <summary>
        /// Departamento donde está la entidad
        /// </summary>
        [DataMember]
        public string CDepartamento
        {
            get
            {
                return _cDepartamento;
            }
            set
            {
                if (value == _cDeclarante) return;
                _cDepartamento = value;
            }
        }

        /// <summary>
        /// Municipio donde está la entidad
        /// </summary>
        [DataMember]
        public string CMunicipio
        {
            get
            {
                return _cMunicipio;
            }
            set
            {
                if (value == _cMunicipio) return;
                _cMunicipio = value;
            }
        }

        /// <summary>
        /// Nombre de la entidad
        /// </summary>
        [DataMember]
        public string CEntidad
        {
            get
            {
                return _cEntidad;
            }
            set
            {
                if (value == _cEntidad) return;
                _cEntidad = value;
            }
        }

        /// <summary>
        /// Identificador Del FUD.
        /// </summary>
        [DataMember]
        public string CNumeroFud
        {
            get
            {
                return _cNumeroFud;
            }
            set
            {
                if (value == _cNumeroFud) return;
                _cNumeroFud = value;
            }
        }

        /// <summary>
        /// Nombre del declarante el cual se obtiene desde BD.
        /// </summary>
        [DataMember]
        public string CDeclarante
        {
            get 
            {
                return _cDeclarante;
            }
            set
            {
                if (value == _cDeclarante) return;
                _cDeclarante = value;
            }
        }

        /// <summary>
        /// Dirección de la entidad municipio donde está radicado. Se obtiene desde BD y puede actualizar la tabla de Entidad Municipio
        /// </summary>
        [DataMember]
        public string CDireccion
        {
            get
            {
                return _cDireccion;
            }
            set 
            {
                _cDireccion = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Teléfono de la entidad municipio donde está radicado. Se obtiene desde BD y puede actualizar la tabla de Entidad Municipio
        /// </summary>
        [DataMember]
        public int NTelefono
        {
            get
            {
                return _nTelefono;
            }
            set 
            {
               _nTelefono = value;
               OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Nombre del funcionario encargado de la entidad municipio donde está radicado. Se obtiene desde BD y puede actualizar la tabla de Entidad Municipio
        /// </summary>
        [DataMember]
        public string CFuncionario
        {
            get
            {
                return _cFuncionario;
            }
            set 
            {
                _cFuncionario = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Número de guía que va a usar el lider de devolución
        /// </summary>
        [DataMember]
        public string CNumeroGuia
        {
            get
            {
                return _cNumeroGuia;
            }
            set 
            {
               _cNumeroGuia = value;
               OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Observaciones de la devolución.
        /// </summary>
        [DataMember]
        public string CObservaciones
        {
            get
            {
                return _cObservaciones;
            }
            set
            {
                _cObservaciones = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Parte emotiva que puede modificar o no el lider de devolución
        /// </summary>
        [DataMember]
        public string CParteEmotivaModificada
        {
            get
            {
                return _cParteEmotivaModificada;
            }
            set
            {
                if (value == _cParteEmotivaModificada) return;
                _cParteEmotivaModificada = value;
            }
        }

        /// <summary>
        /// Id de cada uno de los causales de devolución seleccionados
        /// </summary>
        [DataMember]
        public List<int> LstCausalesDevolucion
        {
            get { return _lstCausalesDevolucion; }
            set
            {
                _lstCausalesDevolucion = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        #endregion
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Protected methods

        protected void OnPropertyChanged(string sPropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                sPropertyName = sPropertyName.Replace(resx::General.PropiedadSet, string.Empty);
                PropertyChanged(this, new PropertyChangedEventArgs(sPropertyName));
            }
        }

        #endregion
        #region Private methods

        private bool formatoFechaValido(DateTime? propiedad)
        {
            DateTime fechaTent;
            if ((DateTime.TryParse(propiedad.ToString(), out fechaTent))
                && propiedad > new DateTime(1980, 1, 1) && propiedad < new DateTime(2020, 1, 1)
                && propiedad < DateTime.Today.AddDays(1)
                )
                return true;
            else
                return false;
        }

        #endregion
    }
}
