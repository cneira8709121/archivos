using System;
using System.Collections.Generic;
using System.Data;
using Ruv.Data.Valoracion.Asignacion;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

namespace Ruv.Business.Valoracion.Asignacion
{
    public class ListaDeclaracionesSinValorar
    {
        public static List<clsDeclaracionValoraracion> GetData()
        {
            List<clsDeclaracionValoraracion> declaracionesSinValorar = new List<clsDeclaracionValoraracion>();

            entAsignacion bd = new entAsignacion();
            DataTable table = bd.getDeclaracionesSinValorar();
            foreach (DataRow item in table.Rows)
            {
                clsDeclaracionValoraracion declaracion = new clsDeclaracionValoraracion();
                ParseDataToView(item, ref declaracion);
                declaracionesSinValorar.Add(declaracion);
            }
            return declaracionesSinValorar;
        }


        private static void ParseDataToView(DataRow declaracionData, ref clsDeclaracionValoraracion declaracionView)
        {
            //TODO: Cambiar DataRow por EF de una vista de la base de datos
            declaracionView.ID = Convert.ToInt32(declaracionData["ID"]);
            declaracionView.NombreDeclarante = declaracionData["NombreDeclarante"].ToString();
            declaracionView.DocumentoDeclarante = declaracionData["DocumentoDeclarante"].ToString();
            declaracionView.FechaRadicado = (declaracionData["FechaRadicado"] != DBNull.Value) ? Convert.ToDateTime(declaracionData["FechaRadicado"]) : DateTime.Now;
            declaracionView.NumeroFormulario = declaracionData["NumeroFormulario"].ToString();
            declaracionView.TotalHV = (declaracionData["TotalHv"] != DBNull.Value) ? Convert.ToInt32(declaracionData["TotalHv"]) : 0;
            declaracionView.Departamento = declaracionData["Departamento"].ToString();
            declaracionView.Municipio = declaracionData["Municipio"].ToString();
            declaracionView.Entidad = declaracionData["Entidad"].ToString();
            //declaracionView.Estado = declaracionData["ESTADO"].ToString();
            //declaracionView.RegimenEspecial = declaracionData["REGIMENESPECIAL"].ToString();
            //declaracionView.Genero = declaracionData["GENERO"].ToString();
            //declaracionView.Etnia = declaracionData["ETNIA"].ToString();
            //declaracionView.FechaDeclaracion = (declaracionData["FECHADECLARACION"] != DBNull.Value) ? Convert.ToDateTime(declaracionData["FECHADECLARACION"]) : DateTime.Now;
            //declaracionView.FechaVencimiento = (declaracionData["FECHAVENCIMIENTO"] != DBNull.Value) ? Convert.ToDateTime(declaracionData["FECHAVENCIMIENTO"]) : DateTime.Now;

        }

        internal static List<clsDeclaracionValoraracion> GetDataPaginado(int Inicio, int Fin, string sortColumns, string filtro, string Valor)
        {
            List<clsDeclaracionValoraracion> declaracionesSinValorar = new List<clsDeclaracionValoraracion>();

            entAsignacion bd = new entAsignacion();
            DataTable table = bd.getDeclaracionesSinValorarPaginado(Inicio, Fin, sortColumns, filtro, Valor).Tables[0];
            foreach (DataRow item in table.Rows)
            {
                clsDeclaracionValoraracion declaracion = new clsDeclaracionValoraracion();
                ParseDataToView(item, ref declaracion);
                declaracionesSinValorar.Add(declaracion);
            }
            return declaracionesSinValorar;
        }

        internal static int GetCantidad(string filtro, string Valor)
        {
            entAsignacion bd = new entAsignacion();
            int Cantidad = bd.getDeclaracionesSinValorarCantidad(filtro, Valor);
            return Cantidad;
        }
    }
}
