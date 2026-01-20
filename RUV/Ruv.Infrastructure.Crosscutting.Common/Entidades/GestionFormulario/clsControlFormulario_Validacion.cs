using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario
{
    public partial class clsControlFormulario : IDataErrorInfo
    {
        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string sResultado = null;
                string sResDistribucionPorFiltro = null;
                switch (columnName)
                {
                    case "NDesde":
                        if (!(NDesde.HasValue || NHasta.HasValue) || (NDesde.HasValue && !NHasta.HasValue)) break;
                        if (NHasta.HasValue && !NDesde.HasValue)
                        {
                            sResultado = "El rango no puede tener un valor final sin uno inicial";
                        }
                        else if (NDesde.Value > NHasta.Value) sResultado = "El rango ingresado para la búsqueda es inválido";
                        break;
                    case "NHasta":
                        if (!(NDesde.HasValue || NHasta.HasValue) || (NDesde.HasValue && !NHasta.HasValue)) break;
                        if (NHasta.HasValue && !NDesde.HasValue)
                        {
                            sResultado = "El rango no puede tener un valor final sin uno inicial";
                        }
                        else if (NDesde.Value > NHasta.Value) sResultado = "El rango ingresado para la búsqueda es inválido";
                        break;
                    case "DGenerado":
                        if (DGenerado.HasValue && DGenerado.Value > DateTime.Now) sResultado = "La fecha de generación del formulario a consultar no puede ser mayor que la actual";
                        break;
                    case "NPaisIdFiltro":
                    case "NDepartamentoIdFiltro":
                    case "NMunicipioIdFiltro":
                    case "NEntidadMunicipioIdFiltro":
                        if (!NPaisIdFiltro.HasValue || !NDepartamentoIdFiltro.HasValue || !NMunicipioIdFiltro.HasValue || !NEntidadMunicipioIdFiltro.HasValue) sResDistribucionPorFiltro = sResultado = "No se pueden distribuir los documentos sin una ubicación geográfica ingresada";
                        break;
                    default:
                        break;
                }

                BSePuedeBuscar = string.IsNullOrEmpty(sResultado);
                BSePuedeDistribuir = BSePuedeBuscar && (NDesde.HasValue || NHasta.HasValue);
                BSePuedeSeparar = BSePuedeBuscar && (NDesde.HasValue || NHasta.HasValue);
                BSePuedeDistribuirFiltro = string.IsNullOrEmpty(sResDistribucionPorFiltro);

                return sResultado;
            }
        }
    }
}
