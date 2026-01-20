using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class ListaTareasValoracion
    {
        public static List<clsValoradorTareas> GetListaValoraciones(int ValoradorId)
        {
            entListaTareas objListaTareas = new entListaTareas();
            List<clsValoradorTareas> valoraciones = new List<clsValoradorTareas>();
            DataTable dt = objListaTareas.GetValoracionesPorValorador(ValoradorId);

            foreach (DataRow item in dt.Rows)
            {
                clsValoradorTareas tarea = new clsValoradorTareas();
                ParseDataToView(item, ref tarea);
                valoraciones.Add(tarea);
            }
            return valoraciones;
        }

        private static void ParseDataToView(DataRow data, ref clsValoradorTareas view)
        {
            view.ValoracionId = Convert.ToInt32(data["ID"]);
            view.ValoradorId = Convert.ToInt32(data["id_valorador"]);
            view.Declarante = data["Declarante"].ToString();
            view.DocumentoDeclarante = data["DocumentoDeclarante"].ToString();
            view.FechaRadicacion = (data["FechaRadicacion"] != DBNull.Value)?Convert.ToDateTime(data["FechaRadicacion"]) : DateTime.Now;
            view.NumeroFormulario = data["Formulario"].ToString();
            view.HechosVictimizantes = data["Hechos"].ToString();
            view.TotalHv = (data["TotalHV"] != DBNull.Value) ? Convert.ToInt32(data["TotalHV"]): 0;
            view.FechaAsignacion = Convert.ToDateTime(data["FechaAsignacion"]);
            view.Estado = data["Estado"].ToString();
        }

        public static void GetListaPaginada(ref clsConsultaValoracion eConsulta, ref string error)
        {
            entValoracion objDb = new entValoracion();
            List<Ruv.Business.DTO.Valoracion.clsTareasValorador> result = objDb.getListaTareas(eConsulta, ref error);
            if (result != null && result.Count > 0)
            {
                eConsulta.Tareas = result.Select(x => new clsValoradorTareas
                {
                    ValoracionId = x.ValoracionId,
                    ValoradorId = x.ValoradorId,
                    Declarante = x.Declarante,
                    DocumentoDeclarante = x.DocumentoDeclarante,
                    FechaRadicacion = x.FechaRadicacion,
                    NumeroFormulario = x.NumeroFormulario,
                    HechosVictimizantes = x.HechosVictimizantes,
                    TotalHv = x.TotalHv,
                    FechaAsignacion = x.FechaAsignacion,
                    Estado = x.Estado,
                    Observacion = x.Observacion,
                    FechaActualizacion = x.FechaActualizacion,
                    IdDeclaracion = x.IdDeclaracion
                }).ToList();
            }
        }

        public static void GetListaCantidad(ref clsConsultaValoracion eConsulta, ref string error)
        {
            entValoracion objDb = new entValoracion();
            objDb.getListaTareasCantidad(ref eConsulta, ref error);
        }
    }
}
