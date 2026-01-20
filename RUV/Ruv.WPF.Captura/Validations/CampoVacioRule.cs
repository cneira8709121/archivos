using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;

namespace Ruv.WPF.Captura.Validations
{
    public class CampoVacioRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, Errores.CampoVacio);
            return new ValidationResult(true, null);
        }
    }
}
