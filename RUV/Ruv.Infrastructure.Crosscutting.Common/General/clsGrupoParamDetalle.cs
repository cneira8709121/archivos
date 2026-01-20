using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  [DataContract]
  public class clsGrupoParamDetalle
  {
    /// <summary>
    /// El nombre del conjunto.
    /// </summary>
    [DataMember]
    public eGruposParametros Conjunto { get; set; }
    [DataMember]
    public int ParametroId { get; set; }
    [DataMember]
    public int Orden { get; set; }
  }
}
