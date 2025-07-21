using System;
using System.Runtime.InteropServices;

//TODO: This might need the same treatment as CMP_MipSet and CMP_CompressOptions, but i don't want to touch it atm - Prime
namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Sequential)]
    public class CMP_Texture: IDisposable
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint size = (uint)Marshal.SizeOf<CMP_Texture>();
        [MarshalAs(UnmanagedType.U4)]
        public uint width;
        [MarshalAs(UnmanagedType.U4)]
        public uint height;
        [MarshalAs(UnmanagedType.U4)]
        public uint pitch;

        [MarshalAs(CMP_Format_Extensions.CMP_FORMAT_MARSHAL)]
        public CMP_FORMAT format;
        [MarshalAs(CMP_Format_Extensions.CMP_FORMAT_MARSHAL)]
        public CMP_FORMAT transcodeFormat;

        [MarshalAs(UnmanagedType.U1)]
        public byte blockHeight;
        [MarshalAs(UnmanagedType.U1)]
        public byte blockWidth;
        [MarshalAs(UnmanagedType.U1)]
        public byte blockDepth;

        [MarshalAs(UnmanagedType.U4)]
        public uint dataSize;
        public IntPtr data;

        public IntPtr mipSet;

        public void CalculateDataSize()
        {
            dataSize = SDK_NativeMethods.CMP_CalculateBufferSize(this);
        }

        public void AllocateDataPointer()
        {
            if (dataSize == 0)
                CalculateDataSize();

            data = Marshal.AllocHGlobal((int)dataSize);
        }

        public void CopyDimensionsFrom(CMP_Texture sourceTexture)
        {
            this.width =  sourceTexture.width;
            this.height = sourceTexture.height;
            this.blockHeight = sourceTexture.blockHeight;
            this.blockWidth = sourceTexture.blockWidth;
            this.blockDepth = sourceTexture.blockDepth;
            this.pitch = sourceTexture.pitch;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("Dipose Texture: " + this.format);
            if (data != IntPtr.Zero)
                Marshal.FreeHGlobal(data);
        }

        ~CMP_Texture()
        {
            Dispose(false);
        }
    }
}
