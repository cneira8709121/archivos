using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data;
using Ruv.Data.Valoracion.Asignacion;

namespace Ruv.Business.Valoracion.Asignacion
{
    public class DetalleDeclaracion
    {

        public static List<clsPersona> GetDetalleDeclaracionPorId(int DeclaracionId)
        {
            List<clsPersona> _personas = new List<clsPersona>();
            entAsignacion entAsignacion = new entAsignacion();
            DataTable dt = entAsignacion.getDetalleDeclaracionPorId(DeclaracionId);
            foreach (DataRow item in dt.Rows)
            {
                clsPersona persona = new clsPersona();
                ParseDataToView(item, ref persona);
                _personas.Add(persona);
            }
            return _personas;
        }

        private static void ParseDataToView(DataRow data, ref clsPersona view)
        {
            view.Id = Convert.ToInt32(data["ID"]);
            view.Persona = data["Nombre_Persona"].ToString();
            view.TipoDocumento = data["TIPO_DOCUMENTO"].ToString();
            view.NumeroDocumento = data["NUMERODOCUMENTO"].ToString();
            view.Relacion = data["RELACION"].ToString();
            view.GeneroNombre = data["GENERO"].ToString();
            view.Edad = (data["EDAD"] != DBNull.Value) ? Convert.ToInt32(data["EDAD"]) : 0;
            view.EtniaNombre = data["ETNIA"].ToString();
            view.Discapacitado = (data["Es_Discapacitado"] != DBNull.Value) ? Convert.ToBoolean(data["Es_Discapacitado"]) : false;
            view.Hechos = data["Hechos"].ToString();
        }

        internal static List<clsPersona> GetDetalleDeclaracionPorId(DataTable ValoracionFullds)
        {
            List<clsPersona> _personas = new List<clsPersona>();
            entAsignacion entAsignacion = new entAsignacion();
            foreach (DataRow item in ValoracionFullds.Rows)
            {
                clsPersona persona = new clsPersona();
                ParseDataToView(item, ref persona);
                _personas.Add(persona);
            }
            return _personas;
        }
    }


}
