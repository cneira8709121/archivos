using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using resx=Ruv.Infrastructure.Crosscutting.Resources;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario
{
    [DataContract]
    public class clsFormulario : INotifyPropertyChanged
    {
        #region Attributes

        private bool _bSelected = false;
        private uint _nId = 0;
        private long? _nIdPais = (long?)ePaises.Colombia;
        private long? _nIdDepartamento = null;
        private long? _nIdMunicipio = null;
        private short? _nIdEntidad = null;
        private eEstadoFormulario _efId = eEstadoFormulario.GENERADO;
        private uint _nIdUsuario = 0;
        private string _cNumeroFormulario = string.Empty;
        private string _cPais = null;
        private string _cDepartamento = null;
        private string _cMunicipio = null;
        private string _cEntidad = null;
        private string _cEstado = string.Empty;
        private string _cTipoVictima = string.Empty;
        private string _cUsuario = string.Empty;
        private bool _bDescargado = false;
        private string _cObservacion = string.Empty;
        private DateTime _dGenerado;
        private DateTime _dUltimaModificacion;
       
        #endregion
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Properties

        [DataMember]
        public bool BSelected
        {
            get { return _bSelected; }
            set
            {
                _bSelected = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        public bool BActive
        {
            get
            {
                bool bActive = false;
                if (_efId != eEstadoFormulario.INACTIVO) bActive = true;
                return bActive;
            }
            set
            {

            }
        }
        
        [DataMember]
        public uint NId
        {
            get { return _nId; }
            set
            {
                if (value == _nId) return;
                _nId = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public long? NIdPais
        {
            get { return _nIdPais; }
            set
            {
                _nIdPais = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public long? NIdDepartamento
        {
            get { return _nIdDepartamento; }
            set
            {
                if (value == _nIdDepartamento) return;
                _nIdDepartamento = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public long? NIdMunicipio
        {
            get { return _nIdMunicipio; }
            set
            {
                if (value == _nIdMunicipio) return;
                _nIdMunicipio = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public short? NIdEntidad
        {
            get { return _nIdEntidad; }
            set
            {
                if (value == _nIdEntidad) return;
                _nIdEntidad = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public eEstadoFormulario EfId
        {
            get { return _efId; }
            set
            {
                if (value == _efId) return;
                _efId = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public uint NIdUsuario
        {
            get { return _nIdUsuario; }
            set
            {
                if (value == _nIdUsuario) return;
                _nIdUsuario = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CNumeroFormulario
        {
            get { return _cNumeroFormulario; }
            set
            {
                if (value == _cNumeroFormulario) return;
                _cNumeroFormulario = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CPais
        {
            get { return _cPais; }
            set
            {
                if (value == _cPais) return;
                _cPais = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CDepartamento
        {
            get { return _cDepartamento; }
            set
            {
                if (value == _cDepartamento) return;
                _cDepartamento = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CMunicipio
        {
            get { return _cMunicipio; }
            set
            {
                if (value == _cMunicipio) return;
                _cMunicipio = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CEntidad
        {
            get { return _cEntidad; }
            set
            {
                if (value == _cEntidad) return;
                _cEntidad = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CEstado
        {
            get { return _cEstado; }
            set
            {
                if (value == _cEstado) return;
                _cEstado = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CTipoVictima
        {
            get { return _cTipoVictima; }
            set
            {
                if (value == _cTipoVictima) return;
                _cTipoVictima = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CUsuario
        {
            get { return _cUsuario; }
            set
            {
                if (value == _cUsuario) return;
                _cUsuario = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public bool BDescargado
        {
            get { return _bDescargado; }
            set
            {
                if (value == _bDescargado) return;
                _bDescargado = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }
        [DataMember]
        public string CObservacion
        {
            get { return _cObservacion; }
            set { _cObservacion = value; }
        }
        [DataMember]
        public DateTime DGenerado
        {
            get { return _dGenerado; }
            set
            {
                _dGenerado = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }
        [DataMember]
        public DateTime DUltimaModificacion
        {
            get { return _dUltimaModificacion; }
            set
            {
                _dUltimaModificacion = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

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
    }
}
