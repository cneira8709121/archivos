using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{
  /// <summary>
  /// Retorna la lista de las víctimas para una anexo dado.
  /// En value se obtiene el objeto de datos del anexo.
  /// </summary>
    class ListaVictimasConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            int NumeroAnexo = System.Convert.ToInt32(parameter);
            object Resultado = null;

            // Anexos 5 y 11 manejan varias listas de víctimas.
            object Lista = null;
            if (NumeroAnexo == 1) Lista = (ObservableCollection<clsAnexo01_Victima>)value;
            if (NumeroAnexo == 2) Lista = (ObservableCollection<clsAnexo02_Victima>)value;
            if (NumeroAnexo == 3) Lista = (ObservableCollection<clsAnexo03_Victima>)value;
            if (NumeroAnexo == 4) Lista = (ObservableCollection<clsAnexo04_Victima>)value;
            if (NumeroAnexo == 5) Lista = (ObservableCollection<clsAnexo05_Victima>)value;
            if (NumeroAnexo == 6) Lista = (ObservableCollection<clsAnexo06_Victima>)value;
            if (NumeroAnexo == 7) Lista = (ObservableCollection<clsAnexo07_Victima>)value;
            if (NumeroAnexo == 8) Lista = (ObservableCollection<clsAnexo08_Victima>)value;
            if (NumeroAnexo == 9) Lista = (ObservableCollection<clsAnexo09_Victima>)value;
            if (NumeroAnexo == 10) Lista = (ObservableCollection<clsAnexo10_Victima>)value;

            if (NumeroAnexo == 1) Resultado = ObtenerVictimas<clsAnexo01_Victima>(Lista as ObservableCollection<clsAnexo01_Victima>);
            if (NumeroAnexo == 2) Resultado = ObtenerVictimas<clsAnexo02_Victima>(Lista as ObservableCollection<clsAnexo02_Victima>);
            if (NumeroAnexo == 3) Resultado = ObtenerVictimas<clsAnexo03_Victima>(Lista as ObservableCollection<clsAnexo03_Victima>);
            if (NumeroAnexo == 4) Resultado = ObtenerVictimas<clsAnexo04_Victima>(Lista as ObservableCollection<clsAnexo04_Victima>);
            if (NumeroAnexo == 5) Resultado = ObtenerVictimas<clsAnexo05_Victima>(Lista as ObservableCollection<clsAnexo05_Victima>);
            if (NumeroAnexo == 6) Resultado = ObtenerVictimas<clsAnexo06_Victima>(Lista as ObservableCollection<clsAnexo06_Victima>);
            if (NumeroAnexo == 7) Resultado = ObtenerVictimas<clsAnexo07_Victima>(Lista as ObservableCollection<clsAnexo07_Victima>);
            if (NumeroAnexo == 8) Resultado = ObtenerVictimas<clsAnexo08_Victima>(Lista as ObservableCollection<clsAnexo08_Victima>);
            if (NumeroAnexo == 9) Resultado = ObtenerVictimas<clsAnexo09_Victima>(Lista as ObservableCollection<clsAnexo09_Victima>);
            if (NumeroAnexo == 10) Resultado = ObtenerVictimas<clsAnexo10_Victima>(Lista as ObservableCollection<clsAnexo10_Victima>);

            return Resultado;
        }

        object ObtenerVictimas<T1>(ObservableCollection<T1> lista) where T1 : class
        {
            var Salida = from vic in lista
                         where (vic as clsEntidadBase).EstadoRegistro != eEstadoRegistro.Eliminado
                         join per in RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas
                         on (vic as IVictima).PersonaAfectadaId equals per.ID
                         orderby per.NombreCompleto
                         select per;
            return Salida;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
