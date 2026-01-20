using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Windows;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Almacena todos los datos que puede contener una Radicación.
    /// </summary>
    [DataContract]
    public partial class clsRadicacion: INotifyPropertyChanged
    {
        public bool HayParametrosMinimosParaRegistrar
        {
            get
            {
                Boolean Resultado;
                if ((bool)MODOFORMULARIO)
                {
                    Resultado =
                        PropiedadConValor(NRO_FORMULARIO) &&
                        //PropiedadConValor(ID_MUNICIPIO.ToString())
                      (PropiedadConValor(ID_ENTIDADMUNICIPIO))
                        //&& PropiedadConValor(PARAM_TIPOENTIDAD)
                      && formatoFechaValido(FECHALLEGADA)
                      && PropiedadConValor(PrimerNombre)
                      && PropiedadConValor(PrimerApellido)
                      && PropiedadConValor(TipoDocumento)
                      && ((Enum.GetValues(typeof(eTipoDocumentoSinNumero)).Cast<int>().Contains((int)TipoDocumento)) || PropiedadConValor(NumeroDocumento))
                      && PropiedadConValor(ID_TIPORADICACION)
                      && PropiedadConValor(RUTAIMAGEN);
                }
                else
                {
                    Resultado =
                      PropiedadConValor(NRO_FORMULARIO)
                      && formatoFechaValido(FECHALLEGADA)
                      && PropiedadConValor(ID_TIPORADICACION)
                      && PropiedadConValor(RUTAIMAGEN);
                }

                //if (Resultado)
                //{
                //  var validarFormulario = formatoNoFormularioValido(NRO_FORMULARIO);
                //  Resultado = Resultado && validarFormulario.Item1;
                //}

                //if(Resultado)                    
                return Resultado;
            }
        }

        /// <summary>
        /// modos que puede tomar el formulario
        /// </summary>
        //private enum MODOFORMULARIO { formulario, devolucion }
        //public Modo ModoFormulario { get; set; }

        private Nullable<global::System.Int32> _ID;
        /// <summary>
        /// Código de la Radicación, llave principal.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        Boolean PropiedadConValor(string propiedad)
        {
            return !string.IsNullOrWhiteSpace(propiedad);
        }
        Boolean PropiedadConValor(DateTime propiedad)
        {
            if (DateTime.MinValue == propiedad)
                return false;
            else
                return true;
        }
        Boolean PropiedadConValor(int? propiedad)
        {
            if (propiedad.HasValue)
                return true;
            else
                return false;
        }
        Boolean formatoFechaValido(DateTime? propiedad)
        {
            DateTime fechaTent;
            if ((DateTime.TryParse(propiedad.ToString(), out fechaTent))
                && propiedad > new DateTime(1980, 1, 1) && propiedad < new DateTime(2100, 1, 1)
                && propiedad < DateTime.Today.AddDays(1)
                )
                return true;
            else
                return false;
        }

        /// <summary>
        /// Se valida que el numero de formulario digitado tenga el formato correcto
        /// Desde la presentación (WPF) se controla que solo se digiten letras y numeros.
        /// </summary>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        Tuple<Boolean, string> formatoNoFormularioValido(string propiedad)
        {
            string numFormulario;
            string numeros;
            int digito;
            int sum = 0;
            List<string> letras = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
            string letra;


            //Validar la longitud del numero de formulario
            if (propiedad.Length != 11)
            {
                var Resultado = new Tuple<bool, string>(false,
                          "El número de formulario debe tener una longitud de 11 caracteres");
                return Resultado;
            }

            numFormulario = propiedad;
            numeros = propiedad.Substring(2, 9);

            //Validar que el primer caracter del numero de formulario no sea un Numero
            //debe ser una letra
            if (int.TryParse(propiedad.Substring(0, 1), out digito))
            {
                var Resultado = new Tuple<bool, string>(false,
                          "El número de formulario debe iniciar por una LETRA");
                return Resultado;
            }

            //Validar que el segundo caracter del numero de formulario no sea un Numero
            //debe ser una Letra
            if (int.TryParse(propiedad.Substring(1, 1), out digito))
            {
                var Resultado = new Tuple<bool, string>(false,
                          "El segundo caracter del número de formulario debe ser una LETRA");
                return Resultado;
            }

            //Hacer el chek sum
            for (int cont = 0; cont <= numeros.Length - 1; cont++)
            {
                if (int.TryParse(numeros.Substring(cont, 1), out digito))
                    sum = sum + Convert.ToInt32(numeros.Substring(cont, 1));
                else
                {
                    var Resultado = new Tuple<bool, string>(false,
                              "Solamente los dos primeros caracteres del número del formulario pueden ser letras");
                    return Resultado;
                }
            }

            numeros = sum.ToString();
            sum = 0;
            for (int cont = 0; cont <= numeros.Length - 1; cont++)
            {
                sum = sum + Convert.ToInt32(numeros.Substring(cont, 1));
            }

            letra = letras[13 - sum];

            if (numFormulario.Substring(1, 1) != letra)
            {
                var Resultado = new Tuple<bool, string>(false,
                          "El número de formulario no cumple con las condiciones de numeración");
                return Resultado;
            }

            var Resul = new Tuple<bool, string>(true,
                        null);
            return Resul;
        }

        #region Método de generador

        /// <summary>
        /// Creación de un objeto que representa una radicación según las propiedades expuestas por los usuarios.
        /// </summary>
        /// <param name="id">Valor inicial de la propiedad ID.</param>
        /// <param name="fECHAREGISTRO">Valor inicial de la propiedad FECHAREGISTRO.</param>
        /// <param name="cONSECUTIVO">Valor inicial de la propiedad CONSECUTIVO.</param>
        public static clsRadicacion CreateclsRadicacion(global::System.Int32 id, global::System.DateTime fECHAREGISTRO, global::System.Int64 cONSECUTIVO)
        {
            clsRadicacion tBRADICACION = new clsRadicacion();
            tBRADICACION.ID = id;
            tBRADICACION.FECHAREGISTRO = fECHAREGISTRO;
            tBRADICACION.CONSECUTIVO = cONSECUTIVO;
            return tBRADICACION;
        }

        #endregion

        #region Propiedades primitivas
        /// <summary>
        /// La fecha de Registro del paquete de información de radicación.
        /// </summary>
        [DataMemberAttribute()]
        public global::System.DateTime FECHAREGISTRO
        {
            get
            {
                return _FECHAREGISTRO;
            }
            set
            {
                if (_FECHAREGISTRO != value)
                {
                    _FECHAREGISTRO = value;
                }
                //ReportarCambioPropiedad("FECHAREGISTRO");
            }
        }
        private global::System.DateTime _FECHAREGISTRO;


        /// <summary>
        /// Id declaracion
        /// </summary>
        [DataMemberAttribute()]
        public global::System.Int32? ID_DECLARACION
        {
            get
            {
                return _ID_DECLARACION;
            }
            set
            {
                if (_ID_DECLARACION != value)
                {
                    _ID_DECLARACION = value;
                }
                //ReportarCambioPropiedad("FECHAREGISTRO");
            }
        }
        private global::System.Int32? _ID_DECLARACION;
        /// <summary>
        /// el número consecutivo de radicación.
        /// </summary>
        [DataMemberAttribute()]
        public global::System.Int64 CONSECUTIVO
        {
            get
            {
                return _CONSECUTIVO;
            }
            set
            {
                if (_CONSECUTIVO != value)
                {
                    _CONSECUTIVO = value;
                }
            }
        }
        private global::System.Int64 _CONSECUTIVO;

        /// <summary>
        /// número de Captura del formulario de RUV.
        /// </summary>
        [DataMemberAttribute()]
        public global::System.String NRO_FORMULARIO
        {
            get
            {
                return _NRO_FORMULARIO;
            }
            set
            {
                if (_NRO_FORMULARIO != value)
                {
                    _NRO_FORMULARIO = value;
                    ReportarCambioPropiedad("NRO_FORMULARIO");

                }
            }
        }
        private global::System.String _NRO_FORMULARIO;

        /// <summary>
        /// Código del Municipio de donde se recibé la radicación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int64> ID_MUNICIPIO
        {
            get
            {
                return _ID_MUNICIPIO;
            }
            set
            {
                _ID_MUNICIPIO = value;
                ReportarCambioPropiedad("ID_MUNICIPIO");
            }
        }
        private Nullable<global::System.Int64> _ID_MUNICIPIO;

        /// <summary>
        /// Código del departamento de donde se recibé la radicación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int64> ID_DEPARTAMENTO
        {
            get
            {
                return _ID_DEPARTAMENTO;
            }
            set
            {
                _ID_DEPARTAMENTO = value;
                ReportarCambioPropiedad("ID_DEPARTAMENTO");
            }
        }
        private Nullable<global::System.Int64> _ID_DEPARTAMENTO;

        /// <summary>
        /// Código del PAIS de donde se recibé la radicación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int64> ID_PAIS
        {
            get
            {
                return _ID_PAIS;
            }
            set
            {
                _ID_PAIS = value;

                if (ID_PAIS == (int)ePaises.Colombia)
                {
                    if (PARAM_TIPOENTIDAD == (int)eEntidadAtiende.Consulado)
                        PARAM_TIPOENTIDAD = null;
                }
                else if (ID_PAIS.HasValue && ID_PAIS.Value != 0 && ID_PAIS.Value != (int)ePaises.Colombia)
                {
                    PARAM_TIPOENTIDAD = (int)eEntidadAtiende.Consulado;
                }
            }
        }
        private Nullable<global::System.Int64> _ID_PAIS = (Nullable<global::System.Int64>)ePaises.Colombia;

        /// <summary>
        /// Codigo de la entidadMunicipio de donde se recibe la radicación
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> ID_ENTIDADMUNICIPIO
        {
            get
            {
                return _ID_ENTIDADMUNICIPIO;
            }
            set
            {
                _ID_ENTIDADMUNICIPIO = value;
                ReportarCambioPropiedad("ID_ENTIDADMUNICIPIO");
            }
        }
        private Nullable<global::System.Int16> _ID_ENTIDADMUNICIPIO;

        /// <summary>
        /// Código de la Unidad territorial que envía el paquete para radicación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_UTERRITORIALENVIA
        {
            get
            {
                return _ID_UTERRITORIALENVIA;
            }
            set
            {
                _ID_UTERRITORIALENVIA = value;
            }
        }
        private Nullable<global::System.Int32> _ID_UTERRITORIALENVIA;

        /// <summary>
        /// Código de la Unidad Territorial que Recibe el paquete de radicación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_UTERRITORIALRECIBE
        {
            get
            {
                return _ID_UTERRITORIALRECIBE;
            }
            set
            {
                _ID_UTERRITORIALRECIBE = value;
            }
        }
        private Nullable<global::System.Int32> _ID_UTERRITORIALRECIBE;

        /// <summary>
        /// El tipo de Entidad que recibé el paquete de radicación ( podría ser cualquiera del Ministerio Público ) .
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PARAM_TIPOENTIDAD
        {
            get
            {
                return _PARAM_TIPOENTIDAD;
            }
            set
            {
                _PARAM_TIPOENTIDAD = value;
                ReportarCambioPropiedad("PARAM_TIPOENTIDAD");
            }
        }
        private Nullable<global::System.Int32> _PARAM_TIPOENTIDAD;

        /// <summary>
        /// Nombre de la Entidad que recibe el paquete de radicación  ( podría ser cualquiera del Ministerio Público ) ..
        /// </summary>
        [DataMemberAttribute()]
        public global::System.String NOMBREENTIDAD
        {
            get
            {
                return _NOMBREENTIDAD;
            }
            set
            {
                _NOMBREENTIDAD = value;
            }
        }
        private global::System.String _NOMBREENTIDAD;

        /// <summary>
        /// Fecha de Envió del paquete de radicación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> FECHAENVIO
        {
            get
            {
                return _FECHAENVIO;
            }
            set
            {
                _FECHAENVIO = value;
            }
        }
        private Nullable<global::System.DateTime> _FECHAENVIO;

        /// <summary>
        /// Fecha de llegada del paquete para radicación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> FECHALLEGADA
        {
            get
            {
                return _FECHALLEGADA;
            }
            set
            {
                _FECHALLEGADA = value;
                ReportarCambioPropiedad("FECHALLEGADA");
            }
        }
        private Nullable<global::System.DateTime> _FECHALLEGADA = DateTime.Now;

        /// <summary>
        /// Cantidad de documentos Foliados que se recibieron en el paquete de radicación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> CANTIDADDOCUMENTOS
        {
            get
            {
                return _CANTIDADDOCUMENTOS;
            }
            set
            {
                _CANTIDADDOCUMENTOS = value;
            }
        }
        private Nullable<global::System.Int32> _CANTIDADDOCUMENTOS;

        /// <summary>
        /// Código de la  Unidad territorial que radica el paquete.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> ID_UTERRITORIALRADICA
        {
            get
            {
                return _ID_UTERRITORIALRADICA;
            }
            set
            {
                _ID_UTERRITORIALRADICA = value;
            }
        }
        private Nullable<global::System.Int16> _ID_UTERRITORIALRADICA;

        /// <summary>
        /// Código del usuario del Aplicativo que realiza la radicación del paquete.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_USUARIO_RADICA
        {
            get
            {
                return _ID_USUARIO_RADICA;
            }
            set
            {
                _ID_USUARIO_RADICA = value;
            }
        }
        private Nullable<global::System.Int32> _ID_USUARIO_RADICA;

        /// <summary>
        /// Indica si la radicación del paquete corresponde a una Urgencia.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_RADICA_URGENCIA
        {
            get
            {
                return _ID_RADICA_URGENCIA;
            }
            set
            {
                _ID_RADICA_URGENCIA = value;
            }
        }
        private Nullable<global::System.Int32> _ID_RADICA_URGENCIA;

        /// <summary>
        /// Corresponde al Tipo de radicación que se realiza, en el caso de que se necesita radicar otro tipo de documentos; por ejemplo Novedades.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PARAM_TIPOACCIONES
        {
            get
            {
                return _PARAM_TIPOACCIONES;
            }
            set
            {
                _PARAM_TIPOACCIONES = value;
            }
        }
        private Nullable<global::System.Int32> _PARAM_TIPOACCIONES;

        /// <summary>
        /// Si hay una Modificación.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> MODIFICACION
        {
            get
            {
                return _MODIFICACION;
            }
            set
            {
                _MODIFICACION = value;
            }
        }
        private Nullable<global::System.Int16> _MODIFICACION;

        /// <summary>
        /// Nonmbre de la entidad que envía..
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PARAM_ENTIDADENVIANOMBRE
        {
            get
            {
                return _PARAM_ENTIDADENVIANOMBRE;
            }
            set
            {
                _PARAM_ENTIDADENVIANOMBRE = value;
            }
        }
        private Nullable<global::System.Int32> _PARAM_ENTIDADENVIANOMBRE;

        /// <summary>
        /// Código del Tipo documental, en caso de ser necesario.
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_TIPODOCUMENTAL
        {
            get
            {
                return _ID_TIPODOCUMENTAL;
            }
            set
            {
                _ID_TIPODOCUMENTAL = value;
            }
        }
        private Nullable<global::System.Int32> _ID_TIPODOCUMENTAL;

        /// <summary>
        /// Id del tipo de radicación
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_TIPORADICACION
        {
            get
            {
                return _ID_TIPORADICACION;
            }
            set
            {
                _ID_TIPORADICACION = value;
                if (value == (int)eTipoRadicacion.RadicacionDeclaracion)
                    MODOFORMULARIO = true;
                else
                    if (value == (int)eTipoRadicacion.RadicacionDevolución)
                        MODOFORMULARIO = false;
                ReportarCambioPropiedad("ID_TIPORADICACION");
            }
        }
        private Nullable<global::System.Int32> _ID_TIPORADICACION;

        /// <summary>
        /// Observaciones de la radicación
        /// </summary>
        [DataMemberAttribute()]
        public global::System.String OBSERVACIONES
        {
            get
            {
                return _OBSERVACIONES;
            }
            set
            {
                _OBSERVACIONES = value;
            }
        }
        private global::System.String _OBSERVACIONES;

        /// <summary>
        /// Nombre del archvo de imagen que se carga al realizar la radicación
        /// </summary>
        [DataMemberAttribute()]
        public global::System.String RUTAIMAGEN
        {
            get
            {
                return _RUTAIMAGEN;
            }
            set
            {
                _RUTAIMAGEN = value;
                ReportarCambioPropiedad("RUTAIMAGEN");
            }
        }
        private global::System.String _RUTAIMAGEN;

        /// <summary>
        /// Resultado de la validación al realizar la radicación
        /// </summary>
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PARAM_RESULTADO_VALIDACION
        {
            get
            {
                return _PARAM_RESULTADO_VALIDACION;
            }
            set
            {
                _PARAM_RESULTADO_VALIDACION = value;
            }
        }
        private Nullable<global::System.Int32> _PARAM_RESULTADO_VALIDACION;

        /// <summary>
        /// Expone los modos que puede tomar el formulario  
        /// </summary>
        public Nullable<global::System.Boolean> MODOFORMULARIO
        {
            get
            {
                return _MODOFORMULARIO;
            }
            set
            {
                _MODOFORMULARIO = value;
                ReportarCambioPropiedad("NRO_FORMULARIO");
                ReportarCambioPropiedad("MODOFORMULARIO");
                ReportarCambioPropiedad("PrimerNombre");
                ReportarCambioPropiedad("PrimerApellido");
                ReportarCambioPropiedad("TipoDocumento");
                ReportarCambioPropiedad("NumeroDocumento");

            }
        }
        private Nullable<global::System.Boolean> _MODOFORMULARIO = true;



        private byte[] _DocumentoDigital;
        /// <summary>
        /// El contenido del documento escaneado.
        /// </summary>
        [DataMember]
        public byte[] DocumentoDigital
        {
            get { return _DocumentoDigital; }
            set
            {
                _DocumentoDigital = value;
                ReportarCambioPropiedad("DocumentoDigital");
            }
        }

        private string _codGestorDocumental;

        [DataMember]
        public string COD_GESTOR_DOCUMENTAL
        {
            get { return _codGestorDocumental; }
            set { _codGestorDocumental = value; }
        }

        private string _numExpedienteSGD;

        [DataMember]
        public string NUM_EXPEDIENTE_SGD
        {
            get { return _numExpedienteSGD; }
            set { _numExpedienteSGD = value; }
        }

        private int? _idExpedienteSGD;
        [DataMember]
        public int? ID_EXPEDIENTE_SGD
        {
            get { return _idExpedienteSGD; }
            set { _idExpedienteSGD = value; }
        }


        private string _ARCHIVO_BASE64;

        [DataMember]
        public string ARCHIVO_BASE64
        {
            get { return _ARCHIVO_BASE64; }
            set { _ARCHIVO_BASE64 = value; }
        }



        #region DATOS DEL DECLARANTE

        private string _PrimerNombre;
        [DataMember]
        public string PrimerNombre
        {
            get { return _PrimerNombre; }
            set
            {
                string oldValue = _PrimerNombre;
                _PrimerNombre = (value != null) ? value.ToUpper() : null;
                if (_PrimerNombre != oldValue)
                {
                    ReportarCambioPropiedad("PrimerNombre");
                }
            }
        }

        private string _SegundoNombre;
        [DataMember]
        public string SegundoNombre
        {
            get { return _SegundoNombre; }
            set
            {
                _SegundoNombre = value;
                ReportarCambioPropiedad("SegundoNombre");
                ReportarCambioPropiedad("NombreCompleto");
            }
        }

        private string _PrimerApellido;
        [DataMember]
        public string PrimerApellido
        {
            get { return _PrimerApellido; }
            set
            {
                string oldValue = _PrimerApellido;
                _PrimerApellido = (value != null) ? value.ToUpper() : null;
                if (_PrimerApellido != oldValue)
                {
                    ReportarCambioPropiedad("PrimerApellido");
                }
            }
        }

        private string _SegundoApellido;
        [DataMember]
        public string SegundoApellido
        {
            get { return _SegundoApellido; }
            set
            {
                string oldValue = _SegundoApellido;
                _SegundoApellido = (value != null) ? value.ToUpper() : null;
                if (_SegundoApellido != oldValue)
                {
                    ReportarCambioPropiedad("SegundoApellido");
                }
            }
        }

        private int? _TipoDocumento;
        [DataMember]
        public int? TipoDocumento
        {
            get { return _TipoDocumento; }
            set
            {
                _TipoDocumento = value;
                if (value == null)
                    NumeroDocumento = null;
                else
                    if (Enum.GetValues(typeof(eTipoDocumentoSinNumero)).Cast<int>().Contains((int)value)) NumeroDocumento = null;
                ReportarCambioPropiedad("TipoDocumento");
                ReportarCambioPropiedad("NumeroDocumento");
            }
        }

        private string _NumeroDocumento;
        [DataMember]
        public string NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set
            {
                _NumeroDocumento = value;
                ReportarCambioPropiedad("NumeroDocumento");
                ReportarCambioPropiedad("TipoDocumento");
            }
        }
        #endregion

        #endregion

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

