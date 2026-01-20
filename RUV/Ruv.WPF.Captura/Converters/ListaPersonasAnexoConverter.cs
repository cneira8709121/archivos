using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Retorna la lista de personas que estan asociadas para el anexo actual segun la relacion hecha en la hoja 2  
    /// </summary>
    public class ListaPersonasAnexoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            if (parameter == null) return null;                     
                                 
            var Lista = (ObservableCollection<clsPersonaAfectada>)value;

            return Lista;
            /* 20120217 Luis.Esteban
             * Alexander Holguin solicita quitar el filtro, debe salir la lista completa de personas en todos
             * los anexos aun cuando no este relacioados en la hoja 2 con el hecho victimizante
            eHechosVictimizantes Item;
            if (Enum.TryParse<eHechosVictimizantes>(parameter.ToString(), out Item))
            {
                var Resultado = from vic in Lista
                                where (vic as clsEntidadBase).EstadoRegistro != eEstadoRegistro.Eliminado
                                && (vic as clsPersonaAfectada).HechosVictimizantes.Contains((int)Item)
                                select vic;
                return Resultado;
            }
            else
                return null;            
             */
            
        }        

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
