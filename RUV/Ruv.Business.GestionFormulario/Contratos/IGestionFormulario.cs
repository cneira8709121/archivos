using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;

namespace Ruv.Business.GestionFormulario.Contratos
{
    public interface IGestionFormulario
    {
        List<clsFormulario> GenerarFormularios(uint nCantidad,
                                               string cSerie,
                                               int nIdUsuario,
                                               int nIdEstado,
                                               int? nIdPais,
                                               int? nIdDepartamento,
                                               int? nIdMunicipio,
                                               int? nIdEntidadmunicipio,
                                               ref string cError);
        List<clsFormulario> GenerarFormulariosWEB(uint nCantidad,
                                                  string cSerie,
                                                  int nIdUsuario,
                                                  int nIdEstado,
                                                  int? nIdEntidadmunicipio,
                                                  ref string cError);
        int ObtenerPaisGeneraFormularioWEB(int? nIdEntidadmunicipio, ref string cError);
        List<clsFormulario> ListarFormularios(ref string cError);
        List<clsFormulario> ListarFormulariosNoRadicados(dto::clsFormulario frm, ref string cError);
        List<clsFormulario> ListarFormulariosPorEstado(eEstadoFormulario efEstado, ref string cError);
        uint? AsignarFormulario(dto::clsFormulario frm, ref string cError);
        bool AsignarFormulario(dto::clsSolicitudFormularioEstado frm, ref string cError);
        uint? InactivarFormulario(uint nIdFormulario, string observacion, ref string cError);
        uint? SepararFormularioImprenta(dto::clsFormulario frm, ref string cError);
        List<dto::clsFormulario> SepararFormularioImprenta(dto::clsSolicitudFormularioEstado frm, ref string cError);
        List<clsFormulario> ObtenerFormulariosPorUsuario(int nIdUsuario, ref string cError);
        uint? MarcarDescargado(uint nIdFormulario, ref string cError);
        bool MarcarRadicado(string cNumeroFormulario, ref string cError);
        List<clsFormulario> ObtenerFormulariosPorUsuarioEstadoPaginado(dto::clsSolicitudFormularioEstado frm, ref string cError);
        int ObtenerCantidadFormulariosPorUsuarioEstado(dto::clsSolicitudFormularioEstado frm, ref string cError);
        int ObtenerCantidadFormulariosActivar(dto.clsFormulario clsFormulario, ref string cError);
        List<clsFormulario> ObtenerFormulariosActivar(dto.clsFormulario clsFormulario, int nPagina, int nTamaño, ref string cError);
    }
}
