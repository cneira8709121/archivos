using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Estados
    {
        public static List<clsEstadosValoracion> GetEstadosValoracionPersona()
        {
            entValoracion objValoracion = new entValoracion();
            List<clsEstadosValoracion> valestados = new List<clsEstadosValoracion>();
            List<TBESTADO_VAL> estados = objValoracion.GetEstadosValoracion();
            foreach (TBESTADO_VAL datos in estados)
            {
                clsEstadosValoracion view =new clsEstadosValoracion();
                ParseDataToView(datos, ref view);
                valestados.Add(view);
            }
            return valestados;
        }

        private static void ParseDataToView(TBESTADO_VAL datos, ref clsEstadosValoracion view)
        {
            view.Id = datos.ID;
            view.Nombre = datos.NOMBRE;
        }
    }
}
