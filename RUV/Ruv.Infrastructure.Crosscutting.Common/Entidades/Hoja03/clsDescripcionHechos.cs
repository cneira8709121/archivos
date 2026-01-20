using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public partial class clsDescripcionHechos : clsEntidadBase, IDataErrorInfo, IValidationEntity
    {
        public clsDescripcionHechos()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;
        }
        public string Scope { get { return "HOJA 3"; } }
        #region PREGUNTA 24

        private string _Narracion;
        [DataMember]
        public string Narracion
        {
            get { return _Narracion; }
            set
            {
                if (value != null)
                    _Narracion = value.Trim();
                ReportarCambioPropiedad("Narracion");
            }
        }

        private string infoHechos;
        [DataMember]
        public string InfoHechos
        {
            get { return infoHechos; }
            set { infoHechos = value;
                ReportarCambioPropiedad("InfoHechos");
            }
        }

        private bool mostroMensaje;

        public bool MostroMensaje
        {
            get { return mostroMensaje; }
            set { mostroMensaje = value; }
        }



        #endregion

    }
}
