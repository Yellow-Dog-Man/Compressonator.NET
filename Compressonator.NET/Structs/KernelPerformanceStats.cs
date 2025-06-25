using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KernelPerformanceStats
    {
        [MarshalAs(UnmanagedType.R4)]
        public float computeShaderElapsedMS;
        [MarshalAs(UnmanagedType.U4)]
        public int numBlocks;
        [MarshalAs(UnmanagedType.R4)]
        public float cmpMTxPerSec;
    }
}
