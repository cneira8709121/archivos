using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  [DataContract]
  public class clsComunidadEtnica
  {
    [DataMember]
    public int Id { get; set; }
    /// <summary>
    /// Grupo étnico
    /// </summary>
    [DataMember]
    public int GrupoEtnicoId { get; set; }
    /// <summary>
    /// Descripción de la comunidad
    /// </summary>
    [DataMember]
    public string Nombre { get; set; }
    [DataMember]
    public int Numero { get; set; }
  }
}
