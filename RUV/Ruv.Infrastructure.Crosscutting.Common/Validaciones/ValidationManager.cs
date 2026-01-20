using System.Collections.Generic;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.Infrastructure.Crosscutting.Common
{

    /// <summary>
    /// Mane
    /// </summary>
    public static class ValidationManager
    {

        /// <summary>
        /// Validates an entity property according to the configuration. Does not take into account the user role, since its purpose is to display potential error messages.
        /// </summary>
        /// <param name="validationConfiguration">Current validation configuration</param>
        /// <param name="scope">Scope for the entity</param>
        /// <param name="validation">Field for validation</param>
        /// <returns>true if the validation should be enforced, otherwise false</returns>
        public static bool ValidateProperty(IList<clsValidaciones> validationConfiguration, string scope, string validation)
        {
            foreach (var element in validationConfiguration)
            {
                if (element.NombreHoja.ToLowerInvariant().Trim() == scope.ToLowerInvariant().Trim() && element.Propiedad.ToLowerInvariant().Trim() == validation.ToLowerInvariant().Trim())
                {
                    //if(element.Valor != eEstadoValidacion.NoAplica)
                    return true;//element.Valor == eEstadoValidacion.Obligatoria || element.Valor == eEstadoValidacion.Flexible;
                }
            }
            return false; // Default enforce value for any validation not present in the configuration
        }

        /// <summary>
        /// Determines whether a form validation should be considered or not, using the role and current database configuration.
        /// Devuelve true si la validacion del parametro se debe aplicar. Si no, devuelve false
        /// </summary>
        /// <param name="currentUser">The current authenticated user. If null, the validation will enforce</param>
        /// <param name="validationConfiguration">Current offline validation configuration</param>
        /// <param name="scope">Scope of the validation to consider. Generally, the parent entity of the field.</param>
        /// <param name="validation">Name of the validation (field)</param>
        /// <returns>true if the validation should be enforced. Otherwise, false</returns>
        public static bool ValidateFromConfiguration(clsUsuario currentUser, IList<clsValidaciones> validationConfiguration, string scope, string validation, ref int skippedValidation) {
            if (currentUser == null) return true;
            else if (currentUser.Permisos.Contains(ePermisosUsuario.Requerir_Validaciones_Obligatorias)) {
                foreach (var element in validationConfiguration)
                {
                    if (element.NombreHoja.ToLowerInvariant().Trim() == scope.ToLowerInvariant().Trim() && element.Propiedad.ToLowerInvariant().Trim() == validation.ToLowerInvariant().Trim())
                    {
                        if (element.Valor == eEstadoValidacion.Flexible) skippedValidation++;
                        return element.Valor == eEstadoValidacion.Obligatoria;
                    }
                }
            }
            else {
                foreach (var element in validationConfiguration)
                {
                    if (element.NombreHoja.ToLowerInvariant().Trim() == scope.ToLowerInvariant().Trim() && element.Propiedad.ToLowerInvariant().Trim() == validation.ToLowerInvariant().Trim())
                    {
                        return element.Valor == eEstadoValidacion.Obligatoria || element.Valor == eEstadoValidacion.Flexible;
                    }
                }
            }
            return false; // Default enforce value for any validation not present in the configuration
        }

    }

}
