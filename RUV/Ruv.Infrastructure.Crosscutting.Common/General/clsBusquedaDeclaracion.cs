using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
  /// <summary>
  /// Contiene los parámetros para buscar una declaración.
  /// </summary>
  public partial class clsBusquedaDeclaracion : INotifyPropertyChanged
  {
    #region PARAMETROS DE RETORNO ÚNICAMENTE

    private int? _Id;
    /// <summary>
    /// El Id de la declaración.
    /// </summary>
    public int? Id
    {
      get { return _Id; }
      set
      {
        _Id = value;
        ReportarCambioPropiedad("Id");
      }
    }

    private DateTime? _FechaDeclaracion;
    public DateTime? FechaDeclaracion
    {
      get { return _FechaDeclaracion; }
      set
      {
        _FechaDeclaracion = value;
        ReportarCambioPropiedad("FechaDeclaracion");
      }
    }

    private eEstadoDeclaracion _EstadoDeclaracion = eEstadoDeclaracion.Ninguno;
    /// <summary>
    /// Estado de la declaración.
    /// </summary>
    public eEstadoDeclaracion EstadoDeclaracion
    {
      get { return _EstadoDeclaracion; }
      set
      {
        _EstadoDeclaracion = value;
        ReportarCambioPropiedad("EstadoDeclaracion");
      }
    }

    #endregion

    #region PARAMETROS DE BUSQUEDA Y RETORNO

    private string _CodigoDeclaracion;
    public string CodigoDeclaracion
    {
      get { return _CodigoDeclaracion; }
      set
      {
        _CodigoDeclaracion = value;
        ReportarCambioPropiedad("CodigoDeclaracion");
      }
    }

    private string _DeclaranteNumeroIdentificacion;
    public string DeclaranteNumeroIdentificacion
    {
      get { return _DeclaranteNumeroIdentificacion; }
      set
      {
        _DeclaranteNumeroIdentificacion = value;
        ReportarCambioPropiedad("DeclaranteNumeroIdentificacion");
      }
    }

    private string _DeclarantePrimerNombre;
    public string DeclarantePrimerNombre
    {
      get { return _DeclarantePrimerNombre; }
      set
      {
        _DeclarantePrimerNombre = value;
        ReportarCambioPropiedad("DeclarantePrimerNombre");
      }
    }

    private string _DeclaranteDemasNombres;
    public string DeclaranteDemasNombres
    {
      get { return _DeclaranteDemasNombres; }
      set
      {
        _DeclaranteDemasNombres = value;
        ReportarCambioPropiedad("DeclaranteDemasNombres");
      }
    }

    private string _DeclarantePrimerApellido;
    public string DeclarantePrimerApellido
    {
      get { return _DeclarantePrimerApellido; }
      set
      {
        _DeclarantePrimerApellido = value;
        ReportarCambioPropiedad("DeclarantePrimerApellido");
      }
    }

    private string _DeclaranteSegundoApellido;
    public string DeclaranteSegundoApellido
    {
      get { return _DeclaranteSegundoApellido; }
      set
      {
        _DeclaranteSegundoApellido = value;
        ReportarCambioPropiedad("DeclaranteSegundoApellido");
      }
    }

    #endregion
  }
}
