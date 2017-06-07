using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

using NisROM_Tuning_Suite.J2534;

namespace NisROM_Tuning_Suite.J2534Logger
{
    public class Loader
    {
        private static readonly Loader instance = new Loader();

        private J2534Device j2534Device;

        private J2534Extended j2534library;

        private Loader()
        {
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            j2534library = new J2534Extended();

            var list = J2534Detect.ListDevices();

            j2534Device = list.FirstOrDefault(d => d.Name == Config.Instance.DeviceName);
            if (j2534Device == null)
            {
                if (list.Count == 1)
                {
                    j2534Device = list.Single();
                }
                else
                {
                    var sd = new SelectDevice();
                    if (sd.ShowDialog() == DialogResult.OK)
                    {
                        j2534Device = sd.Device;

                    }
                }
            }

            j2534library.LoadLibrary(j2534Device);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.WriteLine("Unhandled exception: {0}", e.ExceptionObject.ToString());
        }

        public static J2534Device Device { get { return instance.j2534Device; } }

        public static IJ2534 Lib { get { return instance.j2534library; } }
    }
}
