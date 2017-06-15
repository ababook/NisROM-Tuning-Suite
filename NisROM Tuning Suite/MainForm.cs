using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml;

using NisROM_Tuning_Suite.J2534;
using NisROM_Tuning_Suite.J2534Logger;
using NisROM_Tuning_Suite.Utilities;

namespace NisROM_Tuning_Suite
{
    public partial class MainForm : Form
    {
        public static RomDefinition romDefinition;
        private List<string> definitionCategories;
        public static RomFile ecuRom;
        public static string checksumXOR;
        public static string checksumSum;

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
            IsMdiContainer = true;
            LayoutMdi(MdiLayout.Cascade);
            btnDecrement.Visible = false;
            btnIncrement.Visible = false;
            btnBigDecrement.Visible = false;
            btnBigIncrement.Visible = false;
        }

        private void LoadTreeView(RomFile rom, RomDefinition definition)
        {
            romDefinition = definition;
            ecuRom = rom;
            List<TreeNode> categoryNodes = new List<TreeNode>();
            definitionCategories = new List<string>();
            foreach (XmlNode xmlNode in definition.FullDefinition.ChildNodes)
            {
                foreach (XmlNode innerNode in xmlNode)
                {
                    if (innerNode.Name == "table")
                    {
                        if (!definitionCategories.Contains(GetAttributeValue(innerNode, "category")))
                        {
                            definitionCategories.Add(GetAttributeValue(innerNode, "category"));
                            categoryNodes.Add(new TreeNode(GetAttributeValue(innerNode, "category")));
                        }
                    }
                }
            }
            foreach(XmlNode xNode in definition.FullDefinition.ChildNodes)
            {
                foreach (XmlNode innerNode in xNode)
                {
                    if(innerNode.Name == "table")
                    {
                        string categoryType = innerNode.Attributes["category"].Value;
                        foreach(TreeNode category in categoryNodes)
                        {
                            if(category.Text == categoryType)
                            {
                                category.Nodes.Add(new TreeNode(innerNode.Attributes["name"].Value));
                            }
                        }
                    }
                    if(innerNode.Name == "checksum")
                    {
                        checksumSum = GetAttributeValue(innerNode, "sumloc");
                        checksumXOR = GetAttributeValue(innerNode, "xorloc");
                    }
                }
            }
            foreach(TreeNode category in categoryNodes)
            {
                treeView1.Nodes.Add(category);
            }
        }

        private XmlNode GetNode(XmlDocument xDoc, string elementTag, string identifier)
        {
            var node = xDoc.SelectSingleNode($"//{elementTag}[@name='{identifier}']");
            return node;
        }

        private string GetAttributeValue(XmlNode node, string attribute)
        {
            return node.Attributes[attribute].Value;
        }
        
        private void FixChecksums(uint xorAddress, uint sumAddress)
        {
            uint sum = 0;
            uint xor = 0;
            int startOffset = 0;
            bool hrStyle = false;
            uint check1 = DataFunctions.GetUInt(ecuRom.RomBytes, 0x20008);
            uint check2 = DataFunctions.GetUInt(ecuRom.RomBytes, 0x20010);
            if ((ecuRom.RomBytes.Length == 0x100000) && (check1 == 0xFFFF7FFC) && (check2 == check1))
            {
                hrStyle = true;
            }
            if ((ecuRom.RomBytes.Length == 0x180000) && (check1 == 0xFFFF7FFC) && (check2 == check1))
            {
                hrStyle = true;
            }
            if (hrStyle)
            {
                startOffset = 0x8204;
            }
            for (int count = startOffset; count < ecuRom.RomBytes.Length; count += 4)
            {
                if (count == xorAddress) continue;
                if (count == sumAddress) continue;
                if ((count == 0x20000) && (hrStyle)) continue;
                uint value = DataFunctions.GetUInt(ecuRom.RomBytes, count);
                sum += value;
                xor ^= value;
            }
            int offset = (int)sumAddress;
            ecuRom.RomBytes[offset] = (byte)(sum >> 24);
            ecuRom.RomBytes[offset + 1] = (byte)(sum >> 16);
            ecuRom.RomBytes[offset + 2] = (byte)(sum >> 8);
            ecuRom.RomBytes[offset + 3] = (byte)(sum);
            offset = (int)xorAddress;
            ecuRom.RomBytes[offset] = (byte)(xor >> 24);
            ecuRom.RomBytes[offset + 1] = (byte)(xor >> 16);
            ecuRom.RomBytes[offset + 2] = (byte)(xor >> 8);
            ecuRom.RomBytes[offset + 3] = (byte)(xor);
        }

        private void loadROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select .bin file");
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    RomFile loadedRom = new RomFile { RomBytes = File.ReadAllBytes(ofd.FileName) };
                    MessageBox.Show("Select definition file");
                    using(OpenFileDialog ofd2 = new OpenFileDialog())
                    {
                        if(ofd2.ShowDialog() == DialogResult.OK)
                        {
                            XmlDocument xDoc = new XmlDocument();
                            xDoc.Load(ofd2.FileName);
                            RomDefinition definition = new RomDefinition { FullDefinition = xDoc };
                            treeView1.Nodes.Clear();
                            LoadTreeView(loadedRom, definition);
                            btnIncrement.Visible = true;
                            btnDecrement.Visible = true;
                            btnBigIncrement.Visible = true;
                            btnBigDecrement.Visible = true;
                            fixChecksumsToolStripMenuItem.Enabled = true;
                        }
                    }
                }
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode selectedTable = treeView1.SelectedNode;
            try
            {
                if (definitionCategories.Contains(selectedTable.Text))
                {
                    return;
                }
            }
            catch { }
            XmlNode table = GetNode(romDefinition.FullDefinition, "table", selectedTable.Text);
            RomTable romTable = new RomTable
            {
                Name = GetAttributeValue(table, "name"),
                Type = GetAttributeValue(table, "type"),
                Category = GetAttributeValue(table, "category"),
                StorageType = GetAttributeValue(table, "storagetype"),
                Endian = GetAttributeValue(table, "endian"),
                SizeY = GetAttributeValue(table, "sizey"),
                UserLevel = GetAttributeValue(table, "userlevel"),
                StorageAddress = GetAttributeValue(table, "storageaddress")
            };
            RomTableScaling scaling = new RomTableScaling();
            List<string> dataValues = new List<string>();
            foreach (XmlNode childNode in table)
            {
                if(childNode.Name == "scaling")
                {
                    scaling.Units = GetAttributeValue(childNode, "units");
                    scaling.Expression = GetAttributeValue(childNode, "expression");
                    scaling.To_Byte = GetAttributeValue(childNode, "to_byte");
                    scaling.Format = GetAttributeValue(childNode, "format");
                    scaling.FineIncrement = GetAttributeValue(childNode, "fineincrement");
                    scaling.CoarseIncrement = GetAttributeValue(childNode, "coarseincrement");
                }
                if(childNode.Name == "table" && romTable.Type == "3D")
                {
                    RomTableAxis romAxis = new RomTableAxis
                    {
                        AxisType = GetAttributeValue(childNode, "type"),
                        Name = GetAttributeValue(childNode, "name"),
                        StorageType = GetAttributeValue(childNode, "storagetype"),
                        Endian = GetAttributeValue(childNode, "endian"),
                        StorageAddress = GetAttributeValue(childNode, "storageaddress")
                    };
                    RomTableScaling axisScaling = new RomTableScaling();
                    foreach(XmlNode scalingNode in childNode)
                    {
                        if(scalingNode.Name == "scaling")
                        {
                            axisScaling.Units = GetAttributeValue(scalingNode, "units");
                            axisScaling.Expression = GetAttributeValue(scalingNode, "expression");
                            axisScaling.To_Byte = GetAttributeValue(scalingNode, "to_byte");
                            axisScaling.Format = GetAttributeValue(scalingNode, "format");
                            axisScaling.FineIncrement = GetAttributeValue(scalingNode, "fineincrement");
                            axisScaling.CoarseIncrement = GetAttributeValue(scalingNode, "coarseincrement");
                        }
                        romAxis.Scaling = axisScaling;
                    }
                    if (romAxis.AxisType == "X Axis") romTable.XAxis = romAxis;
                    else romTable.YAxis = romAxis;
                }
                if(childNode.Name == "table" && romTable.Type == "2D")
                {
                    RomTableAxis romAxis = new RomTableAxis
                    {
                        AxisType = GetAttributeValue(childNode, "type"),
                        Name = GetAttributeValue(childNode, "name"),
                    };
                    RomTableScaling axisScaling = new RomTableScaling();
                    
                    foreach (XmlNode scalingNode in childNode)
                    {
                        if (scalingNode.Name == "scaling")
                        {
                            axisScaling.Units = GetAttributeValue(scalingNode, "units");
                            axisScaling.Expression = GetAttributeValue(scalingNode, "expression");
                            axisScaling.To_Byte = GetAttributeValue(scalingNode, "to_byte");
                            axisScaling.Format = GetAttributeValue(scalingNode, "format");
                            axisScaling.FineIncrement = GetAttributeValue(scalingNode, "fineincrement");
                            axisScaling.CoarseIncrement = GetAttributeValue(scalingNode, "coarseincrement");
                        }
                        if(scalingNode.Name == "data")
                        {
                            dataValues.Add(GetAttributeValue(scalingNode, "value"));

                        }
                    }
                    romTable.YAxis = romAxis;
                    romAxis.Scaling = axisScaling;
                }
            }
            romTable.Scaling = scaling;
            if(romTable.Type == "2D")
            {
                if (romTable.Name == "Fuel Injection Multiplier")
                {
                    KMultiplierForm kForm = new KMultiplierForm(romTable) { MdiParent = this };
                    splitContainer2.Panel2.Controls.Add(kForm);
                    kForm.Show();
                }
                else if (romTable.Category == "Limiters")
                {
                    KMultiplierForm limiterForm = new KMultiplierForm(romTable) { MdiParent = this };
                    limiterForm.KMultView.LimiterName = romTable.Name;
                    splitContainer2.Panel2.Controls.Add(limiterForm);
                    limiterForm.Show();
                }
                else
                {
                    Table2DForm table2D = new Table2DForm(romTable) { MdiParent = this, Text = selectedTable.Text, DataValues = dataValues };
                    splitContainer2.Panel2.Controls.Add(table2D);
                    table2D.Show();
                }
            }
            if(romTable.Type == "3D")
            {
                romTable.SizeX = GetAttributeValue(table, "sizex");
                Table3DForm table3D = new Table3DForm(romTable) { MdiParent = this, Text = selectedTable.Text };
                splitContainer2.Panel2.Controls.Add(table3D);
                table3D.Show();
            }
        }

        private void eCUDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DumpOrFlashForm doff = new DumpOrFlashForm { NisprogReady = false };
            doff.Show();
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigureForm configForm = new ConfigureForm
            {
                MaximizeBox = false,
                MinimizeBox = false
            };
            configForm.Show();
        }

        private void saveROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Control table in splitContainer2.Panel2.Controls)
            {
                if(table is Table3DForm)
                {
                    Table3DForm table3D = table as Table3DForm;
                    table3D.TableView.SaveDataOnClose();
                }
                else if(table is Table2DForm)
                {
                    Table2DForm table2D = table as Table2DForm;
                    table2D.TableView.SaveTableOnClose();
                }
                else if(table is KMultiplierForm)
                {
                    KMultiplierForm kForm = table as KMultiplierForm;
                    kForm.KMultView.SaveValueOnClose();
                }
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save ROM...";
            sfd.Filter = "Binary File (*.bin)|*.bin";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(ecuRom.RomBytes);
                bw.Close();
                fs.Close();
            }
        }

        private void btnIncrement_Click(object sender, EventArgs e)
        {
            foreach (Control table in splitContainer2.Panel2.Controls)
            {
                if(table is Table3DForm)
                {
                    Table3DForm topTable = table as Table3DForm;
                    if (topTable.TableView.Grid.SelectedCells != null)
                    {
                        foreach (DataGridViewCell cell in topTable.TableView.Grid.SelectedCells)
                        {
                            topTable.TableView.IncrementCell(cell);
                        }
                    }
                }
                else if(table is Table2DForm)
                {
                    Table2DForm topTable = table as Table2DForm;
                    if (topTable.TableView.Grid.SelectedCells != null)
                    {
                        foreach (DataGridViewCell cell in topTable.TableView.Grid.SelectedCells)
                        {
                            topTable.TableView.IncrementCell(cell);
                        }
                    }
                }
                else if (table is KMultiplierForm)
                {
                    KMultiplierForm topTable = table as KMultiplierForm;
                    topTable.KMultView.IncrementCell();
                }
                break;
            }
        }

        private void btnDecrement_Click(object sender, EventArgs e)
        {
            foreach (Control table in splitContainer2.Panel2.Controls)
            {
                if (table is Table3DForm)
                {
                    Table3DForm topTable = table as Table3DForm;
                    if (topTable.TableView.Grid.SelectedCells != null)
                    {
                        foreach (DataGridViewCell cell in topTable.TableView.Grid.SelectedCells)
                        {
                            topTable.TableView.DecrementCell(cell);
                        }
                    }
                }
                else if(table is Table2DForm)
                {
                    Table2DForm topTable = table as Table2DForm;
                    if (topTable.TableView.Grid.SelectedCells != null)
                    {
                        foreach (DataGridViewCell cell in topTable.TableView.Grid.SelectedCells)
                        {
                            topTable.TableView.DecrementCell(cell);
                        }
                    }
                }
                else if (table is KMultiplierForm)
                {
                    KMultiplierForm topTable = table as KMultiplierForm;
                    topTable.KMultView.DecrementCell();
                }
                break;
            }
        }

        private void btnBigIncrement_Click(object sender, EventArgs e)
        {
            foreach (Control table in splitContainer2.Panel2.Controls)
            {
                if (table is Table3DForm)
                {
                    Table3DForm topTable = table as Table3DForm;
                    if (topTable.TableView.Grid.SelectedCells != null)
                    {
                        foreach (DataGridViewCell cell in topTable.TableView.Grid.SelectedCells)
                        {
                            topTable.TableView.IncrementCellBig(cell);
                        }
                    }
                }
                else if (table is Table2DForm)
                {
                    Table2DForm topTable = table as Table2DForm;
                    if (topTable.TableView.Grid.SelectedCells != null)
                    {
                        foreach (DataGridViewCell cell in topTable.TableView.Grid.SelectedCells)
                        {
                            topTable.TableView.IncrementCellBig(cell);
                        }
                    }
                }
                else if (table is KMultiplierForm)
                {
                    KMultiplierForm topTable = table as KMultiplierForm;
                    topTable.KMultView.IncrementCellBig();
                }
                break;
            }
        }

        private void btnBigDecrement_Click(object sender, EventArgs e)
        {
            foreach (Control table in splitContainer2.Panel2.Controls)
            {
                if (table is Table3DForm)
                {
                    Table3DForm topTable = table as Table3DForm;
                    if (topTable.TableView.Grid.SelectedCells != null)
                    {
                        foreach (DataGridViewCell cell in topTable.TableView.Grid.SelectedCells)
                        {
                            topTable.TableView.DecrementCellBig(cell);
                        }
                    }
                }
                else if(table is Table2DForm)
                {
                    Table2DForm topTable = table as Table2DForm;
                    if (topTable.TableView.Grid.SelectedCells != null)
                    {
                        foreach (DataGridViewCell cell in topTable.TableView.Grid.SelectedCells)
                        {
                            topTable.TableView.DecrementCellBig(cell);
                        }
                    }
                }
                else if (table is KMultiplierForm)
                {
                    KMultiplierForm topTable = table as KMultiplierForm;
                    topTable.KMultView.DecrementCellBig();
                }
                break;
            }
        }

        private void loggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            J2534Extended passThru = new J2534Extended();
            List<J2534Device> availableDevices = J2534Detect.ListDevices();
            J2534Device device;
            SelectDevice selectDeviceForm = new SelectDevice();
            if(selectDeviceForm.ShowDialog() == DialogResult.OK)
            {
                device = selectDeviceForm.Device;
            }
            else
            {
                return;
            }
            LoggerForm logger = new LoggerForm();
            logger.Show();
            passThru.LoadLibrary(device);
            int deviceID = 0;
            passThru.PassThruOpen(IntPtr.Zero, ref deviceID);
            int channelID = 0;
            passThru.PassThruConnect(deviceID, ProtocolID.ISO15765, ConnectFlag.NONE, BaudRate.ISO15765, ref channelID);
            int filterID = 0;
            PassThruMsg maskMsg = new PassThruMsg(ProtocolID.ISO15765, TxFlag.ISO15765_FRAME_PAD, new byte[] { 0xff, 0xff, 0xff, 0xff });
            PassThruMsg patternMsg = new PassThruMsg(ProtocolID.ISO15765, TxFlag.ISO15765_FRAME_PAD, new byte[] { 0x00, 0x00, 0x07, 0xE8 });
            PassThruMsg flowMsg = new PassThruMsg(ProtocolID.ISO15765, TxFlag.ISO15765_FRAME_PAD, new byte[] { 0x00, 0x00, 0x07, 0xE0 });
            IntPtr maskMsgPtr = maskMsg.ToIntPtr();
            IntPtr patternMsgPtr = patternMsg.ToIntPtr();
            IntPtr flowControlMsgPtr = flowMsg.ToIntPtr();
            passThru.PassThruStartMsgFilter(channelID, FilterType.FLOW_CONTROL_FILTER, maskMsgPtr, patternMsgPtr, flowControlMsgPtr, ref filterID);
            passThru.ClearRxBuffer(channelID);
            PassThruMsg txMsg = new PassThruMsg(ProtocolID.ISO15765, TxFlag.ISO15765_FRAME_PAD, new byte[] { 0x00, 0x00, 0x07, 0xdf, 0x01, 0x00 });
            var txMsgPtr = txMsg.ToIntPtr();
            int numMsgs = 1;
            passThru.PassThruWriteMsgs(channelID, txMsgPtr, ref numMsgs, 50);
            numMsgs = 1;
            IntPtr rxMsgs = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PassThruMsg)) * numMsgs);
            J2534Err status = J2534Err.STATUS_NOERROR;
            while (J2534Err.STATUS_NOERROR == status)
            {
                status = passThru.PassThruReadMsgs(channelID, rxMsgs, ref numMsgs, 200);
            }
            if ((J2534Err.ERR_BUFFER_EMPTY == status || J2534Err.ERR_TIMEOUT == status) && numMsgs > 0)
            {
                foreach (PassThruMsg msg in rxMsgs.AsList<PassThruMsg>(numMsgs))
                {
                    logger.LoggerText = msg.ToString(Environment.NewLine);
                }
            }
            passThru.PassThruDisconnect(channelID);
            passThru.FreeLibrary();
        }

        private void cANDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Bin File|*.bin"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                CANDumpForm dumpForm = new CANDumpForm();
                dumpForm.Show();
                dumpForm.ProgressText = "Detecting J2534 Device...";
                J2534Extended passThru = new J2534Extended();
                List<J2534Device> availableJ2534Devices = J2534Detect.ListDevices();
                if (availableJ2534Devices.Count == 0)
                {
                    dumpForm.ProgressText = "Could not find any installed J2534 devices.";
                    return;
                }
                passThru.LoadLibrary(availableJ2534Devices[0]);
                ObdComm comm = new ObdComm(passThru);
                if (!comm.DetectProtocol())
                {
                    dumpForm.ProgressText = String.Format("Error connecting to device. Error: {0}", comm.GetLastError());
                    if (comm.Disconnect())
                    {
                        dumpForm.ProgressText = "Disconnected.";
                        return;
                    }
                    else
                    {
                        dumpForm.ProgressText = "Disconnection failed!!!";
                        return;
                    }
                }
                if (!comm.IsConnected())
                {
                    dumpForm.ProgressText = "Not connected";
                }
                dumpForm.ProgressText = "Setting CAN Session...";
                comm.SetCANSession(0xFB);
                dumpForm.ProgressText = "Checking ECU ROM size...";
                int romSize = comm.GetRomSizeCAN();
                const byte maxPacketSize = 0x3F;
                uint romOffset = 0;
                List<byte> romBytes = new List<byte>();
                byte[] buffer;
                dumpForm.ProgressText = "Dumping ROM...";
                while (true)
                {
                    try
                    {
                        buffer = comm.ReadBytesISO15765Packet(romOffset, maxPacketSize, ProtocolID.ISO15765).ToArray();
                        romBytes.AddRange(buffer);
                        romOffset += maxPacketSize;
                    }
                    catch(Exception ex)
                    {
                        break;
                    }
                }
                buffer = comm.ReadBytesISO15765Packet(romOffset, (byte)(romSize - romOffset), ProtocolID.ISO15765).ToArray();
                romBytes.AddRange(buffer);
                dumpForm.ProgressText = "Dump complete, saving ROM dump...";
                byte[] rom = romBytes.ToArray();
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(rom);
                bw.Close();
                fs.Close();
                dumpForm.ProgressText = "ROM dump saved successfully";
                passThru.FreeLibrary();
            }
        }

        private void checkDTCsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clearDTCsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eCUConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fixChecksumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FixChecksums(Convert.ToUInt32(checksumXOR, 16), Convert.ToUInt32(checksumSum, 16));
                MessageBox.Show("Checksums fixed.");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error fixing checksums. Error : " + ex.ToString());
            }
        }
    }
}
