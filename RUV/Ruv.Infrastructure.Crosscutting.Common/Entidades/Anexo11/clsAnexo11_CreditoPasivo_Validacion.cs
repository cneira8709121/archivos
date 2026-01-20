using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{  
  public partial class clsAnexo11_CreditoPasivo : clsEntidadBase, IDataErrorInfo, IVictima
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
            case "TipoAcreedor":
                if (!TipoAcreedor.HasValue)
              resultado = "Debe indicar el tipo de acreedor";
            break;
            case "NombreAcreedor":
            if (string.IsNullOrWhiteSpace(NombreAcreedor))
                resultado = "Debe indicar el nombre del acreedor";
            break;
            case "FechaContrajoObligacion":
            if (!FechaContrajoObligacion.HasValue)
                resultado = "Debe indicar la fecha en que contrajo la obligación";
            break;
            case "MontoAdeudado":
            double MontoMinimo = 10000;
            if (!MontoAdeudado.HasValue)
                resultado = "Debe indicar el monto adeudado";
            if (MontoAdeudado.HasValue && MontoAdeudado.Value < MontoMinimo)
                resultado =string.Format("El monto adeudado no puede ser menor a {0}", MontoMinimo);
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
