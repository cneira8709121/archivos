using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using dal = Ruv.Data.Devolucion;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using Ruv.Business.DTO.Devolucion;

namespace Ruv.Business.Devolucion
{
    public class Administrador : Contratos.IDevolucion
    {
        public clsDevolucion ObtenerDevolucion(Int32 idDeclaracion, ref string cError)
        {
            Data.Devolucion.Contratos.IDevolucion iDevolucion = (Data.Devolucion.Contratos.IDevolucion)new Data.Devolucion.Administrador();
            return iDevolucion.ObtenerDevolucion(idDeclaracion, ref cError);
        }

        public Boolean ActualizarDevolucion(clsDevolucion devolucion, ref string cError)
        {
            dal::Contratos.IDevolucion iDevolucion = (dal::Contratos.IDevolucion)Spring.GetService(Objetos.DevolucionData);
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                if (iDevolucion.ActualizarDevolucion(devolucion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                else
                {
                    tra.Rollback();
                    return false;
                }
            }
        }

        public Boolean SolicitarDevolucion(clsDevolucion devolucion, ref string cError)
        {
            dal::Contratos.IDevolucion iDevolucion = (dal::Contratos.IDevolucion)Spring.GetService(Objetos.DevolucionData);
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                if (iDevolucion.SolicitarDevolucion(devolucion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                else
                {
                    tra.Rollback();
                    return false;
                }
            }
        }

        public clsDatosparaDevolucion CargaDatosparaDevolucion(int NIdDevolucion, ref string cError)
        {
            Data.Devolucion.Contratos.IDevolucion iDevolucion = (Data.Devolucion.Contratos.IDevolucion)Spring.GetService(Objetos.DevolucionData);
            return iDevolucion.CargaDatosparaDevolucion(NIdDevolucion, ref cError);
        }


        public List<Infrastructure.Crosscutting.Common.General.clsCausal> ObtenerCausalesDevolucion(ref string cError)
        {
            Data.Devolucion.Contratos.IDevolucion iDevolucion = (Data.Devolucion.Contratos.IDevolucion)Spring.GetService(Objetos.DevolucionData);
            List<clsCausalDevolucion> causales = iDevolucion.ObtenerCausalesDevolucion(ref cError);
            return ParseDataToView(causales);
        }

        private List<Infrastructure.Crosscutting.Common.General.clsCausal> ParseDataToView(List<clsCausalDevolucion> causales)
        {
            List<Infrastructure.Crosscutting.Common.General.clsCausal> result = new List<Infrastructure.Crosscutting.Common.General.clsCausal>();
            foreach (clsCausalDevolucion causal in causales)
            {
                Infrastructure.Crosscutting.Common.General.clsCausal nCausal = new Infrastructure.Crosscutting.Common.General.clsCausal();
                nCausal.NId = causal.nId;
                nCausal.CNombre = causal.cNombre;
                nCausal.CParteEmotiva = causal.cParteEmotiva;
                nCausal.EParametroTipoCausal = (Infrastructure.Crosscutting.Common.eTipoParametros)causal.nTipo;
                result.Add(nCausal);
            }
            return result;
        }

    }
}
