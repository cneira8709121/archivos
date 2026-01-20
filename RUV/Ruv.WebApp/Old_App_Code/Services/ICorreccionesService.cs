using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Ruv.Business.DTO.Correcciones;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Correcciones;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;

[ServiceContract]
public interface ICorreccionesService
{
    [OperationContract]
    bool SolicitarCorreccion(int IdRegPersona, int idUsuarioSolicita, IList<clsCorreccion> correcciones, ref string cError);
    [OperationContract]
    int SolicitarCorreccionOut(int IdRegPersona, int idUsuarioSolicita, IList<clsCorreccion> correcciones, ref string cError);
    [OperationContract]
    List<entidad::clsCargaDatosCorreccion> CargaDatosCorreccion(int IdRegPersona, ref string cError);
    [OperationContract]
    entidad::clsCargaDatosCorreccion ConsultarCorreccion(int idCorreccion, ref string cError);
    [OperationContract]
    bool RechazarCorreccion(int idCorreccion, int idUsuarioRechaza, string observaciones, ref string cError);
    [OperationContract]
    int ConsultarEstadoDeclaracionConteo(clsConsultarEstadoDeclaracionSolicitud cesPersona, ref string cError);
    [OperationContract]
    clsConsultarEstadoDeclaracionRespuesta ConsultarEstadoDeclaracionPaginado(clsConsultarEstadoDeclaracionSolicitud cesPersona, int numeroPagina, int registrosPorPagina, ref string cError);
    [OperationContract]
    bool AprobarCorreccion(int idCorreccion, int idUsuarioAprueba, string observaciones, ref string cError);
    [OperationContract]
    IList<entidad::clsCorreccion> ConsultarCamposCorreccion(int idCorreccion, ref string cError);
    [OperationContract]
    string ObtieneNombreSubEtnia(int nIdSubetnia, ref string cError);
}