using System;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    /// <summary>
    /// Convinience utility for managing and updating an unmanaged copy of a struct or class.
    /// </summary>
    /// <typeparam name="T">Struct/Class to handle</typeparam>
    /// <remarks>
    /// It helps to create a consistent marshaling experience across all target operating systems.
    /// </remarks>
    public class MarshaledStruct<T>: IDisposable where T : class, new()
    {
        public MarshaledStruct(T input)
        {
            Write(input);
        }

        public MarshaledStruct()
        {

        }

        public IntPtr UnmanagedPointer { get; private set; }

        public T Read()
        {
            return Marshal.PtrToStructure<T>(UnmanagedPointer);
        }

        public IntPtr Write(T t)
        {
            if (UnmanagedPointer == IntPtr.Zero)
            { 
                UnmanagedPointer = Marshal.AllocHGlobal(Marshal.SizeOf(t));
                Marshal.StructureToPtr(t, UnmanagedPointer, false);
            }
            else
                Marshal.StructureToPtr(t, UnmanagedPointer, true);

            return UnmanagedPointer;
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (UnmanagedPointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(UnmanagedPointer);
                disposed = true;
            }
        }

        ~MarshaledStruct()
        {
            Dispose(false);
        }
    }
}
