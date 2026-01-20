using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    public interface IPertenenciaEtnica
    {
        int? PertenenciaEtnica { get; set; }
        
        int? ComunidadEtnica1 { get; set; }
                
        int? ComunidadEtnica2 { get; set; }
                
        string OtraComunidadEtnica { get; set; }
    }
}
