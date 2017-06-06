using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisROM_Tuning_Suite.Utilities
{
    public class DataFunctions
    {

        public static bool Compare(byte[] array, byte[] needle, int startIndex)
        {
            int needleLen = needle.Length;
            for (int i = 0, p = startIndex; i < needleLen; i++, p++)
            {
                if (array[p] != needle[i]) return false;
            }
            return true;
        }

        public static string ArrayToHex(byte[] data)
        {
            return ArrayToHex(data, 0, data.Length);
        }

        public static string ArrayToHex(byte[] data, int start, int length)
        {
            StringBuilder result = new StringBuilder(length * 3);
            for (int i = start; i < start + length; i++)
            {
                result.Append(data[i].ToString("X2")).Append(" ");
            }
            return result.ToString().Substring(0, result.Length - 1);
        }

        public static uint GetUInt(byte[] data, int index)
        {
            return (uint)((data[index] << 24) + (data[index + 1] << 16) + (data[index + 2] << 8) + data[index + 3]);
        }

        public static uint GetUInt(byte[] data, uint index)
        {
            return (uint)((data[index] << 24) + (data[index + 1] << 16) + (data[index + 2] << 8) + data[index + 3]);
        }

        public static ushort GetUShort(byte[] data, int index)
        {
            return (ushort)((data[index] << 8) + data[index + 1]);
        }

        public static bool HasBytesAtOffset(byte[] data, int offset, byte b1, byte b2)
        {
            return data[offset] == b1 && data[offset + 1] == b2;
        }

        public static byte[] HexStringToByteArray(String hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        public static void WriteToArray(byte[] data, int offset, ushort value)
        {
            data[offset + 0] = (byte)(value >> 8);
            data[offset + 1] = (byte)(value);
        }

        public static void WriteToArray(byte[] data, int offset, uint value)
        {
            data[offset + 0] = (byte)(value >> 24);
            data[offset + 1] = (byte)(value >> 16);
            data[offset + 2] = (byte)(value >> 8);
            data[offset + 3] = (byte)(value);
        }

        public static void WriteToArray(byte[] data, int offset, string value)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value.ToString());
            Array.Copy(bytes, 0, data, offset, bytes.Length);
        }
    }
}
