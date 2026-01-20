using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;

[ServiceContract]
public interface IConsultarEstadoPersonaService
{
    [OperationContract]
    int ConsultarEstadoDeclaracionConteo(clsConsultarEstadoDeclaracionSolicitud cesPersona, ref string cError);
    [OperationContract]
    clsConsultarEstadoDeclaracionRespuesta ConsultarEstadoDeclaracion(clsConsultarEstadoDeclaracionSolicitud cesPersona, ref string cError);
    [OperationContract]
    clsConsultarEstadoDeclaracionRespuesta ConsultarEstadoDeclaracionPaginado(clsConsultarEstadoDeclaracionSolicitud cesPersona, int numeroPagina, int registrosPorPagina, ref string cError);
    [OperationContract]
    clsConsultarEstadoDetalleDeclaracionRespuesta ConsultarEstadoDetalleDeclaracion(int nIdDeclaracion, ref string cError);
}
