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
using System.Xml;

namespace NisROM_Tuning_Suite
{
    public partial class MainForm : Form
    {

        private List<string> defCategories = new List<string> { "Ignition Timing", "Variable Cam Timing", "Fuel", "Airflow", "Limiters", "Electronic Throttle", "Misc", };
        public static RomDefinition romDefinition;
        public static RomFile ecuRom;

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
            IsMdiContainer = true;
            LayoutMdi(MdiLayout.Cascade);
        }

        private void LoadTreeView(RomFile rom, RomDefinition definition)
        {
            romDefinition = definition;
            ecuRom = rom;
            List<TreeNode> categoryNodes = new List<TreeNode>();
            foreach(string category in defCategories)
            {
                TreeNode node = new TreeNode(category);
                categoryNodes.Add(node);
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
                        }
                    }
                }
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode selectedTable = treeView1.SelectedNode;
            if (defCategories.Contains(selectedTable.Text))
            {
                return;
            }
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
                    splitContainer1.Panel2.Controls.Add(kForm);
                    kForm.Show();
                }
                else
                {
                    Table2DForm table2D = new Table2DForm(romTable) { MdiParent = this, Text = selectedTable.Text, DataValues = dataValues };
                    splitContainer1.Panel2.Controls.Add(table2D);
                    table2D.Show();
                }
            }
            if(romTable.Type == "3D")
            {
                romTable.SizeX = GetAttributeValue(table, "sizex");
                Table3DForm table3D = new Table3DForm(romTable) { MdiParent = this, Text = selectedTable.Text };
                splitContainer1.Panel2.Controls.Add(table3D);
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
    }
}
