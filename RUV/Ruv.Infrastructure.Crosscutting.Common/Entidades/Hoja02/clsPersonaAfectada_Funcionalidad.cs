using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsPersonaAfectada : clsEntidadBase, IDataErrorInfo
  {
    /// <summary>
    /// Alimenta las colecciones de esta entidad con la información de otra.
    /// </summary>
    /// <param name="origen"></param>
    public void CopiarColeccionesDesde(clsPersonaAfectada origen)
    {
      HechosVictimizantes =
        clsUtils.CopiarListOf<int>(origen.HechosVictimizantes);
      Discapacidades =
        clsUtils.CopiarListOf<int>(origen.Discapacidades);
    }

    /// <summary>
    /// Reporta hacia TomaDeclaración algún cambio en los datos del declarante.
    /// </summary>
    /// <param name="nombrePropiedad"></param>
    void ReportarHaciaElDeclarante(string nombrePropiedad)
    {
      if (ID != null
        && PersonasAfectadas != null
        && PersonasAfectadas.Declaracion != null
        && PersonasAfectadas.Declaracion.TomaDeclaracion != null
        && PersonasAfectadas.Declaracion.TomaDeclaracion.DeclaranteId == ID)
        PersonasAfectadas.Declaracion
          .TomaDeclaracion.ReportarCambioPropiedad(nombrePropiedad);
    }
  }
}
