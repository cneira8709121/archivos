using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsAnexo07 : clsEntidadBase, IDataErrorInfo
  {
    #region VALIDACIONES

    public string this[string columnName]
    {
      get
      {
        string resultado = null;
        switch (columnName)
        {
            case "JefeGrupoFamiliarId":
                if (!JefeGrupoFamiliarId.HasValue)
                    resultado = "Debe seleccionar víctima 1";
                break;
            case "LugarAccidente":
                if (string.IsNullOrWhiteSpace(LugarAccidente))
                    resultado = "Falta la descripción del lugar donde ocurrió el accidente";
                break;
                /*FICHA CONTROL DE CAMBIOS RUV 27-03-12
                    No se debe bloquear ningun campo cuando se marque la persona como "no víctima" 
            case "Victimas":
                if (!Victimas.Any())
                    resultado = "Debe relacionarse al menos una persona en la lista de víctimas";

                if (!Victimas.Any(item => item.VictimaDeEsteHecho == 1))
                    resultado = "Debe marcar al menos una persona como vítima de este hecho";

                break;
                 * */
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
