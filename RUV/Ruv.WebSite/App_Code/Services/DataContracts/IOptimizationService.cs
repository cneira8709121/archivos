using System.ServiceModel;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.Web.Services.DataContracts {

    [ServiceContract]
    public interface IOptimizationService {

        /// <summary>
        /// Obtiene una declaración a partir de su identificador
        /// </summary>
        /// <param name="id">Identificador de la declaración</param>
        /// <param name="tipoDeclaracion">Tipo de declaración</param>
        /// <returns><see cref="clsDeclaracion"/> con la información de la declaración</returns>
        [OperationContract]
        clsDeclaracion ObtenerDeclaracion(int id, string tipoDeclaracion);

    }

}