using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using t = WintabDN;

namespace Ruv.Infrastructure.Crosscutting.Utilities.Wacom
{
    public class Info
    {
        public bool IsActive { 
            get {
                return t::CWintabInfo.IsWintabAvailable() && t::CWintabInfo.IsStylusActive();
            }
        }

        public string Name { get { return t::CWintabInfo.GetDeviceInfo(); } }
        public uint NumberOfDevices { get { return t::CWintabInfo.GetNumberOfDevices(); } }
    }
}
