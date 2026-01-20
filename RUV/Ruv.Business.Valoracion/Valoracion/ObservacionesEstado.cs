using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class ObservacionesEstado
    {
        public static List<clsObservacionEstado> GetObsedervacionestadoPorestadoId(int estadoId)
        {

            entValoracion objValoracion = new entValoracion();
            List<clsObservacionEstado> vieobservaciones = new List<clsObservacionEstado>();
            List<TBOBSERVACION_VAL> observacion = objValoracion.GetObservacionesEstadoPorEstadoId(estadoId);
            foreach (TBOBSERVACION_VAL datos in observacion)
            {
                clsObservacionEstado view = new clsObservacionEstado();
                ParseDataToView(datos, ref view);
                vieobservaciones.Add(view);
            }
            return vieobservaciones;
        }

        private static void ParseDataToView(TBOBSERVACION_VAL datos, ref clsObservacionEstado view)
        {
            view.Id = datos.ID;
            view.Nombre = datos.NOMBRE;
            view.EstadoId = (datos.ID_ESTADO_VAL.HasValue)?datos.ID_ESTADO_VAL.Value : 0;
        }

        internal static List<clsObservacionEstado> GetObsevacionEstado()
        {
            entValoracion objValoracion = new entValoracion();
            List<clsObservacionEstado> vieobservaciones = new List<clsObservacionEstado>();
            List<TBOBSERVACION_VAL> observacion = objValoracion.GetObservacionesEstado();
            foreach (TBOBSERVACION_VAL datos in observacion)
            {
                clsObservacionEstado view = new clsObservacionEstado();
                ParseDataToView(datos, ref view);
                

                vieobservaciones.Add(view);
            }
            return vieobservaciones;
        }
    }
}
