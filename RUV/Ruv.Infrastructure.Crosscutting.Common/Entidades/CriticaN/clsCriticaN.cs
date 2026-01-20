using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.CriticaN
{
    [DataContract]
    public class clsCriticaN : INotifyPropertyChanged
    {
        #region Attributes

        private int _nId;
        private string _cObservacion;
        private List<int> _lstCausal = new List<int>();
        private List<int> _lstValidacion = new List<int>();

        #endregion
        #region Properties

        [DataMember]
        public int NId
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
        public string CObservacion
        {
            get { return _cObservacion; }
            set
            {
                if (value == _cObservacion) return;
                _cObservacion = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public List<int> LstCausal
        {
            get { return _lstCausal; }
            set
            {
                if (value == _lstCausal) return;
                _lstCausal = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }

        [DataMember]
        public List<int> LstValidacion
        {
            get { return _lstValidacion; }
            set
            {
                if (value == _lstValidacion) return;
                _lstValidacion = value;
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
