using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo11_CreditoPasivo : clsEntidadBase, IDataErrorInfo, IVictima
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo11_CreditoPasivo R = Resultado as clsAnexo11_CreditoPasivo;
      R.ID = this.ID;
      R.MontoAdeudado = this.MontoAdeudado;
      R.NombreAcreedor = this.NombreAcreedor;
      R.TipoAcreedor = this.TipoAcreedor;
      R.FechaContrajoObligacion = this.FechaContrajoObligacion;
      R.EstadoRegistro = this.EstadoRegistro;
      
      return Resultado;
    }

  }
}
