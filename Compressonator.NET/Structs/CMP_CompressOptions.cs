using System;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    public unsafe struct AMD_CMDS
    {
        public fixed byte cmdSet[Constants.ALL_CMD_SETS_SIZE];
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CMP_PrintInfoStr([MarshalAs(UnmanagedType.LPStr)] string infoStr);

    [StructLayout(LayoutKind.Sequential)]
    public class CMP_CompressOptions
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint size = (uint) Marshal.SizeOf<CMP_CompressOptions>();

        // New to v4.5
        // Flags to control parameters in Brotli-G compression preconditioning
        [MarshalAs(UnmanagedType.U1)]
        public bool doPreconditionBRLG;
        [MarshalAs(UnmanagedType.U1)]
        public bool doDeltaEncodeBRLG;
        [MarshalAs(UnmanagedType.U1)]
        public bool doSwizzleBRLG;

        // New to v4.3

        [MarshalAs(UnmanagedType.U4)]
        public uint pageSize;

        // New to v4.2

        [MarshalAs(UnmanagedType.U1)]
        public bool useRefinementSteps;
        [MarshalAs(UnmanagedType.I4)]
        public int refinementSteps;

        // v4.1 and older settings

        [MarshalAs(UnmanagedType.U1)]
        public bool useChannelWeighting;
        [MarshalAs(UnmanagedType.R4)]
        public float weightingRed;
        [MarshalAs(UnmanagedType.R4)]
        public float weightingGreen;
        [MarshalAs(UnmanagedType.R4)]
        public float weightingBlue;
        [MarshalAs(UnmanagedType.U1)]
        public bool useAdaptiveWeighting;

        [MarshalAs(UnmanagedType.U1)]
        public bool DXT1UseAlpha;
        [MarshalAs(UnmanagedType.U1)]
        public bool useGPUDecompress;
        [MarshalAs(UnmanagedType.U1)]
        public bool useCGCompress;

        [MarshalAs(UnmanagedType.U1)]
        public byte alphaThreshold;

        [MarshalAs(UnmanagedType.U1)]
        public bool disableMultiThreading;

        [MarshalAs(UnmanagedType.U4)]
        public CMP_Speed compressionSpeed;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_GPUDecode GPUDecode;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_ComputeType encodeWidth;
        [MarshalAs(UnmanagedType.U4)]
        public uint numThreads;

        [MarshalAs(UnmanagedType.R4)]
        public float quality;

        [MarshalAs(UnmanagedType.U1)]
        public bool restrictColour;
        [MarshalAs(UnmanagedType.U1)]
        public bool restrictAlpha;

        [MarshalAs(UnmanagedType.U4)]
        public uint modeMask;

        [MarshalAs(UnmanagedType.I4)]
        public int numCmds;
        [MarshalAs(UnmanagedType.Struct)]
        public AMD_CMDS cmdSet;

        [MarshalAs(UnmanagedType.R4)]
        public float inputDefog;
        [MarshalAs(UnmanagedType.R4)]
        public float inputExposure;
        [MarshalAs(UnmanagedType.R4)]
        public float inputKneeLow;
        [MarshalAs(UnmanagedType.R4)]
        public float inputKneeHigh;
        [MarshalAs(UnmanagedType.R4)]
        public float inputGamma;
        [MarshalAs(UnmanagedType.R4)]
        public float inputFilterGamma;

        [MarshalAs(UnmanagedType.I4)]
        public int cmpLevel;
        [MarshalAs(UnmanagedType.I4)]
        public int posBits;
        [MarshalAs(UnmanagedType.I4)]
        public int texCbits;
        [MarshalAs(UnmanagedType.I4)]
        public int normalBits;
        [MarshalAs(UnmanagedType.I4)]
        public int genericBits;

        [MarshalAs(UnmanagedType.I4)]
        public int vCacheSize;
        [MarshalAs(UnmanagedType.I4)]
        public int vCacheFIFOsize;
        [MarshalAs(UnmanagedType.R4)]
        public float overdrawACMR;
        [MarshalAs(UnmanagedType.I4)]
        public int simplifyLOD;
        [MarshalAs(UnmanagedType.U1)]
        public bool vertexFetch;

        [MarshalAs(CMP_Format_Extensions.CMP_FORMAT_MARSHAL)]
        public CMP_FORMAT sourceFormat;
        [MarshalAs(CMP_Format_Extensions.CMP_FORMAT_MARSHAL)]
        public CMP_FORMAT destFormat;
        [MarshalAs(UnmanagedType.U1)]
        public bool format_support_hostEncoder;

        public CMP_PrintInfoStr printInfoStr;

        [MarshalAs(UnmanagedType.U1)]
        public bool getPerfStats;
        [MarshalAs(UnmanagedType.Struct)]
        public KernelPerformanceStats perfStats;
        [MarshalAs(UnmanagedType.U1)]
        public bool getDeviceInfo;
        [MarshalAs(UnmanagedType.Struct)]
        public KernelDeviceInfo deviceInfo;
        [MarshalAs(UnmanagedType.U1)]
        public bool genGPUMipMaps;
        [MarshalAs(UnmanagedType.U1)]
        public bool useSRGBFrames;
        [MarshalAs(UnmanagedType.I4)]
        public int miplevels;

        //TODO: When we can drop, earlier .Nets we can look into:
        //https://learn.microsoft.com/en-us/dotnet/standard/native-interop/custom-marshalling-source-generation
        private IntPtr unmanagedPtr = IntPtr.Zero;
        internal IntPtr UnmanagedCopy
        {
            get
            {
                bool hasData = false;
                if (unmanagedPtr == IntPtr.Zero)
                    unmanagedPtr = Marshal.AllocHGlobal((int)size);
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

        ~CMP_CompressOptions()
        {
            Dispose(false);
        }
    }
}
