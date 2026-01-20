using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

[ServiceContract]
public interface IControlDocumentosService
{
    [OperationContract]
    List<clsFormulario> GenerarFormularios(uint nCantidad,
                                           string cSerie,
                                           int nIdUsuario,
                                           int nIdEstado,
                                           int? nIdPais,
                                           int? nIdDepartamento,
                                           int? nIdMunicipio,
                                           int? nIdEntidadmunicipio,
                                           ref string cError);
    [OperationContract]
    List<clsFormulario> GenerarFormulariosWEB(uint nCantidad,
                                                     string cSerie,
                                                     int nIdUsuario,
                                                     int nIdEstado,
                                                     int? nIdEntidadmunicipio,
                                                     ref string cError);
    [OperationContract]
    int ObtenerPaisGeneraFormularioWEB(int? nIdEntidadmunicipio, ref string cError);
    [OperationContract]
    List<clsFormulario> ListarFormularios(ref string cError);
    [OperationContract]
    List<clsFormulario> ListarFormulariosNoRadicados(clsFormularioSolicitudNoRadicados frmSolicitud, ref string cError);
    [OperationContract]
    List<clsFormulario> ListarFormulariosPorEstado(ushort nIdEstado, ref string cError);
    [OperationContract]
    void AsignarFormulario(List<clsFormulario> FormulariosEnviar, ref string cError);
    [OperationContract]
    bool AsignarFormularioFiltro(clsSolicitudFormularioEstado frm, ref string cError);
    [OperationContract]
    uint? InactivarFormulario(uint nIdFormulario, string observacion, ref string cError);
    [OperationContract]
    void SepararFormularioImprenta(IEnumerable<clsSeparacionFormularioSolicitud> frmFormularioASeparar, ref string cError);
    [OperationContract]
    List<clsSeparacionFormularioSolicitud> SepararFormularioImprentaFiltro(clsSolicitudFormularioEstado frm, ref string cError);
    [OperationContract]
    List<clsFormulario> ObtenerFormulariosPorUsuario(int nIdUsuario, ref string cError);
    [OperationContract]
    uint? MarcarDescargado(uint nIdFormulario, ref string cError);
    [OperationContract]
    bool MarcarRadicado(string cNumeroFormulario, ref string cError);
    [OperationContract]
    eResultadoValidacionRadicacion ValidarNumeroFormulario(clsRadicacion radicacion);
    [OperationContract]
    List<clsFormulario> ObtenerFormulariosPorUsuarioEstadoPaginado(clsSolicitudFormularioEstado frm, ref string cError);
    [OperationContract]
    int ObtenerCantidadFormulariosPorUsuarioEstado(clsSolicitudFormularioEstado frm, ref string cError);
    [OperationContract]
    int ObtenerCantidadFormulariosActivar(clsFormularioSolicitudNoRadicados Solicitud, ref string cError);
    [OperationContract]
    List<clsFormulario> ObtenerFormulariosActivar(clsFormularioSolicitudNoRadicados Solicitud, int nPagina, int nTamaño, ref string cError);
    [OperationContract]
    clsFormulario ObtenerFormulario(string cNumeroFormulario, ref string cError);
}
