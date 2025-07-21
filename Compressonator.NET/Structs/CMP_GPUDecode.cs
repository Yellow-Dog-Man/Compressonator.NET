namespace Compressonator.NET
{
    public enum CMP_GPUDecode: uint
    {
        GPUDecode_OPENGL = 0,  ///< Use OpenGL   to decode Textures (default)
        GPUDecode_DIRECTX,     ///< Use DirectX  to decode Textures
        GPUDecode_VULKAN,      ///< Use Vulkan  to decode Textures
        GPUDecode_INVALID
    }
}
