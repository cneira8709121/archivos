using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.WPF.Captura.Infrastructure.Impresion;

namespace Ruv.WPF.Captura.Infrastructure.Configuracion
{
    public class clsConfiguracion
    {

        public clsConfiguracion()
        {
            ConfiguracionGeneral = new clsConfiguracionRUV();
            Ubicaciones = new clsConfiguracionUbicaciones();
        }
        public int Id { get; set; }
        private clsImpresion _Impresion;
        /// <summary>
        /// Toda la funcionalidad de impresión.
        /// </summary>
        public clsImpresion Impresion
        {
            get
            {
                if (_Impresion == null) _Impresion = new clsImpresion();
                return _Impresion;
            }
        }

        public clsConfiguracionRUV ConfiguracionGeneral { get; set; }
        public clsConfiguracionUbicaciones Ubicaciones { get; set; }

        /// <summary>
        /// Graba localmente esta configuración.
        /// </summary>
        public void Grabar()
        {
            RUV.I.LocalDB.Save<clsConfiguracion>(this);
            RUV.I.LocalDB.Flush();
        }
    }
}
