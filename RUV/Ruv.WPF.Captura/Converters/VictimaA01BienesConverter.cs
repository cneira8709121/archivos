using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{
  /// <summary>
  /// Para una víctima en el anexo 01, retorna la lista de los bienes afectados.
  /// </summary>
    class VictimaA01BienesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            //string parametro = System.Convert.ToString(parameter);
            //if (string.IsNullOrWhiteSpace(parametro)) return null;

            var Bienes = value as ObservableCollection<clsAnexo01_Victima_Bien>;
            if (Bienes == null) return null;

            var Resultado = new List<Tuple<string, string>>();

            foreach (var Bien in Bienes)
            {
                var Tipo = RUV.I.InfoGeneral.ListaParametros
                      .FirstOrDefault(x => System.Convert.ToInt32(x.Id) == Bien.TipoBien.Value);
                string TipoTexto = Tipo == null ? "" : Tipo.Nombre;

                var Calidad = RUV.I.InfoGeneral.ListaParametros
                      .FirstOrDefault(x => System.Convert.ToInt32(x.Id) == Bien.CalidadDeLaVictima);
                string CalidadTexto = Calidad == null ? "" : Calidad.Nombre;

                var UnItem = new Tuple<string, string>(
                  String.Format("{0}: {1}", TipoTexto, Bien.Descripcion),
                  CalidadTexto);

                Resultado.Add(UnItem);
            }

            return Resultado;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
