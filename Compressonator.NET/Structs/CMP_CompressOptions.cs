using System;
using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct AMD_CMD
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string strCommand;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string strParameter;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CMP_PrintInfoStr([MarshalAs(UnmanagedType.LPStr)] string infoStr);

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
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
        public bool disableMultiThreading = false;

        [MarshalAs(UnmanagedType.U4)]
        public CMP_Speed compressionSpeed;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_GPUDecode GPUDecode;
        [MarshalAs(UnmanagedType.U4)]
        public CMP_ComputeType encodeWidth = CMP_ComputeType.CMP_HPC;
        [MarshalAs(UnmanagedType.U4)]
        public uint numThreads = 0;

        [MarshalAs(UnmanagedType.R4)]
        public float quality = 1.0f;

        [MarshalAs(UnmanagedType.U1)]
        public bool restrictColour;
        [MarshalAs(UnmanagedType.U1)]
        public bool restrictAlpha;

        [MarshalAs(UnmanagedType.U4)]
        public uint modeMask;

        [MarshalAs(UnmanagedType.I4)]
        public int numCmds = 0; // Should always be 0, unless actually being used.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.AMD_MAX_CMDS)]
        public AMD_CMD[] cmdSet;

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

        /// <summary>
        /// We're currently unable to marshal this correctly. See this
        /// <see href="https://github.com/Yellow-Dog-Man/Compressonator.NET/issues/18">GitHub Issue</see>
        /// but it must be <see cref="Constants.PRINT_INFO_SIZE"/> in length.
        /// We do also have a WIP structure to marshal this: <seealso cref="CMP_PrintInfoStr"/>
        /// </summary>
        //public CMP_PrintInfoStr printInfoStr;
        [Obsolete("DO NOT USE see: https://github.com/Yellow-Dog-Man/Compressonator.NET/issues/18")]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.PRINT_INFO_SIZE)]
        public string printInfoStr;

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
    }
}
