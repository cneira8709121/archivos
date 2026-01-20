using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ruv.Business.DTO.Devolucion;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.Business.Devolucion.Contratos
{
    public interface IDevolucion
    {

        clsDevolucion ObtenerDevolucion(Int32 idDeclaracion, ref string cError);

        Boolean ActualizarDevolucion(clsDevolucion devolucion, ref string cError);

        Boolean SolicitarDevolucion(clsDevolucion devolucion, ref string cError);

        clsDatosparaDevolucion CargaDatosparaDevolucion(int NIdDevolucion, ref string cError);

        List<clsCausal> ObtenerCausalesDevolucion(ref string cError);
    }
}
