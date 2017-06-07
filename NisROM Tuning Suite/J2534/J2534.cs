using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite.J2534
{
    public class J2534 : IJ2534
    {
        protected J2534Device m_device;
        protected J2534DllWrapper m_wrapper;

        public bool LoadLibrary(J2534Device device)
        {
            m_device = device;
            m_wrapper = new J2534DllWrapper();
            return m_wrapper.LoadJ2534Library(m_device.FunctionLibrary);
        }

        public bool FreeLibrary()
        {
            return m_wrapper.FreeLibrary();
        }

        public J2534Err PassThruOpen(IntPtr name, ref int deviceId)
        {
            return (J2534Err)m_wrapper.Open(name, ref deviceId);
        }

        public J2534Err PassThruClose(int deviceId)
        {
            return (J2534Err)m_wrapper.Close(deviceId);
        }

        public J2534Err PassThruConnect(int deviceId, ProtocolID protocolId, ConnectFlag flags, BaudRate baudRate, ref int channelId)
        {
            return (J2534Err)m_wrapper.Connect(deviceId, (int)protocolId, (int)flags, (int)baudRate, ref channelId);
        }

        public J2534Err PassThruDisconnect(int channelId)
        {
            return (J2534Err)m_wrapper.Disconnect(channelId);
        }

        public J2534Err PassThruReadMsgs(int channelId, IntPtr msgs, ref int numMsgs, int timeout)
        {
            return (J2534Err)m_wrapper.ReadMsgs(channelId, msgs, ref numMsgs, timeout);
        }

        public J2534Err PassThruWriteMsgs(int channelId, IntPtr msgs, ref int numMsgs, int timeout)
        {
            return (J2534Err)m_wrapper.WriteMsgs(channelId, msgs, ref numMsgs, timeout);
        }

        public J2534Err PassThruStartPeriodicMsg(int channelId, IntPtr msg, ref int msgId, int timeInterval)
        {
            return (J2534Err)m_wrapper.StartPeriodicMsg(channelId, msg, ref msgId, timeInterval);
        }

        public J2534Err PassThruStopPeriodicMsg(int channelId, int msgId)
        {
            return (J2534Err)m_wrapper.StopPeriodicMsg(channelId, msgId);
        }

        public J2534Err PassThruStartMsgFilter(int channelid, FilterType filterType, IntPtr maskMsg,
            IntPtr patternMsg, IntPtr flowControlMsg, ref int filterId)
        {
            return
                (J2534Err)
                    m_wrapper.StartMsgFilter(channelid, (int)filterType, maskMsg, patternMsg,
                        flowControlMsg, ref filterId);
        }

        public J2534Err PassThruStopMsgFilter(int channelId, int filterId)
        {
            return (J2534Err)m_wrapper.StopMsgFilter(channelId, filterId);
        }

        public J2534Err PassThruSetProgrammingVoltage(int deviceId, PinNumber pinNumber, int voltage)
        {
            return (J2534Err)m_wrapper.SetProgrammingVoltage(deviceId, (int)pinNumber, voltage);
        }

        public J2534Err PassThruReadVersion(int deviceId, IntPtr firmwareVersion, IntPtr dllVersion, IntPtr apiVersion)
        {
            return (J2534Err)m_wrapper.ReadVersion(deviceId, firmwareVersion, dllVersion, apiVersion);
        }

        public J2534Err PassThruGetLastError(IntPtr errorDescription)
        {
            return (J2534Err)m_wrapper.GetLastError(errorDescription);
        }

        public J2534Err PassThruIoctl(int channelId, int ioctlID, IntPtr input, IntPtr output)
        {
            return (J2534Err)m_wrapper.Ioctl(channelId, ioctlID, input, output);
        }
    }
}
