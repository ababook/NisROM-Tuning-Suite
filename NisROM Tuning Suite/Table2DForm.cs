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
    public partial class Table2DForm : Form
    {
        private RomTable romTable;
        
        public List<string> DataValues
        {
            set
            {
                staticYAxisView1.AxisValues = value;
            }
        }
        public Table2DForm(RomTable romTable)
        {
            InitializeComponent();
            this.romTable = romTable;
            staticYAxisView1.RomTable = romTable;
        }

        private void Table2DForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            staticYAxisView1.SaveTableOnClose();
        }
    }
}
