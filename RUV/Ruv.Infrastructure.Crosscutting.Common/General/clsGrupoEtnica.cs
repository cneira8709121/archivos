using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  [DataContract]
  public class clsGrupoEtnica
  {
    [DataMember]
    public int Id { get; set; }
    /// <summary>
    /// Etnia
    /// </summary>
    [DataMember]
    public int EtniaId { get; set; }
    /// <summary>
    /// Descripción de la comunidad
    /// </summary>
    [DataMember]
    public string Nombre { get; set; }

  }
}
