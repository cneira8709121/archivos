using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.ComponentModel;
using resx = Ruv.Infrastructure.Crosscutting.Resources;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public class clsGeografia
    {
        #region Attributes

        private long? _nIdPais = null;
        private long? _nIdDepartamento = null;
        private long? _nIdMunicipio = null;
        private short? _nIdEntidadMunicipio = null;

        #endregion
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Properties

        [DataMember]
        public long? NIdPais
        {
            get { return _nIdPais; }
            set
            {
                if (value == _nIdPais) return;
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
        public short? NIdEntidadMunicipio
        {
            get { return _nIdEntidadMunicipio; }
            set
            {
                if (value == _nIdEntidadMunicipio) return;
                _nIdEntidadMunicipio = value;
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
