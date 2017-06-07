using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NisROM_Tuning_Suite.Utilities;

namespace NisROM_Tuning_Suite.J2534
{
    public struct SConfigList
    {
        public int Count { get; set; }

        public IntPtr ListPtr { get; set; }

        public List<SConfig> GetList()
        {
            if (ListPtr == IntPtr.Zero)
                return new List<SConfig>();
            return ListPtr.AsList<SConfig>(Count);
        }
    }
}
