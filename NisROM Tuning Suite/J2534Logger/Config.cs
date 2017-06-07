using System;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite.J2534Logger
{
    public class Config
    {
        private readonly static Config instance = new Config();

        public static Config Instance
        {
            get { return instance; }
        }

        private Config()
        {
            var location = Assembly.GetExecutingAssembly().Location;

            var config = ConfigurationManager.OpenExeConfiguration(location);

            var configValue = config.AppSettings.Settings["DeviceName"];
            DeviceName = configValue != null ? configValue.Value : string.Empty;

            configValue = config.AppSettings.Settings["FileName"];
            if (configValue != null)
            {
                FileName = configValue.Value;
            }

            if (string.IsNullOrEmpty(FileName))
            {
                FileName = string.Format(Path.Combine(Path.GetDirectoryName(location),
                    string.Format("{0:yyyyMMdd-HHmmss}.log", DateTime.Now)));
            }
        }

        public string FileName { get; private set; }

        public string DeviceName { get; private set; }
    }
}
