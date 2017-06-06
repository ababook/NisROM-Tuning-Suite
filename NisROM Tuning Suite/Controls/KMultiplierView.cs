using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NisROM_Tuning_Suite.Controls
{
    public partial class KMultiplierView : UserControl
    {
        public string MultiplierValue
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public RomTable RomTable { get; set; }

        public KMultiplierView()
        {
            InitializeComponent();
        }

        public void SaveValueOnClose()
        {
            uint addr = Convert.ToUInt32(RomTable.StorageAddress, 16);
            uint currentValue = Convert.ToUInt16(MultiplierValue);
            MainForm.ecuRom.RomBytes[addr] = (byte)(currentValue >> 8);
            MainForm.ecuRom.RomBytes[addr + 1] = (byte)currentValue;
        }

        private void KMultiplierView_Load(object sender, EventArgs e)
        {
            uint addr = Convert.ToUInt32(RomTable.StorageAddress, 16);
            ushort kMult = BitConverter.ToUInt16(new byte[2] { MainForm.ecuRom.RomBytes[addr + 1], MainForm.ecuRom.RomBytes[addr] }, 0);
            MultiplierValue = kMult.ToString();
        }

        private void KMultiplierView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Oemplus || e.KeyData == Keys.Add)
            {
                double increment = Convert.ToDouble(RomTable.Scaling.FineIncrement);
                double currentValue = Convert.ToDouble(MultiplierValue);
                MultiplierValue = (currentValue + increment).ToString();
            }
        }
    }
}
