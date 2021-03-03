using System;
using System.Collections.Generic;
using System.Text;

namespace Compressonator.NET
{
    public enum CMP_FORMAT
    {
        Unknown = 0,

        // Channel Component formats --------------------------------------------------------------------------------
        ARGB_8888,         // ARGB format with 8-bit fixed channels.
        ABGR_8888,         // ABGR format with 8-bit fixed channels.
        RGBA_8888,         // RGBA format with 8-bit fixed channels.
        BGRA_8888,         // BGRA format with 8-bit fixed channels.
        RGB_888,         // RGB format with 8-bit fixed channels.
        BGR_888,         // BGR format with 8-bit fixed channels.
        RG_8,         // Two component format with 8-bit fixed channels.
        R_8,         // Single component format with 8-bit fixed channels.
        ARGB_2101010,         // ARGB format with 10-bit fixed channels for color & a 2-bit fixed channel for alpha.
        ARGB_16,         // ARGB format with 16-bit fixed channels.
        ABGR_16,         // ABGR format with 16-bit fixed channels.
        RGBA_16,         // RGBA format with 16-bit fixed channels.
        BGRA_16,         // BGRA format with 16-bit fixed channels.
        RG_16,         // Two component format with 16-bit fixed channels.
        R_16,         // Single component format with 16-bit fixed channels.
        RGBE_32F,         // RGB format with 9-bit floating point each channel and shared 5 bit exponent
        ARGB_16F,         // ARGB format with 16-bit floating-point channels.
        ABGR_16F,         // ABGR format with 16-bit floating-point channels.
        RGBA_16F,         // RGBA format with 16-bit floating-point channels.
        BGRA_16F,         // BGRA format with 16-bit floating-point channels.
        RG_16F,         // Two component format with 16-bit floating-point channels.
        R_16F,         // Single component with 16-bit floating-point channels.
        ARGB_32F,         // ARGB format with 32-bit floating-point channels.
        ABGR_32F,         // ABGR format with 32-bit floating-point channels.
        RGBA_32F,         // RGBA format with 32-bit floating-point channels.
        BGRA_32F,         // BGRA format with 32-bit floating-point channels.
        RGB_32F,         // RGB format with 32-bit floating-point channels.
        BGR_32F,         // BGR format with 32-bit floating-point channels.
        RG_32F,         // Two component format with 32-bit floating-point channels.
        R_32F,         // Single component with 32-bit floating-point channels.

        // Compression formats -----------------------------------------------------------------------------------
        ASTC, // ASTC (Adaptive Scalable Texture Compression) open texture compression standard
        ATI1N, // Single component compression format using the same technique as DXT5 alpha. Four bits per pixel.
        ATI2N, //     Two component compression format using the same technique as DXT5 alpha. Designed for compression of tangent space normal maps. Eight bits per pixel.
        ATI2N_XY, //    Two component compression format using the same technique as DXT5 alpha. The same as ATI2N but with the channels swizzled. Eight bits per pixel.
        ATI2N_DXT5, //    ATI2N like format using DXT5. Intended for use on GPUs that do not natively support ATI2N. Eight bits per pixel.
        ATC_RGB, // CMP - a compressed RGB format.
        ATC_RGBA_Explicit, // CMP - a compressed ARGB format with explicit alpha.
        ATC_RGBA_Interpolated, // CMP - a compressed ARGB format with interpolated alpha.
        BC1, // A four component opaque (or 1-bit alpha) compressed texture format for Microsoft DirectX10. Identical to DXT1.  Four bits per pixel.
        BC2, // A four component compressed texture format with explicit alpha for Microsoft DirectX10. Identical to DXT3. Eight bits per pixel.
        BC3, // A four component compressed texture format with interpolated alpha for Microsoft DirectX10. Identical to DXT5. Eight bits per pixel.
        BC4, // A single component compressed texture format for Microsoft DirectX10. Identical to ATI1N. Four bits per pixel.
        BC5, // A two component compressed texture format for Microsoft DirectX10. Identical to ATI2N_XY. Eight bits per pixel.
        BC6H, // BC6H compressed texture format (UF)
        BC6H_SF, // BC6H compressed texture format (SF)
        BC7, // BC7  compressed texture format
        DXT1, // An DXTC compressed texture matopaque (or 1-bit alpha). Four bits per pixel.
        DXT3, //   DXTC compressed texture format with explicit alpha. Eight bits per pixel.
        DXT5, //   DXTC compressed texture format with interpolated alpha. Eight bits per pixel.
        DXT5_xGBR, // DXT5 with the red component swizzled into the alpha channel. Eight bits per pixel.
        DXT5_RxBG, // swizzled DXT5 format with the green component swizzled into the alpha channel. Eight bits per pixel.
        DXT5_RBxG, // swizzled DXT5 format with the green component swizzled into the alpha channel & the blue component swizzled into the green channel. Eight bits per pixel.
        DXT5_xRBG, // swizzled DXT5 format with the green component swizzled into the alpha channel & the red component swizzled into the green channel. Eight bits per pixel.
        DXT5_RGxB, // swizzled DXT5 format with the blue component swizzled into the alpha channel. Eight bits per pixel.
        DXT5_xGxR, // two-component swizzled DXT5 format with the red component swizzled into the alpha channel & the green component in the green channel. Eight bits per pixel.
        ETC_RGB, // ETC   GL_COMPRESSED_RGB8_ETC2  backward compatible
        ETC2_RGB, // ETC2  GL_COMPRESSED_RGB8_ETC2
        ETC2_SRGB, // ETC2  GL_COMPRESSED_SRGB8_ETC2
        ETC2_RGBA, // ETC2  GL_COMPRESSED_RGBA8_ETC2_EAC
        ETC2_RGBA1, // ETC2  GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2
        ETC2_SRGBA, // ETC2  GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC
        ETC2_SRGBA1, // ETC2  GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2
        PVRTC,

        // Transcoder formats - ------------------------------------------------------------
        GTC,        ///< GTC   Fast Gradient Texture Compressor
        BASIS,      ///< BASIS compression

        // End of list
        MAX = BASIS
    }
}
