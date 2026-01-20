using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Ruv.Business.DTO.Notificacion;

namespace Ruv.Data.Notificacion.Contratos
{
    public interface INotificacionData
    {
        IList<clsNotificacion> ObtenerNotificaciones(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas, string sortColumns, int startRow, int pageSize);

        /// <summary>
        /// Trae todas las notificaciones que esten estado enviado o envio rechazado
        /// </summary>
        /// <param name="cError"></param>
        /// <returns></returns>
        IList<clsNotificacion> ObtenerNotificaciones(ref string cError);

        int ObtenerNotificacionesCantidad(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas);

        clsNotificacion ObtenerNotificacionPorId(int idNotificacion, ref string cError);

        int? CrearPaqueteNotificacionDesdeFiltro(int idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, string direccionCitacion, string ubicacionNotificacion, bool soloAsignadas, ref string cError);

        bool InsertaNotificacion(int nIdDeclaracion, DbTransaction tra, ref string cError);

        bool ActualizarNotificacion(int idNotificacion, string direccion, DbTransaction tra, ref string cError);

        bool ActualizarPuntoNotificacion(clsNotificacion notificacion, DbTransaction tra, ref string cError);

        /// <summary>
        /// Actualiza el estado de una lista de notificaciones
        /// </summary>
        /// <param name="eNotificacion">Lista de notificaciones a actualizar su estado</param>
        /// <param name="tra">Transacción</param>
        /// <param name="cError"></param>
        /// <returns></returns>
        bool ActualizarEstadoNotificacion(IEnumerable<clsNotificacion> eNotificacion, DbTransaction tra, ref string cError);

        clsNotificacionDetalle DetalleNotificaciones(int nIdNotificacion);

        int? CreaPaqueteNotificacion(int nIdUsuario, DbTransaction tra, ref string cError);

        bool InsertaIdPaquete(List<clsNotificacion> lstNotificacion, int? nIdNotificacion, DbTransaction tra, ref string cError);

        bool InsertaIdPaquete(List<int> lstNotificacion, int? nIdNotificacion, DbTransaction tra, ref string cError);

        bool SolicitaCorreccion(int nIdNotificacion, int nIdPuntoNotificacion, int nIdEstadoNotificacion, DbTransaction tra, ref string cError);

        /// <summary>
        /// Solicita la corrección de una lista de notificaciones
        /// </summary>
        /// <param name="eNotificacion">Lista de notificaciones a solicitar corrección</param>
        /// <param name="tra">Transacción</param>
        /// <param name="cError"></param>
        /// <returns></returns>
        bool SolicitarCorreccion(IEnumerable<clsNotificacion> eNotificacion, DbTransaction tra, ref string cError);

        /// <summary>
        /// Carga los datos del archivo enviado por el Courier con los estados de envío de los  paquetes de notificación
        /// </summary>
        /// <param name="cNombreArchivo">Nombre del archivo de excel con el reporte</param>
        /// <param name="cError"></param>
        /// <returns>Lista con los datos enviados por el Courier</returns>
        List<clsReporteCourier> CargarRegistrosCourier(string cNombreArchivo, ref string cError);

        IList<clsNotificacion> ObtenerNotificacionesEntregadas(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, string sortColumns, int startRow, int pageSize, ref string cError);

        int ObtenerNotificacionesEntregadasCantidad(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, ref string cError);

        bool CierraNotificacion(int nIdNotificacion, DbTransaction tra, ref string cError);

        bool CambiarEstadoNotificacion(int nIdNotificacion, int idEstado, DateTime? fechaFinal, string cObservacion, DbTransaction tra, ref string cError);

        /// <summary>
        /// Obtiene el total de paquetes de notificacion generados, filtrados por usuario actor, orden de servicio y fecha de generación
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario actor</param>
        /// <param name="ordenServicio">Filtro de orden de servicio</param>
        /// <param name="fechaInicio">Filtro de fecha generacion</param>
        /// <param name="fechaFin">Filtro de fecha generacion</param>
        /// <returns>Total de registros de paquete de notificación</returns>
        int ObtenerPaquetesConteo(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, ref string cError);

        /// <summary>
        /// Obtiene la lista de paquetes de notificacion generados, filtrados por usuario actor, orden de servicio y fecha de generación
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario actor</param>
        /// <param name="ordenServicio">Filtro de orden de servicio</param>
        /// <param name="fechaInicio">Filtro de fecha generacion</param>
        /// <param name="fechaFin">Filtro de fecha generacion</param>
        /// <returns>Colección de <see cref="clsPaqueteNotificacion"/></returns>
        IList<clsPaqueteNotificacion> ObtenerPaquetes(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, int numeroPagina, int registrosPorPagina, ref string cError);

        /// <summary>
        /// Obtiene el paquete de notificacion correspondiente a un id
        /// </summary>
        /// <param name="id">Identificador del paquete</param>
        /// <returns><see cref="clsPaqueteNotificacion"/> correspondiente al identificador</returns>
        clsPaqueteNotificacion ObtenerPaquetePorId(int id, ref string cError);

        /// <summary>
        /// Obtiene el total de notificaciones de un paquete
        /// </summary>
        /// <param name="idPaqueteNotificacion">Identificador del paquete</param>
        /// <returns>Total de registros de notificacion del paquete</returns>
        int ObtenerDetallePaqueteCount(int idPaqueteNotificacion, ref string cError);

        /// <summary>
        /// Obtiene la lista de notificaciones de un paquete
        /// </summary>
        /// <param name="idPaqueteNotificacion">Identificador del paquete</param>
        /// <returns>Coleccion de <see cref="clsNotificacion"/></returns>
        IList<clsNotificacion> ObtenerDetallePaquete(int idPaqueteNotificacion, int numeroPagina, int registrosPorPagina, ref string cError);
    
        bool AgregaOrdenServicio(int nIdNotificacion, string OrdenServicio, DbTransaction tra, ref string cError);

        bool ObservacionNotificacion(int nIdNotificacion, string ObservacionNotificacion, DbTransaction tra, ref string cError);

        bool AprobarNotificacion(int idNotificacion, DbTransaction tra, ref string cError);

        bool AsociaCodigoGuiaNotificacion(IEnumerable<clsNotificacion> eNotificacion, DbTransaction tra, ref string cError);

        bool ConfirmarEnvioNotificacion(int idPaqueteNotificacion, DbTransaction tra, ref string cError);

        int ConsultaDatosCentroAtencionCount(int? idPais, int? idDepto, int? idMunicipio, ref string cError);

        IList<clsDatosCentroAtencion> ConsultaDatosCentroAtencion(int? idPais, int? idDepto, int? idMunicipio, int numeroPagina, int registrosPorPagina, ref string cError);

        IList<clsDetalleDatosCentrosAtencion> ObtenerDetalleCentroAtencion(int nIdCentroAtencion, int nTipoCentro, int numeroPagina, int registrosPorPagina, ref string cError);

        int DetalleCentroAtencioncontador(int nIdCentroAtencion, int nTipoCentro, ref string cError);

        IList<clsHistoricoNotificacion> ObtenerHistorico(int idNotificacion);

        IList<clsHistoricoNotificacion> ObtenerHistoricoPaquete(int idPaqueteNotificacion);

        int ObtieneTipoLey(int nIdNotficacion, ref string cError);

        IList<clsEncargadoEntidad> ObtenerEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, int numeroPagina, int registrosPorPagina, ref string cError);

        int ContadorEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, ref string cError);

        IList<clsEstadosNotificacion> ObtenerEstadosDeNotificacion(ref string cError);
    }
}
