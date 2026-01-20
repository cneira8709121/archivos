using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  /// <summary>
  /// Contiene los parámetros para buscar una declaración.
  /// </summary>
  public partial class clsBusquedaDeclaracion : INotifyPropertyChanged
  {
    /// <summary>
    /// 1=Hay por lo menos un parámetro para hacer búsqueda.
    /// </summary>
    public bool HayParametrosMinimosParaBusqueda
    {
      get
      {
        var Resultado =
          PropiedadConValor(CodigoDeclaracion)
          || PropiedadConValor(DeclaranteNumeroIdentificacion)
          || PropiedadConValor(DeclarantePrimerNombre)
          || PropiedadConValor(DeclaranteDemasNombres)
          || PropiedadConValor(DeclarantePrimerApellido)
          || PropiedadConValor(DeclaranteSegundoApellido);

        return Resultado;
      }
    }

    Boolean PropiedadConValor(string propiedad)
    {
      return !string.IsNullOrWhiteSpace(propiedad);
    }

    /// <summary>
    /// Campo calculado, no requiere almacenamiento.
    /// </summary>
    public string NombreCompleto
    {
      get
      {
        string cadena = "";
        return cadena.UnirCadenas(
          DeclarantePrimerNombre, DeclaranteDemasNombres,
          DeclarantePrimerApellido, DeclaranteSegundoApellido);
      }
      set { }
    }


    #region INotifyPropertyChanged

    void ReportarCambioPropiedad(string nombrePropiedad)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
        PropertyChanged(this, new PropertyChangedEventArgs("HayParametrosMinimosParaBusqueda"));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
