﻿using System;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    public static unsafe class FrameworkNativeMethods
    {
        const string LIBRARY_NAME = "CMP_Framework";

        [DllImport(LIBRARY_NAME)]
        public static extern int CMP_CalcMinMipSize(int height, int width, int mipsLevel);

        [DllImport(LIBRARY_NAME)]
        public static extern int CMP_GenerateMIPLevels([In][Out] CMP_MipSet mipSet, int minSize);

        [DllImport(LIBRARY_NAME)]
        public static extern CMP_ERROR CMP_CreateCompressMipSet([In][Out] CMP_MipSet mipSetCMP, [In][Out] CMP_MipSet mipSetSRC);

        [DllImport(LIBRARY_NAME)]
        public static extern CMP_ERROR CMP_LoadTexture(string sourceFile, [In][Out] CMP_MipSet mipSet);

        [DllImport(LIBRARY_NAME)]
        public static extern CMP_ERROR CMP_SaveTexture(string destinationFile, [In][Out] CMP_MipSet mipSet);

        [DllImport(LIBRARY_NAME)]
        private static extern CMP_ERROR CMP_SaveTextureEx(string destinationFile, [In][Out] CMP_Texture texture);

        // Aliasesed to avoid, exposing API consumers to multiple functions here.
        public static CMP_ERROR CMP_SaveTexture(string destinationFile, CMP_Texture texture)
        {
            return CMP_SaveTextureEx(destinationFile, texture);
        }

        [DllImport(LIBRARY_NAME)]
        public static extern CMP_ERROR CMP_ProcessTexture([In][Out] CMP_MipSet srcMipSet, [In][Out] CMP_MipSet dstMipSet, [MarshalAs(UnmanagedType.Struct)] KernelOptions kernelOptions, IntPtr feedbackProc);

        [DllImport(LIBRARY_NAME)]
        public static extern CMP_ERROR CMP_CompressTexture(ref KernelOptions options, CMP_MipSet srcMipSet, CMP_MipSet dstMipSet, IntPtr feedbackProc);

        [DllImport(LIBRARY_NAME)]
        public static extern void CMP_Format2FourCC(CMP_FORMAT format, CMP_MipSet mipSet);

        [DllImport(LIBRARY_NAME)]
        public static extern CMP_FORMAT CMP_ParseFormat(string format);

        [DllImport(LIBRARY_NAME)]
        public static extern bool CMP_IsValidFormat([In] CMP_FORMAT format);

        [DllImport(LIBRARY_NAME)]
        public static extern int CMP_NumberOfProcessors();

        [DllImport(LIBRARY_NAME)]
        public static extern void CMP_FreeMipSet(CMP_MipSet mipSet);

        [DllImport(LIBRARY_NAME)]
        public static extern void CMP_GetMipLevel(CMP_MipLevel data, CMP_MipSet mipSet, int mipLevel, int faceOrSlice);

        [DllImport(LIBRARY_NAME)]
        public static extern CMP_ERROR CMP_CreateComputeLibrary(CMP_MipSet src, ref KernelOptions kernelOptions, IntPtr reserved);

        [DllImport(LIBRARY_NAME)]
        public static extern void CMP_InitFramework();

        public static bool IsSupported => _isSupported.Value;

        static readonly Lazy<bool> _isSupported = new Lazy<bool>(() =>
        {
            // a bit hacky way to detect if it's working, but should work for now
            try
            {
                var procNum = CMP_NumberOfProcessors();

                return true;
            }
            catch
            {
                return false;
            }
        });
    }
}
