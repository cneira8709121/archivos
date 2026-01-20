using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using dto = Ruv.Business.DTO.Notificacion;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INotificacionService" in both code and config file together.
[ServiceContract]
public interface INotificacionService {

    [OperationContract]
    IList<entidad::clsNotificacion> ObtenerNotificaciones(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas, string sortColumns, int startRow, int pageSize);

    [OperationContract]
    int ObtenerNotificacionesCantidad(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas);

    [OperationContract]
    entidad::clsNotificacion ObtenerNotificacionPorId(int idNotificacion, ref string cError);

    [OperationContract]
    dto::clsPaqueteNotificacion CrearPaqueteNotificacionDesdeFiltro(int idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, string direccionCitacion, string ubicacionNotificacion, bool soloAsignadas, ref string cError);

    [OperationContract]
    dto::clsNotificacionDetalle DetalleNotificacion(int nIdNotificacion);

    [OperationContract]
    bool IngresaPaquete(List<dto::clsNotificacion> lstnotificacion, int nIdUsuario, ref string cError);

    [OperationContract]
    int? IngresaPaquete(List<int> lstnotificacion, int nIdUsuario, ref string cError);

    [OperationContract]
    bool ActualizarNotificacion(int idNotificacion, string direccion, ref string cError);

    [OperationContract]
    bool ActualizarPuntoNotificacion(entidad::clsNotificacion clsNotificacion, ref string cError);

    [OperationContract]
    bool SolicitarCorreccion(int nIdNotificacion, int nIdPuntoNotificacion, int nIdEstadoNotificacion, ref string cError);

    /// <summary>
    /// Compara el archivo enviado por el Courier con los estados de envío de los  paquetes de notificación y
    /// actualiza los registros de notificación
    /// </summary>
    /// <param name="cNombreArchivo">Nombre del archivo de excel con el reporte</param>
    /// <param name="nIdUsuario"></param>
    /// <param name="cError"></param>
    /// <returns></returns>
    [OperationContract]
    bool CompararRegistrosCourier(int nIdPaqueteNotificacion, string cNombreArchivo, int nIdUsuario, ref string cError);

    [OperationContract]
    IList<entidad::clsNotificacion> ObtenerNotificacionesEntregadas(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, string sortColumns, int startRow, int pageSize, ref string cError);

    [OperationContract]
    int ObtenerNotificacionesEntregadasCantidad(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, ref string cError);

    [OperationContract]
    bool CierraNotificacion(int nIdNotificacion, ref string cError);

    [OperationContract]
    bool CambiarEstadoNotificacion(int nIdNotificacion, int idEstado, int DiasHabiles, string cObservacion, ref string cError);

    [OperationContract]
    bool AgregaOrdenServicioService(int nIdNotificacion, string OrdenServicio, ref string cError);

    [OperationContract]
    bool ObservacionNotificacion(int nIdNotificacion, string ObservacionNotifica, ref string cError);

    [OperationContract]
    bool AprobarNotificacion(int idNotificacion, ref string cError);

    [OperationContract]
    bool AsociarCodigosGuiaNotificacion(int nIdPaqueteNotificacion, string cNombreArchivo, int nIdUsuario, ref string cError);

    [OperationContract]
    bool ConfirmarEnvioNotificacion(int idPaqueteNotificacion, ref string cError);

    [OperationContract]
    List<dto::clsDatosCentroAtencion> ConsultaCentrosAtencion(int? idPais, int? idDepto, int? idMunicipio, int numeroPagina, int registrosPorPagina, ref string cError);
    
    [OperationContract]
    int ConsultaCentrosAtencionConteo(int? idPais, int? idDepto, int? idMunicipio, ref string cError);
    
    [OperationContract]
    List<dto::clsDetalleDatosCentrosAtencion> DetalleCentrosAtencion(int nIdCentroAtencion, int TipoCentroAtencion, int numeroPagina, int registrosPorPagina, ref string cError);
    
    [OperationContract]
    int DetalleCentrosAtencionConteo(int nIdCentroAtencion, int TipoCentroAtencion, ref string cError);
    
    [OperationContract]
    bool ConfirmarPublicacionEdicto(int nIdNotificacion, int idEstado, int diasHabiles, string cObservacion, ref string cError);
    
    [OperationContract]
    bool ConfirmarDesfijarEdicto(int nIdNotificacion, int idEstado, string cObservacion, ref string cError);

    [OperationContract]
    IList<dto::clsEncargadoEntidad> ObtenerEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, int numeroPagina, int registrosPorPagina, ref string cError);
    
    [OperationContract]
    int ContadorEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, ref string cError);

    [OperationContract]
    IList<dto::clsEstadosNotificacion> ObtenerEstadosDeNotificacion(ref string cError);
}
