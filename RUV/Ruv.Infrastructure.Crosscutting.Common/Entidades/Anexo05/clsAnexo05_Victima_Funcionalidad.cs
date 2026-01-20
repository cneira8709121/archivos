using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo05_Victima : clsEntidadBase, IDataErrorInfo, IVictima
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo05_Victima R = Resultado as clsAnexo05_Victima;
      R.ID = this.ID;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.SeDesplazo = this.SeDesplazo;
      R.EstadoRegistro = this.EstadoRegistro;

      return Resultado;
    }
  }
}
