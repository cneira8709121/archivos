using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.WPF.Server
{
  /// <summary>
  /// Provee información de caracter general.
  /// </summary>
    public class clsGeneral
    {

        /// <summary>
        /// Obtiene la lista de los parámetros generales.
        /// </summary>
        /// <returns></returns>
        public clsDatosGenerales ObtenerParametrosGenerales()
        {
            Ruv.WPF.Data.clsGeneral Gen = new Ruv.WPF.Data.clsGeneral();
            return Gen.ObtenerParametrosGenerales();
        }
    }
}
