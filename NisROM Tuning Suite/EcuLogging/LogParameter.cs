using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite.EcuLogging
{
    public class LogParameter
    {
        public string ParamName { get; set; }
        public string ParamShortName { get; set; }
        public string Address { get; set; }
        public string Units { get; set; }
        public List<double> receivedValues = new List<double>();
    }
}
