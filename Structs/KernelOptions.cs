using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KernelOptions
    {
        public CMP_ComputeExtensions extensions;
        public uint height;
        public uint width;
        public float quality;
        public CMP_FORMAT format;
        public CMP_ComputeType encodeWidth;
        public int threads;

        // Private
        uint size;
        IntPtr data;
        IntPtr dataSVM;
        IntPtr srcfile;
    }
}
