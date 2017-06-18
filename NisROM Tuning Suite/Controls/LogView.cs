using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NisROM_Tuning_Suite.EcuLogging;
using NisROM_Tuning_Suite.Utilities;

namespace NisROM_Tuning_Suite.Controls
{
    public partial class LogView : UserControl
    {

        public DataGridView Grid
        {
            get
            {
                return this.dataGridView1;
            }
        }

        public CheckedListBox ParamList
        {
            get
            {
                return this.checkedListBox1;
            }
        }

        public LogView()
        {
            InitializeComponent();
            Grid.DoubleBuffered(true);
        }

        private void dataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(e.CurrentValue == CheckState.Unchecked)
            {
                foreach(DataGridViewColumn col in Grid.Columns)
                {
                    if(col.Name == ((CheckedListBox)sender).SelectedItem.ToString())
                    {
                        col.Visible = true;
                        return;
                    }
                }
                DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = ((CheckedListBox)sender).SelectedItem.ToString();
                newColumn.Name = ((CheckedListBox)sender).SelectedItem.ToString();
                Grid.Columns.Add(newColumn);
            }
            else
            {
                Grid.Columns[((CheckedListBox)sender).SelectedItem.ToString()].Visible = false;
            }
        }
    }
}
