using System;
using System.Runtime.InteropServices;

//TODO: This might need the same treatment as CMP_MipSet and CMP_CompressOptions, but i don't want to touch it atm - Prime
namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class CMP_Texture
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint size = (uint)Marshal.SizeOf<CMP_Texture>();
        [MarshalAs(UnmanagedType.U4)]
        public uint width;
        [MarshalAs(UnmanagedType.U4)]
        public uint height;
        [MarshalAs(UnmanagedType.U4)]
        public uint pitch;

        [MarshalAs(CMP_Format_Extensions.CMP_FORMAT_MARSHAL)]
        public CMP_FORMAT format;
        [MarshalAs(CMP_Format_Extensions.CMP_FORMAT_MARSHAL)]
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

        public void CalculateDataSize()
        {
            dataSize = SDK_NativeMethods.CMP_CalculateBufferSize(this);
        }
    }
}
