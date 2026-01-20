using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsAnexo11_BienInmueble : clsEntidadBase, IDataErrorInfo
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
            if (!PersonaAfectadaId.HasValue)
              resultado = "Debe seleccionar la 'persona'";
            break;
          case "TipoInmueble":
            if (!TipoInmueble.HasValue)
              resultado = "Debe seleccionar el tipo de inmueble";
            break;
          case "LocalizacionDepartamento":
            if (!LocalizacionDepartamento.HasValue)
                resultado = "Debe seleccionar el departamento del inmueble";
            break;
          case "LocalizacionMunicipio":
            if (!LocalizacionMunicipio.HasValue)
                resultado = "Debe seleccionar el municipio del inmueble";
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
