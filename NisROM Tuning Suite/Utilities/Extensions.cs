﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NisROM_Tuning_Suite.J2534;
using System.Reflection;

namespace NisROM_Tuning_Suite.Utilities
{
    public static class Extensions
    {
        public static string AsString(this IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        public static IntPtr ToIntPtr(this PassThruMsg msg)
        {
            IntPtr msgPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PassThruMsg)));
            Marshal.StructureToPtr(msg, msgPtr, true);
            return msgPtr;
        }

        public static List<PassThruMsg> AsMsgList(this IntPtr ptr, int count)
        {
            List<PassThruMsg> list = new List<PassThruMsg>(count);
            for (int index = 0; index < count; ++index)
            {
                list.Add(ptr.AsStruct<PassThruMsg>());
            }
            return list;
        }

        public static List<T> AsList<T>(this IntPtr ptr, int count) where T : struct
        {
            List<T> list = new List<T>(count);
            for (int index = 0; index < count; ++index)
            {
                IntPtr ptr1 = (IntPtr)((int)ptr + index * Marshal.SizeOf(typeof(T)));
                list.Add(ptr1.AsStruct<T>());
            }
            return list;
        }

        public static T AsStruct<T>(this IntPtr ptr) where T : struct
        {
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }

        public static T? AsNullableStruct<T>(this IntPtr ptr) where T : struct
        {
            if (ptr == IntPtr.Zero)
                return new T?();
            return new T?((T)Marshal.PtrToStructure(ptr, typeof(T)));
        }

        public static string AsString(this List<PassThruMsg> list)
        {
            string str = string.Empty;
            for (int index = 0; index < list.Count; ++index)
                str = string.Concat(new object[4]
                {
                    (object) str,
                    (object) index,
                    (object) " -------------------------------",
                    (object) Environment.NewLine
                }) + list[index].ToString() + (object)index + " -------------------------------";
            return str;
        }

        public static void DoubleBuffered(this System.Windows.Forms.DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
