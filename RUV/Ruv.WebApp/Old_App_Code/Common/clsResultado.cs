using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Runtime.Serialization;

  [DataContract]
  public class clsResultado
  {
    public clsResultado()
    {
      ErroresDB = new List<string>();
      AdvertenciasDB = new List<string>();
    }

    private clsDeclaracion _Declaracion;
    /// <summary>
    /// La declaración.
    /// </summary>
    [DataMember]
    public clsDeclaracion Declaracion
    {
      get { return _Declaracion; }
      set { _Declaracion = value; }
    }

    private List<string> _ErroresDB;
    /// <summary>
    /// La lista de los errores encontrados que no permitieron la grabación exitosa 
    /// de la declaración.
    /// </summary>
    [DataMember]
    public List<string> ErroresDB
    {
      get { return _ErroresDB; }
      set { _ErroresDB = value; }
    }

    private List<string> _AdvertenciasDB;
    /// <summary>
    /// Lista advertencias que se presentaron a pesar que la grabación y registro 
    /// de la declaración en la base de datos fué exitosa.
    /// </summary>
    [DataMember]
    public List<string> AdvertenciasDB
    {
      get { return _AdvertenciasDB; }
      set { _AdvertenciasDB = value; }
    }
  }

