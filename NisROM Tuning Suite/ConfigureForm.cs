using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NisROM_Tuning_Suite
{
    public partial class ConfigureForm : Form
    {
        public static string interfaceType = "";
        public static string portNum = "";
        public static string dumbOptions = "";
        public static string protocolType = "";
        public static string initialize = "";
        public static string destinationAddress = "";
        public static string testerID = "";
        public static string addressType = "";
        public static string configuration = "";
        public static string kernelCmd = "";

        public ConfigureForm()
        {
            InitializeComponent();
        }

        private bool IniExists()
        {
            string appPath = Application.ExecutablePath;
            appPath = Path.GetDirectoryName(appPath);
            appPath += @"\nisprog.ini";
            return File.Exists(appPath);
        }

        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            List<string> comPorts = SerialPort.GetPortNames().ToList();
            foreach(string port in comPorts)
            {
                comPortsComboBox.Items.Add(port);
            }
            if (IniExists())
            {
                string iniPath = Application.ExecutablePath;
                iniPath = Path.GetDirectoryName(iniPath);
                iniPath += @"\nisprog.ini";
                List<string> iniLines = File.ReadAllLines(iniPath).ToList();
                string[] interfaceType = iniLines[1].Split(' ');
                txtInterface.Text = interfaceType[1];
                string[] port = iniLines[2].Split(' ');
                string portNum = port[1].Replace(@"\\.\", "");
                comPortsComboBox.Text = portNum;
                string[] dumbopts = iniLines[3].Split(' ');
                txtDumbopts.Text = dumbopts[1];
                string[] protocol = iniLines[4].Split(' ');
                protocolComboBox.Text = protocol[1].ToUpper();
                string[] initMode = iniLines[5].Split(' ');
                txtInitMode.Text = initMode[1];
                string[] testerId = iniLines[6].Split(' ');
                txtTesterId.Text = testerId[1];
                string[] destAddr = iniLines[7].Split(' ');
                txtDestAddr.Text = destAddr[1];
                string[] addrType = iniLines[8].Split(' ');
                txtAddrType.Text = addrType[1];
                string[] npConf = iniLines[11].Split(' ');
                txtNpConf.Text = npConf[1] + " " + npConf[2];
                string[] kernel = iniLines[12].Replace("npk_", "").Split(' ');
                kernelComboBox.Text = Path.GetFileNameWithoutExtension(kernel[1]);
            }
        }

        private void ConfigureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string iniPath = Application.ExecutablePath;
            iniPath = Path.GetDirectoryName(iniPath);
            iniPath += @"\nisprog.ini";
            if (IniExists())
            {
                File.Delete(iniPath);
            }
            using (StreamWriter sw = new StreamWriter(iniPath))
            {
                sw.WriteLine("set");
                sw.WriteLine("interface " + txtInterface.Text.ToLower());
                sw.WriteLine(@"port \\.\" + comPortsComboBox.Text);
                sw.WriteLine("dumpopts " + txtDumbopts.Text);
                sw.WriteLine("l2protocol " + protocolComboBox.Text.ToLower());
                sw.WriteLine("initmode " + txtInitMode.Text.ToLower());
                sw.WriteLine("testerid " + txtTesterId.Text.ToLower());
                sw.WriteLine("destaddr " + txtDestAddr.Text.ToLower());
                sw.WriteLine("addrtype " + txtAddrType.Text.ToLower());
                sw.WriteLine("up");
                sw.WriteLine("nc");
                sw.WriteLine("npconf " + txtNpConf.Text.ToLower());
                sw.WriteLine("runkernel " + Path.GetDirectoryName(iniPath) + @"\npk_" + kernelComboBox.Text + ".bin");
            }
        }
    }
}
