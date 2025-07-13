namespace Compressonator.NET
{
    public enum CMP_Speed: uint
    {
        CMP_Speed_Normal,     ///< Highest quality mode
        CMP_Speed_Fast,       ///< Slightly lower quality but much faster compression mode - DXTn & ATInN only
        CMP_Speed_SuperFast,  ///< Slightly lower quality but much, much faster compression mode - DXTn & ATInN only
    }
}
