using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public partial class clsVerificacionProcedimiento : clsEntidadBase, IDataErrorInfo, IValidationEntity
    {

        public clsVerificacionProcedimiento()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;
        }
        public string Scope { get { return "HOJA 4"; } }
        #region PREGUNTA 25

        private int? _EnmendarDeclaracion;
        [DataMember]
        public int? EnmendarDeclaracion
        {
            get { return _EnmendarDeclaracion; }
            set
            {
                _EnmendarDeclaracion = value;
                if (value != 1) EnmendarDeclaracionTexto = null;
                if (value == 0) SeIncluyeronCorrecciones = 0;
                ReportarCambioPropiedad("EnmendarDeclaracion");
                ReportarCambioPropiedad("EnmendarDeclaracionTexto");
            }
        }

        private string _EnmendarDeclaracionTexto;
        [DataMember]
        public string EnmendarDeclaracionTexto
        {
            get { return _EnmendarDeclaracionTexto; }
            set
            {
                _EnmendarDeclaracionTexto = value;
                ReportarCambioPropiedad("EnmendarDeclaracion");
                ReportarCambioPropiedad("EnmendarDeclaracionTexto");
            }
        }

        #endregion

        #region PREGUNTA 26

        private int _NumeroTotalAnexos;
        /// <summary>
        /// Este campo se calcula, no debe validarse.
        /// </summary>
        [DataMember]
        public int NumeroTotalAnexos
        {
            get { return _NumeroTotalAnexos; }
            set
            {
                _NumeroTotalAnexos = value;
                ReportarCambioPropiedad("NumeroTotalAnexos");
                ReportarCambioPropiedad("NumeroTotalFolios");
            }
        }

        private string _DescripcionFolios;
        /// <summary>
        /// Este campo se digita no debe validarse.
        /// </summary>
        [DataMember]
        public string DescripcionFolios
        {
            get { return _DescripcionFolios; }
            set
            {
                _DescripcionFolios = value;
                ReportarCambioPropiedad("DescripcionFolios");
            }
        }

        private int _NumeroTotalFolios;
        /// <summary>
        /// Este campo se calcula, no debe validarse.
        /// </summary>
        [DataMember]
        public int NumeroTotalFolios
        {
            get {
                _NumeroTotalFolios = ((clsDeclaracion.DeclaracionActual != null) ? clsDeclaracion.DeclaracionActual.NumeroDeAnexos : 0) + _NumeroTotalSoportes + _NumeroTotalSoportesOtros + 4;
                return _NumeroTotalFolios; 
            }
            set
            {
                _NumeroTotalFolios = value;
                ReportarCambioPropiedad("NumeroTotalFolios");
            }
        }


        private int _NumeroTotalSoportes;
        [DataMember]
        public int NumeroTotalSoportes
        {
            get { return _NumeroTotalSoportes; }
            set
            {
                _NumeroTotalSoportes = value;
                ReportarCambioPropiedad("NumeroTotalSoportes");
                ReportarCambioPropiedad("NumeroTotalFolios");
            }
        }

        private int _NumeroTotalSoportesOtros;
        [DataMember]
        public int NumeroTotalSoportesOtros
        {
            get { return _NumeroTotalSoportesOtros; }
            set
            {
                _NumeroTotalSoportesOtros = value;
                ReportarCambioPropiedad("NumeroTotalSoportesOtros");
                ReportarCambioPropiedad("NumeroTotalFolios");
            }
        }
        private string _NumeroTotalSoportesOtrosDesc;
        [DataMember]
        public string NumeroTotalSoportesOtrosDesc
        {
            get { return _NumeroTotalSoportesOtrosDesc; }
            set
            {
                _NumeroTotalSoportesOtrosDesc = value;
                ReportarCambioPropiedad("NumeroTotalSoportesOtrosDesc");
            }
        }


        #endregion

        #region PREGUNTA 27 a 33

        private int? _RealizoEntrevistaPrevia;
        [DataMember]
        public int? RealizoEntrevistaPrevia
        {
            get { return _RealizoEntrevistaPrevia; }
            set
            {
                _RealizoEntrevistaPrevia = value;
                ReportarCambioPropiedad("RealizoEntrevistaPrevia");
            }
        }

        private int? _LeyoAlDeclaranteLaDeclaracion;
        [DataMember]
        public int? LeyoAlDeclaranteLaDeclaracion
        {
            get { return _LeyoAlDeclaranteLaDeclaracion; }
            set
            {
                _LeyoAlDeclaranteLaDeclaracion = value;
                ReportarCambioPropiedad("LeyoAlDeclaranteLaDeclaracion");
            }
        }

        private int? _SeIncluyeronCorrecciones;
        [DataMember]
        public int? SeIncluyeronCorrecciones
        {
            get { return _SeIncluyeronCorrecciones; }
            set
            {
                _SeIncluyeronCorrecciones = value;
                ReportarCambioPropiedad("SeIncluyeronCorrecciones");
            }
        }

        private int? _RealizoTomaJuramento;
        [DataMember]
        public int? RealizoTomaJuramento
        {
            get { return _RealizoTomaJuramento; }
            set
            {
                _RealizoTomaJuramento = value;
                ReportarCambioPropiedad("RealizoTomaJuramento");
            }
        }

        private int? _HuboOrientacionParaCorregir;
        [DataMember]
        public int? HuboOrientacionParaCorregir
        {
            get { return _HuboOrientacionParaCorregir; }
            set
            {
                _HuboOrientacionParaCorregir = value;
                ReportarCambioPropiedad("HuboOrientacionParaCorregir");
            }
        }

        private string _ObservacionesSobreDiligenciamiento;
        [DataMember]
        public string ObservacionesSobreDiligenciamiento
        {
            get { return _ObservacionesSobreDiligenciamiento; }
            set
            {
                _ObservacionesSobreDiligenciamiento = value;
                ReportarCambioPropiedad("ObservacionesSobreDiligenciamiento");
            }
        }

        #endregion

        #region PREGUNTA 33

        private int? _UsoDatosPersonales;
        [DataMember]
        public int? UsoDatosPersonales
        {
            get { return _UsoDatosPersonales; }
            set { _UsoDatosPersonales = value;
                ReportarCambioPropiedad("UsoDatosPersonales");
            }
        }


        private int? _DeclaranteSabeFirmar;
        [DataMember]
        public int? DeclaranteSabeFirmar
        {
            get { return _DeclaranteSabeFirmar; }
            set
            {
                _DeclaranteSabeFirmar = value;
                ReportarCambioPropiedad("DeclaranteSabeFirmar");
            }
        }

        private bool _DebeCargarDeclaracionEscaneada;
        [DataMember]
        public bool DebeCargarDeclaracionEscaneada
        {
            get { return _DebeCargarDeclaracionEscaneada; }
            set
            {
                _DebeCargarDeclaracionEscaneada = value;
                ReportarCambioPropiedad("DebeCargarDeclaracionEscaneada");
            }
        }

        private bool _LinkDocumentos;
        [DataMember]
        public bool LinkDocumentos
        {
            get { return _LinkDocumentos; }
            set
            {
                _LinkDocumentos = value;
                ReportarCambioPropiedad("LinkDocumentos");
            }
        }

        private int _NumeroAnexos;
        [DataMember]
        public int NumeroAnexos
        {
            get { return _NumeroAnexos; }
            set
            {
                _NumeroAnexos = value;
                ReportarCambioPropiedad("NumeroAnexos");
            }
        }


        #endregion

        #region PREGUNTA 34

        // Sólo muestra datos que ya existen en la entidad "clsTomaDeclaracion".

        #endregion

        #region PREGUNTA 35

        private string _FuncionarioNombre;
        [DataMember]
        public string FuncionarioNombre
        {
            get { return _FuncionarioNombre; }
            set
            {
                _FuncionarioNombre = value;
                ReportarCambioPropiedad("FuncionarioNombre");
            }
        }

        private string _FuncionarioCargo;
        [DataMember]
        public string FuncionarioCargo
        {
            get { return _FuncionarioCargo; }
            set
            {
                _FuncionarioCargo = value;
                ReportarCambioPropiedad("FuncionarioCargo");
            }
        }

        private string _FuncionarioDocumentoIdentidad;
        [DataMember]
        public string FuncionarioDocumentoIdentidad
        {
            get { return _FuncionarioDocumentoIdentidad; }
            set
            {
                _FuncionarioDocumentoIdentidad = value;
                ReportarCambioPropiedad("FuncionarioDocumentoIdentidad");
            }
        }

        #endregion

        #region PREGUNTA 36

        // Muestra datos que ya existen en la entidad "clsTomaDeclaracion".

        #endregion

    }
}
