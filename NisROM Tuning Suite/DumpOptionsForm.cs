using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NisROM_Tuning_Suite
{
    public partial class DumpOptionsForm : Form
    {
        public DumpOptionsForm()
        {
            InitializeComponent();
        }

        private void btnDump_Click(object sender, EventArgs e)
        {
            if(txtFilename.Text == "" || txtFilename.Text == String.Empty || memSizeComboBox.Text == "")
            {
                MessageBox.Show("File name and memory size required");
                return;
            }
            if (!txtFilename.Text.Contains(".bin"))
            {
                MessageBox.Show("File name must have '.bin' extension");
                return;
            }
            string appPath = Application.ExecutablePath;
            appPath = Path.GetDirectoryName(appPath);
            if(File.Exists(appPath + "\\nisprog.ini"))
            {
                List<string> iniLines = File.ReadAllLines(appPath + "\\nisprog.ini").ToList();
                string[] interfaceType = iniLines[1].Split(' ');
                ConfigureForm.interfaceType = interfaceType[1];
                string[] port = iniLines[2].Split(' ');
                string portNum = port[1].Replace(@"\\.\", "");
                ConfigureForm.portNum = portNum;
                string[] dumbopts = iniLines[3].Split(' ');
                ConfigureForm.dumbOptions = dumbopts[1];
                string[] protocol = iniLines[4].Split(' ');
                ConfigureForm.protocolType = protocol[1].ToUpper();
                string[] initMode = iniLines[5].Split(' ');
                ConfigureForm.initialize = initMode[1];
                string[] testerId = iniLines[6].Split(' ');
                ConfigureForm.testerID = testerId[1];
                string[] destAddr = iniLines[7].Split(' ');
                ConfigureForm.destinationAddress = destAddr[1];
                string[] addrType = iniLines[8].Split(' ');
                ConfigureForm.addressType = addrType[1];
                string[] npConf = iniLines[11].Split(' ');
                ConfigureForm.configuration = npConf[1] + " " + npConf[2];
                string[] kernel = iniLines[12].Split(' ');
                ConfigureForm.kernelCmd = Path.GetFileNameWithoutExtension(kernel[1]);
                File.Delete(appPath + "\\nisprog.ini");
            }
            using (StreamWriter sw = new StreamWriter(appPath + @"\nisprog.ini"))
            {
                sw.WriteLine("set");
                sw.WriteLine("interface " + ConfigureForm.interfaceType.ToLower());
                sw.WriteLine(@"port \\.\" + ConfigureForm.portNum);
                sw.WriteLine("dumpopts " + ConfigureForm.dumbOptions);
                sw.WriteLine("l2protocol " + ConfigureForm.protocolType.ToLower());
                sw.WriteLine("initmode " + ConfigureForm.initialize.ToLower());
                sw.WriteLine("testerid " + ConfigureForm.testerID.ToLower());
                sw.WriteLine("destaddr " + ConfigureForm.destinationAddress.ToLower());
                sw.WriteLine("addrtype " + ConfigureForm.addressType.ToLower());
                sw.WriteLine("up");
                sw.WriteLine("nc");
                sw.WriteLine("npconf " + ConfigureForm.configuration.ToLower());
                sw.WriteLine("runkernel " + Path.GetDirectoryName(appPath + "\\nisprog.ini") + @"\npk_" + ConfigureForm.kernelCmd + ".bin");
                string fileSize = "";
                if (memSizeComboBox.Text == ".5MB") fileSize = "524288";
                else if (memSizeComboBox.Text == "1MB") fileSize = "1048576";
                sw.WriteLine("dumpmem " + txtFilename.Text + " 0 " + fileSize);
                sw.WriteLine("stopkernel");
                sw.WriteLine("npdisc");
                sw.WriteLine("quit");
            }
            appPath += @"\nisprog.exe";
            System.Diagnostics.Process.Start(appPath);
            this.Close();
        }
    }
}
