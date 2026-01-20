using System.ServiceModel;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Server
{
    public static class clsUtilsServer
    {
        /// <summary>
        /// Retorna un error generico que puede presentarse al momento de validar
        /// las credenciales de un usuario.
        /// </summary>
        /// <param name="errorType"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static FaultException GetGenericFault(eErrores errorType, string errorCode, string errorMessage)
        {
            return new FaultException(
                new FaultReason(errorMessage),
                new FaultCode(errorType.ToString(),
                  new FaultCode(errorCode.ToString())
                  ));
        }
    }
}
