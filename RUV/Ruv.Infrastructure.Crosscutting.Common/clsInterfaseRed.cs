using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common
{
  [DataContract]
  public class clsInterfaseRed
  {
    [DataMember]
    public string Mac { get; set; }
    [DataMember]
    public string Dns { get; set; }
    [DataMember]
    public string Ips { get; set; }
    [DataMember]
    public string PcName { get; set; }
  }
}
