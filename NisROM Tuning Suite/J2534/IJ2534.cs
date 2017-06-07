using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite.J2534
{
    public interface IJ2534
    {
        bool LoadLibrary(J2534Device device);
        bool FreeLibrary();

        J2534Err PassThruOpen(IntPtr name, ref int deviceId);
        J2534Err PassThruClose(int deviceId);

        J2534Err PassThruConnect(int deviceId, ProtocolID protocolId, ConnectFlag flags, BaudRate baudRate, ref int channelId);

        J2534Err PassThruDisconnect(int channelId);
        J2534Err PassThruReadMsgs(int channelId, IntPtr msgs, ref int numMsgs, int timeout);
        J2534Err PassThruWriteMsgs(int channelId, IntPtr msg, ref int numMsgs, int timeout);
        J2534Err PassThruStartPeriodicMsg(int channelId, IntPtr msg, ref int msgId, int timeInterval);
        J2534Err PassThruStopPeriodicMsg(int channelId, int msgId);

        J2534Err PassThruStartMsgFilter(int channelid, FilterType filterType, IntPtr maskMsg,
            IntPtr patternMsg, IntPtr flowControlMsg, ref int filterId);

        J2534Err PassThruStopMsgFilter(int channelId, int filterId);
        J2534Err PassThruSetProgrammingVoltage(int deviceId, PinNumber pinNumber, int voltage);
        J2534Err PassThruReadVersion(int deviceId, IntPtr firmwareVersion, IntPtr dllVersion, IntPtr apiVersion);
        J2534Err PassThruGetLastError(IntPtr errorDescription);
        J2534Err PassThruIoctl(int channelId, int ioctlID, IntPtr input, IntPtr output);
    }
}
