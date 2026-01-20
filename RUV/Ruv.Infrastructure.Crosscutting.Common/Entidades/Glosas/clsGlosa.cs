using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    ///  Clase para manejo de Glosas.
    /// </summary>
    [DataContractAttribute(IsReference = true)]
    public partial class clsGlosa : INotifyPropertyChanged
    {
         
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                }
            }
        }
        private global::System.Int32 _ID;

        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PARAM_PROCESO
        {
            get
            {
                return _PARAM_PROCESO;
            }
            set
            {
                _PARAM_PROCESO = value;
            }
        }
        private Nullable<global::System.Int32> _PARAM_PROCESO;

        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_PROCESO
        {
            get
            {
                return _ID_PROCESO;
            }
            set
            {
                _ID_PROCESO = value;
            }
        }
        private Nullable<global::System.Int32> _ID_PROCESO;

        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PARAM_CATEGORIAGLOSA
        {
            get
            {
                return _PARAM_CATEGORIAGLOSA;
            }
            set
            {
                _PARAM_CATEGORIAGLOSA = value;
            }
        }
        private Nullable<global::System.Int32> _PARAM_CATEGORIAGLOSA;

        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PARAM_CONCEPTOGLOSA
        {
            get
            {
                return _PARAM_CONCEPTOGLOSA;
            }
            set
            {
                _PARAM_CONCEPTOGLOSA = value;
            }
        }
        private Nullable<global::System.Int32> _PARAM_CONCEPTOGLOSA;

        [DataMemberAttribute()]
        public global::System.String DESCRIPCIONGLOSA
        {
            get
            {
                return _DESCRIPCIONGLOSA;
            }
            set
            {
                _DESCRIPCIONGLOSA = value;
                ReportarCambioPropiedad("DESCRIPCIONGLOSA");
            }
        }
        private global::System.String _DESCRIPCIONGLOSA;

        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> FECHAGLOSA
        {
            get
            {
                return _FECHAGLOSA;
            }
            set
            {
                _FECHAGLOSA = value;
            }
        }
        private Nullable<global::System.DateTime> _FECHAGLOSA;

        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> FECHAATENCION
        {
            get
            {
                return _FECHAATENCION;
            }
            set
            {
                _FECHAATENCION = value;
            }
        }
        private Nullable<global::System.DateTime> _FECHAATENCION;

        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> FECHAESPERADAATEN
        {
            get
            {
                return _FECHAESPERADAATEN;
            }
            set
            {
                _FECHAESPERADAATEN = value;
            }
        }
        private Nullable<global::System.DateTime> _FECHAESPERADAATEN;

        [DataMemberAttribute()]
        public Nullable<global::System.Int16> GLOSAATEND
        {
            get
            {
                return _GLOSAATEND;
            }
            set
            {
                _GLOSAATEND = value;
            }
        }
        private Nullable<global::System.Int16> _GLOSAATEND;

        [DataMemberAttribute()]
        public Nullable<global::System.Int16> GLOSANOATEND
        {
            get
            {
                return _GLOSANOATEND;
            }
            set
            {
                _GLOSANOATEND = value;
            }
        }
        private Nullable<global::System.Int16> _GLOSANOATEND;

        [DataMemberAttribute()]
        public global::System.String MOTIVONOATEN
        {
            get
            {
                return _MOTIVONOATEN;
            }
            set
            {
                _MOTIVONOATEN = value;
            }
        }
        private global::System.String _MOTIVONOATEN;

        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_USUARIOCREA
        {
            get
            {
                return _ID_USUARIOCREA;
            }
            set
            {
                _ID_USUARIOCREA = value;
            }
        }
        private Nullable<global::System.Int32> _ID_USUARIOCREA;

        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_USUARIOATIENDE
        {
            get
            {
                return _ID_USUARIOATIENDE;
            }
            set
            {
                _ID_USUARIOATIENDE = value;
            }
        }
        private Nullable<global::System.Int32> _ID_USUARIOATIENDE;

        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_USUARIOCOORDINA
        {
            get
            {
                return _ID_USUARIOCOORDINA;
            }
            set
            {
                _ID_USUARIOCOORDINA = value;
            }
        }
        private Nullable<global::System.Int32> _ID_USUARIOCOORDINA;

        [DataMemberAttribute()]
        public global::System.String MOTIVOSIATEN
        {
            get
            {
                return _MOTIVOSIATEN;
            }
            set
            {
                _MOTIVOSIATEN = value;
            }
        }
        private global::System.String _MOTIVOSIATEN;

        [DataMemberAttribute()]
        public Nullable<global::System.Int16> DEVOLUCION
        {
            get
            {
                return _DEVOLUCION;
            }
            set
            {
                _DEVOLUCION = value;
            }
        }
        private Nullable<global::System.Int16> _DEVOLUCION;


        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PARAM_CONCEPTODEVOLUCION
        {
            get
            {
                return _PARAM_CONCEPTODEVOLUCION;
            }
            set
            {
                _PARAM_CONCEPTODEVOLUCION = value;
            }
        }
        private Nullable<global::System.Int32> _PARAM_CONCEPTODEVOLUCION;

        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_USUARIO
        {
            get
            {
                return _ID_USUARIO;
            }
            set
            {
                _ID_USUARIO = value;
            }
        }
        private Nullable<global::System.Int32> _ID_USUARIO;

        [DataMemberAttribute()]
        public Nullable<global::System.Int16> ID_UTERRITORIAL
        {
            get
            {
                return _ID_UTERRITORIAL;
            }
            set
            {
                _ID_UTERRITORIAL = value;
            }
        }
        private Nullable<global::System.Int16> _ID_UTERRITORIAL;

        [DataMember]
        public eEstadoRegistro EstadoRegistro
        {
            get { return _EstadoRegistro; }
            set
            {
                _EstadoRegistro = value;
                ReportarCambioPropiedad("EstadoRegistro");
            }
        }
        private eEstadoRegistro _EstadoRegistro = eEstadoRegistro.SinModificaciones;

        [DataMember]
        public Nullable<global::System.Int32> PARAM_ESTADOGLOSA
        {
            get
            {  return _PARAM_ESTADOGLOSA;}
            set
            {
                _PARAM_ESTADOGLOSA = value;
            }
        }
        private Nullable<global::System.Int32> _PARAM_ESTADOGLOSA;



        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void ReportarCambioPropiedad(string nombrePropiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
                PropertyChanged(this, new PropertyChangedEventArgs("HayParametrosMinimosParaRegistrar"));
            }
        }
        #endregion

    }        
}
