using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Ruv.Data;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Principios
    {
        public static List<clsPrincipioEstado> GetPrincipiosPorEstadoId(int estadoId)
        {
            List<clsPrincipioEstado> viewprincipios = new List<clsPrincipioEstado>();
            entPrincipio objPrincipio = new entPrincipio();
            List<TBPRINCIPIO> principios = objPrincipio.GetPrincipiosPorEstadoId(estadoId);
            foreach (TBPRINCIPIO data in principios)
            {
                clsPrincipioEstado view = new clsPrincipioEstado();
                ParseDataToView(data, ref view);
                viewprincipios.Add(view);
            }
            return viewprincipios;
        }

        private static void ParseDataToView(TBPRINCIPIO data, ref clsPrincipioEstado view)
        {
            view.Id = data.ID;
            view.Nombre = data.NOMBRE;
            view.EstadoId = (data.ID_ESTADO_VAL.HasValue) ? data.ID_ESTADO_VAL.Value : 0;
        }


        public static void Insertar(int principioId, int valAnexoPerId, DbTransaction tra)
        {
            entPrincipio objPrincipio = new entPrincipio();
            objPrincipio.Insertar(principioId, valAnexoPerId, tra);
        }

        public static void InsertarCausal(int causalId, int valId)
        {
            entPrincipio objPrincipio = new entPrincipio();
            objPrincipio.InsertarCausal(causalId, valId);
        }

        public static void Eliminar(int valAnexoPerId, DbTransaction tra)
        {
            entPrincipio objPrincipio = new entPrincipio();
            objPrincipio.Eliminar(valAnexoPerId, tra);
        }

        public static List<int> GetPrincipiosPorValAnexoPerId(int valAnexoPerId)
        {
            List<int> viewprincipios = new List<int>();
            entPrincipio objPrincipio = new entPrincipio();
            List<TBPRINCIPIO> principios = objPrincipio.GetPrincipiosPorValAnexoPerId(valAnexoPerId);
            foreach (TBPRINCIPIO data in principios)
            {
                viewprincipios.Add(data.ID);
            }

            return viewprincipios;
        }

        public static List<int> GetPrincipiosPorValoracion(int valId)
        {
            List<int> viewprincipios = new List<int>();
            entPrincipio objPrincipio = new entPrincipio();
            List<TBPRINCIPIO> principios = objPrincipio.GetPrincipiosPorValoracion(valId);
            foreach (TBPRINCIPIO data in principios)
            {
                viewprincipios.Add(data.ID);
            }

            return viewprincipios;
        }

        internal static List<clsPrincipioEstado> GetPrincipios()
        {
            List<clsPrincipioEstado> viewprincipios = new List<clsPrincipioEstado>();
            entPrincipio objPrincipio = new entPrincipio();
            List<TBPRINCIPIO> principios = objPrincipio.GetPrincipios();
            foreach (TBPRINCIPIO data in principios)
            {
                clsPrincipioEstado view = new clsPrincipioEstado();
                ParseDataToView(data, ref view);
                viewprincipios.Add(view);
            }
            return viewprincipios;
        }

        internal static List<int> GetPrincipiosPorValoracion(DataTable dtPrincipios)
        {
            List<int> viewprincipios = new List<int>();
            foreach (DataRow data in dtPrincipios.Rows)
            {
                viewprincipios.Add(Convert.ToInt32(data["Id"]));
            }
            return viewprincipios;
        }
    }
}
