using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;

namespace Ruv.Business.Valoracion
{
    public class DecretoLey
    {
        private static void ParseDataToView(TBPARAMETROS datos, ref clsDecretoLey view)
        {
            view.Id = datos.ID;
            view.Nombre = datos.NOMBRE;
        }

        internal static List<clsDecretoLey> GetDecretoLey()
        {
            int idTipoParametro = 60;// TipoParametro de nombre si/no
            entValoracion objValoracion = new entValoracion();
            List<clsDecretoLey> listaHechoEnmarcado = new List<clsDecretoLey>();
            List<TBPARAMETROS> listaParametros = objValoracion.GetDecretoLey(idTipoParametro);
            foreach (TBPARAMETROS datos in listaParametros)
            {
                clsDecretoLey view = new clsDecretoLey();
                ParseDataToView(datos, ref view);
                listaHechoEnmarcado.Add(view);
            }
            return listaHechoEnmarcado;
        }
    }
}
