using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  [DataContract]
  public partial class clsAnexo11_BienMueble : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
  {

    public clsAnexo11_BienMueble()
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
    private int? _TipoBien;
    [DataMember]
    public int? TipoBien
    {
      get { return _TipoBien; }
      set
      {
        _TipoBien = value;
        ReportarCambioPropiedad("TipoBien");
      }
    }

    private string _Descripcion;
    [DataMember]
    public string Descripcion
    {
      get { return _Descripcion; }
      set
      {
        _Descripcion = value;
        ReportarCambioPropiedad("Descripcion");
      }
    }

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

    private double? _Cantidad;
    [DataMember]
    public double? Cantidad
    {
      get { return _Cantidad; }
      set
      {
        _Cantidad = value;
        ReportarCambioPropiedad("Cantidad");
      }
    }
  }
}
