using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  [DataContract]
  public partial class clsAnexo01_Victima_Bien : clsEntidadBase, IDataErrorInfo, IEditableObject, IValidationEntity
  {
    public clsAnexo01_Victima_Bien()
    {
      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    public string Scope { get { return "Anexo 01"; } }

    private int? _TipoBien;
    /// <summary>
    /// Mueble / Inmueble.
    /// </summary>
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

    private int? _CalidadDeLaVictima;
    [DataMember]
    public int? CalidadDeLaVictima
    {
      get { return _CalidadDeLaVictima; }
      set
      {
        _CalidadDeLaVictima = value;
        ReportarCambioPropiedad("CalidadDeLaVictima");
      }
    }

  }
}
