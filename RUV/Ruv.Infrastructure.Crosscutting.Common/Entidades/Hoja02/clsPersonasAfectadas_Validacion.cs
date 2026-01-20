using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsPersonasAfectadas : clsEntidadBase, IDataErrorInfo
  {
    #region VALIDACIONES

    [System.Xml.Serialization.XmlIgnore]
    public string this[string columnName]
    {
      get
      {
        string resultado = null;
        switch (columnName)
        {
          case "ListaPersonas":
          case "ListaPersonasOrdenada":
              if (!ListaPersonas.Any())
                  resultado = "Debe relacionarse al menos una persona en la lista de afectados";
              else
              {
                if (ListaPersonas.Count(x => x.Relacion.HasValue && x.Relacion.Value == (int)eRelacion.Jefe_de_hogar) == 0)
                  resultado = "Debe existir un Jefe(a) de hogar";
              }                
              
            break;
        }

        return resultado;
      }
    }

    [System.Xml.Serialization.XmlIgnore]
    public string Error
    {
      get { return null; }
    }

    #endregion
  }
}
