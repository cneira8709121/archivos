using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Almacena todos los datos que puede contener una declaración.
    /// </summary>
    [DataContract]
    public partial class clsDeclaracion : clsEntidadBase
    {
        #region CONSTRUCTOR

        public clsDeclaracion()
        {
            TomaDeclaracion = new clsTomaDeclaracion(this);
            DescripcionHechos = new clsDescripcionHechos();
            VerificacionProcedimiento = new clsVerificacionProcedimiento();
            PersonasAfectadas = new clsPersonasAfectadas();
            //PersonasAfectadas = new clsPersonasAfectadas(this);
            
            A01 = new List<clsAnexo01>();
            A02 = new List<clsAnexo02>();
            A03 = new List<clsAnexo03>();
            A04 = new List<clsAnexo04>();
            A05 = new List<clsAnexo05>();
            A06 = new List<clsAnexo06>();
            A07 = new List<clsAnexo07>();
            A08 = new List<clsAnexo08>();
            A09 = new List<clsAnexo09>();
            A10 = new List<clsAnexo10>();
            A11 = new List<clsAnexo11>();
            A13 = new List<clsAnexo13>();

        }

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// Especifica si la declaración es autogenerada por una radicación, para bloquear los datos de nombre y documento.
        /// </summary>
        [DataMember]
        public bool AutoGeneradoPorRadicacion
        {
            get;
            set;
        }

        private int _VersionFUD = 2;
        /// <summary>
        /// Controla a versión con la que se trabaja el FUD
        /// </summary>
        [DataMember]
        public int VersionFUD
        {
            get { return _VersionFUD; }
            set { _VersionFUD = value;
                ReportarCambioPropiedad("VersionFUD");
            }
        }


        public List<Versiones> _Versiones;
        public List<Versiones> Versiones
        {
            get { return _Versiones; }
            set { _Versiones = value; }
        }


        private string _DeclaracionNumero;
        /// <summary>
        /// El número de esta declaración
        /// </summary>
        [DataMember]
        public string DeclaracionNumero
        {
            get { return _DeclaracionNumero; }
            set
            {
                _DeclaracionNumero = value;
                ReportarCambioPropiedad("DeclaracionNumero");
            }
        }


        private eEstadoDeclaracion _EstadoDeclaracion = eEstadoDeclaracion.Ninguno;
        /// <summary>
        /// El estado de la declaración.
        /// </summary>
        [DataMember]
        public eEstadoDeclaracion EstadoDeclaracion
        {
            get { return _EstadoDeclaracion; }
            set
            {
                _EstadoDeclaracion = value;
                ReportarCambioPropiedad("EstadoDeclaracion");
            }
        }

        /// <summary>
        /// Flag que determina que estado tomara la declaracion al ser guardada en la base de datos
        /// </summary>
        [DataMember]
        public bool PendienteGlosas { get; set; }

        [DataMember]
        public int IdValoracion { get; set; }

        #endregion

        #region PARTES DE LA DECLARACIÓN

        private clsTomaDeclaracion _TomaDeclaracion;
        /// <summary>
        /// Toma de la declaración, hoja 1 de 4.
        /// </summary>
        [DataMember]
        public clsTomaDeclaracion TomaDeclaracion
        {
            get { return _TomaDeclaracion; }
            set { _TomaDeclaracion = value; }
        }

        private clsDescripcionHechos _DescripcionHechos;
        /// <summary>
        /// La descripción de los hechos, hoja 3 de 4.
        /// </summary>
        [DataMember]
        public clsDescripcionHechos DescripcionHechos
        {
            get { return _DescripcionHechos; }
            set { _DescripcionHechos = value; }
        }

        private clsVerificacionProcedimiento _VerificacionProcedimiento;
        /// <summary>
        /// La verificación de los procedimientos, hoja 4 de 4.
        /// </summary>
        [DataMember]
        public clsVerificacionProcedimiento VerificacionProcedimiento
        {
            get { return _VerificacionProcedimiento; }
            set { _VerificacionProcedimiento = value; }
        }

        private clsPersonasAfectadas _PersonasAfectadas;
        /// <summary>
        /// La lista de las personas afectadas, hoja 2 de 4.
        /// </summary>
        [DataMember]
        public clsPersonasAfectadas PersonasAfectadas
        {
            get { return _PersonasAfectadas; }
            set { _PersonasAfectadas = value; }
        }

        //private List<IAnexo> _Anexos;
        ///// <summary>
        ///// La lista de los anexos.
        ///// </summary>
        //[DataMember]
        //[System.Xml.Serialization.XmlIgnore()]
        //public List<IAnexo> Anexos
        //{
        //  get { return _Anexos; }
        //  set { _Anexos = value; }
        //}

        private int? _RadicacionId;
        /// <summary>
        /// El código de la radicación.
        /// </summary>
        [DataMember]
        public int? RadicacionId
        {
            get { return _RadicacionId; }
            set
            {
                _RadicacionId = value;
                ReportarCambioPropiedad("RadicacionId");
            }
        }

        private clsNotificacionElectronica notificacionElectronica;
        [DataMember]
        public clsNotificacionElectronica NotificacionElectronica
        {
            get { return notificacionElectronica; }
            set { notificacionElectronica = value; }
        }


        #endregion

        #region LA LISTA DE LOS ANEXOS.

        private List<clsAnexo01> _A01;
        [DataMember]
        public List<clsAnexo01> A01
        {
            get { return _A01; }
            set
            {
                _A01 = value;
                ReportarCambioPropiedad("A01");
            }
        }

        private List<clsAnexo02> _A02;
        [DataMember]
        public List<clsAnexo02> A02
        {
            get { return _A02; }
            set
            {
                _A02 = value;
                ReportarCambioPropiedad("A02");
            }
        }

        private List<clsAnexo03> _A03;
        [DataMember]
        public List<clsAnexo03> A03
        {
            get { return _A03; }
            set
            {
                _A03 = value;
                ReportarCambioPropiedad("A03");
            }
        }

        private List<clsAnexo04> _A04;
        [DataMember]
        public List<clsAnexo04> A04
        {
            get { return _A04; }
            set
            {
                _A04 = value;
                ReportarCambioPropiedad("A04");
            }
        }

        private List<clsAnexo05> _A05;
        [DataMember]
        public List<clsAnexo05> A05
        {
            get { return _A05; }
            set
            {
                _A05 = value;
                ReportarCambioPropiedad("A05");
            }
        }

        private List<clsAnexo06> _A06;
        [DataMember]
        public List<clsAnexo06> A06
        {
            get { return _A06; }
            set
            {
                _A06 = value;
                ReportarCambioPropiedad("A06");
            }
        }

        private List<clsAnexo07> _A07;
        [DataMember]
        public List<clsAnexo07> A07
        {
            get { return _A07; }
            set
            {
                _A07 = value;
                ReportarCambioPropiedad("A07");
            }
        }

        private List<clsAnexo08> _A08;
        [DataMember]
        public List<clsAnexo08> A08
        {
            get { return _A08; }
            set
            {
                _A08 = value;
                ReportarCambioPropiedad("A08");
            }
        }

        private List<clsAnexo09> _A09;
        [DataMember]
        public List<clsAnexo09> A09
        {
            get { return _A09; }
            set
            {
                _A09 = value;
                ReportarCambioPropiedad("A09");
            }
        }

        private List<clsAnexo10> _A10;
        [DataMember]
        public List<clsAnexo10> A10
        {
            get { return _A10; }
            set
            {
                _A10 = value;
                ReportarCambioPropiedad("A10");
            }
        }

        private List<clsAnexo11> _A11;
        [DataMember]
        public List<clsAnexo11> A11
        {
            get { return _A11; }
            set
            {
                _A11 = value;
                ReportarCambioPropiedad("A11");
            }
        }

        private List<clsAnexo13> _A13;
        [DataMember]
        public List<clsAnexo13> A13
        {
            get { return _A13; }
            set
            {
                _A13 = value;
                ReportarCambioPropiedad("A13");
            }
        }


        #endregion

        #region EL DOCUMENTO ESCANEADO

        private string _DocumentoDigitalNombre;
        /// <summary>
        /// El nombre del archivo local que contiene el documento escaneado.
        /// </summary>
        [DataMember]
        public string DocumentoDigitalNombre
        {
            get { return _DocumentoDigitalNombre; }
            set
            {
                _DocumentoDigitalNombre = value;
                ReportarCambioPropiedad("DocumentoDigitalNombre");
            }
        }

        private string _DocumentosSoportesNombre;

        public string DocumentosSoporteNombre
        {
            get { return _DocumentosSoportesNombre; }
            set { _DocumentosSoportesNombre = value;
                ReportarCambioPropiedad("DocumentosSoporteNombre");
            }
        }



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

        private byte[] _DocumentoAnexo;
        /// <summary>
        /// El contenido del documento escaneado.
        /// </summary>
        [DataMember]
        public byte[] DocumentoAnexo
        {
            get { return _DocumentoAnexo; }
            set
            {
                _DocumentoAnexo = value;
                ReportarCambioPropiedad("DocumentoAnexo");
            }
        }

        private List<clsFirma> _Firmas;

        [DataMember]
        public List<clsFirma> Firmas
        {
            get { return _Firmas; }
            set
            {
                _Firmas = value;
                ReportarCambioPropiedad("Firmas");
            }
        }

        #endregion

        #region GLOSAS
        private ObservableCollection<clsGlosa> _Glosas;
        [DataMember]
        public ObservableCollection<clsGlosa> Glosas
        {
            get { return _Glosas; }
            set
            {
                _Glosas = value;
                ReportarCambioPropiedad("Glosas");
            }
        }
        private ObservableCollection<clsGlosaIntencion> _IGlosas;
        [DataMember]
        public ObservableCollection<clsGlosaIntencion> IGlosas
        {
            get { return _IGlosas; }
            set
            {
                _IGlosas = value;
                ReportarCambioPropiedad("IGlosas");
            }
        }

        #endregion


    }
}
