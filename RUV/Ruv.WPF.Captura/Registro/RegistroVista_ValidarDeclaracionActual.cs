using System.Windows.Controls;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Registro
{
    public partial class RegistroVista : Page
    {
        /// <summary>
        /// Realiza la validación de una declaración y abre la ventana de errores en caso de 
        /// existir errores.
        /// </summary>
        /// <returns></returns>
        eResultadoValidacion ValidarDeclaracion(clsDeclaracion declaracion)
        {
            return RUV.I.Util.ValidarDeclaracion(declaracion);
        }
    }
}