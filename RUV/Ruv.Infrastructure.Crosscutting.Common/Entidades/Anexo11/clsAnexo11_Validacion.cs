using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo11 : clsEntidadBase, IDataErrorInfo, IAnexo
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
                    case "BienesInmuebles":
                        if (!BienesMuebles.Any() && !BienesInmuebles.Any())
                            resultado = "Debe selecionar a menos un inmueble o un mueble";
                        break;
                    case "BienesMuebles":
                        // Todos deben tener la persona seleccionada.
                        if (!BienesMuebles.Any() && !BienesInmuebles.Any())
                            resultado = "Debe selecionar a menos un mueble o un inmueble";
                        break;
                    case "LoteFueDespojado":
                        if (!LoteFueDespojado.HasValue)
                            resultado = "Debe indicar si el lote fue o no despojado";
                        break;
                    case "DespojoTipo":
                        if (LoteFueDespojado.HasValue && LoteFueDespojado.Value == (int)eSiNoNsNr.Si && !DespojoTipo.HasValue)
                            resultado = "Debe indicar el tipo de despojo";
                        break;
                    case "DespojoQuien":
                        if (LoteFueDespojado.HasValue && LoteFueDespojado.Value == (int)eSiNoNsNr.Si && string.IsNullOrWhiteSpace(DespojoQuien))
                            resultado = "Debe indicar el autor del despojo";
                        break;
                    case "EstadoActualLote":
                        if (LoteFueDespojado.HasValue && LoteFueDespojado.Value == (int)eSiNoNsNr.Si && !EstadoActualLote.HasValue)
                            resultado = "Debe indicar la situación actual del lote";
                        break;
                    case "SolicitaProteccionMuebles":
                        if (!SolicitaProteccionMuebles.HasValue)
                            resultado = "Debe indicar si solicita que le sea tramitada la protección de los bienes inmuebles";
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
