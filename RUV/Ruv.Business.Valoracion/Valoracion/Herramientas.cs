using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;
using System.Data.Common;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Herramientas
    {
        
        public static void Insertar(clsHerramientaAnexoPer her, DbTransaction tra)
        {
            entHerramientas objHerramienta = new entHerramientas();
            TBHERRAMIENTA_ANEXO_PER data = new TBHERRAMIENTA_ANEXO_PER();
            ParseViewToDataAnexoPer(ref data, her);
            objHerramienta.Insertar(data, tra);
        }

        public static bool EliminarPorAnexoId(int AnexoId, DbTransaction tra)
        {
            entHerramientas objHerramientas = new entHerramientas();
            return objHerramientas.Eliminar(AnexoId, tra);
        }


        public static List<clsTipoHerramienta> GetTiposHeramienta()
        {
            entHerramientas objHerramientas = new entHerramientas();
            List<TBTIPO_HERRAMIENTAVAL> her = objHerramientas.GetTiposHerramientas();
            List<clsTipoHerramienta> herrami = new List<clsTipoHerramienta>();
            foreach (TBTIPO_HERRAMIENTAVAL item in her)
            {
                clsTipoHerramienta hrview = new clsTipoHerramienta();
                ParseDataToViewTipo(item, ref hrview);
                herrami.Add(hrview);
            }
            return herrami;
        }

        public static clsTipoHerramienta GetTipoHeramientaPorId(int TipodId)
        {
            entHerramientas objHerramientas = new entHerramientas();
            TBTIPO_HERRAMIENTAVAL her = objHerramientas.GetTiposHerramientasPorId(TipodId);
            clsTipoHerramienta herrami = new clsTipoHerramienta();
            ParseDataToViewTipo(her, ref herrami);
            return herrami;
        }

        public static List<clsHerramientas> GetHeramientasPorTipo(int TipoId)
        {
            entHerramientas objHerramientas = new entHerramientas();
            List<TBHERRAMIENTAVAL> her = objHerramientas.GetHerramientaPorTipoId(TipoId);
            List<clsHerramientas> herrami = new List<clsHerramientas>();
            foreach (TBHERRAMIENTAVAL item in her)
            {
                clsHerramientas hrview = new clsHerramientas();
                ParseDataToView(item, ref hrview);
                herrami.Add(hrview);
            }
            return herrami;
        }

        private static void ParseDataToViewTipo(TBTIPO_HERRAMIENTAVAL item, ref clsTipoHerramienta hrview)
        {
            hrview.Id = item.ID;
            hrview.Nombre = item.NOMBRE;
        }

        private static void ParseDataToView(TBHERRAMIENTAVAL item, ref clsHerramientas her)
        {
            her.Id = item.ID;
            her.Nombre = item.NOMBRE;
            her.TipoId = item.ID_TIPO_HERRAMIENTA.Value;
        }

        private static void ParseDataToViewAnexoPer(TBHERRAMIENTA_ANEXO_PER item, ref clsHerramientaAnexoPer her)
        {
            her.AnexoPerId = item.ID_VALANEXO_PER;
            her.HerramientaId = item.ID_HERRAMIENTA;
            her.Descripcion = item.DETALLE;
            her.Fecha = (item.FECHA.HasValue) ? item.FECHA.Value: DateTime.Now;

            if (item.TBHERRAMIENTAVAL != null)
            {
                her.Herramienta = new clsHerramientas();
                her.Herramienta.Id = item.TBHERRAMIENTAVAL.ID;
                her.Herramienta.TipoId = item.TBHERRAMIENTAVAL.ID_TIPO_HERRAMIENTA.Value;
                her.Herramienta.Nombre = item.TBHERRAMIENTAVAL.NOMBRE;

                if (item.TBHERRAMIENTAVAL.TBTIPO_HERRAMIENTAVAL != null)
                {
                    her.Herramienta.Tipo = new clsTipoHerramienta();
                    her.Herramienta.Tipo.Id = item.TBHERRAMIENTAVAL.TBTIPO_HERRAMIENTAVAL.ID;
                    her.Herramienta.Tipo.Nombre = item.TBHERRAMIENTAVAL.TBTIPO_HERRAMIENTAVAL.NOMBRE;
                }
            }
        }
        
        
        private static void ParseViewToDataAnexoPer(ref TBHERRAMIENTA_ANEXO_PER data, clsHerramientaAnexoPer her)
        {
            data.ID_VALANEXO_PER = her.AnexoPerId;
            data.ID_HERRAMIENTA = her.HerramientaId;
            data.USAPARADESICION = Convert.ToInt16(her.UsadoParaDesicion);
            data.DETALLE = her.Descripcion;
            data.FECHA = her.Fecha;
            if (her.Herramienta.Id == 0)
            {
                data.TBHERRAMIENTAVAL = new TBHERRAMIENTAVAL();
                data.TBHERRAMIENTAVAL.ID = her.Herramienta.Id;
                data.TBHERRAMIENTAVAL.ID_TIPO_HERRAMIENTA = her.Herramienta.TipoId;
                data.TBHERRAMIENTAVAL.NOMBRE = her.Herramienta.Nombre;
            }
        }

        public static List<clsHerramientaAnexoPer> GetHerramientasPorAnexoId(int anexoId)
        {
            entHerramientas ObjHerramientas = new entHerramientas();
            List<clsHerramientaAnexoPer> herramientas = new List<clsHerramientaAnexoPer>();
            List<TBHERRAMIENTA_ANEXO_PER> datosherramientas = ObjHerramientas.GetHerramientaPorAnexoVal(anexoId);
            foreach (TBHERRAMIENTA_ANEXO_PER item in datosherramientas)
            {
                clsHerramientaAnexoPer her = new clsHerramientaAnexoPer();
                ParseDataToViewAnexoPer(item, ref her);
                herramientas.Add(her);
            }
            return herramientas;
        }

        internal static List<clsHerramientas> GetHeramientas()
        {
            entHerramientas objHerramientas = new entHerramientas();
            List<TBHERRAMIENTAVAL> her = objHerramientas.GetHerramientas();
            List<clsHerramientas> herrami = new List<clsHerramientas>();
            foreach (TBHERRAMIENTAVAL item in her)
            {
                clsHerramientas hrview = new clsHerramientas();
                ParseDataToView(item, ref hrview);
                herrami.Add(hrview);
            }
            return herrami;
        }
    }
}
