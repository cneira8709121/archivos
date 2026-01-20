using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common.General;


namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public partial class clsTomaDeclaracion : clsEntidadBase, IDataErrorInfo, IValidationEntity
    {
        /// <summary>
        /// Referencia a la declaración padre.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public clsDeclaracion Declaracion { get; set; }

        public string Scope { get { return "HOJA 1"; } }

        #region CONSTRUCTOR

        public clsTomaDeclaracion(clsDeclaracion declaracion)
        {
            Declaracion = declaracion;

            ConstructorGeneral();
        }

        public clsTomaDeclaracion()
        {
            ConstructorGeneral();
        }

        public void InicializarHechos()
        {
            // Inicializar la cantidad por anexo a 1.
            Hechos = new BindingList<int>();
            for (int i = 0; i < 12; i++)
                Hechos.Add(0);

            // Suscribirse al cambio en la lista de hechos.
            Hechos.ListChanged += delegate
            {
                ReportarCambioPropiedad("Hechos");
                ReportarCambioPropiedad("HechosOtrosCual");
            };
        }

        private void ConstructorGeneral()
        {
            InicializarHechos();

            Encargado = new clsTomaDeclaracion_Encargado()
            {
                ID = int.MinValue,
                EstadoRegistro = eEstadoRegistro.Insertar
            };

            _EstadoRegistro = eEstadoRegistro.Insertar;
        }
        #endregion

        private bool _ModificarTipoDocumentoDeclarante;

        public bool ModificarTipoDocumentoDeclarante
        {
            get { return _ModificarTipoDocumentoDeclarante; }
            set { _ModificarTipoDocumentoDeclarante = value;
                ReportarCambioPropiedad("ModificarTipoDocumentoDeclarante");
            }
        }


        #region PREGUNTA 1

        private Int64? _LugarDeclaracionPais;
        [DataMember]
        public Int64? LugarDeclaracionPais
        {
            get { return _LugarDeclaracionPais; }
            set
            {
                _LugarDeclaracionPais = value;
                ReportarCambioPropiedad("LugarDeclaracionPais");
            }
        }

        private Int64? _LugarDeclaracionDepartamento;
        [DataMember]
        public Int64? LugarDeclaracionDepartamento
        {
            get { return _LugarDeclaracionDepartamento; }
            set
            {
                _LugarDeclaracionDepartamento = value;
                ReportarCambioPropiedad("LugarDeclaracionDepartamento");
            }
        }

        private Int64? _LugarDeclaracionMunicipio;
        [DataMember]
        public Int64? LugarDeclaracionMunicipio
        {
            get { return _LugarDeclaracionMunicipio; }
            set
            {
                _LugarDeclaracionMunicipio = value;
                ReportarCambioPropiedad("LugarDeclaracionMunicipio");
            }
        }

        private Int16? _LugarDeclaracionEntidadMunicipio;
        [DataMember]
        public Int16? LugarDeclaracionEntidadMunicipio
        {
            get { return _LugarDeclaracionEntidadMunicipio; }
            set
            {
                _LugarDeclaracionEntidadMunicipio = value;
                ReportarCambioPropiedad("LugarDeclaracionEntidadMunicipio");
            }
        }

        #endregion

        #region PREGUNTA 2

        private int? _EntidadQueAtiende;
        [DataMember]
        public int? EntidadQueAtiende
        {
            get { return _EntidadQueAtiende; }
            set
            {
                _EntidadQueAtiende = value;
                ReportarCambioPropiedad("EntidadQueAtiende");
                ReportarCambioPropiedad("LugarDeclaracionPais");
            }
        }

        #endregion

        #region PREGUNTA 3

        private DateTime? _FechaDeclaracion;
        [DataMember]
        public DateTime? FechaDeclaracion
        {
            get { return _FechaDeclaracion; }
            set
            {
                _FechaDeclaracion = value;
                ReportarCambioPropiedad("FechaDeclaracion");
            }
        }

        #endregion

        #region PREGUNTA 4

        private clsTomaDeclaracion_Encargado _Encargado;
        [DataMember]
        public clsTomaDeclaracion_Encargado Encargado
        {
            get { return _Encargado; }
            set
            {
                _Encargado = value;

                if (_Encargado.TomaDeclaracion == null)
                    _Encargado.TomaDeclaracion = this;
                ReportarCambioPropiedad("Encargado");
            }
        }

        #endregion

        #region PREGUNTA 5 a 7

        private int? _DeclaranteId;
        /// <summary>
        /// Código del declarante.
        /// </summary>
        [DataMember]
        public int? DeclaranteId
        {
            get { return _DeclaranteId; }
            set
            {
                _DeclaranteId = value;
                if (!value.HasValue || Declaracion == null)
                    PADeclarante = null;
                else
                    PADeclarante = Declaracion.PersonasAfectadas.ListaPersonas
                    .FirstOrDefault(x => x.ID == DeclaranteId);

                ReportarCambioPropiedad("DeclaranteId");

                ReportarCambioPropiedad("DeclarantePrimerNombre");
                ReportarCambioPropiedad("DeclarantePrimerApellido");
                ReportarCambioPropiedad("DeclaranteSegundoNombre");
                ReportarCambioPropiedad("DeclaranteSegundoApellido");
                ReportarCambioPropiedad("DeclaranteTipoDocumento");
                ReportarCambioPropiedad("DeclaranteNumeroDocumento");
                ReportarCambioPropiedad("DeclaranteTipoDocumento");
                ReportarCambioPropiedad("DeclaranteFechaNacimiento");
            }
        }

        #endregion

        #region PREGUNTA 8

        private string _DatoContactoDireccion;
        [DataMember]
        public string DatoContactoDireccion
        {
            get { return _DatoContactoDireccion; }
            set
            {
                _DatoContactoDireccion = value;
                ReportarCambioPropiedad("DatoContactoDireccion");
                ReportarCambioPropiedad("DatoContactoDepartamento");
            }
        }

        private int? _TieneDireccionCorrespondencia = 1;
        [DataMember]
        public int? TieneDireccionCorrespondencia
        {
            get { return _TieneDireccionCorrespondencia; }
            set
            {
                _TieneDireccionCorrespondencia = value;

                _DatoContactoDireccion = value.HasValue && value > 0 ? (_DatoContactoDireccion == "SIN NOMENCLATURA" ? null : _DatoContactoDireccion) : "SIN NOMENCLATURA";

                ReportarCambioPropiedad("TieneDireccionCorrespondencia");
                ReportarCambioPropiedad("DatoContactoDireccion");
            }
        }
        

        // -------------------------- \\

        private eTipoEntorno? _DatoContactoTipoEntorno;
        [DataMember]
        public eTipoEntorno? DatoContactoTipoEntorno
        {
            get { return _DatoContactoTipoEntorno; }
            set
            {
                _DatoContactoTipoEntorno = value;
                ReportarCambioPropiedad("DatoContactoTipoEntorno");
            }
        }

        private int? _DatoContactoBarrioVeredaId;
        [DataMember]
        public int? DatoContactoBarrioVeredaId
        {
            get { return _DatoContactoBarrioVeredaId; }
            set
            {
                _DatoContactoBarrioVeredaId = value;
                ReportarCambioPropiedad("DatoContactoBarrioVeredaId");
            }
        }

        private int? _DatoContactoLocalidadCorregimientoId;
        [DataMember]
        public int? DatoContactoLocalidadCorregimientoId
        {
            get { return _DatoContactoLocalidadCorregimientoId; }
            set
            {
                _DatoContactoLocalidadCorregimientoId = value;
                ReportarCambioPropiedad("DatoContactoLocalidadCorregimientoId");
            }
        }

        private string _DatoContactoBarrioVeredaNombre;
        [DataMember]
        public string DatoContactoBarrioVeredaNombre
        {
            get { return _DatoContactoBarrioVeredaNombre; }
            set
            {
                _DatoContactoBarrioVeredaNombre = value;
                ReportarCambioPropiedad("DatoContactoBarrioVeredaNombre");
            }
        }

        private string _DatoContactoLocalidadCorregimientoNombre;
        [DataMember]
        public string DatoContactoLocalidadCorregimientoNombre
        {
            get { return _DatoContactoLocalidadCorregimientoNombre; }
            set
            {
                _DatoContactoLocalidadCorregimientoNombre = value;
                ReportarCambioPropiedad("DatoContactoLocalidadCorregimientoNombre");
            }
        }

        //private string _DatoContactoEntornoOtro;
        //[DataMember]
        //public string DatoContactoEntornoOtro
        //{
        //  get { return _DatoContactoEntornoOtro; }
        //  set
        //  {
        //    _DatoContactoEntornoOtro = value;
        //    ReportarCambioPropiedad("EntornoOtro");
        //  }
        //}

        private Int64? _DatoContactoPais = (long)ePaises.Colombia;
        [DataMember]
        public Int64? DatoContactoPais
        {
            get { return _DatoContactoPais; }
            set
            {

                _DatoContactoPais = value;
                ReportarCambioPropiedad("DatoContactoPais");
                //ReportarCambioPropiedad("DatoContactoDepartamento");
                //ReportarCambioPropiedad("DatoContactoMunicipio");
            }
        }

        private Int64? _DatoContactoDepartamento;
        [DataMember]
        public Int64? DatoContactoDepartamento
        {
            get { return _DatoContactoDepartamento; }
            set
            {
                _DatoContactoDepartamento = value;
                ReportarCambioPropiedad("DatoContactoDepartamento");
                ReportarCambioPropiedad("DatoContactoMunicipio");
            }
        }

        private Int64? _DatoContactoMunicipio;
        [DataMember]
        public Int64? DatoContactoMunicipio
        {
            get { return _DatoContactoMunicipio; }
            set
            {
                _DatoContactoMunicipio = value;
                ReportarCambioPropiedad("DatoContactoMunicipio");
                ReportarCambioPropiedad("DatoContactoDireccion");
            }
        }

        private string _DatoContactoIndicativo;
        [DataMember]
        public string DatoContactoIndicativo
        {
            get { return _DatoContactoIndicativo; }
            set { _DatoContactoIndicativo = value;
            ReportarCambioPropiedad("DatoContactoIndicativo");
            }
        }
        
        private string _DatoContactoTelefonoFijo;
        [DataMember]
        public string DatoContactoTelefonoFijo
        {
            get { return _DatoContactoTelefonoFijo; }
            set
            {
                _DatoContactoTelefonoFijo = value;
                ReportarCambioPropiedad("DatoContactoTelefonoFijo");
            }
        }

        private string _DatoContactoTelefonoCelular;
        [DataMember]
        public string DatoContactoTelefonoCelular
        {
            get { return _DatoContactoTelefonoCelular; }
            set
            {
                _DatoContactoTelefonoCelular = value;
                ReportarCambioPropiedad("DatoContactoTelefonoCelular");
            }
        }

        private string _DatoContactoCorreoElectronico;
        [DataMember]
        public string DatoContactoCorreoElectronico
        {
            get { return _DatoContactoCorreoElectronico; }
            set
            {
                _DatoContactoCorreoElectronico = value;
                ReportarCambioPropiedad("DatoContactoCorreoElectronico");
            }
        }

        private int? _TieneCorreoElectronico = 1;
        [DataMember]
        public int? TieneCorreoElectronico
        {
            get { return _TieneCorreoElectronico; }
            set
            {
                _TieneCorreoElectronico = value;

                _DatoContactoCorreoElectronico = value.HasValue && value > 0 ? (_DatoContactoCorreoElectronico == "NO INFORMA" ? null : _DatoContactoCorreoElectronico) : "NO INFORMA";

                ReportarCambioPropiedad("TieneCorreoElectronico");
                ReportarCambioPropiedad("DatoContactoCorreoElectronico");
            }
        }


        #endregion

        #region PREGUNTA 9

        private string _DatoAlternoContactoDireccion;
        [DataMember]
        public string DatoAlternoContactoDireccion
        {
            get { return _DatoAlternoContactoDireccion; }
            set
            {
                _DatoAlternoContactoDireccion = value;
                ReportarCambioPropiedad("DatoAlternoContactoDireccion");
                ReportarCambioPropiedad("DatoAlternoContactoDepartamento");
                ReportarCambioPropiedad("DatoAlternoContactoTelefonoFijo");
            }
        }

        private eTipoEntorno? _DatoAlternoContactoTipoEntorno;
        [DataMember]
        public eTipoEntorno? DatoAlternoContactoTipoEntorno
        {
            get { return _DatoAlternoContactoTipoEntorno; }
            set
            {
                _DatoAlternoContactoTipoEntorno = value;
                ReportarCambioPropiedad("DatoAlternoContactoTipoEntorno");
            }
        }

        private int? _DatoAlternoContactoBarrioVeredaId;
        [DataMember]
        public int? DatoAlternoContactoBarrioVeredaId
        {
            get { return _DatoAlternoContactoBarrioVeredaId; }
            set
            {
                _DatoAlternoContactoBarrioVeredaId = value;
                ReportarCambioPropiedad("DatoAlternoContactoBarrioVeredaId");
            }
        }

        private int? _DatoAlternoContactoLocalidadCorregimientoId;
        [DataMember]
        public int? DatoAlternoContactoLocalidadCorregimientoId
        {
            get { return _DatoAlternoContactoLocalidadCorregimientoId; }
            set
            {
                _DatoAlternoContactoLocalidadCorregimientoId = value;
                ReportarCambioPropiedad("DatoAlternoContactoLocalidadCorregimientoId");
            }
        }

        private string _DatoAlternoContactoBarrioVeredaNombre;
        [DataMember]
        public string DatoAlternoContactoBarrioVeredaNombre
        {
            get { return _DatoAlternoContactoBarrioVeredaNombre; }
            set
            {
                _DatoAlternoContactoBarrioVeredaNombre = value;
                ReportarCambioPropiedad("DatoAlternoContactoBarrioVeredaNombre");
            }
        }

        private string _DatoAlternoContactoLocalidadCorregimientoNombre;
        [DataMember]
        public string DatoAlternoContactoLocalidadCorregimientoNombre
        {
            get { return _DatoAlternoContactoLocalidadCorregimientoNombre; }
            set
            {
                _DatoAlternoContactoLocalidadCorregimientoNombre = value;
                ReportarCambioPropiedad("DatoAlternoContactoLocalidadCorregimientoNombre");
            }
        }

        private Int64? _DatoAlternoContactoPais = (long)ePaises.Colombia;
        [DataMember]
        public Int64? DatoAlternoContactoPais
        {
            get { return _DatoAlternoContactoPais; }
            set
            {
                _DatoAlternoContactoPais = value;
                ReportarCambioPropiedad("DatoAlternoContactoPais");
            }
        }

        private Int64? _DatoAlternoContactoDepartamento;
        [DataMember]
        public Int64? DatoAlternoContactoDepartamento
        {
            get { return _DatoAlternoContactoDepartamento; }
            set
            {
                _DatoAlternoContactoDepartamento = value;
                ReportarCambioPropiedad("DatoAlternoContactoDepartamento");
            }
        }

        private Int64? _DatoAlternoContactoMunicipio;
        [DataMember]
        public Int64? DatoAlternoContactoMunicipio
        {
            get { return _DatoAlternoContactoMunicipio; }
            set
            {
                _DatoAlternoContactoMunicipio = value;
                ReportarCambioPropiedad("DatoAlternoContactoMunicipio");
                ReportarCambioPropiedad("DatoAlternoContactoTelefonoFijo");
                ReportarCambioPropiedad("DatoAlternoContactoDireccion");
            }
        }


        private string _DatoContactoAlternoIndicativo;
        [DataMember]
        public string DatoContactoAlternoIndicativo
        {
            get { return _DatoContactoAlternoIndicativo; }
            set
            {
                _DatoContactoAlternoIndicativo = value;
                ReportarCambioPropiedad("DatoContactoAlternoIndicativo");
            }
        }

        private string _DatoAlternoContactoTelefonoFijo;
        [DataMember]
        public string DatoAlternoContactoTelefonoFijo
        {
            get { return _DatoAlternoContactoTelefonoFijo; }
            set
            {
                _DatoAlternoContactoTelefonoFijo = value;
                ReportarCambioPropiedad("DatoAlternoContactoTelefonoFijo");
                ReportarCambioPropiedad("DatoAlternoContactoDepartamento");
                ReportarCambioPropiedad("DatoAlternoContactoDireccion");
            }
        }

        private string _DatoAlternoContactoTelefonoCelular;
        [DataMember]
        public string DatoAlternoContactoTelefonoCelular
        {
            get { return _DatoAlternoContactoTelefonoCelular; }
            set
            {
                _DatoAlternoContactoTelefonoCelular = value;
                ReportarCambioPropiedad("DatoAlternoContactoTelefonoCelular");
            }
        }


        
        private string _DatoAlternoContactoCorreoElectronico;
        [DataMember]
        public string DatoAlternoContactoCorreoElectronico
        {
            get { return _DatoAlternoContactoCorreoElectronico; }
            set
            {
                if (value != null)
                    _DatoAlternoContactoCorreoElectronico = value.Trim().ToUpper();
                else
                    _DatoAlternoContactoCorreoElectronico = value;
                ReportarCambioPropiedad("DatoAlternoContactoCorreoElectronico");
            }
        }

        private int? _MedioDeContactoMensajeTexto;
        [DataMember]
        public int? MedioDeContactoMensajeTexto
        {
            get { return _MedioDeContactoMensajeTexto; }
            set
            {
                _MedioDeContactoMensajeTexto = value;
                ReportarCambioPropiedad("MedioDeContactoMensajeTexto");
            }
        }

        private int? _MedioDeContactoCorreoElectronico;
        [DataMember]
        public int? MedioDeContactoCorreoElectronico
        {
            get { return _MedioDeContactoCorreoElectronico; }
            set
            {
                _MedioDeContactoCorreoElectronico = value;
                ReportarCambioPropiedad("MedioDeContactoCorreoElectronico");
            }
        }

        private int? _MedioDeContactoMensajeVoz;
        [DataMember]
        public int? MedioDeContactoMensajeVoz
        {
            get { return _MedioDeContactoMensajeVoz; }
            set
            {
                _MedioDeContactoMensajeVoz = value;
                ReportarCambioPropiedad("MedioDeContactoMensajeVoz");
            }
        }

        private string _MedioDeContactoOtro;
        [DataMember]
        public string MedioDeContactoOtro
        {
            get { return _MedioDeContactoOtro; }
            set
            {
                _MedioDeContactoOtro = value;
                ReportarCambioPropiedad("MedioDeContactoOtro");
            }
        }

        #endregion

        #region PREGUNTA 10

        private BindingList<int> _Hechos;
        [System.Xml.Serialization.XmlIgnore]
        public BindingList<int> Hechos
        {
            get { return _Hechos; }
            set
            {
                _Hechos = value;
                ReportarCambioPropiedad("Hechos");
                ReportarCambioPropiedad("HechosOtrosCual");
            }
        }

        private string _HechosOtrosCual;
        [DataMember]
        public string HechosOtrosCual
        {
            get { return _HechosOtrosCual; }
            set
            {
                _HechosOtrosCual = value;
                if (!string.IsNullOrWhiteSpace(value))
                ReportarCambioPropiedad("HechosOtrosCual");
            }
        }

        #endregion

    }
}
