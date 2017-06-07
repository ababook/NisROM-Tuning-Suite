using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite.J2534
{
    public class J2534Device
    {
        public string Vendor { get; set; }
        public string Name { get; set; }
        public string FunctionLibrary { get; set; }
        public string ConfigApplication { get; set; }
        public int CANChannels { get; set; }
        public int ISO15765Channels { get; set; }
        public int J1850PWMChannels { get; set; }
        public int J1850VPWChannels { get; set; }
        public int ISO9141Channels { get; set; }
        public int ISO14230Channels { get; set; }
        public int SCI_A_ENGINEChannels { get; set; }
        public int SCI_A_TRANSChannels { get; set; }
        public int SCI_B_ENGINEChannels { get; set; }
        public int SCI_B_TRANSChannels { get; set; }

        public bool IsCANSupported
        {
            get { return CANChannels > 0; }
        }

        public bool IsISO15765Supported
        {
            get { return ISO15765Channels > 0; }
        }

        public bool IsJ1850PWMSupported
        {
            get { return J1850PWMChannels > 0; }
        }

        public bool IsJ1850VPWSupported
        {
            get { return J1850VPWChannels > 0; }
        }

        public bool IsISO9141Supported
        {
            get { return ISO9141Channels > 0; }
        }

        public bool IsISO14230Supported
        {
            get { return ISO14230Channels > 0; }
        }

        public bool IsSCI_A_ENGINESupported
        {
            get { return SCI_A_ENGINEChannels > 0; }
        }

        public bool IsSCI_A_TRANSSupported
        {
            get { return SCI_A_TRANSChannels > 0; }
        }

        public bool IsSCI_B_ENGINESupported
        {
            get { return SCI_B_ENGINEChannels > 0; }
        }

        public bool IsSCI_B_TRANSSupported
        {
            get { return SCI_B_TRANSChannels > 0; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
