using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Utilities
{
    public class Serialize
    {
        public string Serializado(List<int> Parametros,ref string cError)
        {
            string Discapacidad = string.Empty;

            foreach (int x in Parametros)
            {
                Discapacidad = x.ToString() + (x < Parametros.Count - 1 ? "|" : "");
            }
            return Discapacidad;
        }
    }
}
