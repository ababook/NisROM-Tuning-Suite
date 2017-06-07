using System;
using System.Runtime.InteropServices;

using NisROM_Tuning_Suite.Utilities;

namespace NisROM_Tuning_Suite.J2534
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SByteArray
    {
        public int NumOfBytes;
        public IntPtr BytePtr;

        public override string ToString()
        {
            var byteList = BytePtr.AsList<byte>(NumOfBytes);
            return BitConverter.ToString(byteList.ToArray()).Replace("-", " ");
        }
    }
}
