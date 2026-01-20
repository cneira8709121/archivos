using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using u = Ruv.Infrastructure.Crosscutting.Utilities;
using b = Ruv.Business.CriticaN;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.CriticaN;
using dto = Ruv.Business.DTO.CriticaN;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class CriticaNService : ICriticaNService
{
    #region Public methods

    #region Services implementation

    public byte[] ObtenerImagenRadicacion(long nId, ref string cNombreImagen, ref string cError)
    {
        b::Contratos.ICriticaN iGestion = (b::Contratos.ICriticaN)u::Spring.GetService(Objetos.CriticaNBusiness);
        return iGestion.ObtenerImagenRadicacion(nId, ref cNombreImagen, ref cError);
    }


    public bool InsertaCriticaN(List<clsRespuestaCritica> lstRespuesta, ref string cError)
    {
        b::Contratos.ICriticaN iGestion = (b::Contratos.ICriticaN)u::Spring.GetService(Objetos.CriticaNBusiness);
        return iGestion.InsertaCriticaN(lstRespuesta.Select(rc => new dto::clsRespuestaCritica
        {
            NIdCriticaN = rc.NIdCriticaN,
            NRespuesta = rc.NRespuesta,
            NIdUsuario = rc.NIdUsuario,
            NIdRadicacion = rc.NIdRadicacion,
            CObservacion = rc.CObservacion
        }).ToList(), ref cError);
    }

    #endregion

    #endregion
}

