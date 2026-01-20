using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo01_Victima_Bien : clsEntidadBase, IDataErrorInfo, IEditableObject
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo01_Victima_Bien R = Resultado as clsAnexo01_Victima_Bien;
      R.ID = this.ID;
      R.TipoBien = this.TipoBien;
      R.Descripcion = this.Descripcion;
      R.CalidadDeLaVictima = this.CalidadDeLaVictima;
      R.EstadoRegistro = this.EstadoRegistro;

      return Resultado;
    }

    #region IEditableObject

    [System.Xml.Serialization.XmlIgnore()]
    clsAnexo01_Victima_Bien CopiaTemporal = null;

    public void BeginEdit()
    {
      CopiaTemporal = ObtenerCopia<clsAnexo01_Victima_Bien>();
    }

    public void CancelEdit()
    {
      if (CopiaTemporal == null) return;

      ID = CopiaTemporal.ID;
      TipoBien = CopiaTemporal.TipoBien;
      Descripcion = CopiaTemporal.Descripcion;
      CalidadDeLaVictima = CopiaTemporal.CalidadDeLaVictima;
      EstadoRegistro = CopiaTemporal.EstadoRegistro;
      CopiaTemporal = null;
    }

    public void EndEdit()
    {
      CopiaTemporal = null;
    }

    #endregion

  }
}
