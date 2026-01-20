using System.Collections.Generic;
using System.Windows.Controls;

namespace Ruv.WPF.Captura.Impresion
{
    /// <summary>
    /// Contiene la información de configuración para la impresión de una seccción.
    /// </summary>
    public class clsInformacionImpresion
    {
        /// <summary>
        /// El tipo de contenido a insertar en la página.
        /// </summary>
        public eTipoContenido TipoContenido { get; set; }
        /// <summary>
        /// La orientación del papel.
        /// </summary>
        public eOrientacionPapel OrientacionPapel { get; set; }
        /// <summary>
        /// La lista de los encabezados que deben estar en todas las páginas.
        /// </summary>
        public List<UserControl> Encabezados { get; set; }
        /// <summary>
        /// El objeto que se quiere insertar en el papel (no incluye encabezados).
        /// </summary>
        public object ObjetoCuerpo { get; set; }

    }
}
