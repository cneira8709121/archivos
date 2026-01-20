using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion
{
    public class clsFirma
    {
        private FirmaOwner _firmaOwner;

        public FirmaOwner firmaOwner
        {
            get
            {
                return _firmaOwner;
            }
            set
            {
                this._firmaOwner = value;
            }
        }

        private byte[] _firma;

        public byte[] firma
        {
            get
            {
                return _firma;
            }
            set
            {
                this._firma = value;
            }
        }

    }

    public enum FirmaOwner
    {
        DECLARANTE = 0,
        TUTOR = 1,
        FUNCIONARIO = 2
    }
}
