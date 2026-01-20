using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  [DataContract]
  public partial class clsAnexo11_BienInmueble : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
  {
    public clsAnexo11_BienInmueble()
    {
      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    private int? _PersonaAfectadaId;
    [DataMember]
    public int? PersonaAfectadaId
    {
      get { return _PersonaAfectadaId; }
      set
      {
        _PersonaAfectadaId = value;
        ReportarCambioPropiedad("PersonaAfectadaId");
      }
    }
    public string Scope { get { return "Anexo 11"; } }
    private int? _TipoInmueble;
    [DataMember]
    public int? TipoInmueble
    {
      get { return _TipoInmueble; }
      set
      {
        _TipoInmueble = value;
        ReportarCambioPropiedad("TipoInmueble");
      }
    }

    private Int64? _LocalizacionPais = 48L;
    [DataMember]
    public Int64? LocalizacionPais
    {
        get { return _LocalizacionPais; }
        set
        {
            _LocalizacionPais = value;
            ReportarCambioPropiedad("LocalizacionPais");
        }
    }

    private Int64? _LocalizacionDepartamento;
    [DataMember]
    public Int64? LocalizacionDepartamento
    {
      get { return _LocalizacionDepartamento; }
      set
      {
        _LocalizacionDepartamento = value;
        ReportarCambioPropiedad("LocalizacionDepartamento");
      }
    }

    private Int64? _LocalizacionMunicipio;
    [DataMember]
    public Int64? LocalizacionMunicipio
    {
      get { return _LocalizacionMunicipio; }
      set
      {
        _LocalizacionMunicipio = value;
        ReportarCambioPropiedad("LocalizacionMunicipio");
      }
    }

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

    // ----------------------------

    private int? _TipoTenencia;
    [DataMember]
    public int? TipoTenencia
    {
      get { return _TipoTenencia; }
      set
      {
        _TipoTenencia = value;
        ReportarCambioPropiedad("TipoTenencia");
      }
    }

    private string _NombreDireccion;
    [DataMember]
    public string NombreDireccion
    {
      get { return _NombreDireccion; }
      set
      {
        _NombreDireccion = value;
        ReportarCambioPropiedad("NombreDireccion");
      }
    }

    private double? _ExtensionArea;
    [DataMember]
    public double? ExtensionArea
    {
      get { return _ExtensionArea; }
      set
      {
        _ExtensionArea = value;
        ReportarCambioPropiedad("ExtensionArea");
      }
    }

    private int? _ExtensionUnidadDeArea;
    [DataMember]
    public int? ExtensionUnidadDeArea
    {
      get { return _ExtensionUnidadDeArea; }
      set
      {
        _ExtensionUnidadDeArea = value;
        ReportarCambioPropiedad("ExtensionUnidadDeArea");
      }
    }

  }
}
