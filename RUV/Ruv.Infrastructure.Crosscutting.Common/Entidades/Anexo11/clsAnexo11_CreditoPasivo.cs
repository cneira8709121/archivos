using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  [DataContract]
  public partial class clsAnexo11_CreditoPasivo : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
  {
    public clsAnexo11_CreditoPasivo()
    {
      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    private int? _PersonaAfectadaId;
    /// <summary>
    /// PARA ESTE ANEXO ESTA PROPIEDAD NO TIENE USO.
    /// SE AGREGA PARA CUMPLIR CON LA INTERFÁS IVICTIMA.
    /// </summary>
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

    private int? _TipoAcreedor;
    [DataMember]
    public int? TipoAcreedor
    {
      get { return _TipoAcreedor; }
      set
      {
        _TipoAcreedor = value;
        ReportarCambioPropiedad("TipoAcreedor");
      }
    }

    private string _NombreAcreedor;
    [DataMember]
    public string NombreAcreedor
    {
      get { return _NombreAcreedor; }
      set
      {
        _NombreAcreedor = value;
        ReportarCambioPropiedad("NombreAcreedor");
      }
    }

    private DateTime? _FechaContrajoObligacion;
    [DataMember]
    public DateTime? FechaContrajoObligacion
    {
      get { return _FechaContrajoObligacion; }
      set
      {
        //System.Diagnostics.Debug.Write("\n\n>>>Fecha obligacion: ");
        //System.Diagnostics.Debug.WriteLine(value);
        //System.Diagnostics.Debug.Write("\n");

        _FechaContrajoObligacion = value;
        ReportarCambioPropiedad("FechaContrajoObligacion");
      }
    }

    private double? _MontoAdeudado;
    [DataMember]
    public double? MontoAdeudado
    {
      get { return _MontoAdeudado; }
      set
      {
        _MontoAdeudado = value;
        ReportarCambioPropiedad("MontoAdeudado");
      }
    }

  }
}
