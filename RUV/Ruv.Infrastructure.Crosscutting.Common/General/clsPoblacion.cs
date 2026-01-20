using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  /// <summary>
  /// Unidad básica equivalente a un tipo de población.
  /// </summary>
  [DataContract]
  public class clsPoblacion
  {
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string Nombre { get; set; }
    /// <summary>
    /// El tipo de población.
    /// </summary>
    [DataMember]
    public eTipoPoblacion TipoPoblacion { get; set; }
    /// <summary>
    /// El departamento
    /// </summary>
    [DataMember]
    public int MunicipioId { get; set; }
  }

}
