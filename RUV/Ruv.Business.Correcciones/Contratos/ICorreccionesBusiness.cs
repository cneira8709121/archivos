using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.Correcciones.Contratos;
using dto = Ruv.Business.DTO.Correcciones;
using System.Data.Common;
using Ruv.Data.Correcciones.Contratos;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Correcciones;
using Ruv.Business.DTO.Reporteador;

namespace Ruv.Business.Correcciones.Contratos
{
    public interface ICorreccionesBusiness
    {
        bool SolicitarCorreccion(int IdRegPersona, int idUsuarioSolicita, IList<dto::clsCorreccion> correcciones, ref string cError);
        int SolicitarCorreccionOut(int IdRegPersona, int idUsuarioSolicita, IList<dto::clsCorreccion> correcciones, ref string cError);
        List<entidad::clsCargaDatosCorreccion> CargaDatosCorreccion(int IdRegPersona, ref string cError);
        entidad::clsCargaDatosCorreccion ConsultarCorreccion(int idCorreccion, ref string cError);
        bool RechazarCorreccion(int idCorreccion, int idUsuarioRechaza, string observaciones, ref string cError);
        int ConsultarEstadoDeclaracionConteo(DTO.Reporteador.clsDeclarante declarante, ref string cError);
        List<clsDeclarante> ConsultarEstadoDeclaracion(clsDeclarante declarante, int numeroPagina, int registrosPorPagina, ref string cError);
        bool AprobarCorreccion(int idCorreccion, int idUsuarioAprueba, string observaciones, ref string cError);
        IList<entidad::clsCorreccion> ConsultarCamposCorreccion(int idCorreccion, ref string cError);
        string ObtienenombreSubEtnia(int nIdSubetnia, ref string cError);
    }
}
