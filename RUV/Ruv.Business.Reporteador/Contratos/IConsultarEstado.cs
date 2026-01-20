using Ruv.Business.DTO.Reporteador;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;

namespace Ruv.Business.Reporteador.Contratos
{
    public interface IConsultarEstado
    {
        int ConsultarEstadoDeclaracionConteo(clsDeclarante declarante, ref string cError);
        clsConsultarEstadoDeclaracionRespuesta ConsultarEstadoDeclaracion(clsDeclarante declarante, int numeroPagina, int registrosPorPagina, ref string cError);
        clsConsultarEstadoDetalleDeclaracionRespuesta ConsultarDetalleDeclaracion(int nIdDeclaracion, ref string cError);
    }
}
