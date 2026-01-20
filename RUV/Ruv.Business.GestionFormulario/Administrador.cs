using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO.GestionFormulario;
using u = Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Data.Common;
using Ruv.Data;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;

namespace Ruv.Business.GestionFormulario
{
    public class Administrador : Contratos.IGestionFormulario, Contratos.IGetFormulario
    {
        #region Public methods

        #region Services implementation

        public List<clsFormulario> GenerarFormularios(uint nCantidad,
                                                      string cSerie,
                                                      int nIdUsuario,
                                                      int nIdEstado,
                                                      int? nIdPais,
                                                      int? nIdDepartamento,
                                                      int? nIdMunicipio,
                                                      int? nIdEntidadmunicipio,
                                                      ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            List<dto::clsFormulario> lstFormulario = iFormulario.GenerarFormularios(nCantidad, cSerie, nIdUsuario, nIdEstado, nIdPais, nIdDepartamento, nIdMunicipio, nIdEntidadmunicipio, ref cError);
            List<clsFormulario> lstRespuesta = null;
            if (lstFormulario != null)
            {
                lstRespuesta = lstFormulario.Select(x => new clsFormulario
                {
                    CDepartamento = x.CDepartamento,
                    CEntidad = x.CEntidad,
                    CEstado = x.CEstado,
                    CMunicipio = x.CMunicipio,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = x.CPais,
                    CUsuario = x.CUsuario,
                    NId = x.NId,
                    NIdDepartamento = x.NIdDepartamento,
                    NIdEntidad = x.NIdEntidad,
                    EfId = (eEstadoFormulario)x.NIdEstado,
                    NIdMunicipio = x.NIdMunicipio,
                    NIdPais = x.NIdPais,
                    NIdUsuario = x.NIdUsuario,
                    BDescargado = x.BDescargado
                }).ToList();
            }
            return lstRespuesta;
        }

        /// <summary>
        /// Purpose : Generar Formularios WEB
        /// Author  : John Henao
        /// Date    : 7/6/2013
        /// </summary>
        /// <param name="nCantidad"></param>
        /// <param name="cSerie"></param>
        /// <param name="nIdUsuario"></param>
        /// <param name="nIdEstado"></param>
        /// <param name="nIdEntidadmunicipio"></param>
        /// <param name="cError"></param>
        /// <returns></returns>
        public List<clsFormulario> GenerarFormulariosWEB(uint nCantidad,
                                                      string cSerie,
                                                      int nIdUsuario,
                                                      int nIdEstado,
                                                      int? nIdEntidadmunicipio,
                                                      ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            List<dto::clsFormulario> lstFormulario = iFormulario.GenerarFormulariosWEB(nCantidad, cSerie, nIdUsuario, nIdEstado, nIdEntidadmunicipio, ref cError);
            List<clsFormulario> lstRespuesta = null;
            if (lstFormulario != null)
            {
                lstRespuesta = lstFormulario.Select(x => new clsFormulario
                {
                    CDepartamento = x.CDepartamento,
                    CEntidad = x.CEntidad,
                    CEstado = x.CEstado,
                    CMunicipio = x.CMunicipio,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = x.CPais,
                    CUsuario = x.CUsuario,
                    NId = x.NId,
                    NIdDepartamento = x.NIdDepartamento,
                    NIdEntidad = x.NIdEntidad,
                    EfId = (eEstadoFormulario)x.NIdEstado,
                    NIdMunicipio = x.NIdMunicipio,
                    NIdPais = x.NIdPais,
                    NIdUsuario = x.NIdUsuario,
                    BDescargado = x.BDescargado
                }).ToList();
            }
            return lstRespuesta;
        }

        public List<clsFormulario> ListarFormularios(ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            List<dto::clsFormulario> lstFormulario = iFormulario.ListarFormularios(ref cError);
            List<clsFormulario> lstRespuesta = null;
            if (lstFormulario != null)
            {
                lstRespuesta = lstFormulario.Select(x => new clsFormulario
                {
                    CDepartamento = x.CDepartamento,
                    CEntidad = x.CEntidad,
                    CEstado = x.CEstado,
                    CMunicipio = x.CMunicipio,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = x.CPais,
                    CUsuario = x.CUsuario,
                    NId = x.NId,
                    NIdDepartamento = x.NIdDepartamento,
                    NIdEntidad = x.NIdEntidad,
                    EfId = (eEstadoFormulario)x.NIdEstado,
                    NIdMunicipio = x.NIdMunicipio,
                    NIdPais = x.NIdPais,
                    NIdUsuario = x.NIdUsuario,
                    BDescargado = x.BDescargado
                }).ToList();
            }
            return lstRespuesta;
        }

        /// <summary>
        /// Purpose : Obtiene ID PAIS que Genera Formularios WEB
        /// Author  : John Henao
        /// Date    : 7/6/2013
        /// </summary>
        /// <param name="nIdEntidadmunicipio"></param>
        /// <param name="cError"></param>
        /// <returns></returns>
        public int ObtenerPaisGeneraFormularioWEB(int? nIdEntidadmunicipio, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            return iFormulario.ObtenerPaisGeneraFormularioWEB(nIdEntidadmunicipio, ref cError);
                    
        }

        public List<clsFormulario> ListarFormulariosNoRadicados(dto::clsFormulario frm, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            List<dto::clsFormulario> lstFormulario = iFormulario.ListarFormulariosNoRadicados(frm, ref cError);
            List<clsFormulario> lstRespuesta = null;
            if (lstFormulario != null)
            {
                lstRespuesta = lstFormulario.Select(x => new clsFormulario
                {
                    CDepartamento = x.CDepartamento,
                    CEntidad = x.CEntidad,
                    CEstado = x.CEstado,
                    CMunicipio = x.CMunicipio,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = x.CPais,
                    CUsuario = x.CUsuario,
                    NId = x.NId,
                    NIdDepartamento = x.NIdDepartamento,
                    NIdEntidad = x.NIdEntidad,
                    EfId = (eEstadoFormulario)x.NIdEstado,
                    NIdMunicipio = x.NIdMunicipio,
                    NIdPais = x.NIdPais,
                    NIdUsuario = x.NIdUsuario,
                    BDescargado = x.BDescargado
                }).ToList();
            }
            return lstRespuesta;
        }

        public List<clsFormulario> ListarFormulariosPorEstado(Infrastructure.Crosscutting.Common.eEstadoFormulario efEstado, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            List<dto::clsFormulario> lstFormulario = iFormulario.ListarFormulariosPorEstado((ushort)efEstado, ref cError);
            List<clsFormulario> lstRespuesta = null;
            if (lstFormulario != null)
            {
                lstRespuesta = lstFormulario.Select(x => new clsFormulario
                {
                    CDepartamento = x.CDepartamento,
                    CEntidad = x.CEntidad,
                    CEstado = x.CEstado,
                    CMunicipio = x.CMunicipio,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = x.CPais,
                    CUsuario = x.CUsuario,
                    NId = x.NId,
                    NIdDepartamento = x.NIdDepartamento,
                    NIdEntidad = x.NIdEntidad,
                    EfId = (eEstadoFormulario)x.NIdEstado,
                    NIdMunicipio = x.NIdMunicipio,
                    NIdPais = x.NIdPais,
                    NIdUsuario = x.NIdUsuario,
                    BDescargado = x.BDescargado
                }).ToList();
            }
            return lstRespuesta;
        }

        public uint? AsignarFormulario(dto::clsFormulario frm, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            return iFormulario.AsignarFormulario(frm, ref cError);
        }

        public bool AsignarFormulario(dto::clsSolicitudFormularioEstado frm, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioData);
            using (DbTransaction tra = Dao.InitTransaction())
            {
                if (iFormulario.AsignarFormulario(frm, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        public uint? InactivarFormulario(uint nIdFormulario, string observacion, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            return iFormulario.InactivarFormulario(nIdFormulario, observacion, ref cError);
        }

        public uint? SepararFormularioImprenta(dto::clsFormulario frm, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            return iFormulario.SepararFormularioImprenta(frm, ref cError);
        }

        public List<dto::clsFormulario> SepararFormularioImprenta(dto::clsSolicitudFormularioEstado frm, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioData);
            using(DbTransaction tra = Dao.InitTransaction())
            {
                List<dto::clsFormulario> lstSeparados = iFormulario.SepararFormularioImprenta(frm, tra, ref cError);
                if (lstSeparados == null || !string.IsNullOrEmpty(cError))
                {
                    tra.Rollback();
                    return null;
                }
                tra.Commit();
                return lstSeparados;
            }
        }

        public clsFormulario ObtenerFormulario(string cNumeroFormulario, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGetFormulario iFormulario = (Data.GestionFormulario.Contratos.IGetFormulario)new Data.GestionFormulario.Administrador();
            dto::clsFormulario frm = iFormulario.ObtenerFormulario(cNumeroFormulario, ref cError);
            if (!string.IsNullOrEmpty(cError) || frm == null) return null;
            return new clsFormulario
            {
                CDepartamento = frm.CDepartamento,
                CEntidad = frm.CEntidad,
                CEstado = frm.CEstado,
                CMunicipio = frm.CMunicipio,
                CNumeroFormulario = frm.CNumeroFormulario,
                CPais = frm.CPais,
                CUsuario = frm.CUsuario,
                NId = frm.NId,
                NIdDepartamento = frm.NIdDepartamento,
                NIdEntidad = frm.NIdEntidad,
                EfId = (eEstadoFormulario)frm.NIdEstado,
                NIdMunicipio = frm.NIdMunicipio,
                NIdPais = frm.NIdPais,
                NIdUsuario = frm.NIdUsuario,
                BDescargado = frm.BDescargado
            };
        }
        
        public List<clsFormulario> ObtenerFormulariosPorUsuario(int nIdUsuario, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            List<dto::clsFormulario> lstFormulario = iFormulario.ObtenerFormulariosPorUsuario(nIdUsuario, ref cError);
            List<clsFormulario> lstRespuesta = null;
            if (lstFormulario != null)
            {
                lstRespuesta = lstFormulario.Select(x => new clsFormulario
                {
                    CDepartamento = x.CDepartamento,
                    CEntidad = x.CEntidad,
                    CEstado = x.CEstado,
                    CMunicipio = x.CMunicipio,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = x.CPais,
                    CUsuario = x.CUsuario,
                    NId = x.NId,
                    NIdDepartamento = x.NIdDepartamento,
                    NIdEntidad = x.NIdEntidad,
                    EfId = (eEstadoFormulario)x.NIdEstado,
                    NIdMunicipio = x.NIdMunicipio,
                    NIdPais = x.NIdPais,
                    NIdUsuario = x.NIdUsuario,
                    BDescargado = x.BDescargado
                }).ToList();
            }
            return lstRespuesta;
        }

        public uint? MarcarDescargado(uint nIdFormulario, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            return iFormulario.MarcarDescargado(nIdFormulario, ref cError);
        }

        public bool MarcarRadicado(string cNumeroFormulario, ref string cError)
        {
            using (DbTransaction tra = Dao.InitTransaction())
            {
                Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
                iFormulario.MarcarRadicado(cNumeroFormulario, tra, ref cError);
                if (string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        public List<clsFormulario> ObtenerFormulariosPorUsuarioEstadoPaginado(dto::clsSolicitudFormularioEstado frm, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            List<dto::clsFormulario> lstFormulario = iFormulario.ObtenerFormulariosPorUsuarioEstadoPaginado(frm, ref cError);
            List<clsFormulario> lstRespuesta = null;
            if (lstFormulario != null)
            {
                lstRespuesta = lstFormulario.Select(x => new clsFormulario
                {
                    CDepartamento = x.CDepartamento,
                    CEntidad = x.CEntidad,
                    CEstado = x.CEstado,
                    CMunicipio = x.CMunicipio,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = x.CPais,
                    CUsuario = x.CUsuario,
                    NId = x.NId,
                    NIdDepartamento = x.NIdDepartamento,
                    NIdEntidad = x.NIdEntidad,
                    EfId = (eEstadoFormulario)x.NIdEstado,
                    NIdMunicipio = x.NIdMunicipio,
                    NIdPais = x.NIdPais,
                    NIdUsuario = x.NIdUsuario,
                    BDescargado = x.BDescargado,
                    DGenerado = x.DGenerado
                }).ToList();
            }
            return lstRespuesta;
        }

        public int ObtenerCantidadFormulariosPorUsuarioEstado(dto::clsSolicitudFormularioEstado frm, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            return iFormulario.ObtenerCantidadFormulariosPorUsuarioEstado(frm, ref cError);
        }

        public int ObtenerCantidadFormulariosActivar(dto.clsFormulario clsFormulario, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            return iFormulario.ObtenerCantidadFormulariosActivar(clsFormulario, ref cError);
        }

        public List<clsFormulario> ObtenerFormulariosActivar(dto.clsFormulario clsFormulario, int nPagina, int nTamaño, ref string cError)
        {
            Data.GestionFormulario.Contratos.IGestionFormulario iFormulario = (Data.GestionFormulario.Contratos.IGestionFormulario)new Data.GestionFormulario.Administrador();
            List<dto::clsFormulario> lstFormulario = iFormulario.ObtenerFormulariosActivar(clsFormulario, nPagina, nTamaño, ref cError);
            List<clsFormulario> lstRespuesta = null;
            if (lstFormulario != null)
            {
                lstRespuesta = lstFormulario.Select(x => new clsFormulario
                {
                    CDepartamento = x.CDepartamento,
                    CEntidad = x.CEntidad,
                    CEstado = x.CEstado,
                    CMunicipio = x.CMunicipio,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = x.CPais,
                    CUsuario = x.CUsuario,
                    NId = x.NId,
                    NIdDepartamento = x.NIdDepartamento,
                    NIdEntidad = x.NIdEntidad,
                    EfId = (eEstadoFormulario)x.NIdEstado,
                    NIdMunicipio = x.NIdMunicipio,
                    NIdPais = x.NIdPais,
                    NIdUsuario = x.NIdUsuario,
                    BDescargado = x.BDescargado,
                    CObservacion = x.CObservacion,
                    DUltimaModificacion = x.DUltimaModificacion
                }).ToList();
            }
            return lstRespuesta;
        }
        
        #endregion

        #endregion
    }
}
