using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct BC15Options
    {
        [FieldOffset(0)]
        public bool useChannelWeights;
        [FieldOffset(4)]
        public fixed float channelWeights[3];
        [FieldOffset(16)]
        public bool useAdaptiveWeights;
        [FieldOffset(20)]
        public bool useAlphaThreshold;
        [FieldOffset(24)]
        public int alphaThreshold;
        [FieldOffset(28)]
        public bool useRefinementSteps;
        [FieldOffset(32)]
        public int refinementSteps;
    }
}
