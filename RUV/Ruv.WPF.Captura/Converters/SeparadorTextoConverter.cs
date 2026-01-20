using System;
using System.Linq;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Separa un texto en base a sus letras mayúsculas.
    /// </summary>
    public class SeparadorTextoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            if (parameter != null)
            {
                string Param = System.Convert.ToString(parameter).ToLower();
                if (Param == "estadoproceso")
                {
                    // Se espera un int que equivale a un valor en la enumeración eEstadoProceso.
                    int Id = System.Convert.ToInt32(value);
                    eEstadoProcesoCola EP = eEstadoProcesoCola.Ninguno;
                    Enum.TryParse<eEstadoProcesoCola>(Id.ToString(), out EP);
                    value = EP.ToString();
                }
            }

            string Texto = System.Convert.ToString(value);
            const string Mayusculas = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÚÍÓÄËÏÜÖ";

            for (int i = Texto.Length - 1; i > 0; i--)
                if (Mayusculas.Contains(Texto[i]))
                    Texto = Texto.Insert(i, " ");

            return Texto;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
