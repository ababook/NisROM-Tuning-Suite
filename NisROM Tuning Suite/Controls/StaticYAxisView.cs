using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NisROM_Tuning_Suite.Controls
{
    public partial class StaticYAxisView : UserControl
    {
        private List<Color> heatMapColors = new List<Color>
        {
            Color.Green,
            Color.LightGreen,
            Color.Yellow,
            Color.Orange,
            Color.Red
        };

        public RomTable RomTable { get; set; }

        public List<string> AxisValues { get; set; }

        public DataGridView Grid
        {
            get
            {
                return this.dataGridView1;
            }
        }

        public StaticYAxisView()
        {
            InitializeComponent();
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView_EditingControlShowing);
        }

        private void SetTable()
        {
            dataGridView1.Columns[0].HeaderText = RomTable.YAxis.Name;
            dataGridView1.Columns[1].HeaderText = RomTable.Name;
            int tableSize = AxisValues.Count();
            uint tableAddr = Convert.ToUInt32(RomTable.StorageAddress, 16);
            if (RomTable.StorageType == "uint16")
            {
                ushort[] table = new ushort[tableSize];
                uint j = tableAddr;
                for (uint i = tableAddr; i < (tableAddr + (tableSize * 2)); i += 2)
                {
                    table[j - tableAddr] = (ushort)ConvertFromExpression(BitConverter.ToUInt16(new byte[2] { MainForm.ecuRom.RomBytes[i + 1], MainForm.ecuRom.RomBytes[i] }, 0), RomTable.Scaling.Expression);
                    j++;
                }
                for (int i = 0; i < tableSize; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.RowTemplate.Clone();
                    row.CreateCells(dataGridView1, AxisValues[i], table[i]);
                    row.Cells[0].Style.BackColor = Color.Green;
                    row.Cells[0].ReadOnly = true;
                    dataGridView1.Rows.Add(row);
                }
            }
            else
            {
                double[] table = new double[tableSize];
                for (uint i = tableAddr; i < (tableAddr + tableSize); i++)
                {
                    table[i - tableAddr] = ConvertFromExpression(MainForm.ecuRom.RomBytes[i], RomTable.Scaling.Expression);
                }
                for (int i = 0; i < tableSize; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.RowTemplate.Clone();
                    row.CreateCells(dataGridView1, AxisValues[i], table[i]);
                    row.Cells[0].Style.BackColor = Color.Green;
                    row.Cells[0].ReadOnly = true;
                    dataGridView1.Rows.Add(row);
                }
            }
        }

        private double ConvertFromExpression(byte data, string expr)
        {
            DataTable dt = new DataTable();
            expr = expr.Replace("x", Convert.ToString(Convert.ToInt32(data)));
            return Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2);
        }

        private double ConvertFromExpression(ushort data, string expr)
        {
            DataTable dt = new DataTable();
            expr = expr.Replace("x", Convert.ToString(data));
            return Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2);
        }

        private byte ConvertToRomByte(string data, string expr)
        {
            DataTable dt = new DataTable();
            expr = expr.Replace("x", data);
            return Convert.ToByte(dt.Compute(expr, String.Empty));
        }

        private ushort ConvertToUShort(string data, string expr)
        {
            DataTable dt = new DataTable();
            expr = expr.Replace("x", data);
            return Convert.ToUInt16(dt.Compute(expr, String.Empty));
        }

        public void SaveTableOnClose()
        {
            uint tableAddr = Convert.ToUInt32(RomTable.StorageAddress, 16);
            uint i = 0;
            if (RomTable.StorageType == "uint16")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    ushort value = ConvertToUShort(row.Cells[1].Value.ToString(), RomTable.Scaling.To_Byte);
                    MainForm.ecuRom.RomBytes[tableAddr + i] = (byte)(value >> 8);
                    MainForm.ecuRom.RomBytes[tableAddr + 1 + i] = (byte)(value);
                    i += 2;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    MainForm.ecuRom.RomBytes[tableAddr + i] = ConvertToRomByte(row.Cells[1].Value.ToString(), RomTable.Scaling.To_Byte);
                    i++;
                }
            }
        }

        private void LoadHeatMap()
        {
            List<double> values = new List<double>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                values.Add(Convert.ToDouble(row.Cells[1].Value));
            }
            if (values.Count() == 0)
            {
                return;
            }
            double max = values.Max();
            double min = values.Min();
            double i1 = max - ((max - min) / 5);
            double i2 = max - (((max - min) / 5) * 2);
            double i3 = max - (((max - min) / 5) * 3);
            double i4 = max - (((max - min) / 5) * 4);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell cell = row.Cells[1];
                double value = Convert.ToDouble(cell.Value);
                if (value > i1 && value <= max)
                {
                    cell.Style.BackColor = heatMapColors[0];
                }
                else if (value > i2 && value <= i1)
                {
                    cell.Style.BackColor = heatMapColors[1];
                }
                else if (value > i3 && value <= i2)
                {
                    cell.Style.BackColor = heatMapColors[2];
                }
                else if (value > i4 && value <= i3)
                {
                    cell.Style.BackColor = heatMapColors[3];
                }
                else
                {
                    cell.Style.BackColor = heatMapColors[4];
                }
            }
        }

        public void IncrementCell(DataGridViewCell cell)
        {
            double fineIncrement = Convert.ToDouble(RomTable.Scaling.FineIncrement);
            double newValue = Convert.ToDouble(cell.Value) + fineIncrement;
            cell.Value = newValue;
        }

        public void DecrementCell(DataGridViewCell cell)
        {
            double fineIncrement = Convert.ToDouble(RomTable.Scaling.FineIncrement);
            double newValue = Convert.ToDouble(cell.Value) - fineIncrement;
            cell.Value = newValue;
        }

        public void IncrementCellBig(DataGridViewCell cell)
        {
            double coarseIncrement = Convert.ToDouble(RomTable.Scaling.CoarseIncrement);
            double newValue = Convert.ToDouble(cell.Value) + coarseIncrement;
            cell.Value = newValue;
        }

        public void DecrementCellBig(DataGridViewCell cell)
        {
            double coarseIncrement = Convert.ToDouble(RomTable.Scaling.CoarseIncrement);
            double newValue = Convert.ToDouble(cell.Value) - coarseIncrement;
            cell.Value = newValue;
        }

        private void StaticYAxisView_Load(object sender, EventArgs e)
        {
            SetTable();
            LoadHeatMap();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            LoadHeatMap();
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyDown += OnControlKeyDown;
        }

        private void OnControlKeyDown(object sender, KeyEventArgs e)
        {
            Grid.CurrentCell.ReadOnly = true;
            if (e.KeyData == Keys.Oemplus || e.KeyData == Keys.Add)
            {
                foreach (DataGridViewCell cell in Grid.SelectedCells)
                {
                    IncrementCell(cell);
                }
            }
            if (e.KeyData == Keys.OemMinus || e.KeyData == Keys.Subtract)
            {
                foreach (DataGridViewCell cell in Grid.SelectedCells)
                {
                    DecrementCell(cell);
                }
            }
            Grid.CurrentCell.ReadOnly = false;
        }
    }
}
