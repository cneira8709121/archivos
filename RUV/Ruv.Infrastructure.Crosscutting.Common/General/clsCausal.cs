using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.General
{
    public class clsCausal
    {
        private eTipoParametros _eParametroTipoCausal;

        public int NId { get; set; }
        public string CNombre { get; set; }
        public eTipoParametros EParametroTipoCausal
        {
            get { return _eParametroTipoCausal; }
            set
            {
                if (!(value == eTipoParametros.CausalesTodos ||
                      value == eTipoParametros.CausalesLiderRadicacion ||
                      value == eTipoParametros.CausalesCriticaN ||
                      value == eTipoParametros.CausalesGlosas ||
                      value == eTipoParametros.CausalesValoracion)) 
                    _eParametroTipoCausal = eTipoParametros.CausalesTodos;
                else 
                    _eParametroTipoCausal = value;
            }
        }
        public string CParteEmotiva { get; set; }
    }
}
