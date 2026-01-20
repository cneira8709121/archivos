using System;
using System.ServiceModel.Activation;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Web.Services.DataContracts;

namespace Ruv.Web.Services {

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class OptimizationService : IOptimizationService {

        /// <summary>
        /// Obtiene una declaración a partir de su identificador
        /// </summary>
        /// <param name="id">Identificador de la declaración</param>
        /// <param name="tipoDeclaracion">Tipo de declaración</param>
        /// <returns><see cref="clsDeclaracion"/> con la información de la declaración</returns>
        public clsDeclaracion ObtenerDeclaracion(int id, string tipoDeclaracion)
        {
            clsSeguridad Seguridad = new clsSeguridad();
            if (!Seguridad.CredencialesValidas(tipoDeclaracion)) return null;
            clsDeclaracion Resultado = null;
            try
            {
                Ruv.Business.Captura.Procesos Pro = new Ruv.Business.Captura.Procesos();
                Resultado = Pro.ObtenerDeclaracion(id);
                string errorFile = string.Empty;
                string nombreArchivo = string.Empty;
                CriticaNService objCritica = new CriticaNService();
                if (!Resultado.RadicacionId.HasValue)
                {
                    RegistroTraza.I.Registrar(string.Format("La declaración solicitada ({0}) no contiene información de radicación.", "Declaracion ID: " + id));
                    throw new InvalidOperationException(string.Format("La declaración solicitada ({0}) no contiene información de radicación.", "Declaracion ID: " + id));
                }
                Resultado.DocumentoDigital = objCritica.ObtenerImagenRadicacion(Resultado.RadicacionId.Value, ref nombreArchivo, ref errorFile);

                if (Resultado.DocumentoDigital != null)
                    Resultado.DocumentoDigitalNombre = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(nombreArchivo);
                else
                    //clsLog.Registrar(new InvalidOperationException(string.Format("No se pudo encontrar el documento digital asociado a la radicación solicitada ({0}).", "Radicacion ID: " + Resultado.RadicacionId.Value))); // Registrar el hecho que no hay imagen
                    RegistroTraza.I.Registrar(new InvalidOperationException(string.Format("No se pudo encontrar el documento digital asociado a la radicación solicitada ({0}).", "Radicacion ID: " + Resultado.RadicacionId.Value))); // Registrar el hecho que no hay imagen
            }
            catch (Exception ex)
            {
                //clsLog.Registrar(ex);
                RegistroTraza.I.Registrar(ex);
                throw ex;
            }

            return Resultado;
        }

    }

}