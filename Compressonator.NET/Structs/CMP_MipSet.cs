using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class CMP_MipSet
    {
        [MarshalAs(UnmanagedType.I4)]
        public int width;
        [MarshalAs(UnmanagedType.I4)]
        public int height;
        [MarshalAs(UnmanagedType.I4)]
        public int depth;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_FORMAT format;

        [MarshalAs(UnmanagedType.U4)]
        public CMP_ChannelFormat channelFormat;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_TextureDataType textureDataType;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_TextureType textureType;

        [MarshalAs(UnmanagedType.U4)]
        public uint flags;
        [MarshalAs(UnmanagedType.U1)]
        public byte cubeFaceMask;
        [MarshalAs(UnmanagedType.U4)]
        public uint fourCC;
        [MarshalAs(UnmanagedType.U4)]
        public uint fourCC2;
        [MarshalAs(UnmanagedType.I4)]
        public int maxMipLevels;
        [MarshalAs(UnmanagedType.I4)]
        public int mipLevels;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_FORMAT transcodeFormat;
        [MarshalAs(UnmanagedType.U1)]
        public bool compressed;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_FORMAT isDecompressed;
        [MarshalAs(UnmanagedType.U1)]
        public bool swizzled;
        [MarshalAs(UnmanagedType.U1)]
        public byte blockWidth;
        [MarshalAs(UnmanagedType.U1)]
        public byte blockHeight;
        [MarshalAs(UnmanagedType.U1)]
        public byte blockDepth;

        [MarshalAs(UnmanagedType.U4)]
        public uint mipWidth;
        [MarshalAs(UnmanagedType.U4)]
        public uint mipHeight;
        [MarshalAs(UnmanagedType.U4)]
        public uint dataSize;
        public IntPtr data;

        public IntPtr mipLevelTable;
        public IntPtr reservedData;
    }
}
