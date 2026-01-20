using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  public partial class clsAnexo11_BienInmueble : clsEntidadBase, IDataErrorInfo, IVictima, IEditableObject
  {
    /// <summary>
    /// Retorna una copia de esta entidad.
    /// </summary>
    /// <returns></returns>
    public T1 ObtenerCopia<T1>() where T1 : class
    {
      T1 Resultado = Activator.CreateInstance<T1>();
      clsAnexo11_BienInmueble R = Resultado as clsAnexo11_BienInmueble;

      R.ID = this.ID;

      //R.TipoPoblacionId = this.TipoPoblacionId;
      //R.EntornoId = this.EntornoId;
      //R.EntornoOtro = this.EntornoOtro;

      R.TipoEntorno = this.TipoEntorno;
      R.BarrioVeredaId = this.BarrioVeredaId;
      R.BarrioVeredaNombre = this.BarrioVeredaNombre;
      R.LocalidadCorregimientoId = this.LocalidadCorregimientoId;
      R.LocalidadCorregimientoNombre = this.LocalidadCorregimientoNombre;

      R.ExtensionArea = this.ExtensionArea;
      R.ExtensionUnidadDeArea = this.ExtensionUnidadDeArea;
      R.LocalizacionDepartamento = this.LocalizacionDepartamento;
      R.LocalizacionMunicipio = this.LocalizacionMunicipio;
      R.NombreDireccion = this.NombreDireccion;
      R.PersonaAfectadaId = this.PersonaAfectadaId;
      R.TipoInmueble = this.TipoInmueble;
      R.TipoTenencia = this.TipoTenencia;

      R.EstadoRegistro = this.EstadoRegistro;

      return Resultado;
    }

    #region IEditableObject

    [System.Xml.Serialization.XmlIgnore()]
    clsAnexo11_BienInmueble CopiaTemporal = null;

    public void BeginEdit()
    {
      CopiaTemporal = ObtenerCopia<clsAnexo11_BienInmueble>();
    }

    public void CancelEdit()
    {
      ID = CopiaTemporal.ID;

      TipoEntorno = CopiaTemporal.TipoEntorno;
      BarrioVeredaId = CopiaTemporal.BarrioVeredaId;
      BarrioVeredaNombre = CopiaTemporal.BarrioVeredaNombre;
      LocalidadCorregimientoId = CopiaTemporal.LocalidadCorregimientoId;
      LocalidadCorregimientoNombre = CopiaTemporal.LocalidadCorregimientoNombre;

      ExtensionArea = CopiaTemporal.ExtensionArea;
      ExtensionUnidadDeArea = CopiaTemporal.ExtensionUnidadDeArea;
      LocalizacionDepartamento = CopiaTemporal.LocalizacionDepartamento;
      LocalizacionMunicipio = CopiaTemporal.LocalizacionMunicipio;
      NombreDireccion = CopiaTemporal.NombreDireccion;
      PersonaAfectadaId = CopiaTemporal.PersonaAfectadaId;
      TipoInmueble = CopiaTemporal.TipoInmueble;
      TipoTenencia = CopiaTemporal.TipoTenencia;
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
