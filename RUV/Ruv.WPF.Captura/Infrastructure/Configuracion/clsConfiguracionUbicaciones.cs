using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ruv.WPF.Captura.Infrastructure.Configuracion
{
    public class clsConfiguracionUbicaciones
    {
        public string DestinoDescargas { get; set; }
        public string OrigenDeclaraciones { get; set; }

        public void EstablecerUbicaciones()
        {
            string directorio = Path.GetFullPath(RUV.I.Util.RutaArchivosLocales);
            if (!Directory.Exists(directorio))
                Directory.CreateDirectory(directorio);

            if (string.IsNullOrEmpty(DestinoDescargas))
                DestinoDescargas = directorio;

            if (string.IsNullOrEmpty(OrigenDeclaraciones))
                OrigenDeclaraciones = directorio;

            RUV.I.Configuraciones.Grabar();
        }
    }
}
