using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite
{
    public class RomTableAxis
    {
        public string AxisType { get; set; }
        public string Name { get; set; }
        public string StorageType { get; set; }
        public string Endian { get; set; }
        public string StorageAddress { get; set; }
        public RomTableScaling Scaling { get; set; }
    }
}
