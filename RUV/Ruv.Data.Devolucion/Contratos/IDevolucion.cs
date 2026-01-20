using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ruv.Business.DTO.Devolucion;

namespace Ruv.Data.Devolucion.Contratos
{
    public interface IDevolucion
    {
        

        //clsDevolucion ObtenerDevolucion(Int32 idDeclaracion, Int32 idRadicacion, ref string cError);

        clsDevolucion ObtenerDevolucion(Int32 idDeclaracion, ref string cError);

        Boolean ActualizarDevolucion(clsDevolucion devolucion, DbTransaction tra, ref string cError);

        Boolean SolicitarDevolucion(clsDevolucion devolucion, DbTransaction tra, ref string cError);

        clsDatosparaDevolucion CargaDatosparaDevolucion(int NIdDevolucion, ref string cError);

        List<clsCausalDevolucion> ObtenerCausalesDevolucion(ref string cError);
    }
}
