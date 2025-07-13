namespace Compressonator.NET
{
    public enum CMP_TextureType
    {
        TT_2D = 0,  // A regular 2D texture. data stored linearly (rgba,rgba,...rgba)
        TT_CubeMap = 1,  // A cubemap texture.
        TT_VolumeTexture = 2,  // A volume texture.
        TT__2D_Block = 3,  // 2D texture data stored as [Height][Width] blocks as individual channels using cmp_rgb_t or cmp_yuv_t
        TT_Unknown = 4,  // Unknown type of texture : No data is stored for this type
    }
}
