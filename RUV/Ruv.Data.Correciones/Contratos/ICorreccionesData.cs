using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Correcciones;
using System.Data.Common;
using Ruv.Business.DTO.Reporteador;

namespace Ruv.Data.Correcciones.Contratos
{
    public interface ICorreccionesData
    {
        bool SolicitarCorreccion(int IdRegPersona, int idUsuarioSolicita, IList<clsCorreccion> correcciones, DbTransaction tra, ref string cError);
        int SolicitarCorreccionOut(int IdRegPersona, int idUsuarioSolicita, IList<clsCorreccion> correcciones, DbTransaction tra, ref string cError);
        List<clsCargaDatosCorreccion> CargaDatosCorreccion(int IdRegistroPersona, ref string cError);
        clsCargaDatosCorreccion ConsultarCorreccion(int idCorreccion, ref string cError);
        bool RechazarCorreccion(int idCorreccion, int idUsuarioRechaza, string observaciones, DbTransaction tra, ref string cError);
        int ConsultarEstadoDeclaracionConteo(clsDeclarante declarante, ref string cError);
        List<clsDeclarante> ConsultarEstadoDeclaracion(clsDeclarante declarante, int numeroPagina, int registrosPorPagina, ref string cError);
        bool AprobarCorreccion(int IdCorreccion,DbTransaction tra, ref string cError);
        IList<clsCorreccion> ConsultarCamposCorreccion(int idCorreccion, ref string cError);
        clsInformacionCorreccion CargaInformacionCorreccion(int nIdCorreccion, ref string cError);
        string ObtieneNombreSubEtnia(int nIdSubetnia, ref string cError);
    }
}
