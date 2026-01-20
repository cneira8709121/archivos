using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using dal = Ruv.Data.Valoracion.Valoracion;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using not = Ruv.Data.Notificacion.Contratos;
using Ruv.Business.DTO.Valoracion;

namespace Ruv.Business.Valoracion
{
    public class clsLiderValoracion : Contratos.ILiderValoracion
    {
        public bool AprobarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                dal::Contratos.ILiderValoracion iAprobarValoracion = (dal::Contratos.ILiderValoracion)Spring.GetService(Objetos.LiderValoracionData);
                if (iAprobarValoracion.AprobarValoracion(nIdUsuario, nIdDeclaracion, cObservacion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    not::INotificacionData iInsertaNotificacion = (not::INotificacionData)Spring.GetService(Objetos.NotificacionData);
                    if (iInsertaNotificacion.InsertaNotificacion(nIdDeclaracion, tra, ref cError) && string.IsNullOrEmpty(cError))
                    {
                        tra.Commit();
                        return true;
                    }
                }
                tra.Rollback();
                return false;
            }
        }

        public bool RechazarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                dal::Contratos.ILiderValoracion iAprobarValoracion = (dal::Contratos.ILiderValoracion)Spring.GetService(Objetos.LiderValoracionData);
                if (iAprobarValoracion.RechazarValoracion(nIdUsuario, nIdDeclaracion, cObservacion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        public List<clsValoracionHistorico> consultarValoracionHistorico(int nIdValoracion, ref string cError) 
        {
            dal::Contratos.ILiderValoracion iValoracionHistorico = (dal::Contratos.ILiderValoracion)Spring.GetService(Objetos.LiderValoracionData);
            return iValoracionHistorico.consultarValoracionHistorico(nIdValoracion, ref cError);
        }

        public string consultarMotivacionValoracionHistorico(int nIdValoracion, ref string cError) 
        {
            dal::Contratos.ILiderValoracion iValoracionHistorico = (dal::Contratos.ILiderValoracion)Spring.GetService(Objetos.LiderValoracionData);
            return iValoracionHistorico.consultarMotivacionValoracionHistorico(nIdValoracion, ref cError);
        }
    }
}
