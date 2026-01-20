using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo04 : clsEntidadBase, IDataErrorInfo
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
                        {
                            resultado = "Debe seleccionar víctima 1";
                        }
                        else
                        {
                            if (clsDeclaracion.DeclaracionActual.TomaDeclaracion.DeclaranteId == JefeGrupoFamiliarId.Value)
                            {
                                resultado = "La persona no puede estar marcada como declarante y como víctima 1, realice la corrección del caso";
                            }
                        }
                            
                        break;
                    case "Victimas":
                        
                        if (!Victimas.Any(item => item.VictimaDesaparecida == 1))
                            resultado = "Debe marcar al menos una persona como desaparecida en este hecho";

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
