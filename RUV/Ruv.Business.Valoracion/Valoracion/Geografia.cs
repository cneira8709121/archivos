using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Geografia
    {
        public static List<clsGeografia> ObtenerGeografia(int? nivel,int? tipo, int? padre)
        {
            entGeografia objGeografia = new entGeografia();
            List<clsGeografia> _geografias = new List<clsGeografia>();
            DataTable GeoData = objGeografia.ObtenerGeografia(nivel, tipo, padre);

            foreach (DataRow dr in GeoData.Rows)
            {
                clsGeografia view = new clsGeografia();
                view.Id = Convert.ToInt32(dr["Id"]);
                view.Nombre = dr["Nombre"].ToString();
                view.Tipo = Convert.ToInt32(dr["Tipo"]);

                _geografias.Add(view);
            }

            return _geografias;
        }

        internal static List<clsGeografia> ObtenerGeografia()
        {
            entGeografia objGeografia = new entGeografia();
            List<clsGeografia> _geografias = new List<clsGeografia>();
            DataTable GeoData = objGeografia.ObtenerGeografia();

            foreach (DataRow dr in GeoData.Rows)
            {
                clsGeografia view = new clsGeografia();
                view.Id = Convert.ToInt32(dr["Id"]);
                view.Nombre = dr["Nombre"].ToString();
                view.Tipo = Convert.ToInt32(dr["Tipo"]);
                view.Padre = Convert.ToInt32(dr["Padre"]);
                _geografias.Add(view);
            }

            return _geografias;
        }
    }
}
