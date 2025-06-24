using System;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    public static unsafe class SDK_NativeMethods
    {
        const string LIBRARY_NAME = "CMP_Compressonator";

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CMP_CalculateBufferSize([In] CMP_Texture texture);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern CMP_ERROR CMP_ConvertTexture([In][Out] CMP_Texture sourceTexture, [In][Out] CMP_Texture destTexture,
            [In] IntPtr options, IntPtr feedbackProc);

        public static CMP_ERROR CMP_ConvertTexture(CMP_Texture sourceTexture, CMP_Texture destTexture, CMP_CompressOptions options)
        {
            return CMP_ConvertTexture(sourceTexture, destTexture, options.UnmanagedCopy, IntPtr.Zero);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern CMP_ERROR CMP_ConvertMipTexture([In][Out] CMP_MipSet mipSetIn, [In][Out] CMP_MipSet mipSetOut,
             	[In] IntPtr options, IntPtr feedbackProc);

        public static CMP_ERROR CMP_ConvertMipTexture(CMP_MipSet mipSetIn, CMP_MipSet mipSetOut, CMP_CompressOptions options)
        {
            return CMP_ConvertMipTexture(mipSetIn, mipSetOut, options.UnmanagedCopy, IntPtr.Zero);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CMP_IsValidFormat([In]CMP_FORMAT format);

        public static bool IsSupported => _isSupported.Value;

        static readonly Lazy<bool> _isSupported = new Lazy<bool>(() =>
        {
            // a bit hacky way to detect if it's working, but should work for now
            try
            {
                var tex = new CMP_Texture();

                tex.width = 4;
                tex.height = 4;
                tex.pitch = 4 * 4;
                tex.format = CMP_FORMAT.RGBA_8888;
                tex.CalculateDataSize();

                return true;
            }
            catch
            {
                return false;
            }
        });
    }
}
