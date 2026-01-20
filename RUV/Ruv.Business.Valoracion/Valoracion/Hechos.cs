using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;
using System.Reflection;
using System.Data.Common;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Hechos
    {
        public static List<clsHechosValoracion> GetHechosPorValoracion(int ValoracionID)
        {
            List<clsHechosValoracion> hechos = new List<clsHechosValoracion>();
            entValoracion objValoracion = new entValoracion();
            DataTable dt = objValoracion.GetHechosPorValoracionId(ValoracionID);
            foreach (DataRow item in dt.Rows)
            {
                clsHechosValoracion hecho = new clsHechosValoracion();
                ParseDataToView(item, ref hecho);
                hechos.Add(hecho);
            }
            return hechos;
        }

        public static clsHechosValoracion Actualizar(clsHechosValoracion hecho, DbTransaction tra)
        {
            //entValoracionAnexo objValAnexo = new entValoracionAnexo();
            //TBVALORACION_ANEXO data = objValAnexo.GetPorId(hecho.Id);
            //ParseViewToData(ref data, hecho);
            //data = objValAnexo.Actualizar(data);
            //clsHechosValoracion view = new clsHechosValoracion();
            //ParseDataToView(data, ref view);
            clsHechosValoracion view = new entValoracionAnexo().Actualizar(hecho, tra);
            return view;
        }

        public static void ParseDataToView(TBVALORACION_ANEXO data, ref clsHechosValoracion view)
        {
            view.Id = data.ID;
            view.ValoracionId = data.ID_VALORACION;
            view.UltimaFechaEdicion = (data.ULTIMA_FECHAEDICION.HasValue)?data.ULTIMA_FECHAEDICION.Value: DateTime.Now;
            view.TipoHechoId = (data.TIPO_ANEXO.HasValue) ? Convert.ToInt32(data.TIPO_ANEXO.Value) : 0;
            view.HechoId = (data.ID_SINIESTRO.HasValue) ? Convert.ToInt32(data.ID_SINIESTRO.Value) : 0;
        }

        public static void ParseDataToView(DataRow data, ref clsHechosValoracion view)
        {
            view.Id = Convert.ToInt32(data["ID"]);
            view.DeclaracionId = Convert.ToInt32(data["id_declaracion"]);
            view.TipoHecho = data["TipoHecho"].ToString();
            view.TipoHechoId = (data["TipoHechoId"] != DBNull.Value) ? Convert.ToInt32(data["TipoHechoId"]) : 0;
            view.Fecha = (data["Fecha"] != DBNull.Value) ? Convert.ToDateTime(data["Fecha"]) : new DateTime();
            view.TipoEntorno = data["TipoEntorno"].ToString();
            view.LocalidadCorregimiento = data["LocalidadCorregimiento"].ToString();
            view.BarrioVereda = data["BarrioVereda"].ToString();
            view.Departamento = data["Departamento"].ToString();
            view.Municipio = data["Municipio"].ToString();
            view.TotalPersonas = Convert.ToInt32(data["TotalPersonas"]);
            view.Victima1 = data["Victima1"].ToString();
            if (data["FechaDespojo"] != DBNull.Value)
                view.FechaDespojo = Convert.ToDateTime(data["FechaDespojo"]);
            else
                view.FechaDespojo = null;
            if (data["FechaAbandono"] != DBNull.Value)
                view.FechaAbandono = Convert.ToDateTime(data["FechaAbandono"]);
            else
                view.FechaAbandono = null;

        }

        public static void ParseViewToData(ref TBVALORACION_ANEXO data, clsHechosValoracion view)
        {
            data.ULTIMA_FECHAEDICION = view.UltimaFechaEdicion;
        }

        public static bool NuevoHecho(clsHecho hecho)
        {
            entValoracionAnexo objAnexo = new entValoracionAnexo();
            List<object> nhecho = ConstruirObjeto(hecho);
            int ValAnexoId = objAnexo.Nuevo(nhecho);

            foreach (clsPersonaNuevoHecho regPersona in hecho.Personas)
            {
                objAnexo.NuevoAnexo(ValAnexoId, regPersona.PersonaId, regPersona.EstadoEnHecho, hecho.FechaDespojo, hecho.FechaAbandono, hecho.ValorEspecifico, hecho.ValInmuebleAbandono, hecho.ValInmuebleDespojo);
            }

            return true;
        }

        private static List<object> ConstruirObjeto(clsHecho hecho)
        {
            List<object> obj = new List<object>();
            obj.Add(hecho.TipoHecho);
            obj.Add(hecho.Fecha);
            obj.Add(hecho.Departamento);
            obj.Add(hecho.Municipio);
            if (hecho.Tipoentorno > 0) { obj.Add(hecho.Tipoentorno); } else { obj.Add(null); }
            obj.Add(hecho.CorrLocId);
            obj.Add(hecho.BarrVerId);
            obj.Add(hecho.OtraLocCorrId);
            obj.Add(hecho.OtroBarVerId);
            obj.Add(hecho.Personas.First(X=>X.Victima1).PersonaId);
            obj.Add(hecho.Valoracion.Id);
            obj.Add(hecho.TipoHechoOtro);

            return obj;
        }

        internal static List<clsHechosValoracion> GetHechosPorValoracion(DataTable dtHechos)
        {
            List<clsHechosValoracion> hechos = new List<clsHechosValoracion>();
            entValoracion objValoracion = new entValoracion();
            foreach (DataRow item in dtHechos.Rows)
            {
                clsHechosValoracion hecho = new clsHechosValoracion();
                ParseDataToView(item, ref hecho);
                hechos.Add(hecho);
            }
            return hechos;
        }
    }
}
