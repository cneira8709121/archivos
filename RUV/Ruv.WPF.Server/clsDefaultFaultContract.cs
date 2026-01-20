using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Runtime.Serialization;

namespace Ruv.WPF.Server
{
    [DataContract]
    [Serializable()]
    public class clsDefaultFaultContract
    {
        [DataMember]
        public eCodigoAutenticacion Codigo { get; set; }
        [DataMember]
        public string Mensaje { get; set; }
    }
}
