using System.ServiceModel.Activation;
using Ruv.Business.DTO.Reporteador;
using Ruv.Business.Reporteador.Contratos;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;

[AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Required)]
public class ConsultarEstadoPersonaService : IConsultarEstadoPersonaService
{
    #region Métodos públicos

    #region Implementación de la interfaz

    public int ConsultarEstadoDeclaracionConteo(clsConsultarEstadoDeclaracionSolicitud cesPersona, ref string cError)
    {
        IConsultarEstado iConsultaEstado = (IConsultarEstado)new Ruv.Business.Reporteador.ConsultarEstado();
        return iConsultaEstado.ConsultarEstadoDeclaracionConteo(new clsDeclarante
               {
                   CNumeroDocumento = cesPersona.CNumeroDocumento,
                   CPrimerNombre = cesPersona.CPrimerNombre,
                   CPrimerApellido = cesPersona.CPrimerApellido,
                   CNumeroFormulario = cesPersona.CNumeroFormulario
               }, ref cError);
    }

    public clsConsultarEstadoDeclaracionRespuesta ConsultarEstadoDeclaracion(clsConsultarEstadoDeclaracionSolicitud cesPersona, ref string cError)
    {
        IConsultarEstado iConsultaEstado = (IConsultarEstado) new Ruv.Business.Reporteador.ConsultarEstado();
        clsConsultarEstadoDeclaracionRespuesta cedRespuesta = iConsultaEstado.ConsultarEstadoDeclaracion(new clsDeclarante
        {
            CNumeroDocumento = cesPersona.CNumeroDocumento,
            CPrimerNombre = cesPersona.CPrimerNombre,
            CPrimerApellido = cesPersona.CPrimerApellido,
            CNumeroFormulario = cesPersona.CNumeroFormulario
        }, 1, int.MaxValue, ref cError);

        return cedRespuesta;
    }

    public clsConsultarEstadoDeclaracionRespuesta ConsultarEstadoDeclaracionPaginado(clsConsultarEstadoDeclaracionSolicitud cesPersona, int numeroPagina, int registrosPorPagina, ref string cError)
    {
        IConsultarEstado iConsultaEstado = (IConsultarEstado)new Ruv.Business.Reporteador.ConsultarEstado();
        clsConsultarEstadoDeclaracionRespuesta cedRespuesta = iConsultaEstado.ConsultarEstadoDeclaracion(new clsDeclarante
        {
            CNumeroDocumento = cesPersona.CNumeroDocumento,
            CPrimerNombre = cesPersona.CPrimerNombre,
            CPrimerApellido = cesPersona.CPrimerApellido,
            CNumeroFormulario = cesPersona.CNumeroFormulario
        }, numeroPagina, registrosPorPagina, ref cError);

        return cedRespuesta;
    }

    public clsConsultarEstadoDetalleDeclaracionRespuesta ConsultarEstadoDetalleDeclaracion(int nIdDeclaracion, ref string cError)
    {
        IConsultarEstado iConsultaEstado = (IConsultarEstado)new Ruv.Business.Reporteador.ConsultarEstado();
        clsConsultarEstadoDetalleDeclaracionRespuesta cedRespuesta = iConsultaEstado.ConsultarDetalleDeclaracion(nIdDeclaracion, ref cError);

        return cedRespuesta;
    }

    #endregion

    #endregion
}
