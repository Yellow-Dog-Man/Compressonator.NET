using System;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    //TODO: When we can drop, earlier .Nets we can look into:
    //https://learn.microsoft.com/en-us/dotnet/standard/native-interop/custom-marshalling-source-generation
    [Obsolete("DO NOT Inherit THIS IT WILL NOT WORK, Use it as a template to make your formatted classes work")]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal abstract class MarshableClass : IDisposable
    {
        public abstract int dataSize { get; }

        private IntPtr unmanagedPtr = IntPtr.Zero;
        internal IntPtr UnmanagedCopy
        {
            get
            {
                bool hasData = false;
                if (unmanagedPtr == IntPtr.Zero)
                    unmanagedPtr = Marshal.AllocHGlobal(dataSize);
                else
                    hasData = true;

                Marshal.StructureToPtr(this, unmanagedPtr, hasData);

                return unmanagedPtr;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (unmanagedPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(unmanagedPtr);
                disposed = true;
            }
        }

        ~MarshableClass()
        {
            Dispose(false);
        }
    }
}
