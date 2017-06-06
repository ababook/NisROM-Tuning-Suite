using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite
{
    public class RomFile
    {
        public byte[] RomBytes { get; set; }
        public List<RomTable> RomTables { get; set; }
    }
}
