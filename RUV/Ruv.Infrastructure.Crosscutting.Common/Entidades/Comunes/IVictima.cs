using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    public interface IVictima
    {
        int? PersonaAfectadaId { get; set; }
        T1 ObtenerCopia<T1>() where T1 : class;
    }
}
