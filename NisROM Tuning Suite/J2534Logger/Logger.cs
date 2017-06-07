using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RGiesecke.DllExport;
using NisROM_Tuning_Suite.Utilities;
using NisROM_Tuning_Suite.J2534;

namespace NisROM_Tuning_Suite.J2534Logger
{
    public class Logger
    {
        [DllExport("PassThruOpen")]
        public static J2534Err PassThruOpen(IntPtr name, ref int deviceId)
        {
            Log.WriteLine("------------------------------------------");
            Log.WriteLine("start log on {0} at {1}", Loader.Device.Name, DateTime.Now);
            Log.WriteLine("------------------------------------------");

            Log.WriteTimestamp("", "PTOpen({0}, {1})", name.ToString("X8"), deviceId);

            Log.WriteLine("  name: ", name.AsString());

            var result = Loader.Lib.PassThruOpen(name, ref deviceId);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruClose")]
        public static J2534Err PassThruClose(int deviceId)
        {
            Log.WriteTimestamp("", "PTClose({0})", deviceId);

            var result = Loader.Lib.PassThruClose(deviceId);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruConnect")]
        public static J2534Err PassThruConnect(int deviceId, ProtocolID protocolId, ConnectFlag flags, BaudRate baudRate, ref int channelId)
        {
            Log.WriteTimestamp("", "PTConnect({0}, {1}, {2}, {3}, {4})", deviceId, protocolId, flags, baudRate, channelId);

            var result = Loader.Lib.PassThruConnect(deviceId, protocolId, flags, baudRate, ref channelId);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruDisconnect")]
        public static J2534Err PassThruDisconnect(int channelId)
        {
            Log.WriteTimestamp("", "PTDisconnect({0})", channelId);

            var result = Loader.Lib.PassThruDisconnect(channelId);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruReadMsgs")]
        public static J2534Err PassThruReadMsgs(int channelId, IntPtr msgs, ref int numMsgs, int timeout)
        {
            Log.WriteTimestamp("", "PTReadMsgs({0}, 0x{1}, {2}, {3})", channelId, msgs.ToString("X8"), numMsgs, timeout);

            var result = Loader.Lib.PassThruReadMsgs(channelId, msgs, ref numMsgs, timeout);

            Log.WriteLine(msgs.AsMsgList(numMsgs).AsString());
            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);


            return result;
        }

        [DllExport("PassThruWriteMsgs")]
        public static J2534Err PassThruWriteMsgs(int channelId, IntPtr msgs, ref int numMsgs, int timeout)
        {
            Log.WriteTimestamp("", "PTWriteMsgs({0}, 0x{1}, {2}, {3})", channelId, msgs.ToString("X8"), numMsgs, timeout);
            Log.WriteLine(msgs.AsMsgList(numMsgs).AsString());

            var result = Loader.Lib.PassThruWriteMsgs(channelId, msgs, ref numMsgs, timeout);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruStartPeriodicMsg")]
        public static J2534Err PassThruStartPeriodicMsg(int channelId, IntPtr msg, ref int msgId, int timeInterval)
        {
            Log.WriteTimestamp("", "PTStartPeriodicMsg({0}, 0x{1}, {2}, {3})", channelId, msg.ToString("X8"), msgId, timeInterval);
            Log.WriteLine(msg.AsStruct<PassThruMsg>().ToString("\t"));

            var result = Loader.Lib.PassThruStartPeriodicMsg(channelId, msg, ref msgId, timeInterval);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruStopPeriodicMsg")]
        public static J2534Err PassThruStopPeriodicMsg(int channelId, int msgId)
        {
            Log.WriteTimestamp("", "PTStopPeriodicMsg({0}, {1})", channelId, msgId);

            var result = Loader.Lib.PassThruStopPeriodicMsg(channelId, msgId);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruStartMsgFilter")]
        public static J2534Err PassThruStartMsgFilter(int channelid, int filterType, IntPtr maskMsg,
            IntPtr patternMsg, IntPtr flowControlMsg, ref int filterId)
        {
            Log.WriteTimestamp("", "PTStartMsgFilter({0}, {1}, 0x{2}, 0x{3}, 0x{4}, {5})",
                channelid,
                filterType,
                maskMsg.ToString("X8"),
                patternMsg.ToString("X8"),
                flowControlMsg.ToString("X8"),
                filterId);

            Log.WriteLine("  maskMsg: {0}", maskMsg.AsStruct<PassThruMsg>().ToString("\t"));
            Log.WriteLine("  patternMsg: {0}", patternMsg.AsStruct<PassThruMsg>().ToString("\t"));
            Log.WriteLine("  flowControlMsg: {0}", flowControlMsg.AsStruct<PassThruMsg>().ToString("\t"));

            var result = Loader.Lib.PassThruStartMsgFilter(channelid, (FilterType)filterType, maskMsg,
                patternMsg, flowControlMsg, ref filterId);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruStopMsgFilter")]
        public static J2534Err PassThruStopMsgFilter(int channelId, int filterId)
        {
            Log.WriteTimestamp("", "PTStopMsgFilter({0}, {1})", channelId, filterId);

            var result = Loader.Lib.PassThruStopMsgFilter(channelId, filterId);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruSetProgrammingVoltage")]
        public static J2534Err PassThruSetProgrammingVoltage(int deviceId, PinNumber pinNumber, int voltage)
        {
            Log.WriteTimestamp("", "PTSetProgrammingVoltage({0}, {1}, {2})", deviceId, pinNumber, voltage);

            var result = Loader.Lib.PassThruSetProgrammingVoltage(deviceId, pinNumber, voltage);

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruReadVersion")]
        public static J2534Err PassThruReadVersion(
            int deviceId, IntPtr firmwareVersion, IntPtr dllVersion, IntPtr apiVersion)
        {
            Log.WriteTimestamp("", "PTReadVersion({0}, 0x{1}, 0x{2}, 0x{3})",
                deviceId,
                firmwareVersion.ToString("X8"),
                dllVersion.ToString("X8"),
                apiVersion.ToString("X8"));

            var result = Loader.Lib.PassThruReadVersion(deviceId, firmwareVersion, dllVersion, apiVersion);

            Log.WriteLine("  Firmware: " + firmwareVersion.AsString());
            Log.WriteLine("  DLL:      " + dllVersion.AsString());
            Log.WriteLine("  API:      " + apiVersion.AsString());

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return result;
        }

        [DllExport("PassThruGetLastError")]
        public static J2534Err PassThruGetLastError(IntPtr errorDescription)
        {
            var result = Loader.Lib.PassThruGetLastError(errorDescription);

            Log.WriteLine("  error: " + errorDescription.AsString());

            return result;
        }

        [DllExport("PassThruIoctl")]
        public static int PassThruIoctl(int channelId, Ioctl ioctlID, IntPtr input, IntPtr output)
        {
            Log.WriteTimestamp("", "PTIoctl({0}, {1}, 0x{2}, 0x{3})", channelId, ioctlID, input.ToString("X8"),
                output.ToString("X8"));

            if (input == IntPtr.Zero)
            {
                Log.WriteLine("  Input is null");
            }
            else
            {
                Log.Write("  Input: ");
                switch (ioctlID)
                {
                    case Ioctl.SET_CONFIG:
                        var configList = input.AsStruct<SConfigList>();
                        if (configList.Count > 0) Log.WriteLine("");
                        foreach (var config in configList.GetList())
                        {
                            Log.WriteLine("  {0} = {1}", config.Parameter, config.Value);
                        }
                        break;
                    case Ioctl.FAST_INIT:
                        Log.WriteLine(input.AsStruct<PassThruMsg>().ToString("\t"));
                        break;
                    case Ioctl.FIVE_BAUD_INIT:
                    case Ioctl.ADD_TO_FUNCT_MSG_LOOKUP_TABLE:
                    case Ioctl.DELETE_FROM_FUNCT_MSG_LOOKUP_TABLE:
                        Log.WriteLine(Environment.NewLine + "    " + input.AsStruct<SByteArray>().ToString());
                        break;
                    default:
                        Log.WriteLine("");
                        break;
                }
            }

            var result = Loader.Lib.PassThruIoctl(channelId, (int)ioctlID, input, output);

            if (result == J2534Err.STATUS_NOERROR)
            {
                if (output == IntPtr.Zero)
                {
                    Log.WriteLine("  Output is null");
                }
                else
                {
                    Log.Write("  Output:");
                    switch (ioctlID)
                    {
                        case Ioctl.GET_CONFIG:
                            var configList = input.AsStruct<SConfigList>();
                            foreach (var config in configList.GetList())
                            {
                                Log.WriteLine("    {0} = {1}", config.Parameter, config.Value);
                            }
                            break;
                        case Ioctl.READ_VBATT:
                        case Ioctl.READ_PROG_VOLTAGE:
                            Log.WriteLine(" {0:#.000} Volts", output.AsStruct<uint>() / 1000.0);
                            break;
                        case Ioctl.FAST_INIT:
                            Log.WriteLine(output.AsStruct<PassThruMsg>().ToString("\t"));
                            break;
                        case Ioctl.FIVE_BAUD_INIT:
                            Log.WriteLine(Environment.NewLine + "    " + input.AsStruct<SByteArray>().ToString());
                            break;
                        default:
                            Log.WriteLine("");
                            break;
                    }
                }
            }

            Log.WriteTimestamp("  ", "{0}: {1}", (int)result, result);

            return (int)result;
        }
    }
}
