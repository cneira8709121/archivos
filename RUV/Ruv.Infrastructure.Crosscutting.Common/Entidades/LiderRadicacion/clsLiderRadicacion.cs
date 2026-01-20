using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Reflection;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.LiderRadicacion
{
    [DataContract]
    public class clsLiderRadicacion : INotifyPropertyChanged
    {
        #region Attributes

        private clsRadicacion _radActual;
        private clsRadicacion _radExistente;
        private clsFormulario _frmRadicacionPrevia;
        private string _cObservacion;

        #endregion
        #region Properties

        /// <summary>
        /// Radicación actual que evalúa el líder de radicación
        /// </summary>
        [DataMember]
        public clsRadicacion RadActual
        {
            get { return _radActual; }
            set {
                if (value == _radActual) return;
                _radActual = value;
                OnPropertyChanged(MethodBase.GetCurrentMethod().Name);
            }
        }
        /// <summary>
        /// Radicación que ya existe con el número de formulario actual sin importar si este está inactivo
        /// </summary>
        [DataMember]
        public clsRadicacion RadExistente
        {
            get { return _radExistente; }
            set { _radExistente = value; }
        }
        /// <summary>
        /// Formulario de la radicacion previa
        /// </summary>
        [DataMember]
        public clsFormulario FrmRadicacionPrevia
        {
            get { return _frmRadicacionPrevia; }
            set { _frmRadicacionPrevia = value; }
        }
        /// <summary>
        /// Observaciones que debe ingresar el líder de radicación con los cambios realizados
        /// </summary>
        [DataMember]
        public string CObservacion
        {
            get { return _cObservacion; }
            set {
                if (value == _cObservacion) return;
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
