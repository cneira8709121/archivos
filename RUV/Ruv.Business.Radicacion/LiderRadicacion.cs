using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO;
using Ruv.Data.Radicacion.Contratos;
using Ruv.Data.GestionFormulario.Contratos;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using System.Data.Common;

namespace Ruv.Business.Radicacion
{
    public class LiderRadicacion : Contratos.ILiderRadicacion
    {
        #region Public methods

        #region Services implementation

        public dto::Radicacion.clsLiderRadicacion CargarDatos(long nIdDeclaracion, ref string cError)
        {
            dto::Radicacion.clsRadicacion radActual = GetRadicacion(nIdDeclaracion, ref cError);
            if (!string.IsNullOrEmpty(cError)) return null;

            dto::Radicacion.clsRadicacion radOtra = null;
            dto::GestionFormulario.clsFormulario frm = null;
            if (!(radActual == null || radActual.CNumeroFormulario == null))
            {
                if (radActual.NTipoError.Value == eResultadoValidacionRadicacion.NumeroFormularioRadicado.GetHashCode())
                {
                    radOtra = GetRadicacion(nIdDeclaracion, radActual.CNumeroFormulario, ref cError);
                    if (!string.IsNullOrEmpty(cError)) return null;
                }
                else
                {
                    if (radActual.NTipoError.Value == eResultadoValidacionRadicacion.ProcedenciaErronea.GetHashCode())
                    {
                        frm = GetFormulario(radActual.CNumeroFormulario, ref cError);
                        if (!string.IsNullOrEmpty(cError)) return null;
                        radOtra = new dto::Radicacion.clsRadicacion();
                        radOtra.NIdEntidad = frm.NIdEntidad;
                    }
                }
            }

            return new dto::Radicacion.clsLiderRadicacion
            {
                RadActual = radActual,
                RadExistente = radOtra,
                FrmRadicacionPrevia = frm
            };
        }

        public bool ActualizarRadicacion(dto::Radicacion.clsRadicacion rad, string cObservaciones, ref string cError)
        {
            ILiderRadicacion iLiderRadicacion = (ILiderRadicacion)Spring.GetService(Objetos.LiderRadicacionData);
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                if (iLiderRadicacion.UpdateRadicacion(rad, cObservaciones, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        #endregion

        #endregion
        #region Private methods

        private dto::Radicacion.clsRadicacion GetRadicacion(long nIdDeclaracion, ref string cError)
        {
            ILiderRadicacion iLiderRadicacion = (ILiderRadicacion)Spring.GetService(Objetos.LiderRadicacionData);
            return iLiderRadicacion.GetRadicacion(nIdDeclaracion, ref cError);
        }

        private dto::Radicacion.clsRadicacion GetRadicacion(long nIdDeclaracion, string cNumeroFormulario, ref string cError)
        {
            ILiderRadicacion iLiderRadicacion = (ILiderRadicacion)Spring.GetService(Objetos.LiderRadicacionData);
            return iLiderRadicacion.GetRadicacion(nIdDeclaracion, cNumeroFormulario, ref cError);
        }

        private dto::GestionFormulario.clsFormulario GetFormulario(string cNumeroFormulario, ref string cError)
        {
            IGetFormulario iFormulario = (IGetFormulario)Spring.GetService(Objetos.GestionFormularioData);
            return iFormulario.ObtenerFormulario(cNumeroFormulario, ref cError);
        }

        #endregion
    }
}
