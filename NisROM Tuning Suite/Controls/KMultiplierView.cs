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

        public string LimiterName
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public RomTable RomTable { get; set; }

        public KMultiplierView()
        {
            InitializeComponent();
        }

        public void IncrementCell()
        {
            double fineIncrement = Convert.ToDouble(RomTable.Scaling.FineIncrement);
            double newValue = Convert.ToDouble(textBox1.Text) + fineIncrement;
            textBox1.Text = newValue.ToString();
        }

        public void DecrementCell()
        {
            double fineIncrement = Convert.ToDouble(RomTable.Scaling.FineIncrement);
            double newValue = Convert.ToDouble(textBox1.Text) - fineIncrement;
            textBox1.Text = newValue.ToString();
        }

        public void IncrementCellBig()
        {
            double coarseIncrement = Convert.ToDouble(RomTable.Scaling.CoarseIncrement);
            double newValue = Convert.ToDouble(textBox1.Text) + coarseIncrement;
            textBox1.Text = newValue.ToString();
        }

        public void DecrementCellBig()
        {
            double coarseIncrement = Convert.ToDouble(RomTable.Scaling.CoarseIncrement);
            double newValue = Convert.ToDouble(textBox1.Text) - coarseIncrement;
            textBox1.Text = newValue.ToString();
        }

        private double ConvertFromExpression(ushort data, string expr)
        {
            DataTable dt = new DataTable();
            expr = expr.Replace("x", Convert.ToString(data));
            return Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2);
        }

        private uint ConvertToUInt(string data, string expr)
        {
            DataTable dt = new DataTable();
            expr = expr.Replace("x", data);
            return Convert.ToUInt32(dt.Compute(expr, String.Empty));
        }

        public void SaveValueOnClose()
        {
            uint addr = Convert.ToUInt32(RomTable.StorageAddress, 16);
            uint currentValue = ConvertToUInt(MultiplierValue, RomTable.Scaling.To_Byte);
            MainForm.ecuRom.RomBytes[addr] = (byte)(currentValue >> 8);
            MainForm.ecuRom.RomBytes[addr + 1] = (byte)currentValue;
        }

        private void KMultiplierView_Load(object sender, EventArgs e)
        {
            uint addr = Convert.ToUInt32(RomTable.StorageAddress, 16);
            MultiplierValue = ConvertFromExpression(BitConverter.ToUInt16(new byte[2] { MainForm.ecuRom.RomBytes[addr + 1], MainForm.ecuRom.RomBytes[addr] }, 0), RomTable.Scaling.Expression).ToString();
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
