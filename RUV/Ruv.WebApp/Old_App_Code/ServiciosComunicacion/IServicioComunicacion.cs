using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion;
using System.ServiceModel.Web;

[ServiceContract]
public interface IServicioComunicacion
{
    [OperationContract(Name = "ObtenerPersonas")]
    [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Personas/{token}/{pagina}/{tamano}")]
    IList<Persona> ObtenerPersonas(string token, int pagina, int tamano);

    [OperationContract(Name = "Persona")]
    [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Persona/")]
    Persona Persona();

    [OperationContract(Name = "ObtenerPersonaPorDocumento")]
    [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Persona/{token}/{documento}")]
    Persona ObtenerPersonaPorDocumento(string token, string documento);

    [OperationContract(Name = "ObtenerSiniestrosPorIdPersona")]
    [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Siniestro/{token}/{ID}")]
    IList<Siniestro> ObtenerSiniestrosPorIdPersona(string token, string ID);

    [OperationContract(Name = "ObtenerGrupoFamiliar")]
    [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/GrupoFamiliar/{token}/{ID}")]
    IList<GrupoFamiliar> ObtenerGrupoFamiliar(string token, string ID);
}
