using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NisROM_Tuning_Suite.Controls;

namespace NisROM_Tuning_Suite
{
    public partial class KMultiplierForm : Form
    {
        private RomTable romTable;

        public KMultiplierView KMultView
        {
            get
            {
                return this.kMultiplierView1;
            }
        }
        
        public KMultiplierForm(RomTable romTable)
        {
            InitializeComponent();
            this.romTable = romTable;
            kMultiplierView1.RomTable = romTable;
        }

        private void KMultiplierForm_Load(object sender, EventArgs e)
        {
            
        }

        private void KMultiplierForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            kMultiplierView1.SaveValueOnClose();
        }
    }
}
