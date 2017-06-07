using System;

namespace NisROM_Tuning_Suite.J2534Logger
{
    public class Log
    {
        private static readonly Log instance = new Log();

        private DateTime startTime = DateTime.Now;

        private Log()
        {
        }

        public TimeSpan Timestamp
        {
            get { return DateTime.Now - startTime; }
        }

        public static void Write(object val)
        {
            using (var stream = new FormattedStreamWriter(Config.Instance.FileName, true))
            {
                stream.Write(val);
                stream.Flush();
            }
        }

        public static void WriteLine(object val)
        {
            using (var stream = new FormattedStreamWriter(Config.Instance.FileName, true))
            {
                stream.WriteLine(val);
                stream.Flush();
            }
        }

        public static void WriteLine(string format, params object[] args)
        {
            using (var stream = new FormattedStreamWriter(Config.Instance.FileName, true))
            {
                stream.WriteLine(format, args);
                stream.Flush();
            }
        }

        public static void WriteTimestamp(object val)
        {
            using (var stream = new FormattedStreamWriter(Config.Instance.FileName, true))
            {
                stream.WriteLine("{0}s {1}", instance.Timestamp.Milliseconds, val);
                stream.Flush();
            }
        }

        public static void WriteTimestamp(string prefix, string format, params object[] args)
        {
            using (var stream = new FormattedStreamWriter(Config.Instance.FileName, true))
            {
                stream.WriteLine("{0}{1:##.000}s {2}", prefix, instance.Timestamp.TotalMilliseconds / 1000.0, string.Format(format, args));
                stream.Flush();
            }
        }

        public static void WriteTimestamp(string format, params object[] args)
        {
            using (var stream = new FormattedStreamWriter(Config.Instance.FileName, true))
            {
                stream.WriteLine("{0}s {1}", instance.Timestamp.Milliseconds, string.Format(format, args));
                stream.Flush();
            }
        }
    }
}
