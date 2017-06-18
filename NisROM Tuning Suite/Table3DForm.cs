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
    public partial class Table3DForm : Form
    {
        private RomTable romTable;

        private Table3DView tableView;

        public RomTable RomTable
        {
            get
            {
                return romTable;
            }
        }

        public Table3DView TableView
        {
            get
            {
                return this.tableView;
            }
        }
        public Table3DForm()
        {
            InitializeComponent();
        }

        public Table3DForm(RomTable romTable)
        {
            InitializeComponent();
            this.romTable = romTable;
        }

        private void FillTable()
        {
            tableView = new Table3DView
            {
                YAxisText = "RPM",
                XAxisText = "Base Fuel Schedule",
                RomTable = romTable,
                Dock = DockStyle.Fill
            };

            this.Controls.Add(tableView);
        }

        private void Table3DForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                tableView.SaveDataOnClose();
            }
            catch
            {
                MessageBox.Show("Error saving table");
            }
            MainForm.existingTables.Remove(this.Text);
        }

        private void Table3DForm_Load(object sender, EventArgs e)
        {
            FillTable();
        }

        private void Table3DForm_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
