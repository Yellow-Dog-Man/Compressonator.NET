using System.Runtime.InteropServices;

namespace Compressonator.NET
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public unsafe struct EncoderUnion
    {
        [FieldOffset(0)]
        public fixed byte encodeoptions[32];

        [FieldOffset(0)]
        public BC15Options bc15;
    }
}
