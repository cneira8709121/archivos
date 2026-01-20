using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruv.Business.DTO.Correcciones;
using Ruv.Business.DTO.Reporteador;
using u = Ruv.Infrastructure.Crosscutting.Utilities;
using b = Ruv.Business.Correcciones;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Correcciones;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Reporteador;

/// <summary>
/// Summary description for CorreccionesService
/// </summary>
public class CorreccionesService : ICorreccionesService
{
    #region Public methods

    #region Services implementation

    public bool SolicitarCorreccion(int IdRegPersona, int idUsuarioSolicita, IList<clsCorreccion> correcciones, ref string cError)
    {
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        return iCorrecciones.SolicitarCorreccion(IdRegPersona, idUsuarioSolicita, correcciones, ref cError);
    }

    public int SolicitarCorreccionOut(int IdRegPersona, int idUsuarioSolicita, IList<clsCorreccion> correcciones, ref string cError)
    {
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        return iCorrecciones.SolicitarCorreccionOut(IdRegPersona, idUsuarioSolicita, correcciones, ref cError);
    }

    public List<entidad::clsCargaDatosCorreccion> CargaDatosCorreccion(int IdRegPersona, ref string cError)
    {
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        List<entidad::clsCargaDatosCorreccion> listadatos = iCorrecciones.CargaDatosCorreccion(IdRegPersona, ref cError);
        if (!string.IsNullOrEmpty(cError))
        {
            RegistroTraza.I.Registrar(new Exception(cError));
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return listadatos;
    }

    public entidad::clsCargaDatosCorreccion ConsultarCorreccion(int idCorreccion, ref string cError)
    {
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        entidad::clsCargaDatosCorreccion correccion = iCorrecciones.ConsultarCorreccion(idCorreccion, ref cError);
        if (cError != string.Empty)
        {
            RegistroTraza.I.Registrar(new Exception(cError));
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return correccion;
    }

    public bool RechazarCorreccion(int idCorreccion, int idUsuarioRechaza, string observaciones, ref string cError)
    {
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        bool respuesta = iCorrecciones.RechazarCorreccion(idCorreccion, idUsuarioRechaza, observaciones, ref cError);
        if (!respuesta && cError != string.Empty)
        {
            RegistroTraza.I.Registrar(new Exception(cError));
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return respuesta;
    }

    public int ConsultarEstadoDeclaracionConteo(clsConsultarEstadoDeclaracionSolicitud cesPersona, ref string cError)
    {
        if (cesPersona == null) return 0;
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        return iCorrecciones.ConsultarEstadoDeclaracionConteo(new clsDeclarante
        {
            CNumeroDocumento = cesPersona.CNumeroDocumento,
            CPrimerNombre = cesPersona.CPrimerNombre,
            CPrimerApellido = cesPersona.CPrimerApellido,
            CNumeroFormulario = cesPersona.CNumeroFormulario
        }, ref cError);
    }

    public clsConsultarEstadoDeclaracionRespuesta ConsultarEstadoDeclaracionPaginado(clsConsultarEstadoDeclaracionSolicitud cesPersona, int numeroPagina, int registrosPorPagina, ref string cError)
    {
        if (cesPersona == null) return null;
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        List<clsDeclarante> lstPersona = iCorrecciones.ConsultarEstadoDeclaracion(new clsDeclarante
        {
            CNumeroDocumento = cesPersona.CNumeroDocumento,
            CPrimerNombre = cesPersona.CPrimerNombre,
            CPrimerApellido = cesPersona.CPrimerApellido,
            CNumeroFormulario = cesPersona.CNumeroFormulario
        }, numeroPagina, registrosPorPagina, ref cError);
        return lstPersona == null ? null : new clsConsultarEstadoDeclaracionRespuesta()
        {
            LstEstadoDeclaracion = lstPersona.Select(x => new EstadoDeclaracion
            {
                CDepartamento = x.CDepartamento,
                CEstadoProceso = x.CEstadoProceso,
                CMunicipio = x.CMunicipio,
                CNombresApellidos = string.Format("{0} {1} {2} {3}", new string[] { x.CPrimerNombre, x.CSegundoNombre, x.CPrimerApellido, x.CSegundoApellido }),
                CNumeroDocumento = x.CNumeroDocumento,
                CNumeroFormulario = x.CNumeroFormulario,
                CPais = x.CPais,
                CTipoDocumento = x.CTipoDocumento,
                DDeclaracion = x.DDeclaracion,
                NIdDeclaracion = x.NIdDeclaracion,
                NIdRegistroPresona = x.NIdRegistroPresona
            }).ToList()
        };
    }

    public bool AprobarCorreccion(int idCorreccion, int idUsuarioAprueba, string observaciones, ref string cError)
    {
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        return iCorrecciones.AprobarCorreccion(idCorreccion, idUsuarioAprueba, observaciones, ref cError);
    }

    public IList<entidad::clsCorreccion> ConsultarCamposCorreccion(int idCorreccion, ref string cError)
    {
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        IList<entidad::clsCorreccion> listCamposcorreccion = iCorrecciones.ConsultarCamposCorreccion(idCorreccion, ref cError);
        if (cError != string.Empty)
        {
            RegistroTraza.I.Registrar(new Exception(cError));
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(cError));
            //clsLog Log = new clsLog();
            //Log.Registrar(cError.ToString());
            RegistroTraza.I.Registrar(cError.ToString());
        }

        return listCamposcorreccion;
    }

    public string ObtieneNombreSubEtnia(int nIdSubetnia, ref string cError)
    {
        b::Contratos.ICorreccionesBusiness iCorrecciones = (b::Contratos.ICorreccionesBusiness)u::Spring.GetService(Objetos.CorreccionesBusiness);
        return iCorrecciones.ObtienenombreSubEtnia(nIdSubetnia, ref cError);
    }
    #endregion

    #endregion
}