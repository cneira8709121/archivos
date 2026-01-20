using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsDescripcionHechos : clsEntidadBase, IDataErrorInfo
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
                    case "Narracion":
                        const int TamañoMinimoNarracion = 1500;
                        if (string.IsNullOrWhiteSpace(Narracion)
                          || Narracion.Trim().Length < TamañoMinimoNarracion)
                            resultado =
                              string.Format("La narración de los hechos debe contener no menos de {0} caracteres",
                              TamañoMinimoNarracion);

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
