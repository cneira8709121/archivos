using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public partial class clsAnexo13 : clsEntidadBase, IDataErrorInfo, IAnexo, IValidationEntity
    {
        public string Scope { get { return "Anexo 13"; } }
        #region CONSTRUCTOR

        private clsDeclaracion _Declaracion;
        /// <summary>
        /// Referencia a la declaración padre.
        /// No requiere almacenamiento.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public clsDeclaracion Declaracion
        {
            get { return _Declaracion; }
            set { _Declaracion = value; }
        }

        public clsAnexo13()
        {
            ConstructorGeneral();
        }

        private void ConstructorGeneral()
        {
            // Inicializar la lista de personas.
            //_ListaPersonasOC = new ObservableCollection<clsPersonaAfectada>();
            //_ListaPersonasOC.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_ListaPersonasOC_CollectionChanged);
            //_ListaPersonasICV = CollectionViewSource.GetDefaultView(_ListaPersonasOC);
            //_ListaPersonasICV.SortDescriptions.Add(
            //  new SortDescription("NombreCompleto", ListSortDirection.Ascending));
            //_ListaPersonasICV.Filter = new Predicate<object>(FiltroOmitirEliminados);

            //ReportarCambioPropiedad("ListaPersonasOrdenada");
            //ReportarCambioPropiedad("ListaPersonas");


            _ListaPersonasOC = new ObservableCollection<clsAnexo13_Victima>();
            _ListaPersonasOC.CollectionChanged
              += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_ListaPersonasOC_CollectionChanged);

            _EstadoRegistro = eEstadoRegistro.Insertar;
        }


        #endregion

        #region LA LISTA DE LAS PERSONAS AFECTADAS

        void _ListaPersonasOC_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ReportarCambioPropiedad("ListaPersonasOrdenada");
            ReportarCambioPropiedad("ListaPersonas");
        }

        /// <summary>
        /// La vista que permite ordenar la lista de personas.
        /// </summary>
        ICollectionView _ListaPersonasICV;

        /// <summary>
        /// El contenedor de la lista de personas.
        /// </summary>        
        ObservableCollection<clsAnexo13_Victima> _ListaPersonasOC;

        /// <summary>
        /// Lista de personas afectadas, en orden alfabético.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public ICollectionView ListaPersonasOrdenada
        {
            get
            {
                if (_ListaPersonasICV == null)
                {
                    // Inicializar la lista de personas.
                    _ListaPersonasICV = CollectionViewSource.GetDefaultView(_ListaPersonasOC);
                    _ListaPersonasICV.SortDescriptions.Add(
                      new SortDescription("NombreCompleto", ListSortDirection.Ascending));
                    _ListaPersonasICV.Filter = new Predicate<object>(FiltroOmitirEliminados);



                    ReportarCambioPropiedad("ListaPersonasOrdenada");
                    ReportarCambioPropiedad("ListaPersonas");
                }
                return _ListaPersonasICV;
            }
            set { }
        }

        /// <summary>
        /// La lista de personas, modificable.
        /// </summary>
        [DataMember]
        public ObservableCollection<clsAnexo13_Victima> ListaPersonas
        {
            get
            {
                return _ListaPersonasOC;
            }
            set
            {
                _ListaPersonasOC = value;
                ReportarCambioPropiedad("ListaPersonasOrdenada");
                ReportarCambioPropiedad("ListaPersonas");
            }
        }

        #endregion


        #region Pregunta 15

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

        private Int64? _DatoContactoPais = 48L;       //Sipod.I.Usuario.ID_PAIS 
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

        #endregion

        #region PREGUNTA 16

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

        private Int64? _DatoAlternoContactoPais;
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

        #region IAnexo

        private int? _JefeGrupoFamiliarId;

        /// <summary>
        /// Código del jefe del grupo familiar.
        /// </summary>
        [DataMember]
        public int? JefeGrupoFamiliarId
        {
            get { return _JefeGrupoFamiliarId; }
            set
            {
                _JefeGrupoFamiliarId = value;
                ReportarCambioPropiedad("JefeGrupoFamiliarId");
            }
        }

        /// <summary>
        /// Se implementa, aunque no se usa en este anexo
        /// Con el fin de cumplir con la interfaz
        /// </summary>
        private clsAnexo_FechaYLugar _FechaYLugar;
        [DataMember]
        public clsAnexo_FechaYLugar FechaYLugar
        {
            get { return _FechaYLugar; }
            set
            {
                _FechaYLugar = value;
                ReportarCambioPropiedad("FechaYLugar");
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public string Nombre
        {
            get { return "13. Censo Evento Masivo"; }
        }

        [System.Xml.Serialization.XmlIgnore]
        public int Numero
        {
            get { return 13; }
        }

        private int? _HechosFecha;
        [System.Xml.Serialization.XmlIgnore]
        public DateTime HechosFecha
        {
            get { return FechaYLugar.HechosFecha.Value; }
        }

        //ID del anexo al cual pertenece el censo masivo (anexo13)
        private int? _idAnexoRelacionado;

        public int? idAnexoRelacionado
        {
            get { return _idAnexoRelacionado; }
            set { _idAnexoRelacionado = value; }
        }


        private List<Guid> _anexosRelacionados;
        [DataMember]
        public List<Guid> AnexosRelacionados
        {
            get { return _anexosRelacionados; }
            set { _anexosRelacionados = value; }
        }


        #endregion


    }
}
