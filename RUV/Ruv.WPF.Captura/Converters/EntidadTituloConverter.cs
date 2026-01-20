using System;
using System.Windows.Data;
using Ruv.WPF.Captura.Registro.Secciones;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Para el control dado, se determina el título. 
    /// Sólo apara anexos y hojas.
    /// </summary>
    class EntidadTituloConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Objeto = value as ISeccionRegistro;
            if (Objeto == null) return null;

            var Tipo = Objeto.Seccion.ToString();
            var Numero = System.Convert.ToInt32(Tipo.Substring(1, 2)).ToString();
            if (Tipo.StartsWith("A"))
                return "ANEXO " + Numero;
            else
                return "HOJA " + Numero;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
