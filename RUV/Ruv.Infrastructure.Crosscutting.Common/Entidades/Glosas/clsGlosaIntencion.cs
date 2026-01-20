using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// clase para el manejo de Intenciones de Glosa.
    /// </summary>
    [DataContractAttribute(IsReference = true)]
    public partial class clsGlosaIntencion : INotifyPropertyChanged
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
            public Nullable<global::System.Int32> PARAM_CATEGORIAINGLOSA
            {
                get
                {
                    return _PARAM_CATEGORIAINGLOSA;
                }
                set
                {
                    _PARAM_CATEGORIAINGLOSA = value;
                }
            }
            private Nullable<global::System.Int32> _PARAM_CATEGORIAINGLOSA;
        
        [DataMemberAttribute()]
            public global::System.String DESCRIPCIONINGLOSA
            {
                get
                {
                    return _DESCRIPCIONINGLOSA;
                }
                set
                {
                    _DESCRIPCIONINGLOSA = value;
                }
            }
            private global::System.String _DESCRIPCIONINGLOSA;

        [DataMemberAttribute()]
            public Nullable<global::System.DateTime> FECHAINGLOSA
            {
                get
                {
                    return _FECHAINGLOSA;
                }
                set
                {
                    _FECHAINGLOSA = value;
                }
            }
            private Nullable<global::System.DateTime> _FECHAINGLOSA;
        
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

        [DataMember]
            public eEstadoRegistro EstadoRegistro
            {
                get { return _EstadoRegistro; }
                set
                {
                    _EstadoRegistro = value;
                }
            }
            private eEstadoRegistro _EstadoRegistro = eEstadoRegistro.SinModificaciones;
            [DataMember]
            public Nullable<global::System.Int32> PARAM_ESTADOGLOSA
            {
                get
                { return _PARAM_ESTADOGLOSA; }
                set
                {
                    _PARAM_ESTADOGLOSA = value;
                    ReportarCambioPropiedad("PARAM_ESTADOGLOSA");
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
