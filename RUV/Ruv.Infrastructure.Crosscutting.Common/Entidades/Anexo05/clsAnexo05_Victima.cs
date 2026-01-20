using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public partial class clsAnexo05_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
    {
        public clsAnexo05_Victima()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        private int? _PersonaAfectadaId;
        [DataMember]
        public int? PersonaAfectadaId
        {
            get { return _PersonaAfectadaId; }
            set
            {
                _PersonaAfectadaId = value;
                ReportarCambioPropiedad("PersonaAfectadaId");
            }
        }

        private int? _SeDesplazo;
        [DataMember]
        public int? SeDesplazo
        {
            get { return _SeDesplazo; }
            set
            {
                _SeDesplazo = value;
                ReportarCambioPropiedad("SeDesplazo");
            }
        }


        public string Scope
        {
            get { return "Anexo 05"; }
        }
    }
}
