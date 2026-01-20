using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO.Notificacion;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;

namespace Ruv.Business.Notificacion.Contratos
{
    public interface INotificacionBusiness {

        IList<entidad::clsNotificacion> ObtenerNotificaciones(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas, string sortColumns, int startRow, int pageSize);

        int ObtenerNotificacionesCantidad(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas);

        entidad::clsNotificacion ObtenerNotificacionPorId(int idNotificacion, ref string cError);

        dto::clsPaqueteNotificacion CrearPaqueteNotificacionDesdeFiltro(int idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, string direccionCitacion, string ubicacionNotificacion, bool soloAsignadas, ref string cError);

        dto::clsNotificacionDetalle DetalleNotificacion(int nIdNotificacion);

        bool ActualizarNotificacion(int idNotificacion, string direccion, ref string cError);

        bool ActualizarPuntoNotificacion(entidad::clsNotificacion clsNotificacion, ref string cError);

        bool CreaPaqueteNotificacion(List<dto::clsNotificacion> lstnotificacion, int nIdUsuario, ref string cError);

        int? CreaPaqueteNotificacion(List<int> lstnotificacion, int nIdUsuario, ref string cError);

        bool SolicitaCorreccion(int nIdNotificacion, int nIdPuntoNotificacion, int nIdEstadoNotificacion, ref string cError);

        IList<entidad::clsNotificacion> ObtenerNotificacionesEntregadas(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, string sortColumns, int startRow, int pageSize, ref string cError);

        int ObtenerNotificacionesEntregadasCantidad(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, ref string cError);

        /// <summary>
        /// Compara el archivo enviado por el Courier con los estados de envío de los  paquetes de notificación y
        /// actualiza los registros de notificación
        /// </summary>
        /// <param name="cNombreArchivo">Nombre del archivo de excel con el reporte</param>
        /// <param name="cError"></param>
        /// <returns></returns>
        bool CompararRegistrosCourier(int nIdPaqueteNotificacion, string cNombreArchivo, int nIdUsuario, ref string cError);

        bool CierraNotificacion(int nIdNotificacion, ref string cError);

        bool CambiarEstadoNotificacion(int nIdNotificacion, int idEstado, int diasHabiles, string cObservacion, ref string cError);

        int ObtenerPaquetesConteo(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, ref string cError);

        List<dto::clsPaqueteNotificacion> ObtenerPaquetes(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, int numeroPagina, int registrosPorPagina, ref string cError);

        dto::clsPaqueteNotificacion ObtenerPaquete(int id, ref string cError);

        int ObtenerDetallePaqueteConteo(int idPaquete, ref string cError);

        List<dto::clsNotificacion> ObtenerDetallePaquete(int idPaquete, int numeroPagina, int registrosPorPagina, ref string cError);

        bool AgregaOrdenServicioBusiness(int nIdNotificacion, string OrdenServicio, ref string cError);

        bool ObservacionNotificacionBusiness(int nIdNotificacion, string ObservacionNotificacion, ref string cError);

        bool AprobarNotificacion(int idNotificacion, ref string cError);

        bool AsociarCodigosGuiaNotificacion(int nIdPaqueteNotificacion, string cNombreArchivo, int nIdUsuario, ref string cError);

        bool ConfirmarEnvioNotificacion(int idPaqueteNotificacion, ref string cError);

        int ConsultaDatosCentroAtencionConteo(int? idPais, int? idDepto, int? idMunicipio, ref string cError);

        List<dto::clsDatosCentroAtencion> ConsultaDatosCentroAtencion(int? idPais, int? idDepto, int? idMunicipio, int numeroPagina, int registrosPorPagina, ref string cError);

        List<dto::clsDetalleDatosCentrosAtencion> DetalleDatosCentroAtencion(int nIdCentroAtencion, int nTipoCentroAtencion, int numeroPagina, int registrosPorPagina, ref string cError);

        int DetalleDatosCentroAtencionConteo(int nIdCentroAtencion, int nTipoCentroAtencion, ref string cError);

        IList<dto::clsHistoricoNotificacion> ObtenerHistorico(int idNotificacion);

        IList<dto::clsHistoricoNotificacion> ObtenerHistoricoPaquete(int idPaqueteNotificacion);

        bool ConfirmarPublicacionEdicto(int nIdNotificacion, int idEstado, int diasHabiles, string cObservacion, ref string cError);

        bool ConfirmarDesfijarEdicto(int nIdNotificacion, int idEstado, string cObservacion, ref string cError);

        IList<dto::clsEncargadoEntidad> ObtenerEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, int numeroPagina, int registrosPorPagina, ref string cError);

        int ContadorEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, ref string cError);

        IList<dto::clsEstadosNotificacion> ObtenerEstadosDeNotificacion(ref string cError);
    }
}
