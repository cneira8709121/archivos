using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo11_BienMueble : clsEntidadBase, IDataErrorInfo, IVictima
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo11_BienMueble R = Resultado as clsAnexo11_BienMueble;
      R.ID = this.ID;
      R.Cantidad = this.Cantidad;
      R.Descripcion = this.Descripcion;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.TipoBien = this.TipoBien;
      R.TipoTenencia = this.TipoTenencia;
      R.EstadoRegistro = this.EstadoRegistro;

      return Resultado;
    }

  }
}
