using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Business.Radicacion.Contratos;
using Ruv.Business.DTO.Radicacion;
using util = Ruv.Infrastructure.Crosscutting.Utilities;
using com = Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using System.ServiceModel.Activation;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class RadicacionService : IRadicacionService
{
    #region Public methods

    #region Services implementation

    public com::LiderRadicacion.clsLiderRadicacion CargarDatos(long nIdDeclaracion, ref string cError)
	{
        ILiderRadicacion iLiderRadicacion = (ILiderRadicacion)util::Spring.GetService(Objetos.LiderRadicacionBusiness);
        clsLiderRadicacion lRadicacion = iLiderRadicacion.CargarDatos(nIdDeclaracion, ref cError);
        if (lRadicacion == null || !string.IsNullOrEmpty(cError)) return null;

        CriticaNService objCritica = new CriticaNService();

        // Se obtiene el documento de la radicación actual
        string cNombreDocumentoRadActual = string.Empty;
        byte[] bDocumentoRadActual = objCritica.ObtenerImagenRadicacion(lRadicacion.RadActual.NId.Value, ref cNombreDocumentoRadActual, ref cError);

        // Se obtiene el docuento de la radicación existente
        string cNombreDocumentoRadExistente = string.Empty;
        byte[] bDocumentoRadExistente = null;
        if (lRadicacion.RadExistente != null)
        {
            bDocumentoRadExistente = objCritica.ObtenerImagenRadicacion(lRadicacion.RadActual.NId.Value, ref cNombreDocumentoRadExistente, ref cError);
        }

        return new com::LiderRadicacion.clsLiderRadicacion
        {
            RadActual = new com::clsRadicacion
            {
                ID = lRadicacion.RadActual.NId,
                FECHALLEGADA = lRadicacion.RadActual.DLlegada,
                PARAM_RESULTADO_VALIDACION = lRadicacion.RadActual.NTipoError,
                ID_PAIS = lRadicacion.RadActual.NIdPais,
                ID_DEPARTAMENTO = lRadicacion.RadActual.NIdDepartamento,
                ID_MUNICIPIO = lRadicacion.RadActual.NIdMunicipio,
                ID_ENTIDADMUNICIPIO = lRadicacion.RadActual.NIdEntidad,
                ID_TIPORADICACION = lRadicacion.RadActual.NTipoRadicacion,
                NRO_FORMULARIO = lRadicacion.RadActual.CNumeroFormulario,
                PrimerNombre = lRadicacion.RadActual.CPrimerNombre,
                PrimerApellido = lRadicacion.RadActual.CPrimerApellido,
                SegundoApellido = lRadicacion.RadActual.CSegundoApellido,
                SegundoNombre = lRadicacion.RadActual.CSegundoNombre,
                TipoDocumento = lRadicacion.RadActual.NTipoDocumento,
                NumeroDocumento = lRadicacion.RadActual.CNumeroDocumento,
                OBSERVACIONES = lRadicacion.RadActual.CObservaciones,
                RUTAIMAGEN = cNombreDocumentoRadActual,
                DocumentoDigital = bDocumentoRadActual
            },
            RadExistente = lRadicacion.RadExistente == null ? null : new com::clsRadicacion
            {
                ID = lRadicacion.RadExistente.NId,
                FECHALLEGADA = lRadicacion.RadExistente.DLlegada,
                PARAM_RESULTADO_VALIDACION = lRadicacion.RadExistente.NTipoError,
                ID_PAIS = lRadicacion.RadExistente.NIdPais,
                ID_DEPARTAMENTO = lRadicacion.RadExistente.NIdDepartamento,
                ID_MUNICIPIO = lRadicacion.RadExistente.NIdMunicipio,
                ID_ENTIDADMUNICIPIO = lRadicacion.RadExistente.NIdEntidad,
                ID_TIPORADICACION = lRadicacion.RadExistente.NTipoRadicacion,
                NRO_FORMULARIO = lRadicacion.RadExistente.CNumeroFormulario,
                PrimerNombre = lRadicacion.RadExistente.CPrimerNombre,
                PrimerApellido = lRadicacion.RadExistente.CPrimerApellido,
                SegundoApellido = lRadicacion.RadExistente.CSegundoApellido,
                SegundoNombre = lRadicacion.RadExistente.CSegundoNombre,
                TipoDocumento = lRadicacion.RadExistente.NTipoDocumento,
                NumeroDocumento = lRadicacion.RadExistente.CNumeroDocumento,
                OBSERVACIONES = lRadicacion.RadExistente.CObservaciones,
                RUTAIMAGEN = cNombreDocumentoRadExistente,
                DocumentoDigital = bDocumentoRadExistente
            },
            FrmRadicacionPrevia = lRadicacion.FrmRadicacionPrevia == null ? null : new com::GestionFormulario.clsFormulario
            {
                NId = lRadicacion.FrmRadicacionPrevia.NId,
                NIdDepartamento = lRadicacion.FrmRadicacionPrevia.NIdDepartamento,
                NIdMunicipio = lRadicacion.FrmRadicacionPrevia.NIdMunicipio,
                NIdEntidad = lRadicacion.FrmRadicacionPrevia.NIdEntidad,
                NIdPais = lRadicacion.FrmRadicacionPrevia.NIdPais,
                NIdUsuario = lRadicacion.FrmRadicacionPrevia.NIdUsuario,
                EfId = (Ruv.Infrastructure.Crosscutting.Common.eEstadoFormulario)lRadicacion.FrmRadicacionPrevia.NIdEstado
            }
        };
    }

    public bool ActualizarRadicacion(com::clsRadicacion rad, string cObservaciones, ref string cError)
    {
        ILiderRadicacion iLiderRadicacion = (ILiderRadicacion)util::Spring.GetService(Objetos.LiderRadicacionBusiness);
        return iLiderRadicacion.ActualizarRadicacion(new clsRadicacion
        {
            NId = rad.ID,
            NIdMunicipio = rad.ID_MUNICIPIO,
            NIdEntidad = rad.ID_ENTIDADMUNICIPIO,
            NIdUsuarioRadica = rad.ID_USUARIO_RADICA,
            NTipoRadicacion = rad.ID_TIPORADICACION,
            NTipoError = rad.PARAM_RESULTADO_VALIDACION,
            CObservaciones = rad.OBSERVACIONES,
            CRutaImagen = rad.RUTAIMAGEN,
            CNumeroFormulario = rad.NRO_FORMULARIO
        }, cObservaciones, ref cError);
    }

    public Int32 RadicarDevolucion(com::clsRadicacion rad, ref string cError)
    {
        IRadicacionDevolucion iRadicacionDevolucion = (IRadicacionDevolucion)new Ruv.Business.Radicacion.RadicacionDevolucion();
        return iRadicacionDevolucion.RadicarDevolucion(new clsRadicacion
        {
            NIdUsuarioRadica = rad.ID_USUARIO_RADICA,
            CObservaciones = rad.OBSERVACIONES,
            CNumeroFormulario = rad.NRO_FORMULARIO,
            DLlegada = rad.FECHALLEGADA.Value
        }, ref cError);
    }

    #endregion

    #endregion
}
