using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using NisROM_Tuning_Suite.EcuLogging;

namespace NisROM_Tuning_Suite
{
    public partial class LoggerForm : Form
    {
        private Stopwatch stopwatch = new Stopwatch();
        private Thread logThread;
        private List<LogParameter> paramz = new List<LogParameter>()
        {
            new LogParameter
            {
                ParamName = "Coolant Temperature",
                ParamShortName = "Coolant Temp",
                Address = "0x1101",
                Units = "Celsius"
            },
            new LogParameter
            {
                ParamName = "Vehicle Speed Sensor",
                ParamShortName = "Vehicle Speed",
                Address = "0x1102",
                Units = "MPH"
            },
            new LogParameter
            {
                ParamName = "Battery Voltage",
                ParamShortName = "Battery Volts",
                Address = "0x1103",
                Units = "Volts"
            },
                        new LogParameter
            {
                ParamName = "Ignition Timing",
                ParamShortName = "Ign Timing",
                Address = "0x1107",
                Units = "DBTDC"
            },
            new LogParameter
            {
                ParamName = "Air/Fuel Correction - Bank 1",
                ParamShortName = "A/F Corr - B1",
                Address = "0x1123",
                Units = "%"
            },
            new LogParameter
            {
                ParamName = "Air/Fuel Correction - Bank 2",
                ParamShortName = "A/F Corr - B2",
                Address = "0x1124",
                Units = "%"
            },
            new LogParameter
            {
                ParamName = "Air/Fuel Ratio",
                ParamShortName = "AFR",
                Address = "0x1134",
                Units = ""
            },
        };

        public LoggerForm()
        {
            InitializeComponent();
            SetParameterList();
            logThread = new Thread(new ThreadStart(StartLog));
            logThread.Start();
        }

        private void StartLog()
        {
            stopwatch.Start();
            while (true)
            {
                logView1.Grid.Rows[0].Cells[0].Value = ((double)(stopwatch.ElapsedMilliseconds / 1000)).ToString();
                if(stopwatch.ElapsedMilliseconds >= 10000)
                {
                    logView1.Grid.Rows[0].Cells[0].Value = 0;
                    break;
                }
            }
        }

        private void SetParameterList()
        {
            logView1.ParamList.Items.AddRange(paramz.Select(i => i.ParamName).ToArray());
        }

        private void LoggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopwatch.Stop();
        }
    }
}
