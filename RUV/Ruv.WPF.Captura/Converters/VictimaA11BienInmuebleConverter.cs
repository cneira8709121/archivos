using System;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Converters
{
  /// <summary>
  /// Para una víctima en el anexo 11, retorna información del bien inmueble.
  /// </summary>
    class VictimaA11BienInmuebleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            string parametro = System.Convert.ToString(parameter);
            if (string.IsNullOrWhiteSpace(parametro)) return null;

            string Resultado = null;
            var BInmueble = value as clsAnexo11_BienInmueble;
            if (BInmueble == null) return null;

            //NESTOR: Arreglar esto.

            switch (parametro.ToUpper())
            {
                case "ENTORNO":
                    //var Poblacion = Sipod.I.InfoGeneral.ListaPoblaciones
                    //  .FirstOrDefault(x => x.Key == BInmueble.EntornoId
                    //  && BInmueble.EntornoId != null);

                    //if (Poblacion == null)
                    //  Resultado = eTipoPoblacion.Urbano_Barrio.ToString();
                    //else
                    //  Resultado = Sipod.I.InfoGeneral.ListaTiposPoblaciones
                    //    .FirstOrDefault(x => x.Id == System.Convert.ToInt32(Poblacion.LazyValue.Value.TipoPoblacion))
                    //    .Nombre;
                    break;

                case "NOMBREENTORNO":
                    //var NombreEntorno = Sipod.I.InfoGeneral.ListaPoblaciones
                    //  .FirstOrDefault(x => x.Key == BInmueble.EntornoId
                    //  && BInmueble.EntornoId != null);

                    //if (NombreEntorno == null)
                    //{
                    //  // Se asume que el entorno es "Otro".
                    //  Resultado = BInmueble.EntornoOtro;
                    //}
                    //else
                    //  Resultado = NombreEntorno.LazyValue.Value.Nombre;

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
