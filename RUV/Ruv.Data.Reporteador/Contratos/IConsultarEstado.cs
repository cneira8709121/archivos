using System.Collections.Generic;
using Ruv.Business.DTO.Reporteador;

namespace Ruv.Data.Reporteador.Contratos
{
    public interface IConsultarEstado
    {
        int ConsultarEstadoDeclaracionConteo(clsDeclarante declarante, ref string cError);
        List<clsDeclarante> ConsultarEstadoDeclaracion(clsDeclarante declarante, int numeroPagina, int registrosPorPagina, ref string cError);
        List<clsDetalleDeclaracion> ConsultarDetalleDeclaracion(int nIdDeclaracion, ref string cError);
    }
}
