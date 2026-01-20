using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.Collections.Specialized;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  /// <summary>
  /// Almacena todos los datos de la lista de tareas
  /// </summary>
  [DataContract]
  [System.Diagnostics.DebuggerDisplay("{ID} - {Fecha} - {Formulario} - {Accion} - {Declaracion}")]
  public partial class clsListaTareas : clsEntidadBase
  {
    #region CONSTRUCTOR

      public clsListaTareas()
    {
      
    }

    #endregion

    #region PROPIEDADES

    private int _ID;
    /// <summary>
    /// ID
    /// </summary>
    [DataMember]
    public int ID
    {
      get { return _ID; }
      set
      {
        _ID = value;
      }
    }

    private DateTime _Fecha;
    /// <summary>
    /// Fecha
    /// </summary>
    [DataMember]
    public DateTime Fecha
    {
        get { return _Fecha; }
        set
        {
            _Fecha = value;
        }
    }

    private DateTime _FechaLlegada;
    /// <summary>
    /// Fecha llegada de la radicación
    /// </summary>
    [DataMember]
    public DateTime FechaLlegada
    {
        get { return _FechaLlegada; }
        set
        {
            _FechaLlegada = value;
        }
    }

    private string _Accion;
    /// <summary>
    /// Accion
    /// </summary>
    [DataMember]
    public string Accion
    {
        get { return _Accion; }
        set
        {
            _Accion = value;
        }
    }

    private int _IdAccion;
    /// <summary>
    /// Accion (Estado)
    /// </summary>
    [DataMember]
    public int IdAccion
    {
        get { return _IdAccion; }
        set
        {
            _IdAccion = value;
        }
    }

    private string _Formulario;
    /// <summary>
    /// Formulario
    /// </summary>
    [DataMember]
    public string Formulario
    {
      get { return _Formulario; }
      set
      {
        _Formulario = value;
      }
    }

    private int _Declaracion;
    /// <summary>
    /// Declaracion
    /// </summary>
    [DataMember]
    public int Declaracion
    {
        get { return _Declaracion; }
        set
        {
            _Declaracion = value;
        }
    }

    private string _Tipo;
    /// <summary>
    /// Tipo, si es declaracion o correccion
    /// </summary>
    [DataMember]
    public string Tipo
    {
        get { return _Tipo; }
        set { _Tipo = value; }
    }

    private int? _Correccion;
    /// <summary>
    /// idCorreccion
    /// </summary>
    [DataMember]
    public int? Correccion
    {
        get { return _Correccion; }
        set { _Correccion = value; }
    }

    private int? _Regpersona;
    /// <summary>
    /// idRegpersona
    /// </summary>
    [DataMember]
    public int? Regpersona
    {
        get { return _Regpersona; }
        set { _Regpersona = value; }
    }
      
      
    #endregion
  }
}
