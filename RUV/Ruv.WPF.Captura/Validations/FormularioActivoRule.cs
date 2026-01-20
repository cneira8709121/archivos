using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;

namespace Ruv.WPF.Captura.Validations
{
    public class FormularioActivoRule : ValidationRule
    {
        #region Public methods

        #region Methods overrided

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((bool)value) return new ValidationResult(false, Errores.FormularioYaInactivo);
            return new ValidationResult(true, null);
        }

        #endregion

        #endregion
    }
}
