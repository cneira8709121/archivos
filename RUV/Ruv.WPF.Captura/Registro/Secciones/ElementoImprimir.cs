using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Ruv.WPF.Captura.Registro.Secciones
{
    public class ElementoImprimir
    {
        public string NombreAnexo { get; set; }
        public string JefeDeHogar { get; set; }
        public SolidColorBrush ColorEstado { get; set; }
        public clsTomaDeclaracion Hoja1 { get; set; }
        public clsPersonasAfectadas Hoja2 { get; set; }
        public clsDescripcionHechos Hoja3 { get; set; }
        public clsVerificacionProcedimiento Hoja4 { get; set; }
        public IAnexo Anexo { get; set; }
        public int NumeroAnexo { get; set; }
    }

}
