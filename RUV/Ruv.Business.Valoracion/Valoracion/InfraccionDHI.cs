using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data.Common;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class InfraccionDHI
    {
        public static void Insertar(int InfraccionId, int ValAnexoId, DbTransaction tra)
        {
            entInfraccionDIH objInfraccion = new entInfraccionDIH();
            //TBINFRACCION_DIH_VALANEXOPER InfraccionAnexo = new TBINFRACCION_DIH_VALANEXOPER();
            //ParseViewToData(InfraccionId, ValAnexoId, ref InfraccionAnexo);
            objInfraccion.Insertar(InfraccionId, ValAnexoId, tra);
        }
        /*
        private static void ParseViewToData(int InfraccionId, int ValAnexoId, ref TBINFRACCION_DIH_VALANEXOPER data)
        {
            data.ID_INFRACCIONDIH = InfraccionId;
            data.ID_VAL_ANEXO_PER = ValAnexoId;
        }*/

        public static void Eliminar(int ValAnexoId, DbTransaction tra)
        {
            entInfraccionDIH objInfraccion = new entInfraccionDIH();
            objInfraccion.Eliminar(ValAnexoId, tra);
        }

        public static List<clsInfracciones> GetInfracciones()
        {
            entInfraccionDIH objInfraccion = new entInfraccionDIH();
            List<clsInfracciones> infracciones = new List<clsInfracciones>();
            List<TBINFRACCION_DIH> listdata = objInfraccion.GetInfracciones();
            foreach (TBINFRACCION_DIH data in listdata)
            {
                clsInfracciones view = new clsInfracciones();
                ParseDataToView(data, ref view);
                infracciones.Add(view);
            }
            if (infracciones.Count <= 0)
            {
                RegistroTraza.I.Registrar("InfraccionDHI" + ":::GetInfracciones:::autores.Count=0");
            }
            return infracciones;
        }

        public static void ParseDataToView(TBINFRACCION_DIH data, ref clsInfracciones view)
        {
            view.Id = data.ID;
            view.Nombre = data.NOMBRE;
        }

        internal static List<clsInfracciones> GetInfraccionesPorValAnexoPerId(int ValAnexoPerId)
        {
            entInfraccionDIH objInfraccion = new entInfraccionDIH();
            List<clsInfracciones> infracciones = new List<clsInfracciones>();
            List<TBINFRACCION_DIH> listdata = objInfraccion.GetInfraccionesPorValAnexoPerId(ValAnexoPerId);
            foreach (TBINFRACCION_DIH data in listdata)
            {
                clsInfracciones infraccion = new clsInfracciones();
                infraccion.Id = data.ID;
                infraccion.Nombre = data.NOMBRE;
                infracciones.Add(infraccion);
            }
            if (infracciones.Count <= 0)
            {
                RegistroTraza.I.Registrar("InfraccionDHI" + ":::GetInfraccionesPorValAnexoPerId:::autores.Count=0");
            }
            return infracciones;
        }

        internal static List<clsInfracciones> GetInfracciones(int valAnexoPerId)
        {
            entInfraccionDIH objInfraccion = new entInfraccionDIH();
            List<clsInfracciones> infracciones = new List<clsInfracciones>();
            List<TBINFRACCION_DIH> listdata = objInfraccion.GetInfracciones(valAnexoPerId);
            foreach (TBINFRACCION_DIH data in listdata)
            {
                clsInfracciones view = new clsInfracciones();
                ParseDataToView(data, ref view);
                infracciones.Add(view);
            }
            if (infracciones.Count <= 0)
            {
                RegistroTraza.I.Registrar("InfraccionDHI" + ":::GetInfracciones(int valAnexoPerId):::autores.Count=0");
            }
            return infracciones;
        }
    }
}
