using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite
{
    public class RomTable
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string StorageType { get; set; }
        public string Endian { get; set; }
        public string SizeX { get; set; }
        public string SizeY { get; set; }
        public string UserLevel { get; set; }
        public string StorageAddress { get; set; }
        public RomTableScaling Scaling { get; set; }
        public RomTableAxis XAxis { get; set; }
        public RomTableAxis YAxis { get; set; }
        public byte[] TableBytes { get; set; }
    }
}
