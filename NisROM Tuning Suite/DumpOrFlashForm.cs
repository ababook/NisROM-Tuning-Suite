using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NisROM_Tuning_Suite
{
    public partial class DumpOrFlashForm : Form
    {
        private string iniPath;

        public bool NisprogReady { get; set; }

        public DumpOrFlashForm()
        {
            InitializeComponent();
            iniPath = Application.ExecutablePath;
            iniPath = Path.GetDirectoryName(iniPath);
            iniPath += @"\nisprog.ini";

        }

        private void btnDump_Click(object sender, EventArgs e)
        {
            DumpOptionsForm dumpOptions = new DumpOptionsForm();
            dumpOptions.Show();
            this.Close();
        }

        private void btnFlash_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Flashing currently unavailable, check back soon");
        }
    }
}
