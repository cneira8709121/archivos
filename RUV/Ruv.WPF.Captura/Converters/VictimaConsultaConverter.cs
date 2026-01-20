using System;
using System.Linq;
using System.Windows.Data;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Para una víctima de un anexo retorna el dato indicado.
    /// </summary>
    class VictimaConsultaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            string parametro = System.Convert.ToString(parameter);
            if (string.IsNullOrWhiteSpace(parametro)) return null;

            int? PersonaAfectadaId = (int?)value;
            if (!PersonaAfectadaId.HasValue) return null;

            var Persona = RUV.I.Configuraciones.Impresion.DeclaracionEnImpresion.PersonasAfectadas.ListaPersonas
              .FirstOrDefault(x => x.ID == PersonaAfectadaId);
            if (Persona == null) return null;

            string Resultado = null;
            switch (parametro.ToLower())
            {
                case "numeroconsecutivo":
                    Resultado = Persona.NumeroConsecutivo.ToString();
                    break;

                case "nombrecompleto":
                    Resultado = Persona.NombreCompleto.ToString();
                    break;
            }

            return Resultado;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
