using System;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.WPF.Captura.Converters
{
  /// <summary>
  /// Para una persona afectada retorna la información de la etnia como un texto completo.
  /// </summary>
    class EtniaTextoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "";
            //var PA = value as clsPersonaAfectada;
            var PA = value as IPersonaAfectada;
            if (PA == null || !PA.PertenenciaEtnica.HasValue) return "";

            var Etnia = RUV.I.InfoGeneral.ListaEtnias
              .FirstOrDefault(x => x.Id == PA.PertenenciaEtnica.Value);

            clsComunidadEtnica CE1 = null;
            if (PA.ComunidadEtnica1.HasValue)
                CE1 = RUV.I.InfoGeneral.ListaComunidadesEtnicas
                  .FirstOrDefault(x => x.Id == PA.ComunidadEtnica1.Value);

            clsComunidadEtnica CE2 = null;
            if (PA.ComunidadEtnica2.HasValue)
                CE2 = RUV.I.InfoGeneral.ListaComunidadesEtnicas
                  .FirstOrDefault(x => x.Id == PA.ComunidadEtnica2.Value);

            string Resultado = Etnia.Nombre;
            if (CE1 != null)
            {
                var Grupo = RUV.I.InfoGeneral.ListaGruposEtnicos
                  .Where(x => x.EtniaId == Etnia.Id).ElementAt(0);
                Resultado += string.Format("\n{0}: {1}", Grupo.Nombre, CE1.Nombre);
            }
            if (CE2 != null)
            {
                var Grupo = RUV.I.InfoGeneral.ListaGruposEtnicos
                  .Where(x => x.EtniaId == Etnia.Id).ElementAt(1);
                Resultado += string.Format("\n{0}: {1}", Grupo.Nombre, CE2.Nombre);
            }

            return Resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
