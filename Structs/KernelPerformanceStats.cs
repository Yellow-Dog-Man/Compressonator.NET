using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KernelPerformanceStats
    {
        public float computeShaderElapsedMS;
        public int numBlocks;
        public float cmpMTxPerSec;
    }
}
