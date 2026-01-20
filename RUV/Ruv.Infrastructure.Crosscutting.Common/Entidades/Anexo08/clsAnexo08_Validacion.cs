using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo08 : clsEntidadBase, IDataErrorInfo
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
                    case "Victimas":
                        if (!Victimas.Any(x => x.EstadoRegistro != eEstadoRegistro.Eliminado))
                            resultado = "Debe registrar al menos una víctima";
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
