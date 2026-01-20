using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Datos genéricos para algunos anexos.
    /// </summary>  
    [DataContract]
    [System.Diagnostics.DebuggerDisplay("{TipoPoblacionId} - EntornoId:{EntornoId} - EntornoOtro:{EntornoOtro}")]
    public partial class clsAnexo_FechaYLugar : clsEntidadBase, IDataErrorInfo, IValidationEntity
    {

        public clsAnexo_FechaYLugar()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        #region Datos del Contenedor
        [System.Xml.Serialization.XmlIgnore]
        public clsEntidadBase Contenedor { set; get; }

        private string _Titulo;

        [System.Xml.Serialization.XmlIgnore]
        public string Titulo
        {
            get { return _Titulo; }
            set
            {
                _Titulo = value;
                //ReportarCambioPropiedad("Titulo");
            }
        }

        private bool _SkipValidation = false;
        [System.Xml.Serialization.XmlIgnore]
        public bool SkipValidation
        {
            get { return _SkipValidation; }
            set {
                _SkipValidation = value;
            }
        }
        #endregion

        private DateTime? _HechosFecha;
        [DataMember]
        public DateTime? HechosFecha
        {
            get { return _HechosFecha; }
            set
            {
                _HechosFecha = value;
                ReportarCambioPropiedad("HechosFecha");
                if (this.Contenedor is clsAnexo05)
                    ((clsAnexo05)this.Contenedor).RaiseReportarCambioFechas(this);
            }
        }

        private Int64? _HechosPais = 48L;
        [DataMember]
        public Int64? HechosPais
        {
            get { return _HechosPais; }
            set 
            { 
                _HechosPais = value;
                ReportarCambioPropiedad("HechosPais");
            }
        }
        

        private Int64? _HechosDepartamento;
        [DataMember]
        public Int64? HechosDepartamento
        {
            get { return _HechosDepartamento; }
            set
            {
                _HechosDepartamento = value;
                ReportarCambioPropiedad("HechosDepartamento");
            }
        }

        private Int64? _HechosMunicipio;
        [DataMember]
        public Int64? HechosMunicipio
        {
            get { return _HechosMunicipio; }
            set
            {
                _HechosMunicipio = value;
                ReportarCambioPropiedad("HechosMunicipio");
            }
        }


        //==============================================

        // NO USAR ESTAS PROPIEDADES
        //private int? _EntornoId;
        //[DataMember]
        //public int? EntornoId
        //{
        //  get { return _EntornoId; }
        //  set
        //  {
        //    _EntornoId = value;
        //    ReportarCambioPropiedad("EntornoId");
        //  }
        //}

        //// NO USAR ESTAS PROPIEDADES
        //private eTipoPoblacion? _TipoPoblacionId;
        //[DataMember]
        //public eTipoPoblacion? TipoPoblacionId
        //{
        //  get { return _TipoPoblacionId; }
        //  set
        //  {
        //    _TipoPoblacionId = value;
        //    ReportarCambioPropiedad("TipoPoblacionId");
        //  }
        //}

        //// NO USAR ESTAS PROPIEDADES
        //private string _EntornoOtro;
        //[DataMember]
        //public string EntornoOtro
        //{
        //  get { return _EntornoOtro; }
        //  set
        //  {
        //    _EntornoOtro = value;
        //    ReportarCambioPropiedad("EntornoOtro");
        //  }
        //}

        //==============================================

        private eTipoEntorno? _TipoEntorno;
        [DataMember]
        public eTipoEntorno? TipoEntorno
        {
            get { return _TipoEntorno; }
            set
            {
                _TipoEntorno = value;
                ReportarCambioPropiedad("TipoEntorno");
            }
        }

        private int? _BarrioVeredaId;
        [DataMember]
        public int? BarrioVeredaId
        {
            get { return _BarrioVeredaId; }
            set
            {
                _BarrioVeredaId = value;
                ReportarCambioPropiedad("BarrioVeredaId");
            }
        }

        private string _BarrioVeredaNombre;
        [DataMember]
        public string BarrioVeredaNombre
        {
            get { return _BarrioVeredaNombre; }
            set
            {
                _BarrioVeredaNombre = value;
                ReportarCambioPropiedad("BarrioVeredaNombre");
            }
        }

        private int? _LocalidadCorregimientoId;
        [DataMember]
        public int? LocalidadCorregimientoId
        {
            get { return _LocalidadCorregimientoId; }
            set
            {
                _LocalidadCorregimientoId = value;
                ReportarCambioPropiedad("LocalidadCorregimientoId");
            }
        }

        private string _LocalidadCorregimientoNombre;
        [DataMember]
        public string LocalidadCorregimientoNombre
        {
            get { return _LocalidadCorregimientoNombre; }
            set
            {
                _LocalidadCorregimientoNombre = value;
                ReportarCambioPropiedad("LocalidadCorregimientoNombre");
            }
        }
        #region ExternNotifyPropertyChanged
        public void RaiseReportarCambioPropiedad(string nombrePropiedad)
        {
            ReportarCambioPropiedad(nombrePropiedad);
        }
        #endregion



        public string Scope
        {
            get { return "Fecha Lugar"; }
        }
    }
}
