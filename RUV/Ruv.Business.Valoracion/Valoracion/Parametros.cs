using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Parametros
    {

        public static List<clsParametroGeneral> GetParametros()
        {
            List<clsParametroGeneral> parametrosview = new List<clsParametroGeneral>();
            entParametro ObjParametros = new entParametro();
            List<TBPARAMETROS> parametro = ObjParametros.GetParametros();
            foreach (TBPARAMETROS data in parametro)
            {
                clsParametroGeneral view = new clsParametroGeneral();
                view.Id = data.ID;
                view.Nombre = data.NOMBRE;
                view.Tipo = (eTipoParametros)Enum.ToObject(typeof(eTipoParametros), Convert.ToInt32(data.ID_TIPOPARAMETRO));
                parametrosview.Add(view);
            }
            return parametrosview;
        }

    }
}
