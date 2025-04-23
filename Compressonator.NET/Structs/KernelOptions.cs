using System;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KernelOptions
    {
        [MarshalAs(UnmanagedType.U4)]
        public CMP_ComputeExtensions extensions;
        [MarshalAs(UnmanagedType.U4)]
        public uint height;
        [MarshalAs(UnmanagedType.U4)]
        public uint width;
        [MarshalAs(UnmanagedType.R4)]
        public float quality;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_FORMAT format;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_FORMAT srcformat;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_ComputeType encodeWith;
        [MarshalAs(UnmanagedType.I4)]
        public int threads;
        [MarshalAs(UnmanagedType.U1)]
        public bool getPerfStats;
        [MarshalAs(UnmanagedType.Struct)]
        public KernelPerformanceStats perfStats;
        [MarshalAs(UnmanagedType.U1)]
        public bool getDeviceInfo;
        [MarshalAs(UnmanagedType.Struct)]
        public KernelDeviceInfo deviceInfo;
        [MarshalAs(UnmanagedType.U1)]
        public bool genGPUMipMaps;
        [MarshalAs(UnmanagedType.I4)]
        public int miplevels;
        [MarshalAs(UnmanagedType.U1)]
        public bool useSRGBFrames;

        [MarshalAs(UnmanagedType.Struct)]
        public EncoderUnion encoderOptions;

        [MarshalAs(UnmanagedType.U4)]
        public uint size;
        public IntPtr data;
        public IntPtr dataSVM;
        public IntPtr srcfile;
    }
}
