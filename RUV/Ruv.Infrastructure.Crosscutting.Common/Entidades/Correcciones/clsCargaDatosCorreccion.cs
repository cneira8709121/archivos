using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Correcciones
{
    public class clsCargaDatosCorreccion
    {
        
        public string CPrimerNombre { get; set; }
        
        public string CSegundoNombre { get; set; }
        
        public string CPrimerApellido { get; set; }
        
        public string CSegundoApellido { get; set; }
        
        public int NTipoDocumento { get; set; }
        
        public string CNumeroDocumento { get; set; }
        
        public DateTime DNacimiento { get; set; }
        
        public int NGenero { get; set; }
        
        public List<int> LstDiscapacidad { get; set; }
        
        public int NEtnia { get; set; }

        public int NSubetnia { get; set; }
        
        public string CDireccion { get; set; }
        
        public string CCorreo { get; set; }
        
        public string CTelefono { get; set; }
    }
}
