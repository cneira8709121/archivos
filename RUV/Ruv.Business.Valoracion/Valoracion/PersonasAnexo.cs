using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;
using System.Data.Common;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class PersonasAnexo
    {

        public static List<clsPersonaAnexo> GetPersonasPorAnexoId(int anexoId)
        {
            List<clsPersonaAnexo> personas = new List<clsPersonaAnexo>();
            entValoracion objValoracion = new entValoracion();
            DataTable dt = objValoracion.GetPersonasPorAnexoId(anexoId);
            foreach (DataRow data in dt.Rows)
            {
                clsPersonaAnexo view = new clsPersonaAnexo();
                ParseDataToView(data, ref view);
                personas.Add(view);
            }
            return personas;
        }

        private static void ParseDataToView(DataRow data, ref clsPersonaAnexo view)
        {
            view.Id = Convert.ToInt32(data["ID"]);
            view.Persona = data["Persona"].ToString();
            view.PersonaId = (data["id_regpersona"] != DBNull.Value) ? Convert.ToInt32(data["id_regpersona"]) : 0;
            view.TipoDocumento = data["TipoDocumento"].ToString();
            view.NumeroDocumento = data["numerodocumento"].ToString();
            view.Relacion = data["Relacion"].ToString();
            view.Sexo = (data["GeneroId"] != DBNull.Value) ? Convert.ToInt32(data["GeneroId"]) : 0;
            view.Genero = data["Genero"].ToString();
            view.Edad = (data["Edad"] != DBNull.Value) ? Convert.ToInt32(data["Edad"]) : 0;
            view.EtniaId = (data["EtniaId"] != DBNull.Value) ? Convert.ToInt32(data["EtniaId"]) : 0;
            view.Etnia = data["Etnia"].ToString();
            view.Discapacitado = ((data["Discapacitado"] != DBNull.Value)) ? Convert.ToBoolean(data["Discapacitado"]) : false;
            view.Fallecida = (data["Fallecida"] != DBNull.Value) ? Convert.ToBoolean(data["Fallecida"]) : false;
            view.Desaparecida = (data["Desaparecida"] != DBNull.Value) ? Convert.ToBoolean(data["Desaparecida"]) : false;
            view.Secuestrado = (data["Secuestrado"] != DBNull.Value) ? Convert.ToBoolean(data["Secuestrado"]) : false;
            view.EstadoPorMina = data["EstadoPorMina"].ToString();
            view.SeDesplazo = (data["SeDesplazo"] != DBNull.Value) ? Convert.ToBoolean(data["SeDesplazo"]) : false;
            view.Victima = ((data["esvicitma"] != DBNull.Value)) ? Convert.ToBoolean(data["esvicitma"]) : false;
            view.Afectado = ((data["esafectado"] != DBNull.Value)) ? Convert.ToBoolean(data["esafectado"]) : false;
            view.ValAnexoId = ((data["id_val_anexo"] != DBNull.Value)) ? Convert.ToInt32(data["id_val_anexo"]) : 0;
            view.Autores = Autores.GetAutores(view.Id);
            view.InfraccionesDHI = InfraccionDHI.GetInfraccionesPorValAnexoPerId(view.Id);
            view.Herramietas = Herramientas.GetHerramientasPorAnexoId(view.Id);
            view.AfectacionesDetectadas = Afectaciones.GetAfectacionesPorPersona(view.Id);
            view.Principios = Principios.GetPrincipiosPorValAnexoPerId(view.Id);

            if (data["id_estado_val"] != DBNull.Value || !string.IsNullOrEmpty(data["id_estado_val"].ToString()))
            {
                view.EstadoId = Convert.ToInt32(data["id_estado_val"]);
            }
            view.Estado = data["estado_val"].ToString();

            if (data["id_observacion_val"] != DBNull.Value || !string.IsNullOrEmpty(data["id_observacion_val"].ToString()))
            {
                view.ObservacionId = Convert.ToInt32(data["id_observacion_val"]);
            }
            data["observacion"].ToString();

            if (data["param_hechoenmarcado"] != DBNull.Value || !String.IsNullOrEmpty(data["param_hechoenmarcado"].ToString()))
            {
                view.HechoEnmarcadoId = Convert.ToInt32(data["param_hechoenmarcado"]);
            }

            if (data["decreto_ley"] != DBNull.Value || !String.IsNullOrEmpty(data["decreto_ley"].ToString()))
            {
                view.DecretoLey = Convert.ToString(data["decreto_ley"]);
            }
        }

        private static void ParseDataToView(TBVAL_ANEXO_PERSONA data, ref clsPersonaAnexo view)
        {
            view.Id = data.ID;
            view.PersonaId = (data.ID_REGPERSONA.HasValue) ? data.ID_REGPERSONA.Value : 0;
            view.Victima = data.ESVICITMA.HasValue ? Convert.ToBoolean(data.ESVICITMA.Value) : false;
            view.Afectado = (data.ESAFECTADO.HasValue) ? Convert.ToBoolean(data.ESAFECTADO.Value) : false;
            view.EstadoId = data.ID_ESTADO_VAL;
            view.ValAnexoId = data.ID_VAL_ANEXO.Value;
            view.PersonaId = data.ID_REGPERSONA.Value;
            view.ObservacionId = data.ID_OBSERVACION_VAL;
            view.Observacion = data.OBSERVACION;
        }

        private static void ParseViewToData(ref TBVAL_ANEXO_PERSONA data, clsPersonaAnexo view)
        {
            data.ID = view.Id;
            data.ID_REGPERSONA = view.PersonaId;
            data.ESVICITMA = Convert.ToInt16(view.Victima);
            data.ESAFECTADO = Convert.ToInt16(view.Afectado);
            data.ID_ESTADO_VAL = view.EstadoId;
            data.ID_VAL_ANEXO = view.ValAnexoId;
            data.ID_OBSERVACION_VAL = view.ObservacionId;
            data.OBSERVACION = view.Observacion;
        }


        public static clsPersonaAnexo Actualizar(clsPersonaAnexo persona, DbTransaction tra)
        {
            entValAnexoPersona objPer = new entValAnexoPersona();
            //TBVAL_ANEXO_PERSONA anexopersona = new TBVAL_ANEXO_PERSONA();
            //ParseViewToData(ref anexopersona, persona);
            //objPer.Actualizar(anexopersona);
            bool resp = objPer.Actualizar(persona, tra);
            if (resp)
                return objPer.GetPorId(persona.Id);
            //ParseDataToView(anexopersona, ref persona);
            return null;
        }

        internal static List<clsPersonaAnexo> GetPersonasPorAnexoId(int id_Anexo, DataTable dtPersonas)
        {
            List<clsPersonaAnexo> personas = new List<clsPersonaAnexo>();
            entValoracion objValoracion = new entValoracion();
            DataRow[] dr = dtPersonas.Select(string.Format("id_val_anexo = {0}", id_Anexo));
            foreach (DataRow data in dr)
            {
                clsPersonaAnexo view = new clsPersonaAnexo();
                ParseDataToView(data, ref view);
                personas.Add(view);
            }
            return personas;
        }
    }
}
