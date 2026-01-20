using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  /// <summary>
  /// Unidad básica equivalente a una unidad territorial
  /// </summary>
  [DataContract]
  public class clsParametroUT
  {
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Nombre { get; set; }
  }

}
