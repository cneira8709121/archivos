using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class HechoEnmarcado
    {
        private static void ParseDataToView(TBPARAMETROS datos, ref clsHechoEnmarcado view)
        {
            view.Id = datos.ID;
            view.Nombre = datos.NOMBRE;            
        }

        internal static List<clsHechoEnmarcado> GetHechoEnmarcado()
        {
            entValoracion objValoracion = new entValoracion();
            List<clsHechoEnmarcado> listaHechoEnmarcado = new List<clsHechoEnmarcado>();
            List<TBPARAMETROS> listaParametros = objValoracion.GetHechosEnmarcado();
            foreach (TBPARAMETROS datos in listaParametros)
            {
                clsHechoEnmarcado view = new clsHechoEnmarcado();                
                ParseDataToView(datos, ref view);
                listaHechoEnmarcado.Add(view);
            }
            return listaHechoEnmarcado;
        }
    }
}
