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
    public partial class Table3DView : UserControl
    {

        private uint tableAddr;
        private uint xAxisAddr;
        private uint yAxisAddr;

        public DataGridView Grid
        {
            get
            {
                return this.dataGridView1;
            }
        }

        private List<Color> heatMapColors = new List<Color>
        {
            Color.DarkGreen,
            Color.Green,
            Color.LightGreen,
            Color.LightYellow,
            Color.Yellow,
            Color.Orange,
            Color.DarkOrange,
            Color.Red
        };

        public string XAxisText
        {
            get
            {
                return lblXAxis.Text;
            }
            set
            {
                lblXAxis.Text = value;
            }
        }

        public string YAxisText
        {
            get
            {
                return lblYAxis.Text;
            }
            set
            {
                lblYAxis.Text = value;
            }
        }

        public RomTable RomTable { get; set; }

        public Table3DView()
        {
            InitializeComponent();
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView_EditingControlShowing);
            dataGridView1.AllowUserToAddRows = false;
        }

        private void SetTable16x16()
        {
            for(int i = 8; i < 16; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = "Col" + i;
                col.Name = "column" + i;
                dataGridView1.Columns.Add(col);
            }
            YAxisText = RomTable.YAxis.Name;
            XAxisText = RomTable.XAxis.Name;
            ushort[] table = new ushort[256];
            ushort[] xAx;
            ushort[] yAx;
            tableAddr = Convert.ToUInt32(RomTable.StorageAddress, 16);
            xAxisAddr = Convert.ToUInt32(RomTable.XAxis.StorageAddress, 16);
            yAxisAddr = Convert.ToUInt32(RomTable.YAxis.StorageAddress, 16);
            int j = 0;
            if (RomTable.StorageType == "uint16")
            {
                for (uint i = tableAddr; i < (tableAddr + 512); i+=2)
                {
                    try
                    {
                        table[j] = BitConverter.ToUInt16(new byte[2] { MainForm.ecuRom.RomBytes[i + 1], MainForm.ecuRom.RomBytes[i] }, 0);
                    }
                    catch { }
                    j++;
                }
                
                for(int i = 0; i < 16; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.RowTemplate.Clone();
                    row.CreateCells(dataGridView1,
                        ConvertFromExpression(table[0 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[1 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[2 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[3 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[4 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[5 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[6 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[7 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[8 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[9 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[10 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[11 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[12 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[13 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[14 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[15 + (i * 16)], RomTable.Scaling.Expression)
                        );
                    dataGridView1.Rows.Add(row);
                }
            }
            else
            {
                byte[] table8 = new byte[256];
                for (uint i = tableAddr; i < (tableAddr + 256); i++)
                {
                    table8[i - tableAddr] = MainForm.ecuRom.RomBytes[i];
                }
                for (int i = 0; i < 16; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.RowTemplate.Clone();
                    row.CreateCells(dataGridView1,
                        ConvertFromExpression(table8[0 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[1 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[2 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[3 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[4 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[5 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[6 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[7 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[8 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[9 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[10 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[11 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[12 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[13 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[14 + (i * 16)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table8[15 + (i * 16)], RomTable.Scaling.Expression)
                        );
                    dataGridView1.Rows.Add(row);
                }
            }
            if(RomTable.XAxis.StorageType == "uint16")
            {
                xAx = new ushort[16];
                j = 0;
                for(uint i = xAxisAddr; i < (xAxisAddr + 32); i+=2)
                {
                    xAx[j] = BitConverter.ToUInt16(new byte[2] { MainForm.ecuRom.RomBytes[i + 1], MainForm.ecuRom.RomBytes[i] }, 0);
                    j++;
                }
                for(int i = 0; i < 16; i++)
                {
                    dataGridView1.Columns[i].HeaderText = ConvertFromExpression(xAx[i], RomTable.XAxis.Scaling.Expression).ToString();
                }
            }
            else
            {
                byte[] Ax8 = new byte[16];
                for (uint i = xAxisAddr; i < (xAxisAddr + 16); i++)
                {
                    Ax8[i - xAxisAddr] = MainForm.ecuRom.RomBytes[i];
                }
                for (int i = 0; i < 16; i++)
                {
                    dataGridView1.Columns[i].HeaderText = ConvertFromExpression(Ax8[i], RomTable.XAxis.Scaling.Expression).ToString();
                }
            }
            if (RomTable.YAxis.StorageType == "uint16")
            {
                yAx = new ushort[16];
                j = 0;
                for (uint i = yAxisAddr; i < (yAxisAddr + 32); i+=2)
                {
                    yAx[j] = BitConverter.ToUInt16(new byte[2] { MainForm.ecuRom.RomBytes[i + 1], MainForm.ecuRom.RomBytes[i] }, 0);
                    j++;
                }
                for (int i = 0; i < 16; i++)
                {
                    try
                    {
                        dataGridView1.Rows[i].HeaderCell.Value = ConvertFromExpression(yAx[i], RomTable.YAxis.Scaling.Expression).ToString();
                    }
                    catch { }
                }
            }
            else
            {
                byte[] Ax8 = new byte[16];
                for (uint i = yAxisAddr; i < (yAxisAddr + 16); i++)
                {
                    Ax8[i - yAxisAddr] = MainForm.ecuRom.RomBytes[i];
                }
                for (int i = 0; i < 16; i++)
                {
                    try
                    {
                        dataGridView1.Rows[i].HeaderCell.Value = ConvertFromExpression(Ax8[i], RomTable.YAxis.Scaling.Expression).ToString();
                    }
                    catch { }
                }
            }
        }

        private void SetTable()
        {
            YAxisText = RomTable.YAxis.Name;
            XAxisText = RomTable.XAxis.Name;
            byte[] table = new byte[64];
            byte[] xAx = new byte[8];
            byte[] yAx = new byte[8];
            tableAddr = Convert.ToUInt32(RomTable.StorageAddress, 16);
            xAxisAddr = Convert.ToUInt32(RomTable.XAxis.StorageAddress, 16);
            yAxisAddr = Convert.ToUInt32(RomTable.YAxis.StorageAddress, 16);
            if (RomTable.StorageType == "uint8")
            {
                for (uint i = tableAddr; i < (tableAddr + 64); i++)
                {
                    table[i - tableAddr] = MainForm.ecuRom.RomBytes[i];
                }
                for (int i = 0; i < 8; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.RowTemplate.Clone();
                    row.CreateCells(dataGridView1,
                        ConvertFromExpression(table[0 + (i * 8)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[1 + (i * 8)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[2 + (i * 8)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[3 + (i * 8)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[4 + (i * 8)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[5 + (i * 8)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[6 + (i * 8)], RomTable.Scaling.Expression),
                        ConvertFromExpression(table[7 + (i * 8)], RomTable.Scaling.Expression));
                    dataGridView1.Rows.Add(row);
                }
            }
            if (RomTable.XAxis.StorageType == "uint8")
            {
                for (uint i = xAxisAddr; i < (xAxisAddr + 8); i++)
                {
                    xAx[i - xAxisAddr] = MainForm.ecuRom.RomBytes[i];
                }
                for (int i = 0; i < 8; i++)
                {
                    dataGridView1.Columns[i].HeaderText = ConvertFromExpression(xAx[i], RomTable.XAxis.Scaling.Expression).ToString();
                }
            }
            if (RomTable.YAxis.StorageType == "uint8")
            {
                for (uint i = yAxisAddr; i < (yAxisAddr + 8); i++)
                {
                    yAx[i - yAxisAddr] = MainForm.ecuRom.RomBytes[i];
                }
                for (int i = 0; i < 8; i++)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = ConvertFromExpression(yAx[i], RomTable.YAxis.Scaling.Expression).ToString();
                }
            }
        }

        private double ConvertFromExpression(byte data, string expr)
        {
            DataTable dt = new DataTable();
            expr = expr.Replace("x", Convert.ToString(data));
            if (RomTable.Name.Contains("Timing"))
            {
                return Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2) <= 128 ? Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2) : Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2) - 128;
            }
            else
            {
                return Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2);
            }
        }

        private double ConvertFromExpression(ushort data, string expr)
        {
            DataTable dt = new DataTable();
            expr = expr.Replace("x", Convert.ToString(data));
            return Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2) <= 128 ? Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2) : Math.Round(Convert.ToDouble(dt.Compute(expr, String.Empty)), 2) - 128;
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

        public void SaveDataOnClose()
        {
            if (RomTable.SizeX == "8" && RomTable.SizeY == "8")
            {
                byte[] table = new byte[64];
                int k = 0;
                for(int i = 0; i < 8; i++)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        table[k] = ConvertToRomByte(dataGridView1.Rows[i].Cells[j].Value.ToString(), RomTable.Scaling.To_Byte);
                        k++;
                    }
                }
                for(uint i = tableAddr; i < (tableAddr + 64); i++)
                {
                    MainForm.ecuRom.RomBytes[i] = table[i - tableAddr];
                }
            }
            else if(RomTable.StorageType == "uint16")
            {
                ushort[] table = new ushort[256];
                int k = 0;
                for(int i = 0; i < 16; i++)
                {
                    for(int j = 0; j < 16; j++)
                    {
                        table[k] = ConvertToUShort(dataGridView1.Rows[i].Cells[j].Value.ToString(), RomTable.Scaling.To_Byte);
                        k++;
                    }
                }
                k = 0;
                for(uint i = tableAddr; i < (tableAddr + 512); i += 2)
                {
                    MainForm.ecuRom.RomBytes[i] = (byte)(table[k] >> 8);
                    MainForm.ecuRom.RomBytes[i + 1] = (byte)(table[k]);
                    k++;
                }
            }
            else
            {
                byte[] table = new byte[256];
                int k = 0;
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        table[k] = ConvertToRomByte(dataGridView1.Rows[i].Cells[j].Value.ToString(), RomTable.Scaling.To_Byte);
                        k++;
                    }
                }
                for (uint i = tableAddr; i < (tableAddr + 256); i++)
                {
                    MainForm.ecuRom.RomBytes[i] = table[i - tableAddr];
                }
            }
        }

        private void IncrementCell()
        {
            double fineIncrement = Convert.ToDouble(RomTable.Scaling.FineIncrement);
            double newValue = Convert.ToDouble(dataGridView1.CurrentCell.Value) + fineIncrement;
            dataGridView1.CurrentCell.Value = newValue;
        }

        public void IncrementCell(DataGridViewCell cell)
        {
            double fineIncrement = Convert.ToDouble(RomTable.Scaling.FineIncrement);
            double newValue = Convert.ToDouble(cell.Value) + fineIncrement;
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

        public void DecrementCell(DataGridViewCell cell)
        {
            double fineIncrement = Convert.ToDouble(RomTable.Scaling.FineIncrement);
            double newValue = Convert.ToDouble(cell.Value) - fineIncrement;
            cell.Value = newValue;
        }

        private void DecrementCell()
        {
            double fineIncrement = Convert.ToDouble(RomTable.Scaling.FineIncrement);
            double newValue = Convert.ToDouble(dataGridView1.CurrentCell.Value) - fineIncrement;
            dataGridView1.CurrentCell.Value = newValue;
        }

        private void LoadHeatmap()
        {
            List<double> values = new List<double>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    values.Add(Convert.ToDouble(cell.Value));
                }
            }
            if (values.Count() == 0) return;
            double max = values.Max();
            double min = values.Min();
            double i1 = max - ((max - min) / 8);
            double i2 = max - (((max - min) / 8) * 2);
            double i3 = max - (((max - min) / 8) * 3);
            double i4 = max - (((max - min) / 8) * 4);
            double i5 = max - (((max - min) / 8) * 5);
            double i6 = max - (((max - min) / 8) * 6);
            double i7 = max - (((max - min) / 8) * 7);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
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
                    else if (value > i5 && value <= i4)
                    {
                        cell.Style.BackColor = heatMapColors[4];
                    }
                    else if (value > i6 && value <= i5)
                    {
                        cell.Style.BackColor = heatMapColors[5];
                    }
                    else if (value > i7 && value <= i6)
                    {
                        cell.Style.BackColor = heatMapColors[6];
                    }
                    else
                    {
                        cell.Style.BackColor = heatMapColors[7];
                    }
                }
            }
        }
        private void Table3DView_Load(object sender, EventArgs e)
        {
            if(RomTable.SizeX == "8" && RomTable.SizeY == "8")
            {
                SetTable();
            }
            else
            {
                SetTable16x16();
            }
            LoadHeatmap();
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
                foreach(DataGridViewCell cell in Grid.SelectedCells)
                {
                    DecrementCell(cell);
                }
            }
            Grid.CurrentCell.ReadOnly = false;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            LoadHeatmap();
        }
    }
}
