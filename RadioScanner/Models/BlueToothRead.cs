using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class BlueToothRead
    {
        public DateTimeOffset Time;
        public string MACAddress;
        public int SignalStrength;
        public string Vendor;
        public string ClassOfDevice;
    }
}
