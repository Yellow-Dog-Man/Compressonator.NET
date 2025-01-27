using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class CMP_MipLevel
    {
        [MarshalAs(UnmanagedType.I4)]
        public int width;
        [MarshalAs(UnmanagedType.I4)]
        public int height;
        [MarshalAs(UnmanagedType.U4)]
        public uint linearSize;

        public IntPtr data;
    }
}
