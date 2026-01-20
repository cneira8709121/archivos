using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion.Valoracion;
using System.Data;
using System.Web;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Declaraciones
    {
        public static List<clsDeclaracionInfoValoracion> GetInfoDeclaracionPorId(int ValoracionId)
        {
            entDeclaracion objDeclaracion = new entDeclaracion();
            List<clsDeclaracionInfoValoracion> declaracionInfor = new List<clsDeclaracionInfoValoracion>();
            DataTable vdeclaracion = objDeclaracion.GetvDeclaracionPorId(ValoracionId);
            foreach (DataRow item in vdeclaracion.Rows)
            {
                clsDeclaracionInfoValoracion declaracion = new clsDeclaracionInfoValoracion();
                ParceDataToView(item, ref declaracion);
                declaracionInfor.Add(declaracion);
            }
            return declaracionInfor;
        }

        private static void ParceDataToView(DataRow data, ref clsDeclaracionInfoValoracion view)
        {
            view.DeclaracionId = Convert.ToInt32(data["ID"]);
            view.Formulario = data["nro_formulario"].ToString();
            view.FechaRadicado = (data["FECHALLEGADA"] != DBNull.Value) ? Convert.ToDateTime(data["FECHALLEGADA"]) : DateTime.Now;
            view.UnidadTerritorial = data["UNIDADTERRITORIAL"].ToString();
            view.Departamento = data["Departamento"].ToString();
            view.Municipio = data["Municipio"].ToString();
            if (data["VALORADOR"] != null)
            {
                view.Valorador = data["VALORADOR"].ToString();
            }
           
            view.FechaValoracion = (data["FechaValoracion"] != DBNull.Value) ? Convert.ToDateTime(data["FechaValoracion"]) : DateTime.Now;
        }
             
    }
}
