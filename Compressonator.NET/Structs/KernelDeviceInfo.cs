using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct KernelDeviceInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string deviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string version;
        public int maxUCores;
    }
}
