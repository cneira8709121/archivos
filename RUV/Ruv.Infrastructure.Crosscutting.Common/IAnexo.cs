using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common
{
  /// <summary>
  /// Todos los anexos implementan esta interfaz.
  /// </summary>
  public interface IAnexo
  {
    /// <summary>
    /// El título del anexo.
    /// </summary>
    string Nombre { get; }
    /// <summary>
    /// El número del anexo.
    /// </summary>
    int Numero { get; }
    /// <summary>
    /// El id del jefe del grupo familiar en el anexo.
    /// </summary>
    int? JefeGrupoFamiliarId { get; set; }

      /// <summary>
      /// Fecha en que ocurrieron los hechos relacionados con el anexo
      /// </summary>
    DateTime HechosFecha { get; }

    //ID del anexo al cual pertenece el censo masivo (anexo13)
    int? idAnexoRelacionado { get; set; }


    }
}
