using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsAnexo11_BienMueble : clsEntidadBase, IDataErrorInfo
  {
    #region VALIDACIONES

    public string this[string columnName]
    {
      get
      {
          if (!ValidationManager.ValidateProperty(clsDeclaracion.ConfiguracionValidaciones, Scope, columnName))
              return null;
        string resultado = null;
        switch (columnName)
        {
            case "PersonaAfectadaId":
                // Jhon Vargas DGT3
                //if (!PersonaAfectadaId.HasValue)
                if (!PersonaAfectadaId.HasValue || PersonaAfectadaId == 0)
                    resultado = "Debe seleccionar la 'persona'";
                break;
            case "TipoBien":
                if (!TipoBien.HasValue)
                    resultado = "Debe seleccionar el tipo de mueble";
                break;
            case "Descripcion":
                if (string.IsNullOrWhiteSpace(Descripcion))
                    resultado = "Debe digitar la descripción de mueble";
                break;
            case "TipoTenencia":
                if (!TipoTenencia.HasValue)
                    resultado = "Debe seleccionar el tipo de tenencia sobre el mueble";
                break;
            case "Cantidad":
                if (!Cantidad.HasValue)
                    resultado = "Debe digitar la cantidad relacionada con el(los) mueble(s)";
                break;
            case "BienesInmuebles":
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
