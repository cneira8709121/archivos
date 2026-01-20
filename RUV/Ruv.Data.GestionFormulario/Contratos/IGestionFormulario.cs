using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.GestionFormulario;
using System.Data.Common;

namespace Ruv.Data.GestionFormulario.Contratos
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
        List<clsFormulario> ListarFormulariosNoRadicados(clsFormulario frm, ref string cError);
        List<clsFormulario> ListarFormulariosPorEstado(ushort nIdEstado, ref string cError);
        uint? AsignarFormulario(clsFormulario frm, ref string cError);
        bool AsignarFormulario(clsSolicitudFormularioEstado frm, DbTransaction tra, ref string cError);
        uint? InactivarFormulario(uint nIdFormulario, string observacion, ref string cError);
        uint? SepararFormularioImprenta(clsFormulario frm, ref string cError);
        List<clsFormulario> SepararFormularioImprenta(clsSolicitudFormularioEstado frm, DbTransaction tra, ref string cError);
        List<clsFormulario> ObtenerFormulariosPorUsuario(int nIdUsuario, ref string cError);
        uint? MarcarDescargado(uint nIdFormulario, ref string cError);
        void MarcarRadicado(string cNumeroFormulario, DbTransaction transaction, ref string cError);
        List<clsFormulario> ObtenerFormulariosPorUsuarioEstadoPaginado(clsSolicitudFormularioEstado frm, ref string cError);
        int ObtenerCantidadFormulariosPorUsuarioEstado(clsSolicitudFormularioEstado frm, ref string cError);
        int ObtenerCantidadFormulariosActivar(clsFormulario clsFormulario, ref string cError);
        List<clsFormulario> ObtenerFormulariosActivar(clsFormulario clsFormulario, int nPagina, int nTamaño, ref string cError);
    }
}
