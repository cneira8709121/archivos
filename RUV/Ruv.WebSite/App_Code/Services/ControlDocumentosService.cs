using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Business.GestionFormulario;
using Ruv.Business.GestionFormulario.Contratos;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using dto = Ruv.Business.DTO.GestionFormulario;
using u = Ruv.Infrastructure.Crosscutting.Utilities;
using System.ServiceModel.Activation;
using Ruv.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

[AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Required)]
public class ControlDocumentosService : IControlDocumentosService
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
        IGestionFormulario iFormulario = (IGestionFormulario)new Ruv.Business.GestionFormulario.Administrador();
        List<clsFormulario> list  = iFormulario.GenerarFormularios(nCantidad, cSerie, nIdUsuario, nIdEstado, nIdPais, nIdDepartamento, nIdMunicipio, nIdEntidadmunicipio, ref cError);
        //return list
        //TODO; 
        // TODO: modify logic to return apropriate values
        
        // Diego Alvarez - 29/11/2013 - Validar que no sea cero la cantidad solicitada
        if (list != null && list.Count < 10)
            return list;

        return new List<clsFormulario>();
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
        IGestionFormulario iFormulario = (IGestionFormulario)new Ruv.Business.GestionFormulario.Administrador();
        List<clsFormulario> list = iFormulario.GenerarFormulariosWEB(nCantidad, cSerie, nIdUsuario, nIdEstado, nIdEntidadmunicipio, ref cError);

        // Diego Alvarez - 29/11/2013 - Validar que no sea cero la cantidad solicitada
        if (list != null && list.Count < 10)
            return list;

        return new List<clsFormulario>();
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
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ObtenerPaisGeneraFormularioWEB(nIdEntidadmunicipio, ref cError); 
    }

    public List<clsFormulario> ListarFormularios(ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ListarFormularios(ref cError);
    }

    public List<clsFormulario> ListarFormulariosNoRadicados(clsFormularioSolicitudNoRadicados frmSolicitud, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ListarFormulariosNoRadicados(new dto::clsFormulario
        {
            CNumeroFormulario = frmSolicitud.CNumeroFormulario,
            NIdDepartamento = frmSolicitud.NIdDepartamento,
            NIdEntidad = frmSolicitud.NIdEntidad,
            NIdMunicipio = frmSolicitud.NIdMunicipio,
            NIdPais = frmSolicitud.NIdPais,
        }, ref cError);
    }

    public List<clsFormulario> ListarFormulariosPorEstado(ushort nIdEstado, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        eEstadoFormulario efEstado;
        try
        {
            efEstado = (eEstadoFormulario)nIdEstado;
        }
        catch (Exception ex)
        {
            RegistroTraza.I.Registrar(ex);
            cError = string.Format(Errores.General ,ex.Message);
            return null;
        }
        return iFormulario.ListarFormulariosPorEstado(efEstado, ref cError);
    }

    public void AsignarFormulario(List<clsFormulario> FormulariosEnviar, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        foreach (var x in FormulariosEnviar)
        {
            iFormulario.AsignarFormulario(new dto::clsFormulario
            {
                CNumeroFormulario = x.CNumeroFormulario,
                NId = x.NId,
                NIdDepartamento = x.NIdDepartamento,
                NIdEstado = (ushort)x.EfId,
                NIdEntidad = x.NIdEntidad,
                NIdMunicipio = x.NIdMunicipio,
                NIdPais = x.NIdPais,
                NIdUsuario = x.NIdUsuario
            }, ref cError);
        }
    }

    public bool AsignarFormularioFiltro(clsSolicitudFormularioEstado frm, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        if (frm == null)
        {
            cError = Advertencia.SolicitudVacia;
            return false;
        }

        return iFormulario.AsignarFormulario(new dto::clsSolicitudFormularioEstado { CNumeroFormulario = frm.CNumeroFormulario, NDesde = frm.NDesde, NHasta = frm.NHasta, DGenerado = frm.DGenerado, NIdUsuario = frm.NIdUsuario, NIdPais = frm.NIdPais, NIdDepartamento = frm.NIdDepartamento, NIdMunicipio = frm.NIdMunicipio, NIdEntidad = frm.NIdEntidad }, ref cError);
    }

    public uint? InactivarFormulario(uint nIdFormulario, string observacion, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.InactivarFormulario(nIdFormulario, observacion, ref cError);
    }

    public void SepararFormularioImprenta(IEnumerable<clsSeparacionFormularioSolicitud> frmFormularioASeparar, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        foreach (clsSeparacionFormularioSolicitud formulario in frmFormularioASeparar)
        {
            iFormulario.SepararFormularioImprenta(new dto::clsFormulario
            {
                CNumeroFormulario = formulario.CNumeroFormulario,
                NIdUsuario = formulario.NIdUsuario
            }, ref cError);
        }
    }

    public List<clsSeparacionFormularioSolicitud> SepararFormularioImprentaFiltro(clsSolicitudFormularioEstado frm, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        if (frm == null)
        {
            cError = Advertencia.SolicitudVacia;
            return null;
        }

        List<dto::clsFormulario> lstSeparados = iFormulario.SepararFormularioImprenta(new dto::clsSolicitudFormularioEstado { CNumeroFormulario = frm.CNumeroFormulario, NDesde = frm.NDesde, NHasta = frm.NHasta, DGenerado = frm.DGenerado, NIdUsuario = frm.NIdUsuario }, ref cError);
        if (lstSeparados == null || !string.IsNullOrEmpty(cError)) return null;
        return lstSeparados.Select(x => new clsSeparacionFormularioSolicitud { CNumeroFormulario = x.CNumeroFormulario }).ToList();
    }

    public List<clsFormulario> ObtenerFormulariosPorUsuario(int nIdUsuario, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ObtenerFormulariosPorUsuario(nIdUsuario, ref cError);
    }

    public uint? MarcarDescargado(uint nIdFormulario, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.MarcarDescargado(nIdFormulario, ref cError);
    }

    public bool MarcarRadicado(string cNumeroFormulario, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.MarcarRadicado(cNumeroFormulario, ref cError);
    }

    public List<clsFormulario> ObtenerFormulariosPorUsuarioEstadoPaginado(clsSolicitudFormularioEstado frm, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ObtenerFormulariosPorUsuarioEstadoPaginado(new dto::clsSolicitudFormularioEstado { CNumeroFormulario = frm.CNumeroFormulario, NDesde = frm.NDesde, NHasta = frm.NHasta, DGenerado = frm.DGenerado, IdEstado = frm.IdEstado, NIdUsuario = frm.NIdUsuario, NPagina = frm.NPagina, NDatosPorPg = frm.NDatosPorPg }, ref cError);
    }

    public int ObtenerCantidadFormulariosPorUsuarioEstado(clsSolicitudFormularioEstado frm, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ObtenerCantidadFormulariosPorUsuarioEstado(new dto::clsSolicitudFormularioEstado { CNumeroFormulario = frm.CNumeroFormulario, NDesde = frm.NDesde, NHasta = frm.NHasta, DGenerado = frm.DGenerado, IdEstado = frm.IdEstado, NIdUsuario = frm.NIdUsuario }, ref cError);
    }

    public int ObtenerCantidadFormulariosActivar(clsFormularioSolicitudNoRadicados frmSolicitud, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ObtenerCantidadFormulariosActivar(new dto::clsFormulario
        {
            CNumeroFormulario = frmSolicitud.CNumeroFormulario,
            NIdDepartamento = frmSolicitud.NIdDepartamento,
            NIdEntidad = frmSolicitud.NIdEntidad,
            NIdMunicipio = frmSolicitud.NIdMunicipio,
            NIdPais = frmSolicitud.NIdPais,
            Accion = frmSolicitud.EAccion.GetHashCode()
        }, ref cError);
    }

    public List<clsFormulario> ObtenerFormulariosActivar(clsFormularioSolicitudNoRadicados frmSolicitud, int nPagina, int nTamaño, ref string cError)
    {
        IGestionFormulario iFormulario = (IGestionFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ObtenerFormulariosActivar(new dto::clsFormulario
        {
            CNumeroFormulario = frmSolicitud.CNumeroFormulario,
            NIdDepartamento = frmSolicitud.NIdDepartamento,
            NIdEntidad = frmSolicitud.NIdEntidad,
            NIdMunicipio = frmSolicitud.NIdMunicipio,
            NIdPais = frmSolicitud.NIdPais,
            Accion = frmSolicitud.EAccion.GetHashCode()
        }, nPagina, nTamaño, ref cError);
    }

    public clsFormulario ObtenerFormulario(string cNumeroFormulario, ref string cError)
    {
        IGetFormulario iFormulario = (IGetFormulario)u::Spring.GetService(Objetos.GestionFormularioBusiness);
        return iFormulario.ObtenerFormulario(cNumeroFormulario, ref cError);
    }

    public eResultadoValidacionRadicacion ValidarNumeroFormulario(clsRadicacion radicacion)
    {
        //Verifica si el numero de formulario esta vacio
        if (String.IsNullOrEmpty(radicacion.NRO_FORMULARIO))
        {
            return eResultadoValidacionRadicacion.faltaNumeroFormulario;
        }
        else
        {
            string cError = string.Empty;
            Ruv.Data.GestionFormulario.Contratos.IGetFormulario iFormulario = (Ruv.Data.GestionFormulario.Contratos.IGetFormulario)new Ruv.Data.GestionFormulario.Administrador();
            Ruv.Business.DTO.GestionFormulario.clsFormulario formulario = iFormulario.ObtenerFormulario(radicacion.NRO_FORMULARIO, ref cError);

            //Valida que el numero de formulario exista
            if (formulario == null)
            {
                return eResultadoValidacionRadicacion.NumeroFormularioInvalido;
            }
            else
            {
                if (formulario.NIdEstado == (ushort)eEstadoFormulario.GENERADO || formulario.NIdEstado == (ushort)eEstadoFormulario.IMPRENTA)
                {
                    return eResultadoValidacionRadicacion.NumeroFormularioNoAsignado;
                }
                if (formulario.NIdEstado == (ushort)eEstadoFormulario.INACTIVO)
                {
                    return eResultadoValidacionRadicacion.NumeroFormularioInactivo;
                }
                //Valida que no se encuentre Radicado
                if (formulario.NIdEstado == (ushort)(eEstadoFormulario.RADICADO))
                {
                    return eResultadoValidacionRadicacion.NumeroFormularioRadicado;
                }
                if (formulario.NIdEstado == (ushort)(eEstadoFormulario.INACTIVO))
                {
                    return eResultadoValidacionRadicacion.NumeroFormularioInvalido;
                }
                //Valida que la procedencia del formulario sea la correcta
                if (!ProcedenciaFormularioCoherente(radicacion, formulario))
                {
                    return eResultadoValidacionRadicacion.ProcedenciaErronea;
                }
            }
        }

        //Si todas las validaciones estan correctas
        return eResultadoValidacionRadicacion.validacionCorrecta;
        //return 0;
    }

    #endregion

    #endregion

    #region Private methods

    private Boolean ProcedenciaFormularioCoherente(clsRadicacion radicacion, Ruv.Business.DTO.GestionFormulario.clsFormulario formulario)
    {
        var result = true;
        // Comparar municipio
        result &= radicacion.ID_MUNICIPIO.HasValue == formulario.NIdMunicipio.HasValue;
        if (radicacion.ID_MUNICIPIO.HasValue && formulario.NIdMunicipio.HasValue)
            result &= radicacion.ID_MUNICIPIO.Value == formulario.NIdMunicipio.Value;
        // Comparar Entidad Municipio
        result &= radicacion.ID_ENTIDADMUNICIPIO.HasValue == formulario.NIdEntidad.HasValue;
        if (radicacion.ID_ENTIDADMUNICIPIO.HasValue && formulario.NIdEntidad.HasValue)
            result &= radicacion.ID_ENTIDADMUNICIPIO.Value == formulario.NIdEntidad.Value;

        return result;
    }

    #endregion
}
