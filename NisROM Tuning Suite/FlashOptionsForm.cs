using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NisROM_Tuning_Suite
{
    public partial class FlashOptionsForm : Form
    {
        public FlashOptionsForm()
        {
            InitializeComponent();
        }

        private UInt16 CRC16(byte[] packet, int length)
        {
            const UInt16 num = 0x8408;
            UInt16 crc = 0xffff;
            for(int i = 0; i < length; i++)
            {
                crc ^= packet[i];
                for(int j = 0; j < 8; j++)
                {
                    if((crc & 0x0001) > 0)
                    {
                        crc >>= 1;
                        crc ^= num;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }
    }
}
