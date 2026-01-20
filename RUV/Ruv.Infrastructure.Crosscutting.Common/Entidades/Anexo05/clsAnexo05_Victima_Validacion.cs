using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsAnexo05_Victima : clsEntidadBase, IDataErrorInfo
  {
     #region VALIDACIONES

    public string this[string columnName]
    {
      get
      {
        string resultado = null;
        switch (columnName)
        {
            case "SeDesplazo":
                if (!SeDesplazo.HasValue)
              resultado = "Debe indicar si la persona se desplazó";
            break;
        }
        return resultado;
      }
    }

    public string Error
    {
      get { return null; }
    }

    #endregion
  }
}
