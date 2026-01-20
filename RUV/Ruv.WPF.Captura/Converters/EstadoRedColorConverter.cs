using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Ruv.WPF.Captura.Converters
{
    class EstadoRedColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string Parametro = parameter == null ? "color" : System.Convert.ToString(parameter).ToLower();

            switch (Parametro)
            {
                case "descripcion":
                    string Mensaje = null;

                    if (value == null)
                        Mensaje = "Estado desconocido";
                    else
                        switch ((eEstadoRed)value)
                        {
                            case eEstadoRed.NoDisponible:
                                Mensaje = "Sin red";
                                break;
                            case eEstadoRed.EnProcesoDeVerificacion:
                                Mensaje = "Verificando acceso";
                                break;
                            case eEstadoRed.Disponible:
                                Mensaje = "Red disponible";
                                break;
                        }

                    return Mensaje;

                case "color":
                    SolidColorBrush Output = new SolidColorBrush();

                    if (value == null)
                        Output.Color = Colors.Black;
                    else
                        switch ((eEstadoRed)value)
                        {
                            case eEstadoRed.NoDisponible:
                                Output.Color = Colors.Red;
                                break;
                            case eEstadoRed.EnProcesoDeVerificacion:
                                Output.Color = Colors.Yellow;
                                break;
                            case eEstadoRed.Disponible:
                                Output.Color = Colors.LightGreen;
                                break;
                        }

                    return Output;

                default:
                    return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
