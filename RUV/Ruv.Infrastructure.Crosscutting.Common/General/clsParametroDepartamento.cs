using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  /// <summary>
  /// Unidad básica equivalente a nu depto o mcpio.
  /// </summary>
  [DataContract]
  public class clsParametroDepartamento
  {
    [DataMember]
    public Int64? Id { get; set; }

    /// <summary>
    /// El tiop del parámetro.
    /// </summary>
    [DataMember]
    public string Nombre { get; set; }
    
    /// <summary>
    /// El Pais
    /// </summary>
    [DataMember]
    public Int64 PaisId { get; set; }

    /// <summary>
    /// Verificacion si el departamento tiene representación
    /// </summary>
    [DataMember]
    public bool? TieneRepresentacion { get; set; }
  }

}
