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
    public partial class CANDumpForm : Form
    {
        public string ProgressText
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value + Environment.NewLine;
            }
        }

        public string DumpPercent
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value + "%";
            }
        }

        public CANDumpForm()
        {
            InitializeComponent();
        }
    }
}
