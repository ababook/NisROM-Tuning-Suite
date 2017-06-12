using NisROM_Tuning_Suite.J2534;
using NisROM_Tuning_Suite.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace NisROM_Tuning_Suite
{
    public class ObdComm
    {
        private IJ2534Extended m_j2534Interface;
        ProtocolID m_protocol;
        int m_deviceId;
        int m_channelId;
        bool m_isConnected;
        J2534Err m_status;

        public ObdComm(IJ2534Extended j2534Interface)
        {
            m_j2534Interface = j2534Interface;
            m_isConnected = false;
            m_protocol = ProtocolID.ISO15765;
            m_status = J2534Err.STATUS_NOERROR;
        }

        public bool GetFaults(ref string[] faults)
        {
            List<byte> value = new List<byte>();
            if (ReadObdPid(0x03, 0x00, ProtocolID.ISO15765, ref value))
            {
                if (value.Count == 1)
                {
                    return true;
                }
                return true;
            }
            return false;
        }

        public bool ClearFaults()
        {
            List<byte> value = new List<byte>();
            if (ReadObdPid(0x04, 0x00, m_protocol, ref value))
            {
                return true;
            }
            return false;
        }

        private void GetAvailableObdPidsAt(byte start, ref List<byte> availablePids)
        {
            List<byte> value = new List<byte>();
            if (ReadObdPid(0x01, start, ProtocolID.ISO15765, ref value))
            {
                for (int i = 0; i < value.Count; i++)
                {
                    for (int shift = 0; shift < 8; shift++)
                    {
                        byte mask = (byte)(0x80 >> shift);
                        if ((value[i] & mask) != 0)
                        {
                            availablePids.Add((byte)((i * 0x8) + shift + 1 + start));
                        }
                    }
                }

                if (availablePids.Contains((byte)(start + 0x20)))
                {
                    GetAvailableObdPidsAt((byte)(start + 0x20), ref availablePids);
                }
            }
            return;
        }

        public bool GetAvailableObdPids(ref List<byte> availablePids)
        {
            availablePids.Clear();
            GetAvailableObdPidsAt(0x00, ref availablePids);
            return (availablePids.Count > 0);
        }

        public string GetObdPidValue(string pidName)
        {
            return null;
        }

        public string[] GetAllPidValues()
        {
            return null;
        }

        public bool GetBatteryVoltage(ref double voltage)
        {
            int millivolts = 0;
            m_status = m_j2534Interface.ReadBatteryVoltage(m_deviceId, ref millivolts);
            if (J2534Err.STATUS_NOERROR == m_status)
            {
                voltage = millivolts / 1000.0;
                return true;
            }
            return false;
        }

        public bool GetVin(ref string vin)
        {
            List<byte> value = new List<byte>();
            if (ReadObdPid(0x09, 0x02, m_protocol, ref value))
            {
                if (value.Count > 0)
                {
                    // Remove the first byte, it's not part of the VIN
                    value.RemoveAt(0);
                    vin = Encoding.ASCII.GetString(value.ToArray());
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool IsConnected()
        {
            return m_isConnected;
        }

        public bool DetectProtocol()
        {
            // possible return values:
            //  ProtocolID.ISO15765; // CAN
            //  ProtocolID.ISO9141;  // ISO-K
            //  ProtocolID.J1850PWM;  // J1850PWM
            //  ProtocolID.J1850VPW;  // J1850VPW
            m_deviceId = 0;
            m_status = m_j2534Interface.PassThruOpen(IntPtr.Zero, ref m_deviceId);
            if (m_status != J2534Err.STATUS_NOERROR)
                return false;
            if (ConnectIso15765())
            {
                m_protocol = ProtocolID.ISO15765;
                m_isConnected = true;
            }
            return true;
        }

        public bool ConnectIso15765()
        {
            List<byte> value = new List<byte>();

            m_status = m_j2534Interface.PassThruConnect(m_deviceId, ProtocolID.ISO15765, ConnectFlag.NONE, BaudRate.ISO15765, ref m_channelId);
            if (J2534Err.STATUS_NOERROR != m_status)
            {
                return false;
            }

            int filterId = 0;

            byte i;

            for (i = 0; i < 1; i++)
            {
                PassThruMsg maskMsg = new PassThruMsg(ProtocolID.ISO15765, TxFlag.ISO15765_FRAME_PAD, new byte[] { 0xff, 0xff, 0xff, 0xff });
                PassThruMsg patternMsg = new PassThruMsg(ProtocolID.ISO15765, TxFlag.ISO15765_FRAME_PAD, new byte[] { 0x00, 0x00, 0x07, (byte)(0xE8 + i) });
                PassThruMsg flowControlMsg = new PassThruMsg(ProtocolID.ISO15765, TxFlag.ISO15765_FRAME_PAD, new byte[] { 0x00, 0x00, 0x07, (byte)(0xE0 + i) });
                m_status = m_j2534Interface.PassThruStartMsgFilter(m_channelId, FilterType.FLOW_CONTROL_FILTER, maskMsg.ToIntPtr(), patternMsg.ToIntPtr(), flowControlMsg.ToIntPtr(), ref filterId);
                if (J2534Err.STATUS_NOERROR != m_status)
                {
                    m_j2534Interface.PassThruDisconnect(m_channelId);
                    return false;
                }
            }

            if (!ReadObdPid(0x01, 0x00, ProtocolID.ISO15765, ref value))
            {
                m_j2534Interface.PassThruDisconnect(m_channelId);
                return false;
            }
            return true;
        }

        public bool Disconnect()
        {
            m_status = m_j2534Interface.PassThruClose(m_deviceId);
            if (m_status != J2534Err.STATUS_NOERROR)
            {
                return false;
            }
            return true;
        }

        public J2534Err GetLastError()
        {
            return m_status;
        }

        public bool ReadObdPid(byte mode, byte pid, ProtocolID protocolId, ref List<byte> value)
        {
            PassThruMsg txMsg = new PassThruMsg();
            int timeout;
            txMsg.ProtocolID = protocolId;
            switch (protocolId)
            {
                case ProtocolID.ISO15765:
                    txMsg.TxFlags = TxFlag.ISO15765_FRAME_PAD;
                    if (mode == 0x03 || mode == 0x04)
                    {
                        txMsg.SetBytes(new byte[] { 0x00, 0x00, 0x07, 0xdf, mode });
                    }
                    else
                    {
                        txMsg.SetBytes(new byte[] { 0x00, 0x00, 0x07, 0xdf, mode, pid });
                    }
                    timeout = 50;
                    break;
                case ProtocolID.J1850PWM:
                case ProtocolID.J1850VPW:
                case ProtocolID.ISO9141:
                case ProtocolID.ISO14230:
                    byte protocolByte = (byte)((protocolId == ProtocolID.J1850PWM) ? 0x61 : 0x68);
                    txMsg.TxFlags = TxFlag.NONE;
                    txMsg.SetBytes(new byte[] { protocolByte, 0x6A, 0xF1, mode, pid });
                    timeout = 100;
                    break;
                default:
                    return false;
            }

            m_j2534Interface.ClearRxBuffer(m_channelId);

            int numMsgs = 1;
            m_status = m_j2534Interface.PassThruWriteMsgs(m_channelId, txMsg.ToIntPtr(), ref numMsgs, timeout);
            if (J2534Err.STATUS_NOERROR != m_status)
            {
                return false;
            }

            IntPtr rxMsgs = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PassThruMsg)) * numMsgs);
            numMsgs = 1;
            while (J2534Err.STATUS_NOERROR == m_status)
            {
                m_status = m_j2534Interface.PassThruReadMsgs(m_channelId, rxMsgs, ref numMsgs, timeout * 4);
            }

            if (J2534Err.ERR_BUFFER_EMPTY == m_status || J2534Err.ERR_TIMEOUT == m_status)
            {
                if (numMsgs > 0)
                {
                    PassThruMsg msg = rxMsgs.AsMsgList(numMsgs).Last();
                    value = msg.GetBytes().ToList();
                    value.RemoveRange(0, txMsg.GetBytes().Length);
                    return true;
                }
                return false;
            }
            return false;
        }

        public List<byte> ReadBytesISO15765Packet(uint offset, byte length, ProtocolID protocolId)
        {
            List<byte> value = new List<byte>();
            PassThruMsg txMsg = new PassThruMsg
            {
                ProtocolID = protocolId,
                TxFlags = TxFlag.ISO15765_FRAME_PAD
            };
            byte[] buffer = new byte[] { 0x23, (byte)(offset >> 24), (byte)(offset >> 16), (byte)(offset >> 8), (byte)(offset), 0x00, length };
            txMsg.SetBytes(buffer);
            int timeout = 50;
            m_j2534Interface.ClearRxBuffer(m_channelId);
            int numMsgs = 1;
            m_status = m_j2534Interface.PassThruWriteMsgs(m_channelId, txMsg.ToIntPtr(), ref numMsgs, timeout);
            if (J2534Err.STATUS_NOERROR != m_status)
            {
                return null;
            }
            IntPtr rxMsgs = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PassThruMsg)) * numMsgs);
            numMsgs = 1;
            while (J2534Err.STATUS_NOERROR == m_status)
            {
                m_status = m_j2534Interface.PassThruReadMsgs(m_channelId, rxMsgs, ref numMsgs, timeout * 4);
            }

            if (J2534Err.ERR_BUFFER_EMPTY == m_status || J2534Err.ERR_TIMEOUT == m_status)
            {
                if (numMsgs > 0)
                {
                    PassThruMsg msg = rxMsgs.AsMsgList(numMsgs).Last();
                    value = msg.GetBytes().ToList();
                    value.RemoveRange(0, txMsg.GetBytes().Length);
                    return value;
                }
                return null;
            }
            return null;
        }

        public void SetCANSession(byte session)
        {
            PassThruMsg txMsg = new PassThruMsg
            {
                ProtocolID = ProtocolID.ISO15765,
                TxFlags = TxFlag.ISO15765_FRAME_PAD
            };
            byte[] buffer = new byte[] { 0x10, session };
            txMsg.SetBytes(buffer);
            int timeout = 50;
            m_j2534Interface.ClearRxBuffer(m_channelId);
            int numMsgs = 1;
            m_status = m_j2534Interface.PassThruWriteMsgs(m_channelId, txMsg.ToIntPtr(), ref numMsgs, timeout);
        }

        public int GetRomSizeCAN()
        {
            SetCANSession(0xFB);
            int romSize = 0x80000;
            byte[] romCheck;
            try
            {
                romCheck = ReadBytesISO15765Packet(0x80000, 16, ProtocolID.ISO15765).ToArray();
                romSize = 0x100000;
                romCheck = ReadBytesISO15765Packet(0x100000, 16, ProtocolID.ISO15765).ToArray();
                romSize = 0x140000;
                romCheck = ReadBytesISO15765Packet(0x140000, 16, ProtocolID.ISO15765).ToArray();
                romSize = 0x180000;
                romCheck = ReadBytesISO15765Packet(0x180000, 16, ProtocolID.ISO15765).ToArray();
                romSize = 0x200000;
            }
            catch { }
            return romSize;
        }
    }
}
