using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class CMP_Texture
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint size;
        [MarshalAs(UnmanagedType.U4)]
        public uint width;
        [MarshalAs(UnmanagedType.U4)]
        public uint height;
        [MarshalAs(UnmanagedType.U4)]
        public uint pitch;

        [MarshalAs(UnmanagedType.U4)]
        public CMP_FORMAT format;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_FORMAT transcodeFormat;

        [MarshalAs(UnmanagedType.U1)]
        public byte blockHeight;
        [MarshalAs(UnmanagedType.U1)]
        public byte blockWidth;
        [MarshalAs(UnmanagedType.U1)]
        public byte blockDepth;

        [MarshalAs(UnmanagedType.U4)]
        public uint dataSize;
        public IntPtr data;

        public IntPtr mipSet;

        public CMP_Texture()
        {
            Init();
        }

        public void CalculateDataSize()
        {
            dataSize = SDK_NativeMethods.CMP_CalculateBufferSize(this);
        }

        void Init()
        {
            size = (uint)Marshal.SizeOf<CMP_Texture>();
        }
    }
}
