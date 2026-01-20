using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using System.Runtime.Serialization;
using System.Reflection;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.CriticaN
{
    [DataContract]
    public class clsRespuestaCritica
    {
        #region Atributos

        private int? _nIdCriticaN = null;
        private int? _nRespuesta = null;
        private long _nIdUsuario = 0;
        private long _nIdRadicacion = 0;
        private string _cObservacion = null;

        #endregion
        #region Propidades
        [DataMember]
        public int? NIdCriticaN
        {
            get { return _nIdCriticaN; }
            set
            {
                if (_nIdCriticaN == value) return;
                _nIdCriticaN = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }
        [DataMember]
        public int? NRespuesta
        {
            get { return _nRespuesta; }
            set
            {
                if (_nRespuesta == value) return;
                _nRespuesta = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }
        [DataMember]
        public long NIdUsuario 
        { 
            get { return _nIdUsuario; }
            set
            {
                if (_nIdUsuario == value) return;
                _nIdUsuario = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public long NIdRadicacion
        {
            get { return _nIdRadicacion; }
            set
            {
                if (_nIdRadicacion == value) return;
                _nIdRadicacion = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public string CObservacion
        {
            get { return _cObservacion; }
            set
            {
                if (_cObservacion == value) return;
                _cObservacion = value;
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
    }
}
