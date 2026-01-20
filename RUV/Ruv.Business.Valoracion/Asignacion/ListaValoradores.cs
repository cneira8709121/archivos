using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion;
using Ruv.Data.Valoracion.Asignacion;
using Ruv.Data.Valoracion.Valoracion;
using System.Data;



namespace Ruv.Business.Valoracion.Asignacion
{
    public class ListaValoradores
    {
        public static List<clsValorador> GetData()
        {
            List<clsValorador> Lvaloradores = new List<clsValorador>();

            entAsignacion bd = new entAsignacion();
            DataTable table = bd.getValoradores();
            foreach (DataRow item in table.Rows)
            {
                clsValorador declaracion = new clsValorador();
                ParseDataToView(item, ref declaracion);
                Lvaloradores.Add(declaracion);
            }
            return Lvaloradores;
        }


        private static void ParseDataToView(DataRow ValoradorData, ref clsValorador ValoradorView)
        {
            //TODO: Cambiar DataRow por EF de una vista de la base de datos
            ValoradorView.Id = Convert.ToInt32(ValoradorData["ID"]);
            ValoradorView.Nombre = ValoradorData["nombre"].ToString();
        }
    }
}
