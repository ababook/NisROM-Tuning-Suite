using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace NisROM_Tuning_Suite.Serial
{
    public class SerialManager
    {
        private SerialPort port;

        public SerialManager()
        {
            port = new SerialPort();
        }
    }
}
