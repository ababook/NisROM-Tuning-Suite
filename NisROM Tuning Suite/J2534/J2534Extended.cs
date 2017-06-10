using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite.J2534
{
    public class J2534Extended : J2534, IJ2534Extended
    {
        public J2534Err GetConfig(int channelId, ref List<SConfig> config)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;

            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.GET_CONFIG, input, output);
        }

        public J2534Err SetConfig(int channelId, ref List<SConfig> config)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;

            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.SET_CONFIG, input, output);
        }

        public J2534Err ReadBatteryVoltage(int deviceId, ref int voltage)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = Marshal.AllocHGlobal(8);

            J2534Err returnValue = (J2534Err)m_wrapper.Ioctl(deviceId, (int)Ioctl.READ_VBATT, input, output);
            if (returnValue == J2534Err.STATUS_NOERROR)
            {
                voltage = Marshal.ReadInt32(output);
            }

            Marshal.FreeHGlobal(output);

            return returnValue;
        }

        public J2534Err FiveBaudInit(int channelId, byte targetAddress, ref byte keyword1, ref byte keyword2)
        {
            J2534Err returnValue;
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;

            SByteArray inputArray = new SByteArray();
            SByteArray outputArray = new SByteArray();
            inputArray.NumOfBytes = 1;
            unsafe
            {
                //inputArray.BytePtr[0] = targetAddress;
                outputArray.NumOfBytes = 2;

                Marshal.StructureToPtr(inputArray, input, true);
                Marshal.StructureToPtr(outputArray, output, true);

                returnValue = (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.FIVE_BAUD_INIT, input, output);

                Marshal.PtrToStructure(output, outputArray);
            }
            return returnValue;
        }

        public J2534Err FastInit(int channelId, PassThruMsg txMsg, ref PassThruMsg rxMsg)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;
            PassThruMsg uRxMsg = new PassThruMsg();

            Marshal.StructureToPtr(txMsg, input, true);
            Marshal.StructureToPtr(uRxMsg, output, true);

            J2534Err returnValue = (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.FAST_INIT, input, output);
            if (returnValue == J2534Err.STATUS_NOERROR)
            {
                Marshal.PtrToStructure(output, rxMsg);
            }

            return returnValue;
        }

        public J2534Err ClearTxBuffer(int channelId)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;

            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.CLEAR_TX_BUFFER, input, output);
        }

        public J2534Err ClearRxBuffer(int channelId)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;

            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.CLEAR_RX_BUFFER, input, output);
        }

        public J2534Err ClearPeriodicMsgs(int channelId)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;

            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.CLEAR_PERIODIC_MSGS, input, output);
        }

        public J2534Err ClearMsgFilters(int channelId)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;

            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.CLEAR_MSG_FILTERS, input, output);
        }

        public J2534Err ClearFunctMsgLookupTable(int channelId)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;

            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.CLEAR_FUNCT_MSG_LOOKUP_TABLE, input, output);
        }

        public J2534Err AddToFunctMsgLookupTable(int channelId)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;
            // TODO: fix this
            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.ADD_TO_FUNCT_MSG_LOOKUP_TABLE, input, output);
        }

        public J2534Err DeleteFromFunctMsgLookupTable(int channelId)
        {
            IntPtr input = IntPtr.Zero;
            IntPtr output = IntPtr.Zero;
            // TODO: fix this
            return (J2534Err)m_wrapper.Ioctl(channelId, (int)Ioctl.DELETE_FROM_FUNCT_MSG_LOOKUP_TABLE, input, output);
        }
    }
}
