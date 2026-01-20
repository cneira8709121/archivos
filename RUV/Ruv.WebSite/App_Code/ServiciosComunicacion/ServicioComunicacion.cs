using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion;
using Ruv.Business.ServiciosComunicacion.Contratos;
using System.ServiceModel.Web;
using System.Net;
using System.Collections.Specialized;

[AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Required)]

public class ServicioComunicacion : IServicioComunicacion
{
    public IList<Persona> ObtenerPersonas(string token, int pagina, int tamano)
    {
        if (ValidToken(token))
        {
            IOperacionesBusiness iOperacionesBusiness = (IOperacionesBusiness)new Ruv.Business.ServiciosComunicacion.OperacionesBusiness();
            return iOperacionesBusiness.ObtenerPersonas(pagina, tamano);
        }
        else
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
            return null;
        }
    }

    public Persona Persona()
    {
        string token = "";
        string ID = "10000006";

        //return new Persona()
        //{
        //    Id = 10006,
        //    PrimerApellido = "Pelaez",
        //    SegundoApellido = "Casallas",
        //    PrimerNombre = "Juan",
        //    FechaNacimiento = DateTime.Now,
        //    NumeroDocumento = "79672150",
        //    SegundoNombre = "Carlos"
        //};

        if (ValidToken(token))
        {
            IOperacionesBusiness iOperacionesBusiness = (IOperacionesBusiness)new Ruv.Business.ServiciosComunicacion.OperacionesBusiness();
            return iOperacionesBusiness.ObtenerPersonaPorId(int.Parse(ID));
        }
        else
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
            return null;
        }
    }

    public Persona ObtenerPersonaPorDocumento(string token, string documento)
    {
        if (ValidToken(token))
        {
            IOperacionesBusiness iOperacionesBusiness = (IOperacionesBusiness)new Ruv.Business.ServiciosComunicacion.OperacionesBusiness();
            return iOperacionesBusiness.ObtenerPersonaPorDocumento(documento);
        }
        else
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
            return null;
        }
    }

    public IList<Siniestro> ObtenerSiniestrosPorIdPersona(string token, string ID)
    {
        if (ValidToken(token))
        {
            IOperacionesBusiness iOperacionesBusiness = (IOperacionesBusiness)new Ruv.Business.ServiciosComunicacion.OperacionesBusiness();
            return iOperacionesBusiness.ObtenerSiniestrosPorIdPersona(int.Parse(ID));
        }
        else
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
            return null;
        }
    }

    public IList<GrupoFamiliar> ObtenerGrupoFamiliar(string token, string ID)
    {
        if (ValidToken(token))
        {
            IOperacionesBusiness iOperacionesBusiness = (IOperacionesBusiness)new Ruv.Business.ServiciosComunicacion.OperacionesBusiness();
            return iOperacionesBusiness.ObtenerGrupoFamiliar(int.Parse(ID));
        }
        else
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
            return null;
        }
    }

    #region Private Methods

    //private static bool Authenticate(IncomingWebRequestContext context)
    //{
    //    bool Authenticated = false;
    //    string normalizedUrl;
    //    string normalizedRequestParameters;
    //    //context.Headers
    //    NameValueCollection pa = context.UriTemplateMatch.QueryParameters;
    //    if (pa != null && pa["oauth_consumer_key"] != null)
    //    {
    //        // to get uri without oauth parameters
    //        string uri = context.UriTemplateMatch.RequestUri.OriginalString.Replace
    //            (context.UriTemplateMatch.RequestUri.Query, "");
    //        string consumersecret = "suryabhai";
    //        OAuthBase oauth = new OAuthBase();
    //        string hash = oauth.GenerateSignature(
    //            new Uri(uri),
    //            pa["oauth_consumer_key"],
    //            consumersecret,
    //            null, // totken
    //            null, //token secret
    //            "GET",
    //            pa["oauth_timestamp"],
    //            pa["oauth_nonce"],
    //            out normalizedUrl,
    //            out normalizedRequestParameters
    //            );
    //        Authenticated = pa["oauth_signature"] == hash;
    //    }
    //    return Authenticated;
    //}

    private static bool ValidToken(string token)
    {
        //TODO: Realizar la validación del token
        return true;
    }

    #endregion Private Methods
}
