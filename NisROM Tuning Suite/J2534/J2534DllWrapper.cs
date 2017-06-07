using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite.J2534
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
    }

    public class J2534DllWrapper
    {
        private IntPtr m_pDll;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruOpen(IntPtr name, ref int deviceId);

        public PassThruOpen Open;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruClose(int deviceId);

        public PassThruClose Close;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruConnect(int deviceId, int protocolId, int flags, int baudRate, ref int channelId);

        public PassThruConnect Connect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruDisconnect(int channelId);

        public PassThruDisconnect Disconnect;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruReadMsgs(int channelId, IntPtr pMessages, ref int numMsgs, int timeout);

        public PassThruReadMsgs ReadMsgs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruWriteMsgs(int channelId, IntPtr pMessages, ref int numMsgs, int timeout);

        public PassThruWriteMsgs WriteMsgs;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruStartPeriodicMsg(
            int channelId, IntPtr msg, ref int msgId, int timeInterval);

        public PassThruStartPeriodicMsg StartPeriodicMsg;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruStopPeriodicMsg(int channelId, int msgId);

        public PassThruStopPeriodicMsg StopPeriodicMsg;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruStartMsgFilter
            (
            int channelid,
            int filterType,
            IntPtr maskMsg,
            IntPtr patternMsg,
            IntPtr flowControlMsg,
            ref int filterId
            );

        public PassThruStartMsgFilter StartMsgFilter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruStartPassBlockMsgFilter
            (
            int channelid,
            int filterType,
            ref PassThruMsg uMaskMsg,
            ref PassThruMsg uPatternMsg,
            int nada,
            ref int filterId
            );

        public PassThruStartPassBlockMsgFilter StartPassBlockMsgFilter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruStopMsgFilter(int channelId, int filterId);

        public PassThruStopMsgFilter StopMsgFilter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruSetProgrammingVoltage(int deviceId, int pinNumber, int voltage);

        public PassThruSetProgrammingVoltage SetProgrammingVoltage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruReadVersion(
            int deviceId, IntPtr firmwareVersion, IntPtr dllVersion, IntPtr apiVersion);

        public PassThruReadVersion ReadVersion;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruGetLastError(IntPtr errorDescription);

        public PassThruGetLastError GetLastError;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PassThruIoctl(int channelId, int ioctlID, IntPtr input, IntPtr output);

        public PassThruIoctl Ioctl;

        public bool LoadJ2534Library(string path)
        {
            m_pDll = NativeMethods.LoadLibrary(path);

            if (m_pDll == IntPtr.Zero)
                return false;

            IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruOpen");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                Open = (PassThruOpen)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruOpen));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruClose");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                Close = (PassThruClose)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruClose));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruConnect");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                Connect = (PassThruConnect)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruConnect));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruDisconnect");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                Disconnect = (PassThruDisconnect)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruDisconnect));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruReadMsgs");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                ReadMsgs = (PassThruReadMsgs)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruReadMsgs));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruWriteMsgs");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                WriteMsgs = (PassThruWriteMsgs)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruWriteMsgs));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruStartPeriodicMsg");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                StartPeriodicMsg = (PassThruStartPeriodicMsg)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruStartPeriodicMsg));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruStopPeriodicMsg");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                StopPeriodicMsg = (PassThruStopPeriodicMsg)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruStopPeriodicMsg));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruStartMsgFilter");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                StartMsgFilter = (PassThruStartMsgFilter)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruStartMsgFilter));

#warning This address may not be correct
            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruStartMsgFilter");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                StartPassBlockMsgFilter = (PassThruStartPassBlockMsgFilter)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruStartPassBlockMsgFilter));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruStopMsgFilter");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                StopMsgFilter = (PassThruStopMsgFilter)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruStopMsgFilter));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruSetProgrammingVoltage");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                SetProgrammingVoltage = (PassThruSetProgrammingVoltage)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruSetProgrammingVoltage));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruReadVersion");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                ReadVersion = (PassThruReadVersion)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruReadVersion));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruGetLastError");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                GetLastError = (PassThruGetLastError)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruGetLastError));

            pAddressOfFunctionToCall = NativeMethods.GetProcAddress(m_pDll, "PassThruIoctl");
            if (pAddressOfFunctionToCall != IntPtr.Zero)
                Ioctl = (PassThruIoctl)Marshal.GetDelegateForFunctionPointer(
                    pAddressOfFunctionToCall,
                    typeof(PassThruIoctl));

            return true;
        }

        public bool FreeLibrary()
        {
            return NativeMethods.FreeLibrary(m_pDll);
        }
    }
}
