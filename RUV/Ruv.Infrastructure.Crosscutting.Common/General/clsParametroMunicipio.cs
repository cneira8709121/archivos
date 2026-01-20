using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  /// <summary>
  /// Unidad básica equivalente a nu depto o mcpio.
  /// </summary>
  [DataContract]
  public class clsParametroMunicipio
  {
    [DataMember]
    public int? Id { get; set; }

    /// <summary>
    /// El tiop del parámetro.
    /// </summary>
    [DataMember]
    public string Nombre { get; set; }

    /// <summary>
    /// El departamento
    /// </summary>
    [DataMember]
    public int DepartamentoId { get; set; }

    /// <summary>
    /// Codigo Telefonico
    /// </summary>
    [DataMember]
    public Int32? CodigoTelefono { get; set; }

    /// <summary>
    /// Verificacion si el municipio tiene representación
    /// </summary>
    [DataMember]
    public bool? TieneRepresentacion { get; set; }
  }

}
