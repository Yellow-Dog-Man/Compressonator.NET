using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct KernelDeviceInfo
    {
        public fixed byte deviceName[256];
        public fixed byte version[128];
        public int maxUCores;
    }
}
