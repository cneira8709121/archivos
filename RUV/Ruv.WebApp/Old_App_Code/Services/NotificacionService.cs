using System;
using System.Collections.Generic;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using b = Ruv.Business.Notificacion;
using dto = Ruv.Business.DTO.Notificacion;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using u = Ruv.Infrastructure.Crosscutting.Utilities;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NotificacionService" in code, svc and config file together.
public class NotificacionService : INotificacionService
{
    #region Public methods

    public IList<entidad::clsNotificacion> ObtenerNotificaciones(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas, string sortColumns, int startRow, int pageSize) {
        try {
            b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
            return iNotificacion.ObtenerNotificaciones(idUsuario, declaracion, tipoDocumento, documento, nombreDeclarante, paisNotificacion, departamentoNotificacion, municipioNotificacion, puntoNotificacion, direccionCitacion, soloAsignadas, sortColumns, startRow, pageSize);
        }
        catch (Exception ex) {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
           throw ex;
        }
    }

    public int ObtenerNotificacionesCantidad(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas) {
        try {
            b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
            return iNotificacion.ObtenerNotificacionesCantidad(idUsuario, declaracion, tipoDocumento, documento, nombreDeclarante, paisNotificacion, departamentoNotificacion, municipioNotificacion, puntoNotificacion, direccionCitacion, soloAsignadas);
        }
        catch (Exception ex) {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
            throw ex;
        }
    }

    public entidad::clsNotificacion ObtenerNotificacionPorId(int idNotificacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        var element = iNotificacion.ObtenerNotificacionPorId(idNotificacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return element;
    }

    public dto::clsPaqueteNotificacion CrearPaqueteNotificacionDesdeFiltro(int idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, string direccionCitacion, string ubicacionNotificacion, bool soloAsignadas, ref string cError)
    {
        dto::clsPaqueteNotificacion element = null;
        try {
            b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
            element = iNotificacion.CrearPaqueteNotificacionDesdeFiltro(idUsuario, declaracion, tipoDocumento, documento, nombreDeclarante, direccionCitacion, ubicacionNotificacion, soloAsignadas, ref cError);
            if (!string.IsNullOrEmpty(cError))
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
                //clsLog Log = new clsLog();
                //Log.Registrar(cError.ToString());
                RegistroTraza.I.Registrar(cError.ToString());
            }
        }
        catch (Exception ex)
        {
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
        }
        return element;
    }

    public dto::clsNotificacionDetalle DetalleNotificacion(int nIdNotificacion) {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        try {
            return iNotificacion.DetalleNotificacion(nIdNotificacion);
        }
        catch (Exception ex) {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //new clsLog().Registrar(ex.Message);
            RegistroTraza.I.Registrar(ex.Message);
            throw ex;
        }
    }

    public bool IngresaPaquete(List<dto::clsNotificacion> lstnotificacion, int nIdUsuario, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        return iNotificacion.CreaPaqueteNotificacion(lstnotificacion, nIdUsuario, ref cError);
    }

    public int? IngresaPaquete(List<int> lstnotificacion, int nIdUsuario, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        return iNotificacion.CreaPaqueteNotificacion(lstnotificacion, nIdUsuario, ref cError);
    }

    public bool ActualizarNotificacion(int idNotificacion, string direccion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        bool resultado = iNotificacion.ActualizarNotificacion(idNotificacion, direccion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return resultado;
    }

    public bool ActualizarPuntoNotificacion(entidad::clsNotificacion clsNotificacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        bool resultado = iNotificacion.ActualizarPuntoNotificacion(clsNotificacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return resultado;
    }

    public bool SolicitarCorreccion(int nIdNotificacion, int nIdPuntoNotificacion, int nIdEstadoNotificacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        bool resultado = iNotificacion.SolicitaCorreccion(nIdNotificacion, nIdPuntoNotificacion, nIdPuntoNotificacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return resultado;
    }

    public bool CompararRegistrosCourier(int nIdPaqueteNotificacion,string cNombreArchivo, int nIdUsuario, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        return iNotificacion.CompararRegistrosCourier(nIdPaqueteNotificacion,cNombreArchivo, nIdUsuario, ref cError);
    }

    public IList<entidad::clsNotificacion> ObtenerNotificacionesEntregadas(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, string sortColumns, int startRow, int pageSize, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        IList<entidad::clsNotificacion> listadatos = iNotificacion.ObtenerNotificacionesEntregadas(idUsuario, busquedaGlobal, declaracion, tipoDocumento, documento, nombreDeclarante, estadoNotificacion, sortColumns, startRow, pageSize, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return listadatos;
    }

    public int ObtenerNotificacionesEntregadasCantidad(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, ref string cError)
    {
        int cantidad = 0;
        try
        {
            b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
            cantidad = iNotificacion.ObtenerNotificacionesEntregadasCantidad(idUsuario, busquedaGlobal, declaracion, tipoDocumento, documento, nombreDeclarante, estadoNotificacion, ref cError);
            if (!string.IsNullOrEmpty(cError))
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
                //clsLog Log = new clsLog();
                //Log.Registrar(cError.ToString());
                RegistroTraza.I.Registrar(cError.ToString());
            }
        }
        catch (Exception ex)
        {
            //clsLog.Registrar(ex);
            RegistroTraza.I.Registrar(ex);
        }
        return cantidad;
    }

    public bool CierraNotificacion(int nIdNotificacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        bool resultado = iNotificacion.CierraNotificacion(nIdNotificacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return resultado;
    }

    public bool CambiarEstadoNotificacion(int nIdNotificacion, int idEstado,int DiasHabiles, string cObservacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        bool resultado = iNotificacion.CambiarEstadoNotificacion(nIdNotificacion, idEstado, DiasHabiles, cObservacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return resultado;
    }

    public int ObtenerPaquetesConteo(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        return iNotificacion.ObtenerPaquetesConteo(idUsuario, ordenServicio, fechaInicio, fechaFin, ref cError);
    }

    public bool AgregaOrdenServicioService(int nIdNotificacion, string OrdenServicio, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        bool resultado = iNotificacion.AgregaOrdenServicioBusiness(nIdNotificacion,OrdenServicio, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return resultado;
    }

    public List<dto::clsPaqueteNotificacion> ObtenerPaquetes(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, int numeroPagina, int registrosPorPagina, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        var result = iNotificacion.ObtenerPaquetes(idUsuario, ordenServicio, fechaInicio, fechaFin, numeroPagina, registrosPorPagina, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public dto::clsPaqueteNotificacion ObtenerPaquete(int id, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        var result = iNotificacion.ObtenerPaquete(id, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public int ObtenerDetallePaqueteConteo(int idPaquete, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        var result = iNotificacion.ObtenerDetallePaqueteConteo(idPaquete, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public List<dto::clsNotificacion> ObtenerDetallePaquete(int idPaquete, int numeroPagina, int registrosPorPagina, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        var result = iNotificacion.ObtenerDetallePaquete(idPaquete, numeroPagina, registrosPorPagina, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public bool ObservacionNotificacion(int nIdNotificacion, string ObservacionNotifica, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        bool resultado = iNotificacion.ObservacionNotificacionBusiness(nIdNotificacion, ObservacionNotifica, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return resultado;
    }

    public bool AprobarNotificacion(int idNotificacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        var result = iNotificacion.AprobarNotificacion(idNotificacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public bool AsociarCodigosGuiaNotificacion(int nIdPaqueteNotificacion, string cNombreArchivo, int nIdUsuario, ref string cError)
    {
       b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
       bool resultado = iNotificacion.AsociarCodigosGuiaNotificacion(nIdPaqueteNotificacion, cNombreArchivo, nIdUsuario, ref cError);
       if (!string.IsNullOrEmpty(cError))
       {
           Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
           //clsLog Log = new clsLog();
           //Log.Registrar(cError.ToString());
           RegistroTraza.I.Registrar(cError.ToString());
       }
      return resultado;
    }

    public bool ConfirmarEnvioNotificacion(int idPaqueteNotificacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        var result = iNotificacion.ConfirmarEnvioNotificacion(idPaqueteNotificacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public List<dto::clsDatosCentroAtencion> ConsultaCentrosAtencion(int? idPais, int? idDepto, int? idMunicipio, int numeroPagina, int registrosPorPagina, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        var result = iNotificacion.ConsultaDatosCentroAtencion(idPais, idDepto, idMunicipio, numeroPagina, registrosPorPagina, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public int ConsultaCentrosAtencionConteo(int? idPais, int? idDepto, int? idMunicipio, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        var result = iNotificacion.ConsultaDatosCentroAtencionConteo(idPais, idDepto, idMunicipio, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public List<dto::clsDetalleDatosCentrosAtencion> DetalleCentrosAtencion(int nIdCentroAtencion, int TipoCentroAtencion, int numeroPagina, int registrosPorPagina, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        var result = iNotificacion.DetalleDatosCentroAtencion(nIdCentroAtencion,TipoCentroAtencion,numeroPagina, registrosPorPagina, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public int DetalleCentrosAtencionConteo(int nIdCentroAtencion, int TipoCentroAtencion,ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        var result = iNotificacion.DetalleDatosCentroAtencionConteo(nIdCentroAtencion, TipoCentroAtencion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public IList<dto::clsHistoricoNotificacion> ObtenerHistorico(int idNotificacion) {
        var business = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        return business.ObtenerHistorico(idNotificacion);
    }

    public IList<dto::clsHistoricoNotificacion> ObtenerHistoricoPaquete(int idPaqueteNotificacion) {
        var business = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        return business.ObtenerHistoricoPaquete(idPaqueteNotificacion);
    }

    public bool ConfirmarPublicacionEdicto(int nIdNotificacion, int idEstado, int diasHabiles, string cObservacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        var result = iNotificacion.ConfirmarPublicacionEdicto(nIdNotificacion,idEstado,diasHabiles,cObservacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public bool ConfirmarDesfijarEdicto(int nIdNotificacion, int idEstado, string cObservacion, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = (b::Contratos.INotificacionBusiness)u::Spring.GetService(Objetos.NotificacionBusiness);
        var result = iNotificacion.ConfirmarDesfijarEdicto(nIdNotificacion, idEstado, cObservacion, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public IList<dto::clsEncargadoEntidad> ObtenerEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, int numeroPagina, int registrosPorPagina, ref string cError)
    {
        var business = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        return business.ObtenerEncargadosPorEntidad(nIdCentroAtencion, nTipoCentro, numeroPagina, registrosPorPagina, ref cError);
    }

    public int ContadorEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, ref string cError)
    {
        b::Contratos.INotificacionBusiness iNotificacion = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        var result = iNotificacion.ContadorEncargadosPorEntidad(nIdCentroAtencion, nTipoCentro, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }
        return result;
    }

    public IList<dto.clsEstadosNotificacion> ObtenerEstadosDeNotificacion(ref string cError)
    {
        var business = u::Spring.GetService(Objetos.NotificacionBusiness) as b::Contratos.INotificacionBusiness;
        return business.ObtenerEstadosDeNotificacion(ref cError);
    }

    #endregion

}
