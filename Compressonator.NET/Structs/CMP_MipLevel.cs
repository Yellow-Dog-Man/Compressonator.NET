using System;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class CMP_MipLevel: IDisposable
    {
        [MarshalAs(UnmanagedType.I4)]
        public int width;
        [MarshalAs(UnmanagedType.I4)]
        public int height;
        [MarshalAs(UnmanagedType.U4)]
        public uint linearSize;

        public IntPtr data;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // No-op waiiit
                
        }

        ~CMP_MipLevel()
        {
            Dispose(false);
        }
    }
}
