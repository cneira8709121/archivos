using System;
using System.Collections.Generic;

namespace Ruv.WPF.Captura.DesignTime
{
    class clsHechoMotivo
    {
        public string Item { get; set; }
        public string HechoMotivo { get; set; }
        public Boolean Seleccionado { get; set; }
    }

    class clsHechoMotivoCollection : List<clsHechoMotivo>
    {
        public clsHechoMotivoCollection() { }
    }
}
