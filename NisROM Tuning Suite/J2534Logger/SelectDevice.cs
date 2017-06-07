using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NisROM_Tuning_Suite.J2534;

namespace NisROM_Tuning_Suite.J2534Logger
{
    public partial class SelectDevice : Form
    {
        public SelectDevice()
        {
            InitializeComponent();
        }

        public J2534Device Device { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Device = (J2534Device)deviceList.SelectedItem;
            Close();
        }

        private void SelectDevice_Load(object sender, EventArgs e)
        {
            deviceList.DataSource = J2534Detect.ListDevices();
            deviceList.DisplayMember = "Name";
        }
    }
}
