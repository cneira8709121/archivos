using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data;
using Ruv.Data.Valoracion.Asignacion;

namespace Ruv.Business.Valoracion.Asignacion
{
    public class DeclaracionesValorando
    {
        public static List<clsDeclaracionValoraracion> GetData()
        {
            List<clsDeclaracionValoraracion> declaracionesSinValorar = new List<clsDeclaracionValoraracion>();

            entAsignacion bd = new entAsignacion();
            DataTable table = bd.getDeclaracionesValorando();
            foreach (DataRow item in table.Rows)
            {
                clsDeclaracionValoraracion declaracion = new clsDeclaracionValoraracion();
                ParseDataToView(item, ref declaracion);
                declaracionesSinValorar.Add(declaracion);
            }
            return declaracionesSinValorar;
        }


        public static void GetDataPaginado(ref clsConsultaValoracion consulta, ref string error)
        {
            entAsignacion objDb = new entAsignacion();
            List<Ruv.Business.DTO.Valoracion.clsDeclaracionesValoracion> result = objDb.getDeclaracionesValorandoPaginado(consulta, ref error);
            if (result != null && result.Count > 0)
            {
                consulta.Declaraciones = result.Select(x => new clsDeclaracionValoraracion
                {
                    ID = x.ID,
                    NombreDeclarante = x.NombreDeclarante,
                    DocumentoDeclarante = x.DocumentoDeclarante,
                    Departamento = x.Departamento,
                    Municipio = x.Municipio,
                    NumeroFormulario = x.NumeroFormulario,
                    FechaRadicado = x.FechaRadicado,
                    Entidad = x.Entidad,
                    HechoVictimizante = x.HechoVictimizante,
                    TotalHV = x.TotalHV,
                    Valorador = x.Valorador
                }).ToList();
            }
        }

        private static void ParseDataToView(DataRow declaracionData, ref clsDeclaracionValoraracion declaracionView)
        {
            //TODO: Cambiar DataRow por EF de una vista de la base de datos
            declaracionView.ID = Convert.ToInt32(declaracionData["ID"]);
            declaracionView.NombreDeclarante = declaracionData["NOMBRE_PERSONA"].ToString();
            declaracionView.DocumentoDeclarante = declaracionData["NUMERODOCUMENTO"].ToString();
            declaracionView.FechaRadicado = Convert.ToDateTime(declaracionData["FECHA_RADICACION"]);
            declaracionView.NumeroFormulario = declaracionData["NRO_FORMULARIO"].ToString();
            declaracionView.TotalHV = Convert.ToInt32(declaracionData["TOTAL_HV"]);
            declaracionView.Departamento = declaracionData["DEPARTAMENTO"].ToString();
            declaracionView.Municipio = declaracionData["MUNICIPIO"].ToString();
            declaracionView.Entidad = declaracionData["TIPOENTIDAD"].ToString();
            declaracionView.Valorador = declaracionData["VALORADOR"].ToString();
        }
        public static void GetDataCantidad(ref clsConsultaValoracion consulta, ref string error)
        {
            entAsignacion objDb = new entAsignacion();
            objDb.getDeclaracionesValorandoTotal(ref consulta, ref error);
        }

    }
}
