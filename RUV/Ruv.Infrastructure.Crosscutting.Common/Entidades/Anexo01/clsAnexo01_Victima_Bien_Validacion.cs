using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsAnexo01_Victima_Bien : clsEntidadBase, IDataErrorInfo
  {
    #region VALIDACIONES

    public string this[string columnName]
    {
      get
      {
          if (!ValidationManager.ValidateProperty(clsDeclaracion.ConfiguracionValidaciones, Scope, columnName))
              return string.Empty;
        string resultado = null;
        switch (columnName)
        {
          case "TipoBien":
            if (!TipoBien.HasValue)
              resultado = "El 'tipo' de bien es obligatorio";
            break;

          case "Descripcion":
            if (string.IsNullOrWhiteSpace(Descripcion))
                resultado = "La 'descripción' es obligatoria";
            else if (Descripcion.Length > 500)
                resultado = "La descripcion no puede ser mayor a 500 caracteres";
            break;

          case "CalidadDeLaVictima":
            if (!CalidadDeLaVictima.HasValue)
              resultado = "La 'calidad de la víctima' es obligatoria";
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
