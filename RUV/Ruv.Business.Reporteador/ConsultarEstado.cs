using System.Collections.Generic;
using System.Linq;
using Ruv.Business.DTO.Reporteador;
using BusinessContract = Ruv.Business.Reporteador.Contratos;
using DataContract = Ruv.Data.Reporteador.Contratos;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;

namespace Ruv.Business.Reporteador
{
    public class ConsultarEstado : BusinessContract.IConsultarEstado
    {
        #region Métodos públicos

        #region Implementación de servicios

        public int ConsultarEstadoDeclaracionConteo(clsDeclarante declarante, ref string cError) {
            DataContract.IConsultarEstado iConsulta = (DataContract.IConsultarEstado)new Ruv.Data.Reporteador.ConsultarEstado();
            return iConsulta.ConsultarEstadoDeclaracionConteo(declarante, ref cError);
        }

        public clsConsultarEstadoDeclaracionRespuesta ConsultarEstadoDeclaracion(clsDeclarante declarante, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            DataContract.IConsultarEstado iConsulta = (DataContract.IConsultarEstado)new Ruv.Data.Reporteador.ConsultarEstado();
            List<DTO.Reporteador.clsDeclarante> lstDeclarante = iConsulta.ConsultarEstadoDeclaracion(declarante, numeroPagina, registrosPorPagina, ref cError);
            clsConsultarEstadoDeclaracionRespuesta cedRespuesta = new clsConsultarEstadoDeclaracionRespuesta();
            if (lstDeclarante != null)
            {
                    // TODO: jairovg - Add the country to the transformation
                cedRespuesta.LstEstadoDeclaracion = lstDeclarante.Select(x => new EstadoDeclaracion
                {
                    CDepartamento = x.CDepartamento,
                    CEstadoProceso = x.CEstadoProceso,
                    CMunicipio = x.CMunicipio,
                    CNombresApellidos = string.Format("{0} {1} {2} {3}", new string[] { x.CPrimerNombre, x.CSegundoNombre, x.CPrimerApellido, x.CSegundoApellido }),
                    CNumeroDocumento = x.CNumeroDocumento,
                    CNumeroFormulario = x.CNumeroFormulario,
                    CPais = "Cambiar",
                    CTipoDocumento = x.CTipoDocumento,
                    DDeclaracion = x.DDeclaracion,
                    NIdDeclaracion = x.NIdDeclaracion


                }).ToList();
                
            }
            return cedRespuesta;
        }

        public clsConsultarEstadoDetalleDeclaracionRespuesta ConsultarDetalleDeclaracion(int nIdDeclaracion, ref string cError)
        {
            DataContract.IConsultarEstado iConsulta = (DataContract.IConsultarEstado)new Ruv.Data.Reporteador.ConsultarEstado();
            List<DTO.Reporteador.clsDetalleDeclaracion> lstDetalleDeclaracion = iConsulta.ConsultarDetalleDeclaracion(nIdDeclaracion, ref cError);
            clsConsultarEstadoDetalleDeclaracionRespuesta cedRespuesta = null;
            if (lstDetalleDeclaracion != null)
            {
                cedRespuesta = new clsConsultarEstadoDetalleDeclaracionRespuesta
                {
                    LstDetalleDeclaracion = lstDetalleDeclaracion.Select(x => new DetalleDeclaracion
                    {
                        CDocumentoDeclarante = x.CDocumentoDeclarante,
                        CDocumentoVictima = x.CDocumentoVictima,
                        CEstadoActualProceso = x.CEstadoActualProceso,
                        CEstadoValoracion = x.CEstadoValoracion,
                        CHechoVictimizante = x.CHechoVictimizante,
                        CNombresApellidosDeclarante = x.CNombresApellidosDeclarante,
                        CNombresApellidosVictima = x.CNombresApellidosVictima,
                        CNumeroFormulario = x.CNumeroFormulario,
                        CResultadoValoracion = x.CResultadoValoracion,
                        CTipoDocumentoDeclarante = x.CTipoDocumentoDeclarante,
                        CTipoDocumentoVictima = x.CTipoDocumentoVictima,
                        nAnexoId = x.nAnexoId,
                        nTipoAnexo = x.nTipoAnexo,
                        nIdSiniestro = x.nIdSiniestro,
                        DValoracion = x.DValoracion,
                        DHecho = x.DHecho,
                        nIdEstadoProceso = x.nIdEstadoProceso,
                        CTipoVictima = x.CTipoVictima,
                        CMarca = x.CMarca
                    }).ToList()
                };
            }
            return cedRespuesta;
        }
       

        #endregion

        #endregion
    }
}
