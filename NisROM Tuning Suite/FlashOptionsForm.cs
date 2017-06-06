using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NisROM_Tuning_Suite.Utilities;

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

        private void FixChecksum()
        {
            byte[] data = MainForm.ecuRom.RomBytes;
            uint sum = 0;
            uint xorsum = 0;
            int startOffset = 0;
            bool hrStyle = false;
            uint check1 = DataFunctions.GetUInt(data, 0x20008);
            uint check2 = DataFunctions.GetUInt(data, 0x20010);
            if ((data.Length == 0x100000) && (check1 == 0xFFFF7FFC) && (check2 == check1)) hrStyle = true;
            if ((data.Length == 0x180000) && (check1 == 0xFFFF7FFC) && (check2 == check1)) hrStyle = true;
            if (hrStyle) startOffset = 0x8204;
            uint xorAddress = Convert.ToUInt32(MainForm.checksumXOR, 16);
            uint sumAddress = Convert.ToUInt32(MainForm.checksumSum, 16);
            for (int count = startOffset; count < data.Length; count += 4)
            {
                if (count == xorAddress) continue;
                if (count == sumAddress) continue;
                if ((count == 0x20000) && hrStyle) continue;
                uint value = DataFunctions.GetUInt(data, count);
                sum += value;
                xorsum ^= value;
            }
            MessageBox.Show(xorsum.ToString());
            MessageBox.Show(sum.ToString());
            DataFunctions.WriteToArray(data, (int)xorAddress, xorsum);
            DataFunctions.WriteToArray(data, (int)sumAddress, sum);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FixChecksum();
        }
    }
}
