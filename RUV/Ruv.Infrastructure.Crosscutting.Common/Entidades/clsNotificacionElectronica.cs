using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public class clsNotificacionElectronica
    {

        public clsNotificacionElectronica()
        {
            autorizaNotificacion = null;
            tieneFormato = false;
        }
        private bool? tieneFormato = false;
        [DataMember]
        public bool? TieneFormato
        {
            get { return tieneFormato; }
            set { tieneFormato = value; }
        }

        private bool? autorizaNotificacion = null;
        [DataMember]
        public bool? AutorizaNotificacion
        {
            get { return autorizaNotificacion; }
            set { autorizaNotificacion = value; }
        }

    }
}
